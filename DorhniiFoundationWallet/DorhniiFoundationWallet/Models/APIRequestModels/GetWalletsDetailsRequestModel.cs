using System;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// This class use to define class properties for get wallet detalis
    /// </summary>
    public class GetWalletsDetailsRequestModel
    {
        public string walletAddress { get; set; }
        public string seedId { get; set; }
    }
}
