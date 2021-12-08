using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to entry and verify seed phrases.
    /// </summary>
    public class EnterSeedPhraseVM : ObservableObject
    {
        #region Properties
        /// <summary>
        /// This field intialises the verify seed phrase interface service.
        /// </summary>
        private static IVerifySeedPhrasesService apiService;

        /// <summary>
        /// This property gets or sets the image of the app icon.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This public property gets or sets the list of seed entries.
        /// </summary>
        public ObservableCollection<SeedPhraseEntryModel> SeedEntryList { get; set; }

        /// <summary>
        /// This property gets or sets the command for Next Button.
        /// </summary>
        public ICommand NextCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used for entry and verify seed phrases.
        /// </summary>
        public EnterSeedPhraseVM()
        {
            apiService = new VerifySeedPhrasesService();
            NextCommand = new Command(NextButtonClick);
            BindSeedList();
        }

        /// <summary>
        /// This method is used for binding seed phrase lists.
        /// </summary>
        public void BindSeedList()
        {
            SeedEntryList = new ObservableCollection<SeedPhraseEntryModel>();
            for (int i = 1; i < 25; i++)
            {
                SeedPhraseEntryModel seedPhrase = new SeedPhraseEntryModel
                {
                    SeedId = i.ToString()
                };
                SeedEntryList.Add(seedPhrase);
            }
            #region Commented
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "1", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "2", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "3", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "4", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "5", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "6", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "7", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "8", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "9", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "10", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "11", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "12", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "13", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "14", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "15", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "16", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "17", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "18", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "19", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "20", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "21", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "22", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "23", SeedText = "" });
            //SeedEntryList.Add(new SeedPhraseEntryModel() { SeedId = "24", SeedText = "" });
            #endregion
        }

        /// <summary>
        /// This method is used for verify seed phrases and navigation through next button click.
        /// </summary>
        public async void NextButtonClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });

                    SeedPhraseVerifyRequestModel SeedPhraseVerifyRequestModel = new SeedPhraseVerifyRequestModel();
                    SeedPhraseVerifyRequestModel.seedPhrases = new List<SeedPhras>();

                    foreach (var item in SeedEntryList)
                    {
                        SeedPhras seedPhraseItem = new SeedPhras
                        {
                            id = Convert.ToInt32(item.SeedId),
                            val = item.SeedText.Trim()
                        };
                        SeedPhraseVerifyRequestModel.seedPhrases.Add(seedPhraseItem);
                    }
                    SeedPhraseVerifyRequestModel.seedId = Preferences.Get(StringConstant.SeedId, string.Empty);

                    var response = await apiService.VerifySeedPhrase(SeedPhraseVerifyRequestModel);
                    if (response != null)
                    {
                        if (response.status == 200 && response.result)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new SignInPage());
                        }
                        else if (response.status == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new SignInPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, response.error, Resource.txtOk);
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        #endregion
    }
}
