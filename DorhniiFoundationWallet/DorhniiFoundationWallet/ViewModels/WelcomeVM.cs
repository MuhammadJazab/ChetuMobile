using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used during the welcome screen.
    /// </summary>
    public class WelcomeVM : ObservableObject
    {
        #region private properties
        private static IVerifySeedPhrasesService apiService;
        private bool isWalletGateway;
        private string enteredseedPhrase;
        #endregion
        #region Public Properties
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
        public string AppIcon { get; set; } = StringConstant.AppIcon;        
        public ICommand CreateButtonCommand { get; set; }       
        public ICommand RestoreButtonCommand { get; set; }
        public ICommand ConfirmButtonCommand { get; set; }
        #endregion
        #region public  Methods
        /// <summary>
        /// This method is used to create and restore accounts.
        /// </summary>
        public WelcomeVM()
        {
            try
            {               
                apiService = new VerifySeedPhrasesService();
                CreateButtonCommand = new Command(CreateButtonClick);
                RestoreButtonCommand = new Command(RestoreButtonClick);
                IsWalletGateway = false;
                ConfirmButtonCommand = new Command(ConfirmButtonCommandClick);
                bool IsRestoreValidate = false;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This Method used to navigate from Import seed /private key page to AddWallet Page
        /// </summary>
        private int seedPhraseId = 0;
        public async void ConfirmButtonCommandClick()
        {
            try
            {
                bool IsRestoreValidate = false;
                EnteredseedPhrase = EnteredseedPhrase.Trim();
                string[] enteredList = EnteredseedPhrase.Split(' ');
                IsRestoreValidate = true;
                if (enteredList.Length == 24)
                {
                    IsRestoreValidate = true;
                    List<SeedPhras> seedPhraseList = new List<SeedPhras>();
                    foreach (string enteredData in enteredList)
                    {
                        seedPhraseId++;
                        seedPhraseList.Add(new SeedPhras() { Id = seedPhraseId, Val = enteredData });
                    }
                    SeedPhraseVerifyRequestModel seedPhraseVerifyRequestModel = new SeedPhraseVerifyRequestModel
                    {
                        SeedPhrases = seedPhraseList
                    };
                    seedPhraseVerifyRequestModel.SeedId = Preferences.Get("Seedid", string.Empty);
                    ResponseModel response = await apiService.VerifySeedPhrase(seedPhraseVerifyRequestModel);
                    if (response != null)
                    {
                        if (response.Status == 200 && response.Result)
                        {                           
                            await Application.Current.MainPage.Navigation.PushModalAsync(new PasswordSetupPage());
                            Preferences.Set("IsRestoreValidate", IsRestoreValidate);
                        }
                        else if (response.Status == 400)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, response.Error, Resource.txtOk);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, response.Error, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                    }
                }                                
                else
                {     
                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert,Resource.msgRestoreSeedAlert, Resource.txtOk);
                                                        
                }
            }
            catch(Exception ex)
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
                //await Application.Current.MainPage.Navigation.PushModalAsync(new PasswordSetupPage());
                await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
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
                //await Application.Current.MainPage.Navigation.PushModalAsync(new ValidateSeedPhrasePage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
