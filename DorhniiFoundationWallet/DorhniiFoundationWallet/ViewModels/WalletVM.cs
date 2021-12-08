using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
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
    /// This class is used to display wallets and its details.
    /// </summary>
    public class WalletVM : ObservableObject
    {

        #region Properties
        GetWalletDetailsResponseModel getWalletDetailsResponse = null;
        IGetWalletDetailsService apiService;
        #region Image Properties
        /// <summary>
        /// This property gets or sets the image of the edit button.
        /// </summary>
        public string EditIcon { get; set; } = StringConstant.EditIcon;

        /// <summary>
        /// This property gets or sets the image of the save button.
        /// </summary>
        public string SaveIcon { get; set; } = StringConstant.SaveIcon;

        /// <summary>
        /// This property gets or sets the image of the app icon.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This property gets or sets the image of the arrow button.
        /// </summary>
        public string RightArrowClickedIcon { get; set; } = StringConstant.RightArrowClickedIcon;
        #endregion

        #region Command Properties
        /// <summary>
        /// This property gets or sets the command for Editing the wallet name.
        /// </summary>
        public ICommand EditButtonCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Saving the wallet name.
        /// </summary>
        public ICommand SaveButtonCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Arrow Click command.
        /// </summary>
        public ICommand ArrowCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Wallet tab click.
        /// </summary>
        public ICommand WalletBlackCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Stakes tab click.
        /// </summary>
        public ICommand StakesBlackCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Transaction tab click.
        /// </summary>
        public ICommand TransactionBlackCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Settings tab click.
        /// </summary>
        public ICommand SettingsBlackCommand { get; set; }

        #endregion

        #region Private Boolean Properties
        private bool wallet;
        private bool edit;
        private bool editButton;
        private bool saveButton;
        private bool walletBlack;
        private bool walletgray;
        private bool stakesblack;
        private bool stakesgray;
        private bool transactionblack;
        private bool transactiongray;
        private bool settingsblack;
        private bool settingsgray;
        #endregion

        #region Public Properties

        public string txtWalletBalance;

        #endregion

        #region Bindable Properties

        /// <summary>
        /// This property gets sets the visiblity of Edit Button
        /// </summary>
        public bool EditButton
        {
            get
            {
                return editButton;
            }
            set
            {
                if (editButton != value)
                {
                    editButton = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Save Button
        /// </summary>
        public bool SaveButton
        {
            get
            {
                return saveButton;
            }
            set
            {
                if (saveButton != value)
                {
                    saveButton = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet Label Control
        /// </summary>
        public bool Wallet
        {
            get
            {
                return wallet;
            }
            set
            {
                if (wallet != value)
                {
                    wallet = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Edit Wallet Name Entry Control
        /// </summary>
        public bool Edit
        {
            get
            {
                return edit;
            }
            set
            {
                if (edit != value)
                {
                    edit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the value of Wallet Balance
        /// </summary>
        public string WalletBalance
        {
            get
            {
                return txtWalletBalance;
            }
            set
            {
                if (txtWalletBalance != value)
                {
                    txtWalletBalance = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool WalletBlackVisible
        {
            get
            {
                return walletBlack;
            }
            set
            {
                if (walletBlack != value)
                {
                    walletBlack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool WalletGrayVisible
        {
            get
            {
                return walletgray;
            }
            set
            {
                if (walletgray != value)
                {
                    walletgray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool StakesBlackVisible
        {
            get
            {
                return stakesblack;
            }
            set
            {
                if (stakesblack != value)
                {
                    stakesblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool StakesGrayVisible
        {
            get
            {
                return stakesgray;
            }
            set
            {
                if (stakesgray != value)
                {
                    stakesgray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool TransactionBlackVisible
        {
            get
            {
                return transactionblack;
            }
            set
            {
                if (transactionblack != value)
                {
                    transactionblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool TransactionGrayVisible
        {
            get
            {
                return transactiongray;
            }
            set
            {
                if (transactiongray != value)
                {
                    transactiongray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool SettingsBlackVisible
        {
            get
            {
                return settingsblack;
            }
            set
            {
                if (settingsblack != value)
                {
                    settingsblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool SettingGrayVisible
        {
            get
            {
                return settingsgray;
            }
            set
            {
                if (settingsgray != value)
                {
                    settingsgray = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        /// <summary>
        /// This private property gets or sets the list of wallets.
        /// </summary>
        private ObservableCollection<WalletLsitModel> walletsList;
        public ObservableCollection<WalletLsitModel> WalletList
        {
            get { return walletsList ?? (walletsList = new ObservableCollection<WalletLsitModel>()); }
            set
            {
                walletsList = value;
                OnPropertyChanged(nameof(walletsList));
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// This method is used to see the list of coins and coin amount.
        /// </summary>
        public WalletVM()
        {
            apiService = new GetWalletDetailsService();
            Wallet = true;
            Edit = false;
            EditButton = true;
            SaveButton = false;
            WalletBlackVisible = true;
            StakesBlackVisible = true;
            TransactionBlackVisible = true;
            SettingsBlackVisible = true;
            WalletGrayVisible = false;
            StakesGrayVisible = false;
            TransactionGrayVisible = false;
            SettingGrayVisible = false;
            WalletBalance = "1,23,456.987 $";
            EditButtonCommand = new Command(EditButtonClick);
            SaveButtonCommand = new Command(SaveButtonClick);
            ArrowCommand = new Command(ArrowButtonClick);
            WalletBlackCommand = new Command(WalletBlackButtonClick);
            StakesBlackCommand = new Command(StakesBlackButtonClick);
            TransactionBlackCommand = new Command(TransactionBlackButtonClick);
            SettingsBlackCommand = new Command(SettingBlackButtonClick);
            GetWalletDetails().ConfigureAwait(true);
        }
        #endregion

        #region Methods

        /// <summary>
        /// This task is used to get the list of wallet details.
        /// </summary>
        /// <param name="walletModel"></param>
        /// <returns></returns>
        public async Task GetWalletDetails()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    WalletList = new ObservableCollection<WalletLsitModel>();
                    GetWalletsDetailsRequestModel getWalletDetailsRequest = new GetWalletsDetailsRequestModel
                    {
                        seedId = "618eb994ee815937fd53f919",
                        walletAddress = Preferences.Get("WalletAddress", string.Empty)
                    };
                    getWalletDetailsResponse = await apiService.GetWalletDetails(getWalletDetailsRequest);
                    if (getWalletDetailsResponse != null)
                    {
                        if (getWalletDetailsResponse.result && getWalletDetailsResponse.status == 200)
                        {
                            if (getWalletDetailsResponse.data != null)
                            {
                              
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    foreach (WalletDetailsResponseModel item in getWalletDetailsResponse.data)
                                    {
                                        WalletLsitModel wallet = new WalletLsitModel
                                        {
                                            coinName = item.coinName,
                                            coinValue = item.coinValue.ToString()
                                        };
                                        WalletList.Add(wallet);
                                    }
                                });
                               
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
        ///Method to click on Edit Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void EditButtonClick()
        {
            try
            {
                Wallet = false;
                Edit = true;
                EditButton = false;
                SaveButton = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Save Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void SaveButtonClick()
        {
            try
            {
                Wallet = true; ;
                Edit = false; ;
                EditButton = true;
                SaveButton = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Arrow Button Navigation
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void ArrowButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new CoinsPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Wallet Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void WalletBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = false;
                StakesBlackVisible = true;
                TransactionBlackVisible = true;
                SettingsBlackVisible = true;

                WalletGrayVisible = true;
                StakesGrayVisible = false;
                TransactionGrayVisible = false;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Stakes Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void StakesBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = false;
                TransactionBlackVisible = true;
                SettingsBlackVisible = true;

                WalletGrayVisible = false;
                StakesGrayVisible = true;
                TransactionGrayVisible = false;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new StakesPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Transaction Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void TransactionBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = true;
                TransactionBlackVisible = false;
                SettingsBlackVisible = true;

                WalletGrayVisible = false;
                StakesGrayVisible = false;
                TransactionGrayVisible = true;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new TransactionPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Setting Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void SettingBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = true;
                TransactionBlackVisible = true;
                SettingsBlackVisible = false;

                WalletGrayVisible = false;
                StakesGrayVisible = false;
                TransactionGrayVisible = false;
                SettingGrayVisible = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new SettingsPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion

    }
}
