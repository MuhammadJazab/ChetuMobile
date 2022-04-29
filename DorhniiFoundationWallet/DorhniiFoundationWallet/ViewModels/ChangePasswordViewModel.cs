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
    /// This clas is used to change Password function
    /// </summary>
    public class ChangePasswordViewModel : BaseViewModel
    {
        #region Single Property
        readonly ILoginService changePasswordApiService;
        private APIResponseModel APIResponseModel = null;

        #endregion

        #region full Property
        private string oldPassword;
        private string newPassword;
        private string confirmNewPassword;
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ChangePasswordViewModel()
        {
            SavePassword = new Command(SavePasswordclick);
            changePasswordApiService = new LoginService();
            BackButtonCommand = new Command(BackCommandClick);
        }


        #endregion

        #region Method
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
        /// Bacck to setting page
        /// </summary>
        public async void BackCommandClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Save Change Password Method
        /// </summary>
        public async void SavePasswordclick()
        {
            try
            {
                if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword) || string.IsNullOrEmpty(oldPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtEmptyPasswords, Resource.msgEmptyPasswords, Resource.txtOk);
                }
                else if (!Utilities.IsValidPassword(newPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordValidation, Resource.txtOk);
                }
                else if (!(newPassword == confirmNewPassword))
                {
                    _ = Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgConfirmPassword, Resource.txtOk);
                }
                else
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        IsLoading = true;
                        ChangePasswordRequestModel changePasswordRequest = new ChangePasswordRequestModel
                        {
                            OldPassword = OldPassword,
                            NewPassword = NewPassword,
                            UserId = Preferences.Get(StringConstant.UserID, string.Empty),
                        };
                        APIResponseModel = await changePasswordApiService.ChangePassword(changePasswordRequest);
                        if (APIResponseModel != null)
                        {
                            if (APIResponseModel.Result && APIResponseModel.Status == 200)
                            {
                                if (APIResponseModel != null)
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessful, APIResponseModel.Message, Resource.txtOk);
                                    await Application.Current.MainPage.Navigation.PopModalAsync();
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, APIResponseModel.Message, Resource.txtOk);                                    
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, APIResponseModel.Message, Resource.txtOk);                                
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, APIResponseModel.Message, Resource.txtOk);                            
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);                     
                    }
                }
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false; 
            }


        }
        #endregion

        #region Command
        public ICommand SavePassword { get; set; }
        public ICommand BackButtonCommand { get; set; }

        #endregion
    }
}
