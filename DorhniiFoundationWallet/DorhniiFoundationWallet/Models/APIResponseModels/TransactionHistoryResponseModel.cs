using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for transaction
    /// </summary>
    public class TransactionHistoryResponseModel : APIResponseModel
    {
        public List<TransactionHistory> data { get; set; }
    }

    public class TransactionHistory
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string WalletAddressTo { get; set; }
        public string WalletAddressFrom { get; set; }
        public string SeedIdTo { get; set; }
        public string SeedIdFrom { get; set; }
        public int Amount { get; set; }
        public string CoinName { get; set; }
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
