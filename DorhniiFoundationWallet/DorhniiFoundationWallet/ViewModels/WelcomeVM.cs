
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
    /// This class is used during the welcome screen.
    /// </summary>
    public class WelcomeVM : BaseViewModel
    {
        #region private properties
        private static IRestoreWalletService apiService;
        private bool isWalletGateway;
        private string enteredseedPhrase;
        private string userPrivateKey;
        #endregion
        #region Public Properties 
        public string UserRestoreEntry;
        public bool IsDeviceiOS { get; set; }
        public string UserPrivateKey
        {
            get => userPrivateKey;
            set
            {
                userPrivateKey = value; 
                OnPropertyChanged(nameof(UserPrivateKey));
            }
        }

        public string EnteredseedPhrase
        {
            get => enteredseedPhrase;
            set
            {
                enteredseedPhrase = value;
                OnPropertyChanged(nameof(EnteredseedPhrase));
            }
        }
        public bool IsWalletGateway
        {
            get => isWalletGateway;
            set
            {
                isWalletGateway = value;
                OnPropertyChanged(nameof(IsWalletGateway));
            }
        }
        public string DohrniiTextLogo { get; set; } = StringConstant.DohrniiTextLogo;
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
        public string AppIcon { get; set; } = StringConstant.AppIcon;        
        public ICommand CreateButtonCommand { get; set; }       
        public ICommand RestoreButtonCommand { get; set; }
        public ICommand ConfirmButtonCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }
        #endregion
        #region public  Methods
        /// <summary>
        /// This method is used to create and restore accounts.
        /// </summary>
        public WelcomeVM()
        {
            try
            {
                apiService = new RestoreWalletService();
                CreateButtonCommand = new Command(CreateButtonClick);
                RestoreButtonCommand = new Command(RestoreButtonClick);
                IsWalletGateway = false;
                ConfirmButtonCommand = new Command(ConfirmButtonCommandClick);
                BackButtonCommand = new Command(BackButtonClick);
                var platform = DeviceInfo.Platform;
                if (platform == DevicePlatform.iOS)
                {
                    IsDeviceiOS = true;
                }
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This Method used to navigate from Import seed /private key page to AddWallet Page
        /// </summary>
        public async void ConfirmButtonCommandClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    IsLoading = true;
                    bool IstoreAccountHideButton= true;
                    bool IsRestoreValidate = false;
                    string RestoreType = string.Empty;
                    IsRestoreValidate = true;
                    if (!string.IsNullOrEmpty(EnteredseedPhrase))
                    {
                        EnteredseedPhrase = EnteredseedPhrase.Trim();
                        UserRestoreEntry = EnteredseedPhrase;
                        RestoreType = StringConstant.SEED_PHRASE;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(UserPrivateKey))
                        {
                            UserRestoreEntry = UserPrivateKey.Trim();
                            RestoreType = StringConstant.PRIVATE_kEY;
                        }
                    }
                    RestoreWalletRequestModel restoreSeed = new RestoreWalletRequestModel
                    {
                        RestoreType = RestoreType,
                        RestorePayload = UserRestoreEntry,
                    };
                    RestoreWalletResponseModel response = await apiService.RestoreWallet(restoreSeed);
                    if (response != null)
                    {
                        if (response.Status == 200 && response.Result)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessAlert, response.Message, Resource.txtOk);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new PasswordSetupPage());
                            Preferences.Set(StringConstant.IsRestoreValidate, IsRestoreValidate);
                            Preferences.Set(StringConstant.SeedId, response.SeedId);
                            Preferences.Set(StringConstant.walletAddress, response.WalletAddress);
                            Preferences.Set(StringConstant.PrivateKey, response.PrivateKey);
                            Preferences.Set(StringConstant.EncryptedPrivateKey, response.EncryptedPrivateKey);
                            Preferences.Set(StringConstant.IstoreAccountHideButton, IstoreAccountHideButton);
                        }
                        else if (response.Status == 202)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessAlert, response.Message, Resource.txtOk);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
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
                await Application.Current.MainPage.Navigation.PushModalAsync(new WelcomePage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to click on Create Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void CreateButtonClick()
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

        /// <summary>
        /// This method is used to click on Restore Button
        /// </summary>
        public void RestoreButtonClick()
        {
            try
            {
                IsWalletGateway = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
