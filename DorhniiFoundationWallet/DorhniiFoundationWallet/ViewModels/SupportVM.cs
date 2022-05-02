using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for support functionality.
    /// </summary>
    public class SupportVM : BaseViewModel
    {
        #region Properties
        private string messageText;
        public string MessageText
        {
            get => messageText;
            set
            {
                if (messageText != value)
                {
                    messageText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string usermailId;
        public string UsermailId
        {
            get => usermailId;

            set
            {
                if (usermailId != value)
                {
                    usermailId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string userNameEntry;
        public string UserNameEntry
        {
            get => userNameEntry;

            set
            {
                if (userNameEntry != value)
                {
                    userNameEntry = value;
                    OnPropertyChanged();
                }
            }
        }
        ISuportPageService GetSuport;        
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
        public ICommand SubmitCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }
        public APIResponseModel APIResponseModel { get; private set; }       
        #endregion
        #region Methods
        /// <summary>
        /// This method is used for support assistance for the user.
        /// </summary>
        public SupportVM()
        {
            try
            {
                GetSuport = new SupportPageService();
                SubmitCommand = new Command(SubmitCommandClick);
                BackButtonCommand = new Command(BackButtonClick);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method use for submit support request
        /// </summary>
        public async void SubmitCommandClick()
        {
            try
            {
                if(!string.IsNullOrEmpty(MessageText) && !string.IsNullOrEmpty(UsermailId) && !string.IsNullOrEmpty(UserNameEntry))
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });

                        SupportPageRequestModel supportPageRequest = new SupportPageRequestModel
                        {
                            Message = MessageText,
                            UsermailId = UsermailId,
                            UserName = UserNameEntry,
                        };

                        supportPageRequest.WalletAddress = Preferences.Get(StringConstant.walletAddress, string.Empty);
                        APIResponseModel = await GetSuport.SendSupportmail(supportPageRequest);
                        if (APIResponseModel != null)
                        {
                            if (APIResponseModel.Result && APIResponseModel.Status == 200)
                            {
                                await Application.Current.MainPage.DisplayAlert(Resource.txtThankYou, APIResponseModel.Message, Resource.txtOk);
                                await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, APIResponseModel.Message, Resource.txtOk);
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
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, "No field should be empty", Resource.txtOk);
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

        /// <summary>
        /// This method is used to click on Back Button
        /// </summary>       
        public async void BackButtonClick()
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

        #endregion
    }
}
