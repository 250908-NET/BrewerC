using School.Models;

namespace School.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId);
        Task<IEnumerable<RefreshToken>> GetExpiredOrRevokedByUserIdAsync(string userId);
        Task UpdateAsync(RefreshToken refreshToken);
        Task DeleteAsync(RefreshToken refreshToken);
        Task DeleteRangeAsync(IEnumerable<RefreshToken> refreshTokens);
        Task<bool> ExistsAsync(string token);
    }
}