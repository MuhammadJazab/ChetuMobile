using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class GetWalletsDetailsRequestModel
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonProperty("seedId")]
        public string SeedId { get; set; }
      

    }
}
