using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public  class GasEstimateRequestModel
    {
        [JsonProperty("walletAddressTo")]
        public string WalletAddressTo { get; set; }
        [JsonProperty("walletAddressFrom")]
        public string WalletAddressFrom { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }
        [JsonProperty("coinShortName")]
        public string CoinShortName { get; set; }
    }
}
