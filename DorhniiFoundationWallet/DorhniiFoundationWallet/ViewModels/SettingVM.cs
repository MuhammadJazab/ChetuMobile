using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to display the settings page and its functionalities
    /// </summary>
    public class SettingVM : ObservableObject
    {
        #region Private Properties             
        private bool isCheckBoxVisible;
        private bool isBoxEnable;
        #endregion
        #region Image String Properties        
        public string SupportIcon { get; set; } = StringConstant.SupportIcon;       
        public string PrivacyIcon { get; set; } = StringConstant.PrivacyIcon;      
        public string TermsIcon { get; set; } = StringConstant.TermsIcon;       
        public string AccountsIcon { get; set; } = StringConstant.AccountsIcon;       
        public string LanguageIcon { get; set; } = StringConstant.LanguageIcon;       
        public string FingerprintIcon { get; set; } = StringConstant.FingerprintIcon;       
        public string ShowSeedIcon { get; set; } = StringConstant.ShowSeedIcon;      
        public string LogoutIcon { get; set; } = StringConstant.LogoutIcon;
          
        #endregion
        #region Public Properties   
        public ICommand TermofUseCommand { get; set; }
        public ICommand PrivacyPolicyCommand { get; set; }
        public ICommand ChangePassword { get; set; }
        public ICommand SupportCommand { get; set; }
        public ICommand BiometricCommand { get; set; }
        public bool IsBoxEnable
        {
            get => isBoxEnable;
            set
            {
                if(isBoxEnable != value)
                {
                    isBoxEnable = value;
                    OnPropertyChanged();
                }

            }
        }
        public bool IsCheckBoxVisible
        {
            get => isCheckBoxVisible;
            set
            {
                isCheckBoxVisible = value;
                OnPropertyChanged(nameof(IsCheckBoxVisible));
            }


        }

        #endregion
        #region Methods
        /// <summary>
        /// This method is used to display the settings page and its functionalities
        /// </summary>
        public SettingVM()
        {
            
            try
            {               
                IsCheckBoxVisible = false;
                SupportCommand = new Command(SupportClick);
                BiometricCommand = new Command(BiometricCommandClick);
                ChangePassword = new Command(ChangePasswordClick);
                PrivacyPolicyCommand = new Command(PrivacyPolicyClick);
                TermofUseCommand = new Command(TermofUseClick);               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method use to navigate to Terms of Use Page
        /// </summary>
        public async void TermofUseClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new TermsofUsePage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method use to navigate to Privacy Page
        /// </summary>
        public async void PrivacyPolicyClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new PrivacyPolicy());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method use to navigate to Change Password Page
        /// </summary>
        public async void ChangePasswordClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ChangePasswordPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
           
        }

        /// <summary>
        /// Method to click on Support Button
        /// </summary>
        public async void SupportClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SupportPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        ///BioMetric Enable disable method
        public void BiometricCommandClick()
        {
            try
            {
                IsCheckBoxVisible = true;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        #endregion
    }
}
