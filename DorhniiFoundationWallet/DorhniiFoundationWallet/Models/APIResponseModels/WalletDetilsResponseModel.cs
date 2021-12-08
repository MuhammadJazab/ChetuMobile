using System;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for wallet details
    /// </summary>
    public class WalletDetailsResponseModel : APIResponseModel
    {
        public string coinIcon { get; set; }
        public string coinName { get; set; }
        public string coinShortName { get; set; }
        public int coinValue { get; set; }
        public int coinUsdValue { get; set; }
    }
}
