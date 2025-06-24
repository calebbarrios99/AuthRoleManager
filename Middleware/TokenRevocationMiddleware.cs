using System.Security.Claims;
using AuthRoleManager.Services;

namespace AuthRoleManager.Middleware
{
    public class TokenRevocationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenRevocationMiddleware> _logger;

        public TokenRevocationMiddleware(
            RequestDelegate next,
            ILogger<TokenRevocationMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, TokenService tokenService)
        {
            _logger.LogInformation(
                "🔍 TokenRevocationMiddleware executing for {Path}",
                context.Request.Path
            );

            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId =
                    context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? context.User.FindFirst("sub")?.Value;

                var tokenId =
                    context.User.FindFirst("oi_tkn_id")?.Value // OpenIddict Token ID
                    ?? context.User.FindFirst("jti")?.Value; // Fallback a JTI

                _logger.LogInformation(
                    "🎯 UserId: '{UserId}', TokenId: '{TokenId}'",
                    userId ?? "NULL",
                    tokenId ?? "NULL"
                );

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(tokenId))
                {
                    _logger.LogInformation("✅ Both claims exist, checking revocation...");

                    try
                    {
                        var isRevoked = await tokenService.IsTokenRevokedAsync(userId, tokenId);

                        _logger.LogInformation(
                            "🔍 Token revocation result: {IsRevoked}",
                            isRevoked
                        );

                        if (isRevoked)
                        {
                            _logger.LogWarning(
                                "🚫 BLOCKING REVOKED TOKEN - UserId: {UserId}, TokenId: {TokenId}",
                                userId,
                                tokenId
                            );

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(
                                """{"error":"Token has been revoked"}"""
                            );
                            return;
                        }
                        else
                        {
                            _logger.LogInformation("✅ Token is valid, continuing...");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Error checking token revocation");
                    }
                }
                else
                {
                    _logger.LogInformation(
                        "⚠️ Skipping revocation check - Missing UserId or TokenId"
                    );
                }
            }

            await _next(context);
        }
    }
}
