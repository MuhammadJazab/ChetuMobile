using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to view seed phrases.
    /// </summary>
    public class ViewSeedPhraseVM : ObservableObject
    {
        #region Properties
        /// <summary>
        /// This field intialises the view seed phrase interface service.
        /// </summary>
        IGetSeedPhraseService apiService;

        /// <summary>
        /// This field intialises the response of view seed phrase api request.
        /// </summary>
        SeedPhraseViewResponseModel seedListResponse = null;

        /// <summary>
        /// This property gets or sets the image of the app icon.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This public property gets or sets the list of view seed phrases.
        /// </summary>
        public ObservableCollection<SeedPhraseListModel> SeedList { get; set; }

        /// <summary>
        /// This property gets or sets the command for Next Button.
        /// </summary>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Back Button.
        /// </summary>
        public ICommand BackCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Print Button.
        /// </summary>
        public ICommand PrintCommand { get; set; }
        #endregion

        #region Method
        /// <summary>
        /// This method is used to view seed phrases.
        /// </summary>
        public ViewSeedPhraseVM()
        {
            try
            {
                apiService = new GetSeedPhraseService();
                NextCommand = new Command(NextButtonClick);
                BackCommand = new Command(BackButtonClick);
                GetSeedList().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Methods gets the view seed list
        /// </summary>
        public async Task GetSeedList()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });

                    SeedList = new ObservableCollection<SeedPhraseListModel>();
                    seedListResponse = await apiService.GetSeedPhraseList();
                    if (seedListResponse != null)
                    {
                        if (seedListResponse.result && seedListResponse.status == 200)
                        {
                            Preferences.Set(StringConstant.SeedId, seedListResponse._id);
                            if (seedListResponse.seedPhrases != null)
                            {
                                foreach (var item in seedListResponse.seedPhrases)
                                {
                                    SeedPhraseListModel viewSeedModel = new SeedPhraseListModel
                                    {
                                        LabelNumber = item.id.ToString(),
                                        EntryText = item.val
                                    };
                                    SeedList.Add(viewSeedModel);
                                }
                            }
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


        /// <summary>
        ///Method to click on Next Button on Seed Label Page
        /// </summary>
        public async void NextButtonClick()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert(Resource.txtSeedPhraseSave, Resource.msgSeedPhraseSave, Resource.txtOk);
                await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhraseEntry());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Back Button on Seed Label Page
        /// </summary>
        public async void BackButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
