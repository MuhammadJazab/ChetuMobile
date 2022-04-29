using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
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
    public class PasswordSetupVM : BaseViewModel
    {
        #region Private Properties
        private string password;
        private string confirmPassword;
        ILoginService passwordapiService;
        PasswordSetupResponseModel PasswordSetupResponseModele = null;

        #endregion

        #region Public Properties
        public string DohrniiTextLogo { get; set; } = StringConstant.DohrniiTextLogo;
        public string EntryLockICon { get; set; } = StringConstant.EntryLockICon;
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public bool userRestore;
        public bool IsDeviceiOS { get; set; }
        public ICommand BackButtonCommand { get; set; }
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

        #region Constructor
        /// <summary>
        /// This method is used to setup the passwords (class constructor).
        /// </summary>
        public PasswordSetupVM()
        {
            try
            {
                CreatePasswordCommand = new Command(CreatePasswordClick);
                passwordapiService = new LoginService();
                BackButtonCommand = new Command(BackButtonClick);
                var platform = DeviceInfo.Platform;
                if (platform == DevicePlatform.iOS)
                {
                    IsDeviceiOS = true;
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        #endregion

        #region public Method
        /// <summary>
        /// This method is used to click on Back Button
        /// </summary>       
        public async void BackButtonClick()
        {
            try
            {
                _ = await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
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
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        IsLoading = true;
                        PasswordSetupRequestModel passwordSeupRequest = new PasswordSetupRequestModel
                        {
                            Password = password,
                            DeviceName = DependencyService.Get<IDeviceIdGetter>().GetDeviceIDDetail(),
                            Restore= Preferences.Get(StringConstant.IsRestoreValidate, false),
                        };
                        PasswordSetupResponseModele = await passwordapiService.CreatePasswrod(passwordSeupRequest);
                        if (PasswordSetupResponseModele != null)
                        {
                            if (PasswordSetupResponseModele.result && PasswordSetupResponseModele.status == 200)
                            {
                                Preferences.Set(StringConstant.UserID, PasswordSetupResponseModele.userId);

                                IsRestoreValidate = Preferences.Get(StringConstant.IsRestoreValidate, false); //check did user comes via restore option 
                                if (IsRestoreValidate)
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtWalletRestored, PasswordSetupResponseModele.message, Resource.txtOk);
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
                                    IsRestoreValidate = false;
                                    Preferences.Set(StringConstant.IsRestoreValidate, IsRestoreValidate);
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtWalletCreated, PasswordSetupResponseModele.message, Resource.txtOk);
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhrasePage());
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, PasswordSetupResponseModele.message, Resource.txtOk);                               
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
