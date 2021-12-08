using System;

namespace DorhniiFoundationWallet.Models
{
    /// <summary>
    /// This class use to define class properties for Transaction
    /// </summary>
    public class TransactionListModel
    {
        public string TransactionTypeImage { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDateTime { get; set; }
        public string TransactionAmount { get; set; }
        public string CoinName { get; set; }
        public string FeeDetails { get; set; }
    }
}
