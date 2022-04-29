using Newtonsoft.Json;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class EditWalletNameResponseModel: APIResponseModel
    {
        public List<EditedWalletData> data { get; set; }
        public class EditedWalletData
        {
            [JsonProperty("walletAddress")]
            public string WalletAddress { get; set; }
            [JsonProperty("walletName")]
            public string WalletName { get; set; }

        }

    }
}
