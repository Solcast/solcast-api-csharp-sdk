using NUnit.Framework;
using Solcast.Clients;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Solcast.Tests
{
    [TestFixture]
    public class ForecastClientTests
    {
        private ForecastClient _forecastClient;
        private string _originalApiKey;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Capture the original API key (if it exists)
            _originalApiKey = Environment.GetEnvironmentVariable("SOLCAST_API_KEY");
        }

        [SetUp]
        public void Setup()
        {
            // Reuse the original API key (if it exists)
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", _originalApiKey);

            // Initialize the ForecastClient
            _forecastClient = new ForecastClient();
        }

        [TearDown]
        public void TearDown()
        {
            // Reset the environment variable to the original API key after each test
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", _originalApiKey);
        }

        [Test]
        public async Task GetForecastRadiationAndWeather_ShouldReturnValidData()
        {
            // Arrange
            double latitude = -33.856784;
            double longitude = 151.215297;
            List<string> outputParameters = ["dni", "ghi", "air_temp"];
            string format = "json";

            // Act
            var response = await _forecastClient.GetForecastRadiationAndWeather(
                latitude: latitude,
                longitude: longitude,
                outputParameters: outputParameters,
                format: format
            );

            // Assert
            Assert.IsNotNull(response);
            // Perform additional assertions based on expected output format
        }

        [Test]
        public void GetForecastRadiationAndWeather_ShouldThrowMissingApiKeyException_WhenApiKeyIsMissing()
        {
            // Arrange
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", null);

            // Act & Assert
            Assert.Throws<MissingApiKeyException>(() =>
            {
                _forecastClient = new ForecastClient();
            });
        }

        [Test]
        public void GetForecastRadiationAndWeather_ShouldThrowUnauthorizedApiKeyException_WhenApiKeyIsInvalid()
        {
            // Arrange
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", "invalid_api_key");
            _forecastClient = new ForecastClient();
            double latitude = -33.856784;
            double longitude = 151.215297;
            List<string> outputParameters = ["dni", "ghi", "air_temp"];
            string format = "json";

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedApiKeyException>(async () =>
            {
                await _forecastClient.GetForecastRadiationAndWeather(
                    latitude: latitude,
                    longitude: longitude,
                    outputParameters: outputParameters,
                    format: format
                );
            });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Restore the original API key after all tests
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", _originalApiKey);
        }
    }
}
