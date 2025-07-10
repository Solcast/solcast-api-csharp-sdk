using NUnit.Framework;
using Solcast.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solcast.Tests
{
    [TestFixture]
    public class LiveClientTests : IDisposable
    {
        private LiveClient _liveClient;
        private string _originalApiKey;
        private bool _disposed = false;

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

            // Initialize the LiveClient
            _liveClient = new LiveClient();
        }

        [TearDown]
        public void TearDown()
        {
            // After each test, revert back to the original API key (if it exists)
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", _originalApiKey);
        }

        [Test]
        public async Task GetRadiationAndWeather_ShouldReturnValidCsvData()
        {
            // Arrange
            double latitude = -33.856784;
            double longitude = 151.215297;
            List<string> outputParameters = ["dni", "ghi", "air_temp"];
            string format = "csv";

            // Act
            var response = await _liveClient.GetLiveRadiationAndWeather(
                latitude: latitude,
                longitude: longitude,
                outputParameters: outputParameters,
                format: format
            );

            // Assert - checking if the response is in CSV format (string)
            Assert.IsInstanceOf<string>(response.RawResponse);
            Assert.IsNotEmpty(response.RawResponse);
            Assert.IsTrue(response.RawResponse.Contains("dni,ghi,air_temp"), "CSV headers missing or incorrect");
        }

        [Test, Category("Live")]
        public void GetRadiationAndWeather_ShouldThrowMissingApiKeyException_WhenApiKeyIsMissing()
        {
            // Arrange: Simulate missing API key
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", null);

            // Act & Assert
            Assert.Throws<MissingApiKeyException>(() =>
            {
                _liveClient = new LiveClient(); // Should throw MissingApiKeyException
            });
        }

        [Test, Category("Live")]
        public void GetRadiationAndWeather_ShouldThrowUnauthorizedApiKeyException_WhenApiKeyIsInvalid()
        {
            // Arrange: Simulate an invalid API key
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", "invalid_api_key");
            _liveClient = new LiveClient();
            double latitude = -33.856784;
            double longitude = 151.215297;
            List<string> outputParameters = ["dni", "ghi", "air_temp"];
            string format = "csv";

            // Act & Assert
            var ex = Assert.ThrowsAsync<UnauthorizedApiKeyException>(async () =>
            {
                await _liveClient.GetLiveRadiationAndWeather(
                    latitude: latitude,
                    longitude: longitude,
                    outputParameters: outputParameters,
                    format: format
                );
            });

            // Assert the exception message
            Assert.AreEqual("The API key provided is invalid or unauthorized.", ex.Message);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // After all tests are completed, restore the original API key
            Environment.SetEnvironmentVariable("SOLCAST_API_KEY", _originalApiKey);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _liveClient?.Dispose();
                _disposed = true;
            }
        }
    }
}
