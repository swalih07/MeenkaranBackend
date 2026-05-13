using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Payments;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Ṃeenkaran.Infrastructure.Razorpay;

namespace Ṃeenkaran.Application.Services
{
    public class GuidePaymentService:IGuidePaymentService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _factory;
        private readonly Settings _razorpayOptions;


        public GuidePaymentService(AppDbContext context, IHttpClientFactory factory, Settings razorpayOptions)
        {
            _context = context;
            _factory = factory;
            _razorpayOptions = razorpayOptions;
        }

        public async Task<ApiResponse<CreateGuidePaymentOrderResponseDto>> CreateOrderAsync(int userId, CreateGuidePaymentOrderRequestDto request)
        {
            var setting = await GetOrCreateSettingAsync();

            var platformFeeFromGuide = Math.Round(
                request.GuideAmount * setting.GuidePlatformFeePercent / 100m,
                2,
                MidpointRounding.AwayFromZero);

            var guideReceives = Math.Round(
                request.GuideAmount - platformFeeFromGuide,
                2,
                MidpointRounding.AwayFromZero);

            var totalUserPays = Math.Round(
                request.GuideAmount + setting.UserServiceCharge,
                2,
                MidpointRounding.AwayFromZero);

            var amountPaise = (long)Math.Round(totalUserPays * 100m, 0, MidpointRounding.AwayFromZero);

            var payment = new GuidePayment
            {
                GuideId = request.GuideId,
                UserId = userId,
                GuideAmount = request.GuideAmount,
                PlatformFeePercent = setting.GuidePlatformFeePercent,
                PlatformFeeFromGuide = platformFeeFromGuide,
                UserServiceCharge = setting.UserServiceCharge,
                TotalUserPays = totalUserPays,
                GuideReceives = guideReceives,
                Currency = _razorpayOptions.Currency,
                PaymentStatus = "Pending",
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.GuidePayments.Add(payment);
            await _context.SaveChangesAsync();

            var receipt = $"GP{DateTime.UtcNow:yyyyMMddHHmmss}{payment.Id}";
            if (receipt.Length > 40)
                receipt = receipt[..40];

            var orderId = await CreateRazorpayOrderAsync(amountPaise, receipt, request, setting, userId);

            payment.RazorpayOrderId = orderId;
            await _context.SaveChangesAsync();

            var response = new CreateGuidePaymentOrderResponseDto
            {
                PaymentId = payment.Id,
                RazorpayKeyId = _razorpayOptions.KeyId,
                RazorpayOrderId = orderId,
                Currency = _razorpayOptions.Currency,
                AmountPaise = amountPaise,
                GuideAmount = payment.GuideAmount,
                GuidePlatformFeePercent = payment.PlatformFeePercent,
                PlatformFeeFromGuide = payment.PlatformFeeFromGuide,
                UserServiceCharge = payment.UserServiceCharge,
                GuideReceives = payment.GuideReceives,
                TotalUserPays = payment.TotalUserPays,
                Description = payment.Description
            };

            return ApiResponse<CreateGuidePaymentOrderResponseDto>.SuccessResponse(response, "Razorpay order created", 201);
        }
        public async Task<ApiResponse<string>> VerifyPaymentAsync(int userId, VerifyGuidePaymentRequestDto request)
        {
            var payment = await _context.GuidePayments
                .FirstOrDefaultAsync(x => x.RazorpayOrderId == request.RazorpayOrderId && x.UserId == userId);

            if (payment == null)
                return ApiResponse<string>.FailResponse("Payment record not found", 404);

            if (payment.PaymentStatus == "Paid")
                return ApiResponse<string>.FailResponse("Payment already verified", 400);

            var expectedSignature = ComputeSignature(
                request.RazorpayOrderId,
                request.RazorpayPaymentId,
                _razorpayOptions.KeySecret);

            if (!FixedTimeEquals(expectedSignature, request.RazorpaySignature))
                return ApiResponse<string>.FailResponse("Invalid payment signature", 400);

            payment.RazorpayPaymentId = request.RazorpayPaymentId;
            payment.RazorpaySignature = request.RazorpaySignature;
            payment.PaymentStatus = "Paid";
            payment.PaidAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, "Payment verified successfully");
        }

        public async Task<ApiResponse<List<GuidePaymentDto>>> GetUserPaymentsAsync(int userId)
        {
            var items = await _context.GuidePayments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new GuidePaymentDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    GuideId = x.GuideId,
                    GuideAmount = x.GuideAmount,
                    PlatformFeePercent = x.PlatformFeePercent,
                    PlatformFeeFromGuide = x.PlatformFeeFromGuide,
                    UserServiceCharge = x.UserServiceCharge,
                    TotalUserPays = x.TotalUserPays,
                    GuideReceives = x.GuideReceives,
                    Currency = x.Currency,
                    RazorpayOrderId = x.RazorpayOrderId,
                    RazorpayPaymentId = x.RazorpayPaymentId,
                    RazorpaySignature = x.RazorpaySignature,
                    PaymentStatus = x.PaymentStatus,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    PaidAt = x.PaidAt
                })
                .ToListAsync();

            return ApiResponse<List<GuidePaymentDto>>.SuccessResponse(items, "User payments fetched successfully");
        }

        private async Task<PlatformPaymentSetting> GetOrCreateSettingAsync()
        {
            var setting = await _context.PlatformPaymentSettings.FirstOrDefaultAsync(x => x.IsActive);

            if (setting != null)
                return setting;

            setting = new PlatformPaymentSetting
            {
                GuidePlatformFeePercent = 20m,
                UserServiceCharge = 15m,
                IsActive = true,
                UpdatedByAdminId = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.PlatformPaymentSettings.Add(setting);
            await _context.SaveChangesAsync();

            return setting;
        }

        private async Task<string> CreateRazorpayOrderAsync(
            long amountPaise,
            string receipt,
            CreateGuidePaymentOrderRequestDto request,
            PlatformPaymentSetting setting,
            int userId)
        {
            var client = _factory.CreateClient();

            var authBytes = Encoding.ASCII.GetBytes($"{_razorpayOptions.KeyId}:{_razorpayOptions.KeySecret}");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            var payload = new
            {
                amount = amountPaise,
                currency = _razorpayOptions.Currency,
                receipt = receipt,
                notes = new
                {
                    guide_id = request.GuideId,
                    user_id = userId,
                    guide_amount = request.GuideAmount,
                    platform_fee_percent = setting.GuidePlatformFeePercent,
                    user_service_charge = setting.UserServiceCharge,
                    description = request.Description ?? string.Empty
                }
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_razorpayOptions.ApiBaseUrl.TrimEnd('/')}/orders";
            var response = await client.PostAsync(url, content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Razorpay order creation failed: {body}");

            using var doc = JsonDocument.Parse(body);
            var orderId = doc.RootElement.GetProperty("id").GetString();

            if (string.IsNullOrWhiteSpace(orderId))
                throw new InvalidOperationException("Razorpay order id not returned");

            return orderId;
        }

        private static string ComputeSignature(string orderId, string paymentId, string secret)
        {
            var payload = $"{orderId}|{paymentId}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        private static bool FixedTimeEquals(string expected, string actual)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            var actualBytes = Encoding.UTF8.GetBytes(actual);

            return expectedBytes.Length == actualBytes.Length &&
                   CryptographicOperations.FixedTimeEquals(expectedBytes, actualBytes);
        }
    }
}
