using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
     public interface IUpdatedateWalletName
    {
        Task<EditWalletNameResponseModel> UpdateWalletName(EditWalletNameRequestModel requestModel);
    }
}
