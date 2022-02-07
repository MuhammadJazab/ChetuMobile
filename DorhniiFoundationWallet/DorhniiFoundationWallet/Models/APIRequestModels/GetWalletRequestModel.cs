using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class GetWalletRequestModel
    {
        [JsonProperty("seedId")]
        public string SeedId { get; set; }
    }
}
