using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Domain.Entities;
using Ṃeenkaran.Domain.Entities.Admin;
using Ṃeenkaran.Domain.Enums;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Ṃeenkaran.Application.Services.Admin
{
    public class AdminSystemSafetyService:IAdminSystemSafetyService
    {
        private readonly AppDbContext _context;

        public AdminSystemSafetyService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<SafetyAlertDto>> CreateAlertAsync(int adminId, string? adminName, CreateSafetyAlertRequest request)
        {
            var alert = new SafetyAlert
            {
                Title = request.Title.Trim(),
                Message = request.Message.Trim(),
                Audience=request.Audience,
                Severity=request.Severity,
                ExpiresAt=request.ExpiresAt,
                CreatedByAdminId=adminId,
                CreatedByAdminName=adminName,
                IsActive=true,
                CreatedAt=DateTime.UtcNow
            };
            _context.SafetyAlerts.Add(alert);
            await _context.SaveChangesAsync();

            var dto = MapAlert(alert);

            return ApiResponse<SafetyAlertDto>.SuccessResponse(dto,"Safety alert created and broadcasted", 201);

        }
        public async Task<ApiResponse<List<SafetyAlertDto>>> GetActiveAlertsAsync()
        {
            var now = DateTime.UtcNow;

            var alerts = await _context.SafetyAlerts
                .Where(x => x.IsActive && (x.ExpiresAt == null || x.ExpiresAt > now))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapAlert(x))
                .ToListAsync();

            return ApiResponse<List<SafetyAlertDto>>.SuccessResponse(alerts);
        }

        public async Task<ApiResponse<List<SafetyAlertDto>>> GetUserActiveAlertsAsync()
        {
            var now = DateTime.UtcNow;
            var alerts = await _context.SafetyAlerts
                .Where(x => x.IsActive && 
                           (x.ExpiresAt == null || x.ExpiresAt > now) &&
                           (x.Audience == AlertAudience.All || x.Audience == AlertAudience.Users))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapAlert(x))
                .ToListAsync();

            return ApiResponse<List<SafetyAlertDto>>.SuccessResponse(alerts);
        }

        public async Task<ApiResponse<List<SafetyAlertDto>>> GetGuideActiveAlertsAsync()
        {
            var now = DateTime.UtcNow;
            var alerts = await _context.SafetyAlerts
                .Where(x => x.IsActive && 
                           (x.ExpiresAt == null || x.ExpiresAt > now) &&
                           (x.Audience == AlertAudience.All || x.Audience == AlertAudience.Guides))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapAlert(x))
                .ToListAsync();

            return ApiResponse<List<SafetyAlertDto>>.SuccessResponse(alerts);
        }

        public async Task<ApiResponse<List<SafetyAlertDto>>> GetAllAlertsAsync()
        {
            var alerts = await _context.SafetyAlerts
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapAlert(x))
                .ToListAsync();

            return ApiResponse<List<SafetyAlertDto>>.SuccessResponse(alerts);
        }
        public async Task<ApiResponse<List<SystemErrorLogDto>>> GetSystemErrorsAsync()
        {
            var errors = await _context.SystemErrorLogs
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new SystemErrorLogDto
                {
                    Id = x.Id,
                    ErrorMessage = x.ErrorMessage,
                    StackTrace = x.StackTrace,
                    Source = x.Source,
                    RequestPath = x.RequestPath,
                    HttpMethod = x.HttpMethod,
                    UserId = x.Userid,
                    UserEmail = x.UserEmail,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return ApiResponse<List<SystemErrorLogDto>>.SuccessResponse(errors);
        }
        public async Task<ApiResponse<List<SuspiciousloginAttemptDto>>> GetSuspiciousLoginAttemptAsync()
        {
            var items = await _context.SuspiciousLoginAttempts
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new SuspiciousloginAttemptDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    IPAddress = x.IPAddress,
                    UserAgent = x.UserAgent,
                    Reason = x.Reason,
                    FailedCount = x.FailedCount,
                    IsResolved = x.IsResolved,
                    ResolveByAdminId = x.ResolvedByAdminId,
                    ResolvedAt = x.ResolvedAt,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
            return ApiResponse<List<SuspiciousloginAttemptDto>>.SuccessResponse(items);
        }
        public async Task LogSystemErrorAsync(Exception ex, string? requestPath = null, string? httpMethod = null, int? userId = null, string? userEmail = null)
        {
            var log = new SystemErrorLog
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.ToString(),
                Source = ex.Source,
                RequestPath = requestPath,
                HttpMethod = httpMethod,
                Userid = userId,
                UserEmail = userEmail,
                CreatedAt = DateTime.UtcNow
            };
            _context.SystemErrorLogs.Add(log);
            await _context.SaveChangesAsync();
        }
        public async Task LogSuspiciousLoginAsync(string email,string? ipAddress,string? userAgent,string reason)
        {
            var normalizedEmail = email.Trim().ToLower();

            var existing = await _context.SuspiciousLoginAttempts
                .FirstOrDefaultAsync(x =>
                x.Email.ToLower() == normalizedEmail &&
                x.IPAddress == ipAddress &&
                x.IsResolved == false);

            if(existing != null)
            {
                existing.FailedCount += 1;
                existing.Reason = reason;
                await _context.SaveChangesAsync();
                return;
            }
            var item = new SuspiciousLoginAttempt
            {
                Email = email.Trim(),
                IPAddress = ipAddress,
                UserAgent = userAgent,
                Reason = reason,
                FailedCount = 1,
                IsResolved = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.SuspiciousLoginAttempts.Add(item);
            await _context.SaveChangesAsync();
        }
        private static SafetyAlertDto MapAlert(SafetyAlert x)
        {
            return new SafetyAlertDto
            {
                Id = x.Id,
                Title = x.Title,
                Message = x.Message,
                Audience = x.Audience,
                Severity = x.Severity,
                IsActive = x.IsActive,
                CreatedByAdminId = x.CreatedByAdminId,
                CreatedByAdminName = x.CreatedByAdminName,
                ExpiresAt = x.ExpiresAt,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }
    }
}
