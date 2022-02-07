

using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class TransferTokenRequestModel
    {
        [JsonProperty("walletAddressTo")]
        public string WalletAddressTo { get; set; }
        [JsonProperty("walletAddressFrom")]
        public string WalletAddressFrom { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }
        [JsonProperty("coinShortName")]
        public string CoinShortName { get; set; }
    }
}
