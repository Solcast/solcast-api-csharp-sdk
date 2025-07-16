
using System;
using System.Net.Http;
using System.Reflection;
using System.Net;
using System.Text.RegularExpressions;

namespace Solcast.Clients
{
    public abstract class BaseClient : IDisposable
    {
        private static bool _updateChecked = false;
        private bool _disposed = false;
        protected readonly HttpClient _httpClient;

        protected BaseClient(string baseUrl = null, IWebProxy proxy = null, bool checkForUpdates = true)
        {
            if (checkForUpdates && !_updateChecked && !IsSdkUpdateCheckSuppressed())
            {
                CheckForUpdates();
                _updateChecked = true;
            }

            var apiKey = Environment.GetEnvironmentVariable("SOLCAST_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new MissingApiKeyException("The SOLCAST_API_KEY environment variable is not set.");
            }

            var httpClientHandler = new HttpClientHandler();
            if (proxy != null)
            {
                httpClientHandler.Proxy = proxy;
                httpClientHandler.UseProxy = true;
            }

            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(baseUrl ?? SolcastUrls.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var version = GetAssemblyVersion();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"solcast-api-csharp-sdk/{version}");
        }

        private static bool IsSdkUpdateCheckSuppressed()
        {
            var suppressFlag = Environment.GetEnvironmentVariable("SUPPRESS_SDK_UPDATE_CHECK");
            return suppressFlag?.Equals("true", StringComparison.OrdinalIgnoreCase) == true;
        }

        public static void CheckForUpdates()
        {
            const string githubApiUrl = "https://api.github.com/repos/solcast/solcast-api-csharp-sdk/releases/latest";

            try
            {
                string currentVersion = NormalizeVersion(GetAssemblyVersion());

                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("solcast-sdk-version-check");
                var response = client.GetStringAsync(githubApiUrl).Result;
                dynamic releaseInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

                string latestVersionRaw = releaseInfo?.tag_name ?? "unknown";
                string latestVersion = NormalizeVersion(latestVersionRaw);

                if (CompareSemanticVersions(currentVersion, latestVersion) < 0)
                {
                    Console.WriteLine($@"A new version of the SDK is available: {latestVersionRaw}.
To update, run the following command:
    dotnet add package Solcast --version {latestVersion}
Disable this check by setting env variable SUPPRESS_SDK_UPDATE_CHECK='true'
");
                }
            }
            catch (Exception e)
            {
                // Gracefully handle any errors (e.g., network issues or API rate limits)
                Console.WriteLine($"Failed to check for SDK updates: {e.Message}");
            }
        }

        private static bool IsPreReleaseVersion(string version)
        {
            // Check if the version contains a pre-release identifier
            var regex = new Regex(@"-\w+(\.\w+)*$");
            return regex.IsMatch(version);
        }

        private static string NormalizeVersion(string version)
        {
            return version.TrimStart('v', 'V'); // Remove the "v" prefix (case-insensitive)
        }

        private static int CompareSemanticVersions(string currentVersion, string latestVersion)
        {
            var currentParts = ParseSemanticVersion(currentVersion);
            var latestParts = ParseSemanticVersion(latestVersion);

            // Compare numeric components: major, minor, patch
            for (int i = 0; i < 3; i++)
            {
                int currentPart = currentParts.NumericParts[i];
                int latestPart = latestParts.NumericParts[i];

                if (currentPart < latestPart) return -1;
                if (currentPart > latestPart) return 1;
            }

            // Compare pre-release components
            return ComparePreRelease(currentParts.PreRelease, latestParts.PreRelease);
        }

        private static (int[] NumericParts, string PreRelease) ParseSemanticVersion(string version)
        {
            var regex = new Regex(@"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(-(?<preRelease>[a-zA-Z0-9.-]+))?$");
            var match = regex.Match(version);

            if (!match.Success)
                throw new FormatException($"Invalid semantic version: {version}");

            int major = int.Parse(match.Groups["major"].Value);
            int minor = int.Parse(match.Groups["minor"].Value);
            int patch = int.Parse(match.Groups["patch"].Value);
            string preRelease = match.Groups["preRelease"].Value; // May be empty

            return (new[] { major, minor, patch }, preRelease);
        }

        private static int ComparePreRelease(string currentPreRelease, string latestPreRelease)
        {
            // No pre-release means higher precedence
            if (string.IsNullOrEmpty(currentPreRelease) && !string.IsNullOrEmpty(latestPreRelease))
                return 1;
            if (!string.IsNullOrEmpty(currentPreRelease) && string.IsNullOrEmpty(latestPreRelease))
                return -1;

            // If both are null or empty, they are equal
            if (string.IsNullOrEmpty(currentPreRelease) && string.IsNullOrEmpty(latestPreRelease))
                return 0;

            // Split pre-release identifiers by dots and compare lexicographically
            var currentParts = currentPreRelease.Split('.');
            var latestParts = latestPreRelease.Split('.');

            for (int i = 0; i < Math.Max(currentParts.Length, latestParts.Length); i++)
            {
                string currentPart = i < currentParts.Length ? currentParts[i] : "";
                string latestPart = i < latestParts.Length ? latestParts[i] : "";

                int currentNumeric, latestNumeric;
                bool currentIsNumeric = int.TryParse(currentPart, out currentNumeric);
                bool latestIsNumeric = int.TryParse(latestPart, out latestNumeric);

                if (currentIsNumeric && latestIsNumeric)
                {
                    // Compare as numbers
                    if (currentNumeric < latestNumeric) return -1;
                    if (currentNumeric > latestNumeric) return 1;
                }
                else
                {
                    // Compare as strings
                    int comparison = string.Compare(currentPart, latestPart, StringComparison.Ordinal);
                    if (comparison != 0) return comparison;
                }
            }

            return 0; // Versions are equal
        }

        private static string GetAssemblyVersion()
        {
            var attribute = (AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(),
                typeof(AssemblyInformationalVersionAttribute)
            );

            var version = attribute?.InformationalVersion ?? "1.0.0";
            return version.Split('+')[0]; // Return the version without build metadata
        }

        protected void HandleUnauthorizedResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiKeyException("The API key provided is invalid or unauthorized.");
            }
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposed = true;
            }
        }
    }

    public class MissingApiKeyException : Exception
    {
        public MissingApiKeyException(string message) : base(message) { }
    }

    public class UnauthorizedApiKeyException : Exception
    {
        public UnauthorizedApiKeyException(string message) : base(message) { }
    }
}
