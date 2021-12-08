using System;

namespace DorhniiFoundationWallet.Models
{
    /// <summary>
    /// This class use to define class properties for wallet
    /// </summary>
    public class WalletModel
    {
        public string WalletAdress { get; set; }
        public string Qrcode { get; set; }
        public string Balance { get; set; }
        public string WalletName { get; set; }
        public string Image { get; set; }
    }
}
