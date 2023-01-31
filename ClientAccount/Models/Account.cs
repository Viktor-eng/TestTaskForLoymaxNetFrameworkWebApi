using Newtonsoft.Json;

namespace ClientAccount.Models
{
    public class Account
    {
        public int ClientId { get; set; }

        public int Balance { get; set; }

        [JsonIgnore]
        public Client Client { get; set; }
    }
}