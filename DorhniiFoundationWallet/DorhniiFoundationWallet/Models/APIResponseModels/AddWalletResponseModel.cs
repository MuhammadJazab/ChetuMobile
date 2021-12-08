using System;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for add wallet
    /// </summary>
    public class AddWalletResponseModel : APIResponseModel
    {
        public string WalletAddress { get; set; }
        public string Qrcode { get; set; }
        public int Balance { get; set; }
        public string WalletName { get; set; }
    }
}

