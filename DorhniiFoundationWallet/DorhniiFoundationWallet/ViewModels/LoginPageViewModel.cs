using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using DorhniiFoundationWallet.IServices;

namespace DorhniiFoundationWallet.ViewModels
{/// <summary>
/// This class use to Login functionality
/// </summary>
    public class LoginPageViewModel : ObservableObject
    {
        #region Private properties          
        private string enterPassword;
        private bool isTouchVisible;
        private bool isTouchFaceAvailable;
        private Command touchIdPopupCommand;
        private string touchFaceType;
        private string touchIdText;
        #endregion
        #region public properties        
        public string LockIcon { get; set; } = StringConstant.LockIcon;        
        public ICommand PasswordCommand { get; set; }
        public ICommand FingerprintCommand { get; set; }
        public ICommand FaceidCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OKCommand { get; set; }
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
                OKCommand = new Command(LoginClick);
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
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }


        }

        //public class EntryValidatorBehavior : Behavior<Entry>
        //{
        //    const string numberRegex = @"^[a-zA-Z]+$";

        //    static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EntryValidatorBehavior), false);

        //    public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        //    public bool IsValid
        //    {
        //        get { return (bool)base.GetValue(IsValidProperty); }
        //        private set { base.SetValue(IsValidPropertyKey, value); }
        //    }

        //    protected override void OnAttachedTo(Entry bindable)
        //    {
        //        bindable.TextChanged += HandleTextChanged;
        //    }

        //    void HandleTextChanged(object sender, TextChangedEventArgs e)
        //    {
        //        IsValid = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        //        ((Entry)sender).Text = IsValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
        //    }

        //    protected override void OnDetachingFrom(Entry bindable)
        //    {
        //        bindable.TextChanged -= HandleTextChanged;
        //    }
        //}

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
                string myval = Preferences.Get("Password", " ");
                var pass = EnterPassword.ToString().Trim();
                if (myval == pass)
                {
                    //Preferences.Set(StringConstant.DevicePassword, EnterPassword.Trim());                    
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordAlert, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }        
        #endregion

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
                    string myval = Preferences.Get("Password", " ");
                    if (myval == EnterPassword)
                    {
                        Preferences.Set(StringConstant.DevicePassword, EnterPassword.Trim());                       
                        await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Resource.txtInvalidConfirmation, Resource.msgPasswordAlert, Resource.txtOk);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

    }
}