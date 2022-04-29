using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class TransferTokenResponseModel: APIResponseModel
    {
        [JsonProperty("txId")]
        public string TxId { get; set; }
    }
}
