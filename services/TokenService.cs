using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace AuthRoleManager.Services;

public class TokenService
{
    private readonly IOpenIddictTokenManager _tokenManager;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IOpenIddictTokenManager tokenManager, ILogger<TokenService> logger)
    {
        _tokenManager = tokenManager;
        _logger = logger;
    }

    public async Task<bool> RevokeTokenAsync(string tokenId)
    {
        var token = await _tokenManager.FindByIdAsync(tokenId);
        if (token is null)
            return false;

        await _tokenManager.DeleteAsync(token);
        return true;
    }

    public async Task RevokeAllUserTokensAsync(string userId)
    {
        await _tokenManager.RevokeAsync(userId, null, null, null);

        var tokens = await _tokenManager.FindAsync(userId, null, null, null).ToListAsync();
        foreach (var token in tokens)
        {
            Console.WriteLine(
                $"Revoking token: {(token as OpenIddictEntityFrameworkCoreToken)?.ConcurrencyToken}"
            );
            // await _tokenManager.DeleteAsync(token);
        }
    }

    public async Task<bool> IsTokenRevokedAsync(string userId, string tokenId)
    {
        _logger.LogInformation(
            "🔍 Checking token revocation - UserId: '{UserId}', TokenId: '{TokenId}'",
            userId,
            tokenId
        );

        try
        {
            var token = await _tokenManager.FindByIdAsync(tokenId);

            if (token == null)
            {
                _logger.LogInformation(
                    "⚠️ Token not found in database: {TokenId} - Assuming NOT revoked",
                    tokenId
                );
                return false;
            }

            var status = await _tokenManager.GetStatusAsync(token);
            var isRevoked = status == OpenIddictConstants.Statuses.Revoked;

            _logger.LogInformation(
                "📊 Token {TokenId} - Status: '{Status}', IsRevoked: {IsRevoked}",
                tokenId,
                status,
                isRevoked
            );

            return isRevoked;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error in IsTokenRevokedAsync for TokenId: {TokenId}", tokenId);
            return false; // En caso de error, no bloquear
        }
    }
}
