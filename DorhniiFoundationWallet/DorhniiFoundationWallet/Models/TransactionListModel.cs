using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.Models
{
    public class TransactionListModel
    {
        public string TransactionTypeImage { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDateTime { get; set; }
        public string TransactionAmount { get; set; }
        public string CoinName { get; set; }
        public string Fee { get; set; }
        public string FeeCoinShortName { get; set; }
        public string _id { get; set; }
        public string TxID { get; set; }
    }
}
