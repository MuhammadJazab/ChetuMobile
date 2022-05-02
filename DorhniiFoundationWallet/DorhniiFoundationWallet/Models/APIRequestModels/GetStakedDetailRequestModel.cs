using Newtonsoft.Json;
namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class GetStakedDetailRequestModel
    {
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }
        [JsonProperty("walletAddress")]
        public string WalletAdress { get; set; }
        [JsonProperty("stakePeriod")]
        public string StakePeriod { get; set; }
    }
}
