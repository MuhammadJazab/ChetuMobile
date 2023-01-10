using Xamarin.Forms;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for Sign In Functionality
    /// </summary>
    public class SignInVM : ObservableObject
    {

        #region Properties

        #region Boolean Properties
        public bool Iseyeselected = false;
        public bool isPassword = true;
        #endregion

        #region Image String Properties
        /// <summary>
        /// This property gets or sets the image of the App icon.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This property gets or sets the image of the Password Lock icon.
        /// </summary>
        public string PasswordIcon { get; set; } = StringConstant.PasswordIcon;

        /// <summary>
        /// This property gets or sets the image of the Eyecut icon.
        /// </summary>
        private string crossEyeSelected = StringConstant.CrossEyeCut;
        #endregion

        #region Command Properties
        /// <summary>
        /// This property gets or sets the command for Next Button Click.
        /// </summary>
        public ICommand NextButtonCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for New Account Label Gesture Click.
        /// </summary>
        public ICommand NewAccountCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Eye cross clicked.
        /// </summary>
        public ICommand CrossEyeCommand { get; }
        #endregion

        #region Bindable Public Properties

        /// <summary>
        /// This property gets or sets the value if eye cross is visible or not
        /// </summary>
        public string CrossEyeSelected
        {
            get
            {
                return crossEyeSelected;
            }
            set
            {
                if (crossEyeSelected != value)
                {
                    crossEyeSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property is used to validate the password fields
        /// </summary>
        public bool IsPassword
        {
            get
            {
                return isPassword;
            }
            set
            {
                if (isPassword != value)
                {
                    isPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// This method is used for Sign In Functionality
        /// </summary>
        public SignInVM()
        {
            CrossEyeCommand = new Command(CrossEyeClick);
            NewAccountCommand = new Command(NewAccountClick);
            NextButtonCommand = new Command(NextButtonClick);
           
        }
        /// <summary>
        /// Method to change the eye icon and password secure 
        /// </summary>
        public void CrossEyeClick()
        {
            try
            {
                if (!Iseyeselected)
                {
                    Iseyeselected = true;
                    CrossEyeSelected = StringConstant.CrossEyeUnCut;
                    IsPassword = false;
                }
                else
                {
                    Iseyeselected = false;
                    CrossEyeSelected = StringConstant.CrossEyeCut;
                    IsPassword = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to click on Next Button
        /// </summary>
        public async void NextButtonClick()
        {
            try
            {
                Preferences.Set(StringConstant.TabKeyId, 1);
                await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to click on New Account Label Tap Getsure
        /// </summary>
        public async void NewAccountClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new PasswordSetupPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
