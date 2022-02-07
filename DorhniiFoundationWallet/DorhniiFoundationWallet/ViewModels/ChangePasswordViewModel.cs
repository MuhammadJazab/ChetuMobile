using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
   public class ChangePasswordViewModel : ObservableObject
    {
        #region private properties
        private string oldPassword;
        private string newPassword;
        private string confirmNewPassword;
        #endregion 
        #region public  properties
        public string OldPassword
        {
            get => oldPassword;
            set
            {
                if (oldPassword != value)
                {
                    oldPassword = value;
                    OnPropertyChanged(nameof(OldPassword));
                }
            }
        }
        public string NewPassword
        {
            get => newPassword;
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }
        public string ConfirmNewPassword
        {
            get => confirmNewPassword;
            set
            {
                if (confirmNewPassword != value)
                {
                    confirmNewPassword = value;
                    OnPropertyChanged(nameof(ConfirmNewPassword));
                }
            }
        }


        public ICommand SavePassword { get; set; }
        #endregion
        /// <summary>
        /// Class Constructor
        /// </summary>
        public ChangePasswordViewModel()
        {
            SavePassword = new Command(SavePasswordclick);
        }
        public bool PasswordValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtEmptyPasswords, Resource.msgEmptyPasswords, Resource.txtOk);
                }
                else if (!Utilities.IsValidPassword(newPassword) || !Utilities.IsValidPassword(confirmNewPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordValidation, Resource.txtOk);
                }
                else if (!(newPassword == confirmNewPassword))
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
        /// Save Change Password Method
        /// </summary>
        public async void SavePasswordclick()
        {
            var devicePassword = Preferences.Get("Password", " ");
            if (oldPassword == devicePassword && PasswordValidation())
            {
                Preferences.Set(StringConstant.DevicePassword, NewPassword.Trim());
                _= Application.Current.MainPage.DisplayAlert(Resource.txtSuccessful,Resource.msgPasswordChanged, Resource.txtOk);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                if(oldPassword == devicePassword)
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgPasswordValidation, Resource.txtOk);
                }
                else
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgConfirmPassword, Resource.txtOk);
                }
            }
        }
    }
}
