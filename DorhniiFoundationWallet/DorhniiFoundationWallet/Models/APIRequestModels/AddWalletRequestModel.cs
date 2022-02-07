using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// 
    /// </summary>
    public class AddWalletRequestModel
    {
        [JsonProperty("seedId")]
        public string SeedId { get; set; }
        [JsonProperty("walletName")]
        public string WalletName { get; set; }
    }
}
