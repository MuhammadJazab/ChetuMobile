using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.IServices
{
   public interface IFingerPrintPopup
    {
        void ShowPopup();
        void HidePopup();
        void IsFingerPrintCanAuthenticate();

    }
}
