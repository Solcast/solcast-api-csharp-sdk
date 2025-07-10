using NUnit.Framework;
using Solcast.Clients;
using System;
using System.Threading.Tasks;

namespace Solcast.Tests
{
    [TestFixture]
    public class PvPowerSiteClientTests : IDisposable
    {
        private PvPowerSiteClient _client;
        private bool _disposed = false;

        [SetUp]
        public void Setup()
        {
            _client = new PvPowerSiteClient();
        }

        [Test]
        public async Task Test_ListPvPowerSites()
        {
            // Act
            // var response = await _client.GetPvPowerSites(format: "json");
            var response = await _client.GetPvPowerSites();

            // Assert
            Assert.IsNotNull(response);
            // Assert.IsTrue(response.RawResponse.Contains("resource_id"));
            Assert.IsTrue(response.RawResponse.Contains("\"resource_id\":\"ba75-e17a-7374-95ed\""));
        }

        [Test]
        public async Task Test_GetPvPowerSite()
        {
            // Arrange
            var resourceId = UnmeteredLocations.Locations["Sydney Opera House"].ResourceId;

            // Act
            // var response = await _client.GetPvPowerSite(resourceId, format: "json");
            var response = await _client.GetPvPowerSite(resourceId);

            // Assert
            Assert.IsNotNull(response);
            // Assert.AreEqual(response.Data.ResourceId, resourceId);
            Assert.IsTrue(response.RawResponse.Contains("\"resource_id\":\"ba75-e17a-7374-95ed\""));
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _client?.Dispose();
                _disposed = true;
            }
        }
    }
}
