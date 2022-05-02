using Newtonsoft.Json;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class EditWalletNameResponseModel: APIResponseModel
    {
      
       
            [JsonProperty("walletAddress")]
            public string WalletAddress { get; set; }
            [JsonProperty("walletName")]
            public string WalletName { get; set; }

       

    }
}
