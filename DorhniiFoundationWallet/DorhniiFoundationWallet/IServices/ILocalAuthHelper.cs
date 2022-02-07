using System;

namespace DorhniiFoundationWallet.IServices
{
    public interface ILocalAuthHelper
    {
        void Authenticate(Action onSuccess, Action onFailure);
        bool IsLocalAuthAvailable();
        string BiometricType();

    }
}
