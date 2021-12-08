using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for get wallet details
    /// </summary>
    public class GetWalletDetailsResponseModel : APIResponseModel
    {
        public string walletAddress { get; set; }
        public string walletName { get; set; }
        public string qrCode { get; set; }
        public int balance { get; set; }
        public List<WalletDetailsResponseModel> data { get; set; }
    }
}
