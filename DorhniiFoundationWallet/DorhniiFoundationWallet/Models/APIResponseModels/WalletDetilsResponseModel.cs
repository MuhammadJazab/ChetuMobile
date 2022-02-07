using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class WalletDetailsResponseModel : APIResponseModel
    {
        [JsonProperty("coinIcon")]
        public string CoinIcon { get; set; }
        [JsonProperty("coinName")]
        public string CoinName { get; set; }
        [JsonProperty("coinShortName")]
        public string CoinShortName { get; set; }
        [JsonProperty("coinValue")]
        public int CoinValue { get; set; }
        [JsonProperty("coinUsdValue")]
        public int CoinUsdValue { get; set; }
        [JsonProperty("coinStandard")]
        public string CoinStandard { get; set; }
    }
}
