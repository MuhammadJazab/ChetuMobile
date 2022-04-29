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
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to entry and verify seed phrases.
    /// </summary>
    public class ValidateSeedPhraseVM : BaseViewModel
    {
        #region private properties
        private static IVerifySeedPhrasesService apiService;
        private bool isSaveSeedAlertVisible;
        private Command onLabelTap;
        private Command onUpperLabelTap;
        private int EntryIndex = 0;
        private ObservableCollection<SeedPhraseListModel> dataList23;
        private ObservableCollection<SeedPhraseListModel> seedListBindable;
        private bool acountFlag;
        #endregion
        #region public Properties
        public string UserSeedPhrase;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string SEEDPHRASE { get; set; } = StringConstant.SEEDPHRASE;
        public string BackwardAppIcon { get; set; } = StringConstant.BackwardAppIcon; 
        public string ForwardAppIcon { get; set; } = StringConstant.ForwardAppIcon;
        public ICommand BackCommand { get; set; }
        public ICommand NextCommandEnter { get; set; }
        public ICommand CopyCommand { get; set; }        
        public ICommand OkButton { get; set; }
        private double opacityValue;
        public double OpacityValue
        {
            get => opacityValue;
            set
            {
                opacityValue = value;
                OnPropertyChanged(nameof(OpacityValue));
            }
        }
        public bool IsSaveSeedAlertVisible
        {
            get => isSaveSeedAlertVisible;
            set
            {
                isSaveSeedAlertVisible = value;
                OnPropertyChanged(nameof(IsSaveSeedAlertVisible));
            }
        }
        public ObservableCollection<SeedPhraseListModel> DataList23
        {
            get => dataList23;
            set
            { dataList23 = value;
             OnPropertyChanged(nameof(DataList23)); }
        }
        public ObservableCollection<SeedPhraseListModel> SeedListBindable
        {
            get => seedListBindable;
            set { seedListBindable = value; OnPropertyChanged(nameof(SeedListBindable)); }
        }
        public Command OnUpperLabelTap
        {
            get
            {
                onUpperLabelTap = new Command((res) =>
                {
                    SeedPhraseListModel data2 = res as SeedPhraseListModel;
                    SeedPhraseListModel menuList2 = DataList23.FirstOrDefault(x => x.EntryText.Equals(data2.EntryText));
                    int index = DataList23.IndexOf(menuList2);
                    DataList23.RemoveAt(index);
                    SeedListBindable.Add(new SeedPhraseListModel()
                    {
                        EntryText = data2.EntryText,
                        EntryId = EntryIndex,
                        OnLabelTap = OnLabelTap
                    });
                });
                return onUpperLabelTap;
            }
        }
        public Command OnLabelTap
        {
            get
            {
                onLabelTap = new Command((res) =>
                {
                    SeedPhraseListModel data = res as SeedPhraseListModel;
                    SeedPhraseListModel menuList = SeedListBindable.FirstOrDefault(x => x.EntryText.Equals(data.EntryText));
                    int index = SeedListBindable.IndexOf(menuList);
                    SeedListBindable.RemoveAt(index);
                    EntryIndex++;
                    DataList23.Add(new SeedPhraseListModel()
                    {
                        EntryText = data.EntryText,
                        EntryId = EntryIndex,
                        OnUpperLabelTap = OnUpperLabelTap
                    });
                });
                return onLabelTap;
            }
        }
        #endregion

        /// <summary>
        /// Class constructor
        /// </summary>
        public ValidateSeedPhraseVM()
        {
            try
            {
                apiService = new VerifySeedPhrasesService();
                BackCommand = new Command(BackButtonClick);
                NextCommandEnter = new Command(NextCommandEnterClick);
                DataList23 = new ObservableCollection<SeedPhraseListModel>();
                CopyCommand = new Command(CopyCommandClick);
                OkButton = new Command(OkButtonClick);
                IsSaveSeedAlertVisible = false;
                opacityValue = 1;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Methon to generate Random Seed Phrase to show on validate seed phrase page
        /// </summary>
        /// <param name="seedList"></param>
        public void RandomMethod(ObservableCollection<SeedPhraseListModel> seedList)
        {
            try
            {
                SeedListBindable = new ObservableCollection<SeedPhraseListModel>();

                foreach (SeedPhraseListModel item in seedList)
                {
                    SeedListBindable.Add(new SeedPhraseListModel()
                    {
                        EntryText = item.EntryText,
                        OnLabelTap = OnLabelTap
                    });
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is used for verify seed phrases and navigation through next button click.
        /// </summary>
        public async void NextCommandEnterClick()
        {
            try
            {
                if (SeedListBindable.Count != 0)
                {
                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgSeedEntryAlert, Resource.txtOk);                    
                }
                else
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        IsLoading = true;
                        SeedPhraseVerifyRequestModel seedPhraseVerifyRequestModel = new SeedPhraseVerifyRequestModel
                        {
                            SeedPhrases = new List<SeedPhras>()
                        };
                        int ID = 1;
                        foreach (SeedPhraseListModel item in DataList23)
                        {
                            if (ID < 25)
                            {
                                SeedPhras seedPhraseItem = new SeedPhras
                                {
                                    Id = Convert.ToInt32(ID),
                                    Val = item.EntryText.Trim(),
                                };
                                ID++;
                                seedPhraseVerifyRequestModel.SeedPhrases.Add(seedPhraseItem);
                            }
                        }
                        seedPhraseVerifyRequestModel.SeedId = Preferences.Get(StringConstant.SeedId, string.Empty);
                        Models.APIResponseModels.ResponseModel response = await apiService.VerifySeedPhrase(seedPhraseVerifyRequestModel);
                        if (response != null)
                        {
                            if (response.Status == 200 && response.Result)
                            {
                                OpacityValue = 0.3;
                                IsSaveSeedAlertVisible = true;                                                                                                                                                          
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
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                    }
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
        /// Method to click on Back Button to go on  View Seed  Page
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
        ///Method use to copy entered valid seed phrase
        /// </summary>
        public async void CopyCommandClick()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Message !", "Seed Phrase Copied", Resource.txtOk);
                List<string> copySeedlist = new List<string>();
                foreach (var item in dataList23)
                {
                    copySeedlist.Add(item.EntryText);
                }
                string UserSeedPhrase = string.Join(Environment.NewLine, copySeedlist.ToArray());
                string copied = string.Copy(UserSeedPhrase.ToString());               
                await Application.Current.MainPage.DisplayAlert("Security Alert !", "Do not share these seed phrases to anyone", Resource.txtOk);
                await Clipboard.SetTextAsync(copied);
                string text = await Clipboard.GetTextAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        ///Method to Close save seed alert and move to Login seed screen
        /// </summary>
        public async void OkButtonClick()
        {
            try
            {
                acountFlag = true;
                Preferences.Set(StringConstant.AcountFlag, acountFlag);
                await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

    }
}
