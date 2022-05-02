using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class PasswordSetupResponseModel
    {
        public bool result { get; set; }
        public int status { get; set; }
        public string userId { get; set; }
        public string message { get; set; }
    }
}
