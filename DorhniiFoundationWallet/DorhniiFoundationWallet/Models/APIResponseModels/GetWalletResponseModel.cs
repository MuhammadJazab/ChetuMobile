using Newtonsoft.Json;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class GetWalletResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public List<AddWalletResponseModel> Data { get; set; }
    }
}

