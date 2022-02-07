using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class AddWalletResponseModel : APIResponseModel
    {        
        public string WalletAddress { get; set; }
        public string Qrcode { get; set; }
        public int Balance { get; set; }
        public string WalletName { get; set; }
    }
}

