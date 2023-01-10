using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using Microsoft.AppCenter.Crashes;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.Helpers
{
    /// <summary>
    /// This class use for define reusable method
    /// </summary>
    public class Utilities
    {
        public static WalletListModel WalletList { get; set; }
        public static bool IsEnrolledFingerprints;//For android touch finger only
        public delegate void AuthenticationSucceeded();
        public static event AuthenticationSucceeded OnAuthenticationSucceeded;

        public static string Scannedtext { get; set; }

        public static int CoinId = 0;

        /// <summary>
        /// get deleget event on authenticationSucceeded.
        /// </summary>
        public static void OnAuthenticationSuccessfull()
        {
            OnAuthenticationSucceeded();
        }

        /// <summary>
        /// Method is used to Encrypt password
        /// </summary>
        /// <param name="clearText">Password</param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Method is used to Decrypt password
        /// </summary>
        /// <param name="cipherText">password</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        ///Method to check if phone is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string value)
        {
            try
            {
                bool isPassword = Regex.IsMatch(value, StringConstant.PasswordRegex);
                return isPassword;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        /// this method use to return authentication type face or touch
        /// </summary>
        /// <returns></returns>
        public static string TouchFaceAuthenticationType()
        {
            try
            {
                string authenticationType = string.Empty;
                if (Device.RuntimePlatform == Device.Android)
                {
                    authenticationType = StringConstant.Touch_ID;
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    string biometricType = DependencyService.Get<ILocalAuthHelper>().BiometricType();
                    //Checking biometry type.
                    switch (biometricType)
                    {
                        case StringConstant.Face_ID:
                            authenticationType = StringConstant.Face_ID;
                            break;
                        case StringConstant.Touch_ID:
                            authenticationType = StringConstant.Touch_ID;
                            break;
                        case StringConstant.Passcode:
                            authenticationType = StringConstant.Passcode;
                            break;
                    }
                }
                return authenticationType;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// This method is used for the check internet available.
        /// </summary>
        /// <returns></returns>
        public static bool IsNetworkAvailable()
        {
            bool isNetwork = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                isNetwork = true;
            }
            return isNetwork;
        }
    }
}
