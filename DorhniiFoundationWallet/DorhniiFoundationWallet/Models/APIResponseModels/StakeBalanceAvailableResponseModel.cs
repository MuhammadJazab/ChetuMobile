using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class StakeBalanceAvailableResponseModel : APIResponseModel
    {
        [JsonProperty("coinUsdValue")]
        public double CoinUsdValue { get; set; }
        [JsonProperty("coinValue")]
        public double CoinValue { get; set; }
        [JsonProperty("coinShortName")]
        public string CoinShortName { get; set; }     
    }
}
