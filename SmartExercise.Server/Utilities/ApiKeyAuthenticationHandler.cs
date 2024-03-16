using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace SmartExercise.Server.Utilities
{
    /// <summary>
    /// Handles authentication using API key.
    /// </summary>
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
    {
        /// <summary>
        /// The authentication scheme name.
        /// </summary>
        public const string AuthenticationScheme = "ApiKeyAuthentication";

        /// <summary>
        /// Constructor for ApiKeyAuthenticationHandler.
        /// </summary>
        /// <param name="options">The authentication options.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="encoder">The URL encoder.</param>
        /// <param name="clock">The system clock.</param>
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Handles authentication asynchronously.
        /// </summary>
        /// <returns>An authentication result.</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Get the API key from the request headers
            if (!Request.Headers.TryGetValue("Api-Key", out var apiKeyHeaderValues))
            {
                return AuthenticateResult.Fail("Missing API key.");
            }

            var apiKey = apiKeyHeaderValues.ToString();

            // Validate the API key
            if (apiKey != Options.ApiKey)
            {
                return AuthenticateResult.Fail("Invalid API key.");
            }

            // Create a claims identity
            var claims = new[] { new Claim(ClaimTypes.Name, "API user") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
