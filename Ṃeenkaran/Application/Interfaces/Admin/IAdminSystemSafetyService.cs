using Ṃeenkaran.Application.Commen;
using Ṃeenkaran.Application.DTOs.Admin;

namespace Ṃeenkaran.Application.Interfaces.Admin
{
    public interface IAdminSystemSafetyService
    {
        Task<ApiResponse<SafetyAlertDto>> CreateAlertAsync(int adminId, string? adminName, CreateSafetyAlertRequest request);
        Task<ApiResponse<List<SafetyAlertDto>>> GetActiveAlertsAsync();
        Task<ApiResponse<List<SafetyAlertDto>>> GetAllAlertsAsync();
        Task<ApiResponse<List<SystemErrorLogDto>>>GetSystemErrorsAsync();
        Task<ApiResponse<List<SuspiciousloginAttemptDto>>> GetSuspiciousLoginAttemptAsync();
        Task LogSystemErrorAsync(Exception ex, string? requestPath = null, string? httpMethod = null, int? userId = null, string? userEmail = null);
        Task LogSuspiciousLoginAsync(string email, string? ipAddress, string? userAgent, string reason);


        Task<ApiResponse<List<SafetyAlertDto>>> GetUserActiveAlertsAsync();
        Task<ApiResponse<List<SafetyAlertDto>>> GetGuideActiveAlertsAsync();
    }
}
