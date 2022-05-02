using Android;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Support.V4.App;
using DorhniiFoundationWallet.Helpers;
using Xamarin.Essentials;


namespace DorhniiFoundationWallet.Droid
{
    /// <summary>
    /// FingerprintAuthActivity class
    /// </summary>
    [System.Obsolete]
    internal class FingerprintAuthActivity : FingerprintManager.AuthenticationCallback
    {
        private Context fingerprinActivity;
        public FingerprintAuthActivity(Context mainActivity)
        {
            try
            {
                this.fingerprinActivity = mainActivity;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
        /// <summary>
        /// Start Authentication of device
        /// </summary>
        /// <param name="fingerprintManager">class to manage fingerPrint</param>
        /// <param name="cryptoObject"></param>
        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            try
            {
                CancellationSignal cancellationSignal = new CancellationSignal();
                if (ActivityCompat.CheckSelfPermission(fingerprinActivity, Manifest.Permission.UseFingerprint)
                    != (int)Android.Content.PM.Permission.Granted)
                    return;
                fingerprintManager.Authenticate(cryptoObject, cancellationSignal, 0, this, null);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Authentication Failed
        /// </summary>
        [System.Obsolete]
        public override void OnAuthenticationFailed()
        {
            try
            {
                //Toast.MakeText(fingerprinActivity, "Fingerprint is not registered.", ToastLength.Long).Show();
                //App.Current.MainPage.DisplayAlert("Failed", "Fingerprint authentication failed. Please try again...", "Ok");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Authentication Succeeded
        /// </summary>
        /// <param name="result">status</param>
        [System.Obsolete]
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            try
            {
                if (Preferences.Get(StringConstant.KEY_IS_TOUCH_FACE_ID_ENABLED, false))
                {
                    Utilities.OnAuthenticationSuccessfull();
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }

}