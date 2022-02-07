using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionHistoryRequestModel
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
    }
}
