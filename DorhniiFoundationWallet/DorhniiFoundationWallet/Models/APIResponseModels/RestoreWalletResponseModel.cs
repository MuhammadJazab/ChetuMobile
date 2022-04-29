using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class RestoreWalletResponseModel : APIResponseModel
    {
        [JsonProperty("seedId")]
        public string SeedId { get; set; }
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }
        [JsonProperty("encryptedPrivateKey")]
        public string EncryptedPrivateKey { get; set; }

    }
}
