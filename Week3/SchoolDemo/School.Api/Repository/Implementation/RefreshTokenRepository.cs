using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SchoolDbContext _context;
        private readonly ILogger<RefreshTokenRepository> _logger;

        public RefreshTokenRepository(
            SchoolDbContext context,
            ILogger<RefreshTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
        {
            try
            {
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                
                _logger.LogDebug("Created refresh token {TokenId} for user {UserId}", 
                    refreshToken.Id, refreshToken.UserId);
                
                return refreshToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating refresh token for user {UserId}", 
                    refreshToken.UserId);
                throw;
            }
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.RefreshTokens
                    .Include(rt => rt.User)
                    .FirstOrDefaultAsync(rt => rt.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving refresh token");
                throw;
            }
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId)
        {
            try
            {
                return await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId)
                    .OrderByDescending(rt => rt.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving refresh tokens for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<RefreshToken>> GetExpiredOrRevokedByUserIdAsync(string userId)
        {
            try
            {
                var now = DateTime.UtcNow;
                return await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId && 
                                (rt.ExpiresAt < now || rt.RevokedAt != null))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving expired/revoked tokens for user {UserId}", userId);
                throw;
            }
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
                
                _logger.LogDebug("Updated refresh token {TokenId}", refreshToken.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating refresh token {TokenId}", refreshToken.Id);
                throw;
            }
        }

        public async Task DeleteAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
                
                _logger.LogDebug("Deleted refresh token {TokenId}", refreshToken.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting refresh token {TokenId}", refreshToken.Id);
                throw;
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<RefreshToken> refreshTokens)
        {
            try
            {
                _context.RefreshTokens.RemoveRange(refreshTokens);
                await _context.SaveChangesAsync();
                
                _logger.LogDebug("Deleted {Count} refresh tokens", refreshTokens.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting refresh tokens");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(string token)
        {
            try
            {
                return await _context.RefreshTokens
                    .AnyAsync(rt => rt.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if refresh token exists");
                throw;
            }
        }
    }
}