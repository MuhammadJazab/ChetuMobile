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
        #region Properties
        #region Private Properties
        private string password;
        private string confirmPassword;
        #endregion

        #region Public Properties

        /// <summary>
        /// This property gets or sets the image of the app icon.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This command property gets or sets the value of create password event.
        /// </summary>
        public ICommand CreatePasswordCommand { get; set; }

        /// <summary>
        /// This property gets or sets the value of confirm password field.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets or set the value of password field.
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// This method is used to setup the passwords.
        /// </summary>
        public PasswordSetupVM()
        {
            CreatePasswordCommand = new Command(CreatePasswordClick);
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
                    Application.Current.MainPage.DisplayAlert(Resource.txtEmptyPasswords, Resource.msgEmptyPasswords, Resource.txtOk);
                }
                else if (!Utilities.IsValidPassword(Password) || !Utilities.IsValidPassword(ConfirmPassword))
                {
                    Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordValidation, Resource.txtOk);
                }
                else if (!(Password == ConfirmPassword))
                {
                    Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgConfirmPassword, Resource.txtOk);
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
                bool isPassword = Regex.IsMatch(value.Trim(), StringConstant.passwordRegex);
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
                Preferences.Set(StringConstant.DevicePassword, Password.Trim());
                await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhraseLabel());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
