using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
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
    public class ViewSeedPhraseVM : BaseViewModel
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
        public string YOURNEWSEEDPHRASE { get; set; } = StringConstant.YOURNEWSEEDPHRASE; 
        public string BackwardAppIcon { get; set; } = StringConstant.BackwardAppIcon;
        public string SaveSeedAppIcon { get; set; } = StringConstant.SaveSeedAppIcon;
        public string ForwardAppIcon { get; set; } = StringConstant.ForwardAppIcon;
        public ICommand BackCommand { get; set; }      
        public ICommand SaveSeedCommand { get; set; }        
        public ICommand NextCommand { get; set; }      
       
        public SeedPhraseListModel EntryText { get; private set; }
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
                List<string> downloadSeed = new List<string>();
                foreach (var item in SeedList)
                {
                    downloadSeed.Add(item.EntryText);
                }
                string SeedPhraseSrting = string.Join(Environment.NewLine, downloadSeed.ToArray()); 
                DependencyService.Get<IDownloader>().DownloadFile(SeedPhraseSrting.ToString());
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
                    IsLoading = true;
                    SeedList = new ObservableCollection<SeedPhraseListModel>();
                    seedListResponse = await apiService.GetSeedPhraseList();
                    if (seedListResponse != null)
                    {
                        if (seedListResponse.Result && seedListResponse.Status == 200)
                        {
                            Preferences.Set(StringConstant.SeedId, seedListResponse._id);
                            if (seedListResponse.SeedPhrases != null)
                            {
                                foreach (var item in seedListResponse.SeedPhrases)
                                {
                                    SeedPhraseListModel viewSeedModel = new SeedPhraseListModel
                                    {
                                        LabelNumber = item.Id.ToString() + ".",
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
                IsLoading = false;
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
        public async void NextButtonClick()
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
