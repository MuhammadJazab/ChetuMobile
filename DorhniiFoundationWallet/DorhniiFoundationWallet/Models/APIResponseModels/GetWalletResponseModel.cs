using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for get wallet
    /// </summary>
    public class GetWalletResponseModel : APIResponseModel
    {
       public List<AddWalletResponseModel> data { get; set; }
    }       
   
}
    
