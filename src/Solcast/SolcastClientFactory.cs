using System;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Solcast
{
    /// <summary>
    /// Creates a <see cref="SolcastClient"/> authenticated with your Solcast API key.
    ///
    /// The generated client takes a request adapter and an authentication provider, which is more than
    /// a caller should have to know to make their first request. This is the one line the getting
    /// started example needs.
    /// </summary>
    public static class SolcastClientFactory
    {
        /// <summary>The environment variable the key is read from.</summary>
        public const string ApiKeyVariable = "SOLCAST_API_KEY";

        private const string ToolkitUrl = "https://toolkit.solcast.com.au/";

        /// <summary>Creates a client using the key in the SOLCAST_API_KEY environment variable.</summary>
        /// <exception cref="InvalidOperationException">The variable is not set.</exception>
        public static SolcastClient Create()
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyVariable);

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    $"{ApiKeyVariable} is not set. Get an API key from {ToolkitUrl} and set it, "
                    + "or pass one to SolcastClientFactory.Create(apiKey).");
            }

            return Create(apiKey!);
        }

        /// <summary>Creates a client using the key you supply.</summary>
        /// <exception cref="ArgumentException">The key is empty.</exception>
        public static SolcastClient Create(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"An API key is required. Get one from {ToolkitUrl}.", nameof(apiKey));
            }

            var authentication = new ApiKeyAuthenticationProvider(
                apiKey, "api_key", ApiKeyAuthenticationProvider.KeyLocation.QueryParameter);

            return new SolcastClient(new HttpClientRequestAdapter(authentication));
        }
    }
}
