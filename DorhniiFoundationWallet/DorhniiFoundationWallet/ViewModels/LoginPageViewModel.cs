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
{/// <summary>
/// This class use to Login functionality
/// </summary>
    public class LoginPageViewModel : BaseViewModel
    {
        #region Private properties          
        private string enterPassword;
        private bool isTouchVisible;
        private bool isTouchFaceAvailable;
        private Command touchIdPopupCommand;
        private string touchFaceType;
        private string touchIdText;
        ILoginService loginApiService;
        APIResponseModel APIResponseModel = null;
        #endregion
        #region public properties
        public ICommand BackButtonCommand { get; set; }
        public string DohrniiTextLogo { get; set; } = StringConstant.DohrniiTextLogo;
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
        public string LockIcon { get; set; } = StringConstant.LockIcon;
        public ICommand PasswordCommand { get; set; }
        public ICommand FingerprintCommand { get; set; }
        public ICommand FaceidCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public bool IsDeviceiOS { get; set; }
        public string TouchIdText
        {
            get => touchIdText;
            set
            {
                touchIdText = value;
                OnPropertyChanged(nameof(touchIdText));
            }
        }
        public bool IsTouchFaceAvailable
        {
            get => isTouchFaceAvailable;
            set
            {
                isTouchFaceAvailable = value;
                OnPropertyChanged(nameof(IsTouchFaceAvailable));
            }
        }
        public bool IsTouchVisible
        {
            get => isTouchVisible;
            set
            {
                isTouchVisible = value;
                OnPropertyChanged(nameof(IsTouchVisible));
            }
        }
        public string TouchFaceType
        {
            get => touchFaceType;
            set
            {
                touchFaceType = value;
                OnPropertyChanged(nameof(TouchFaceType));
            }
        }
        public string EnterPassword
        {
            get => enterPassword;
            set
            {
                if (enterPassword != value)
                {
                    enterPassword = value;
                    OnPropertyChanged(nameof(EnterPassword));
                }
            }
        }
        public Command TouchIdPopupCommand
        {
            get
            {
                if (touchIdPopupCommand == null)
                {
                    touchIdPopupCommand = new Command(() =>
                    {
                        try
                        {
                            CallTouchFaceID();
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                        }
                    });
                }

                return touchIdPopupCommand;
            }
        }
        #endregion

        #region Public method
        /// <summary>
        /// Cosnructor Login page
        /// </summary>
        public LoginPageViewModel()
        {
            try
            {
                BackButtonCommand = new Command(BackButtonClick);
                loginApiService = new LoginService();
                OKCommand = new Command(LoginClick);
                var platform = DeviceInfo.Platform;
                if (platform == DevicePlatform.iOS)
                {
                    IsDeviceiOS = true;
                }

                #region Face Id login
                if (Device.RuntimePlatform == Device.Android)
                {
                    DependencyService.Get<IFingerPrintPopup>().IsFingerPrintCanAuthenticate();

                    if (Utilities.IsEnrolledFingerprints)
                    {
                        isTouchFaceAvailable = true;
                    }
                    else
                    {
                        isTouchFaceAvailable = false;
                    }
                }
                else
                {
                    if (DependencyService.Get<ILocalAuthHelper>().IsLocalAuthAvailable())
                    {
                        isTouchFaceAvailable = true;
                    }
                    else
                    {
                        isTouchFaceAvailable = false;
                    }
                }
                bool isTouchIDEnabled = Preferences.Get(StringConstant.KEY_IS_TOUCH_FACE_ID_ENABLED, false);
                if (isTouchFaceAvailable && isTouchIDEnabled)
                {
                    IsTouchVisible = true;
                    Utilities.OnAuthenticationSucceeded -= Utilities_OnAuthenticationSucceeded;
                    Utilities.OnAuthenticationSucceeded += Utilities_OnAuthenticationSucceeded;

                    TouchIdText = string.Format("Log in with {0}", Utilities.TouchFaceAuthenticationType());
                    CallTouchFaceID();
                }
                else
                {
                    touchFaceType = Utilities.TouchFaceAuthenticationType();
                    IsTouchVisible = false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to click on Back Button
        /// </summary>       
        public async void BackButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new WelcomePage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to call touch face id popup
        /// </summary>
        private void CallTouchFaceID()
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    DependencyService.Get<IFingerPrintPopup>().ShowPopup();
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    DependencyService.Get<ILocalAuthHelper>().Authenticate(null, null);
                }
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
            bool result;
            try
            {
                if (Utilities.IsValidPassword(EnterPassword))
                {
                    result = true;
                }
                else
                {
                    result = false;
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
        public static bool IsValidEnterPassword(string value)
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
        /// Method use to login command
        /// </summary>
        public async void LoginClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {

                    IsLoading = true;
                    LoginRequestModel loginRequest = new LoginRequestModel
                    {
                        Password = EnterPassword.Trim(),
                        UserID = Preferences.Get(StringConstant.UserID, string.Empty),
                    };
                    APIResponseModel = await loginApiService.LoginFunction(loginRequest);
                    if (APIResponseModel != null)
                    {
                        if (APIResponseModel.Result && APIResponseModel.Status == 200)
                        {
                            if (!IsTouchVisible)
                            {
                                var action = await Application.Current.MainPage.DisplayAlert(string.Empty, String.Format(Resource.msgDoYouWantToEnableTouchOrFaceId, touchFaceType), Resource.txtEnable, Resource.txtNotNow);
                                if (action)
                                {
                                    Preferences.Set(StringConstant.KEY_IS_TOUCH_FACE_ID_ENABLED, true);
                                }
                                else
                                {
                                    Preferences.Set(StringConstant.KEY_IS_TOUCH_FACE_ID_ENABLED, false);
                                }
                            }
                            await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, APIResponseModel.Message, Resource.txtOk);                            
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);                 
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
        #region Private Method
        /// <summary>
        /// delegate method to handle authentication successfull event
        /// </summary>       
        private async void Utilities_OnAuthenticationSucceeded()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        DependencyService.Get<IFingerPrintPopup>().HidePopup();
                    }

                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        #endregion
    }
}