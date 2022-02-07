using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for setup of passwords.
    /// </summary>
    public class PasswordSetupVM : ObservableObject
    {
        #region Private Properties
        private string password;
        private string confirmPassword;
        
        #endregion
        #region Public Properties       
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public bool userRestore;
        public ICommand CreatePasswordCommand { get; set; }       
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsRestoreValidate { get; private set; }
        #endregion
        #region public Method   
        /// <summary>
        /// This method is used to setup the passwords (class constructor).
        /// </summary>
        public PasswordSetupVM()
        {
           try
            {
                CreatePasswordCommand = new Command(CreatePasswordClick);
                
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        /// <summary>
        /// Method to check the all password validation 
        /// </summary>     
        public bool PasswordValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtEmptyPasswords, Resource.msgEmptyPasswords, Resource.txtOk);
                }
                else if (!Utilities.IsValidPassword(Password) || !Utilities.IsValidPassword(ConfirmPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordValidation, Resource.txtOk);
                }
                else if (!(Password == ConfirmPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgConfirmPassword, Resource.txtOk);
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        ///Method to check if password is valid or not
        /// </summary>
        public static bool IsValidPassword(string value)
        {
            try
            {
                bool isPassword = Regex.IsMatch(value.Trim(), StringConstant.PasswordRegex);
                return isPassword;
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }

        /// <summary>
        ///Method to click on Create password 
        /// </summary>
        public async void CreatePasswordClick()
        {
            try
            {
                if (PasswordValidation())
                {
                    IsRestoreValidate = Preferences.Get("IsRestoreValidate", IsRestoreValidate);
                    Preferences.Set(StringConstant.DevicePassword, Password.Trim());                  
                    if (IsRestoreValidate)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
                        IsRestoreValidate = false;
                        Preferences.Set("IsRestoreValidate", IsRestoreValidate);
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhrasePage());
                    }                                        
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
