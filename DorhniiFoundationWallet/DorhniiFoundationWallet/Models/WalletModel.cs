namespace DorhniiFoundationWallet.Models
{
    public class WalletModel
    {
        public string WalletAdress { get; set; }
        public string Qrcode { get; set; }
        public double Balance { get; set; }
        public string WalletFormattedBalance { get; set; }
        public string WalletName { get; set; }
        public string Image { get; set; }
        public string PrivateKey { get; set; }
    }
}
