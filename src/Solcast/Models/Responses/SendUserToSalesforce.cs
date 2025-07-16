using Newtonsoft.Json;

namespace Solcast.Models
{
    public class SendUserToSalesforce
    {
        [JsonProperty("user_id")]
        public int? UserId { get; set; }
    }
}
