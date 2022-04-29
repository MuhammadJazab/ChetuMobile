using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class TransactionHistoryResponseModel : APIResponseModel
    {
        public List<TransactionHistory> data { get; set; }
    }
    public class TransactionHistory
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("walletAddressTo")]
        public string WalletAddressTo { get; set; }
        [JsonProperty("walletAddressFrom")]
        public string WalletAddressFrom { get; set; }
        [JsonProperty("seedIdTo")]
        public string SeedIdTo { get; set; }
        [JsonProperty("seedIdFrom")]
        public string SeedIdFrom { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("coinName")]
        public string CoinName { get; set; }
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("fee")]
        public string Fee { get; set; }
        [JsonProperty("feeCoinShortName")]
        public string FeeCoinShortName { get; set; }
        [JsonProperty("txId")]
        public string TxId { get; set; }


    }
}
