using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is for privacy policy
    /// </summary>
    public class PrivacyPolicyVM : ObservableObject
    {
        #region Private Class Properties
        private string termAndConditionURL;
        private Command backCommand;
        #endregion

        #region Public Class Properties
        
        public string TermAndConditionURL
        {
            get { return termAndConditionURL; }
            set
            {
                termAndConditionURL = value;
                OnPropertyChanged(nameof(TermAndConditionURL));
            }
        }
        
        #endregion

        /// <summary>
        /// This is a class constructor to initialize class property
        /// </summary>
        public PrivacyPolicyVM()
        {
            GetTermAndConditionURL();
        }

        /// <summary>
        /// This method use Get the Privacy policy
        /// </summary>
        private async void GetTermAndConditionURL()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {

                    TermAndConditionURL = "https://www.google.co.in/";

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

        /// <summary>
        /// This command is used for navigate to back Page
        /// </summary>
        public Command BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new Command(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());
                    });
                }

                return backCommand;
            }
        }
    }
}

