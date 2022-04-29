using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using DorhniiFoundationWallet.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using System;
using Xamarin.Forms;
namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is for privacy policy
    /// </summary>
    public class PrivacyPolicyVM : BaseViewModel
    {
        #region Private Class Properties
        private string termAndConditionURL;
        public ICommand BackOnPrivacy { get; set; }
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

        #region Constructor




        #endregion

        #region Method
        /// <summary>
        /// This is a class constructor to initialize class property
        /// </summary>
        public PrivacyPolicyVM()
        {
            GetTermAndConditionURL();
            BackOnPrivacy = new Command(BackCommandClick);
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
        #endregion

        #region Command
        /// <summary>
        /// This command is used for navigate to back Page
        /// </summary>
        public async void BackCommandClick()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());

        }
        #endregion       
    }
}

