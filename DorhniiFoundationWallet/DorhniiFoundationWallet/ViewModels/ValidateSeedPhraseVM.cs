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
    public class ValidateSeedPhraseVM : ObservableObject
    {
        #region private properties
        private static IVerifySeedPhrasesService apiService;
        private Command onLabelTap;
        private Command onUpperLabelTap;
        private int EntryIndex = 0;
        private ObservableCollection<SeedPhraseListModel> dataList23;
        private ObservableCollection<SeedPhraseListModel> seedListBindable;
        #endregion
        #region public Properties
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string ForwardAppIcon { get; set; } = StringConstant.ForwardAppIcon;
        public ICommand NextCommandEnter { get; set; }
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
                NextCommandEnter = new Command(NextCommandEnterClick);
                DataList23 = new ObservableCollection<SeedPhraseListModel>();
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
                List<SeedPhraseListModel> RandomSeedList = seedList.OrderByDescending(V => V.EntryText).ToList();
                foreach (SeedPhraseListModel item in RandomSeedList)
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
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        SeedPhraseVerifyRequestModel seedPhraseVerifyRequestModel = new SeedPhraseVerifyRequestModel
                        {
                            SeedPhrases = new List<SeedPhras>()
                        };

                        foreach (SeedPhraseListModel item in DataList23)
                        {
                            SeedPhras seedPhraseItem = new SeedPhras
                            {
                                Id = Convert.ToInt32(item.EntryId),
                                Val = item.EntryText.Trim()
                            };
                            seedPhraseVerifyRequestModel.SeedPhrases.Add(seedPhraseItem);
                        }
                        seedPhraseVerifyRequestModel.SeedId = Preferences.Get("Seedid", string.Empty);
                        Models.APIResponseModels.ResponseModel response = await apiService.VerifySeedPhrase(seedPhraseVerifyRequestModel);
                        if (response != null)
                        {
                            if (response.Status == 200 && response.Result)
                            {
                                await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

    }
}
