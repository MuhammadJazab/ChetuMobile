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
        #region private properties
        private bool isSaveSeedAlertVisible;
        #endregion
        #region public  Properties
        IGetSeedPhraseService apiService;
        SeedPhraseViewResponseModel seedListResponse = null;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public ObservableCollection<SeedPhraseListModel> SeedList { get; set; }
        public ObservableCollection<SeedPhraseListModel> RandomSeedList { get; set; }      
        public bool IsSaveSeedAlertVisible
        {
            get => isSaveSeedAlertVisible;
            set
            {
                isSaveSeedAlertVisible = value;
                OnPropertyChanged(nameof(IsSaveSeedAlertVisible));
            }
        }       
        public string BackwardAppIcon { get; set; } = StringConstant.BackwardAppIcon;
        public string SaveSeedAppIcon { get; set; } = StringConstant.SaveSeedAppIcon;
        public string ForwardAppIcon { get; set; } = StringConstant.ForwardAppIcon;
        public ICommand BackCommand { get; set; }      
        public ICommand SaveSeedCommand { get; set; }        
        public ICommand NextCommand { get; set; }      
        public ICommand OkButton { get; set; }
        #endregion      
        #region Method
        /// <summary>
        /// class  Constructor methods.
        /// </summary>
        public ViewSeedPhraseVM()
        {
            try
            {
                apiService = new GetSeedPhraseService();                    
                BackCommand = new Command(BackButtonClick);
                SaveSeedCommand = new Command(SaveSeedCommandClick);
                NextCommand = new Command(NextButtonClick);
                OkButton = new Command(OkButtonClick);
                _ = GetSeedList().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is for to save seed phrase in pdf (have to implement)
        /// </summary>
        public void SaveSeedCommandClick()
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhrasePage());
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
                        if (seedListResponse.Result && seedListResponse.Status == 200)
                        {
                            Preferences.Set("Seedid", seedListResponse._id);
                            if (seedListResponse.SeedPhrases != null)
                            {
                                foreach (var item in seedListResponse.SeedPhrases)
                                {
                                    SeedPhraseListModel viewSeedModel = new SeedPhraseListModel
                                    {
                                        LabelNumber = item.Id.ToString(),
                                        EntryText = item.Val
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
        /// Method to click on Back Button on to Password setup Page
        /// </summary>
        public async void BackButtonClick()
        {
            try
            {
                _ = await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        ///Method to click on Next Button  to show save seed alert pop up
        /// </summary>
        public void NextButtonClick()
        {
            try
            {
                IsSaveSeedAlertVisible = true;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        ///Method to Close save seed alert and move to validate seed screen
        /// </summary>
        public async void OkButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ValidateSeedPhrasePage(SeedList));                
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
