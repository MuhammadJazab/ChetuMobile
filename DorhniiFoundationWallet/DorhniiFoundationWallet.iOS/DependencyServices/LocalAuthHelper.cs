using System;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.iOS.DependencyServices;
using Foundation;
using LocalAuthentication;
using UIKit;
using Xamarin.Forms;
using DorhniiFoundationWallet.IServices;

[assembly: Dependency(typeof(LocalAuthHelper))]
namespace DorhniiFoundationWallet.iOS.DependencyServices
{
    public class LocalAuthHelper: ILocalAuthHelper
    {
        private enum LocalAuthType
        {
            None,
            Passcode,
            TouchId,
            FaceId
        }
        public bool IsLocalAuthAvailable()
        {
            bool isstatus = GetLocalAuthType() != LocalAuthType.None;
            return GetLocalAuthType() != LocalAuthType.None;
        }
        public void Authenticate(Action onSuccess, Action onFailure)
        {
            try
            {
                var context = new LAContext();

                if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out NSError AuthError)
                    || context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out AuthError))
                {
                    var replyHandler = new LAContextReplyHandler((success, error) =>
                    {
                        if (success)
                        {
                            onSuccess?.Invoke();
                            context.BeginInvokeOnMainThread(() =>
                            {
                                Utilities.OnAuthenticationSuccessfull();
                            });
                        }
                        else
                        {
                            onFailure?.Invoke();
                        }
                    });
                    var authType = GetLocalAuthType();
                    context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, "Log in with registered " + authType, replyHandler);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private LocalAuthType GetLocalAuthType()
        {
            try
            {
                var localAuthContext = new LAContext();
                if (localAuthContext.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out NSError AuthError))
                {
                    if (localAuthContext.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
                    {
                        if (GetOsMajorVersion() >= 11 && localAuthContext.BiometryType == LABiometryType.FaceId)
                        {
                            return LocalAuthType.FaceId;
                        }
                        return LocalAuthType.TouchId;
                    }
                    return LocalAuthType.Passcode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return LocalAuthType.None;
        }
        // <summary>
        // Gets the os major version.
        // </summary>
        // <returns>The os major version.</returns>
        public int GetOsMajorVersion()
        {
            return int.Parse(UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
        }
        public string BiometricType()
        {
            string biometricType = string.Empty;
            var localAuthContext = new LAContext();
            if (localAuthContext.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out NSError AuthError))
            {
                //Checking biometry type.
                switch (localAuthContext.BiometryType)
                {
                    case LABiometryType.FaceId:
                        biometricType = StringConstant.Face_ID;
                        break;
                    case LABiometryType.TouchId:
                        biometricType = StringConstant.Touch_ID;
                        break;
                    case LABiometryType.None:
                        biometricType = StringConstant.Passcode;
                        break;
                }
            }
            return biometricType;
        }
    }
}

