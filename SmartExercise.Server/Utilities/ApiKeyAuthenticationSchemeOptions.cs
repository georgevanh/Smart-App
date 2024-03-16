using Microsoft.AspNetCore.Authentication;

namespace SmartExercise.Server.Utilities
{
    /// <summary>
    /// Options for API Key authentication scheme.
    /// </summary>
    public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}

