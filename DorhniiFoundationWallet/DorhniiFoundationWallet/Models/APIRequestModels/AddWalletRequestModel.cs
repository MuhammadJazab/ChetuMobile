using System;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// This class use to define class properties for add wallet 
    /// </summary>
    public class AddWalletRequestModel
    {
        public string seedId { get; set; }
        public string walletName { get; set; }

    }
}
