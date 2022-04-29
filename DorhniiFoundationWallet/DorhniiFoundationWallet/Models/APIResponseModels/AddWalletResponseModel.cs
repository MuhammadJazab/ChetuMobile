namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class AddWalletResponseModel : APIResponseModel
    {
        public string WalletAddress { get; set; }
        public string Qrcode { get; set; }
        public double Balance { get; set; }
        public string WalletName { get; set; }
        public string PrivateKey { get; set; }
        public string EncryptedPrivateKey { get; set; }
    }
}

