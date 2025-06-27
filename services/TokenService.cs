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


        try
        {
            var token = await _tokenManager.FindByIdAsync(tokenId);

            if (token == null)
            {

                return false;
            }

            var status = await _tokenManager.GetStatusAsync(token);
            var isRevoked = status == OpenIddictConstants.Statuses.Revoked;


            return isRevoked;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error in IsTokenRevokedAsync for TokenId: {TokenId}", tokenId);
            return false; // En caso de error, no bloquear
        }
    }
}
