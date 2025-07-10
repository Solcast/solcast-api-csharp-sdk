using NUnit.Framework;
using Solcast.Clients;
using System;
using System.Threading.Tasks;

namespace Solcast.Tests
{
    [TestFixture]
    public class AggregationClientTests : IDisposable
    {
        private AggregationClient _aggregationClient;
        private bool _disposed = false;

        [SetUp]
        public void Setup()
        {
            _aggregationClient = new AggregationClient();
        }

        [Test]
        public async Task GetLiveAggregation_ShouldReturnValidData()
        {
            var response = await _aggregationClient.GetLiveAggregations(
                collectionId: "country_total",
                aggregationId: "it_total"
            );
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task GetForecastAggregation_ShouldReturnValidData()
        {
            var response = await _aggregationClient.GetForecastAggregations(
                collectionId: "country_total",
                aggregationId: "it_total",
                outputParameters: ["percentage", "pv_estimate"]
            );
            Assert.IsNotNull(response);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _aggregationClient?.Dispose();
                _disposed = true;
            }
        }
    }
}
