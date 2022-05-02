using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class WalletDetailsResponseModel : APIResponseModel
    {
        [JsonProperty("coinId")]
        public string CoinId { get; set; }
        [JsonProperty("coinIcon")]
        public string CoinIcon { get; set; }
        [JsonProperty("coinName")]
        public string CoinName { get; set; }
        [JsonProperty("coinShortName")]
        public string CoinShortName { get; set; }
        [JsonProperty("coinValue")]
        public double CoinValue { get; set; }
        [JsonProperty("coinUsdValue")]
        public double CoinUsdValue { get; set; }
        [JsonProperty("coinStandard")]
        public string CoinStandard { get; set; }
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }

    }
}
