using System;
using Android;
using Android.App;
using Android.Hardware.Fingerprints;
using Android.Security.Keystore;
using Android.Support.V4.App;
using DorhniiFoundationWallet.Droid.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using Java.Security;
using Javax.Crypto;
using Xamarin.Forms;

[assembly: Dependency(typeof(FingerPrintActivity))]
namespace DorhniiFoundationWallet.Droid.DependencyServices
{
    public class FingerPrintActivity : Activity, IFingerPrintPopup
    {
        AlertDialog fingerPrintDialog;
        Activity fingerPrintActivity;

        /// <summary>
        /// Method for Hide finger print Popup
        /// </summary>
        public void HidePopup()
        {
            try
            {
                fingerPrintDialog.Dismiss();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Method for show fingerprint Popup
        /// </summary>
        [Obsolete]
        public void ShowPopup()
        {
            try
            {
                StartFingerprintService();
                fingerPrintActivity = MainActivity.Instance;
                Android.Views.View FingerPrintDialog = fingerPrintActivity.LayoutInflater.Inflate(Resource.Layout.FingerPrintPopup, null);
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(fingerPrintActivity);
                fingerPrintDialog = dialog.Create();
                fingerPrintDialog.SetView(FingerPrintDialog);
                fingerPrintDialog.SetCanceledOnTouchOutside(false);
                fingerPrintDialog.SetButton("Cancel", (c, ev) =>
                {
                    // Cancel button click task 
                    fingerPrintDialog.Dismiss();
                });
                fingerPrintDialog.Show();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        [Obsolete]
        public void IsFingerPrintCanAuthenticate()
        {
            try
            {
                KeyguardManager keyguardManager = (KeyguardManager)MainActivity.Instance.GetSystemService(KeyguardService);
                FingerprintManager fingerprintManager = (FingerprintManager)MainActivity.Instance.GetSystemService(FingerprintService);
                if (ActivityCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.UseFingerprint)
                    != (int)Android.Content.PM.Permission.Granted)
                {
                    return;
                }

                if (!fingerprintManager.IsHardwareDetected) //checking if there is any hardware supports for fingerprint.
                {
                    //if there is no hardware supports for fingerprint.
                    Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                }
                else
                {   //Checking if there is any fingerprint already registered.
                    if (!fingerprintManager.HasEnrolledFingerprints)
                    {
                        Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                    }
                    else
                    {
                        Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        [Obsolete]
        protected void StartFingerprintService()
        {
            try
            {
                KeyguardManager keyguardManager = (KeyguardManager)MainActivity.Instance.GetSystemService(KeyguardService);
                FingerprintManager fingerprintManager = (FingerprintManager)MainActivity.Instance.GetSystemService(FingerprintService);
                if (ActivityCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
                    return;
                if (!fingerprintManager.IsHardwareDetected) //checking if there is any hardware supports for fingerprint.
                {
                    //if there is no hardware supports for fingerprint.
                    Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                }
                else
                {   //Checking if there is any fingerprint already registered.
                    if (!fingerprintManager.HasEnrolledFingerprints)
                    {
                        Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                    }
                    else
                    {
                        Utilities.IsEnrolledFingerprints = fingerprintManager.HasEnrolledFingerprints;
                        if (keyguardManager.IsKeyguardSecure)
                        {
                            GenerateKey();
                        }
                        if (CipherInit())
                        {
                            FingerprintManager.CryptoObject cryptoObject = new FingerprintManager.CryptoObject(cipher);
                            FingerprintAuthActivity handler = new FingerprintAuthActivity(MainActivity.Instance);
                            handler.StartAuthentication(fingerprintManager, cryptoObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        private KeyStore keyStore;
        private Cipher cipher;
        private string keyName = "Success";
        /// <summary>
        /// Method for cipher status.
        /// </summary>
        /// <returns></returns>
        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes
                    + "/"
                    + KeyProperties.BlockModeCbc
                    + "/"
                    + KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(keyName, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                { return false; }
            }
        }
        /// <summary>
        /// Method for genrate key
        /// </summary>
        private void GenerateKey()
        {
            try
            {
                keyStore = KeyStore.GetInstance("AndroidKeyStore");
                KeyGenerator keyGenerator = null;
                keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");
                keyStore.Load(null);
                keyGenerator.Init(new KeyGenParameterSpec.Builder(keyName, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                    .SetBlockModes(KeyProperties.BlockModeCbc)
                    .SetUserAuthenticationRequired(true)
                    .SetEncryptionPaddings(KeyProperties
                    .EncryptionPaddingPkcs7).Build());
                string abc = keyGenerator.GenerateKey().ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }

}