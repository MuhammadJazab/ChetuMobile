using Newtonsoft.Json;
using System.Collections.Generic;
namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class GetWalletDetailsResponseModel : APIResponseModel
    {
        public string WalletAddress { get; set; }
        public string WalletName { get; set; }
        [JsonProperty("qrcode")]
        public string QrCode { get; set; }
        public double Balance { get; set; }
        [JsonProperty("data")]
        public List<WalletDetailsResponseModel> Data
        {
            get; set;
        }
    }
}
