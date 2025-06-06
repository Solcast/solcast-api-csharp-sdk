using NUnit.Framework;
using Solcast.Clients;
using System.Threading.Tasks;

namespace Solcast.Tests
{
    [TestFixture]
    public class TmyClientTests
    {
        private TmyClient _tmyClient;

        [SetUp]
        public void Setup()
        {
            _tmyClient = new TmyClient();
        }

        [Test]
        public async Task GetTmyRadiationAndWeather_ShouldReturnValidData()
        {
            var response = await _tmyClient.GetTmyRadiationAndWeather(-33.856784, 151.215297, format: "json");
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task GetTmyRooftopPvPower_ShouldReturnValidData()
        {
            var response = await _tmyClient.GetTmyRooftopPvPower(
                latitude: -33.856784,
                longitude: 151.215297,
                capacity: 3,
                format: "json"
            );
            Assert.IsNotNull(response);
        }
    }
}
