using System;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// This class use to define class properties for transaction
    /// </summary>
    public class TransactionHistoryRequestModel
    {
        public string WalletAddress { get; set; }
        public string TransactionType { get; set; }
    }
}
