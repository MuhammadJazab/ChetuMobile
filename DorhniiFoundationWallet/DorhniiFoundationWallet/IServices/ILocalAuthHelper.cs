using System;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used for FingerPrint and face Id
    /// </summary>
    public interface ILocalAuthHelper
    {
        void Authenticate(Action onSuccess, Action onFailure);
        bool IsLocalAuthAvailable();
        string BiometricType();

    }
}
