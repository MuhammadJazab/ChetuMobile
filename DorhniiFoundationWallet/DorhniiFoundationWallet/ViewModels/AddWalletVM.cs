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
    /// This class is used to add, create new wallets and show wallets.
    /// </summary>
    public class AddWalletVM : BaseViewModel
    {
        #region Private Properties
        IAddWalletService apiService;
        IGetWalletService apiGetService;
        AddWalletResponseModel addWalletResponse = null;
        GetWalletResponseModel getWalletResponse = null;
        private ObservableCollection<WalletModel> wallets;
        private bool isCreateWalletVisible { get; set; }
        private Command selectedItemCommand;
        private string walletname;
        public bool IsWalletExist;
        #endregion

        #region Public Properties
        public string BalanceFormatted;
        public ObservableCollection<WalletModel> Wallets
        {
            get => wallets ?? (wallets = new ObservableCollection<WalletModel>());

            set
            {
                wallets = value;
                OnPropertyChanged(nameof(Wallets));
            }
        }
        private bool walleteNotExist;
        public bool WalleteNotExist
        {
            get => walleteNotExist;
            set
            {
                if (walleteNotExist != value)
                {
                    walleteNotExist = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsCreateWalletVisible
        {
            get => isCreateWalletVisible;
            set
            {
                if (isCreateWalletVisible != value)
                {
                    isCreateWalletVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand AddWalletCommand { get; set; }
        public ICommand CreateWalletCommand { get; set; }
        public ICommand CloseCreateWalletCommand { get; set; }
        public string CrossSign { get; set; } = StringConstant.CrossSign; 
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public Command SelectedItemCommand
        {
            get
            {
                if (selectedItemCommand == null)
                {
                    selectedItemCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            WalletModel item = selectedItem as WalletModel;                                                       
                            Preferences.Set(StringConstant.walletAddress, item.WalletAdress);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return selectedItemCommand;
            }
        }
        public string WalletName
        {
            get => walletname;

            set
            {
                if (walletname != value)
                {
                    walletname = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// This  constructor method used to  Add Wallet and wallet list
        /// </summary>
        private void AddWallet()
        {
            try
            {                

                IsCreateWalletVisible = true;               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is used to close the create wallet popup.
        /// </summary>       
        private void CloseWallet()
        {
            try
            {
                IsCreateWalletVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is used to create new wallet.
        /// </summary>       
        private async void CreateWallet()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    IsLoading = true;                 
                    AddWalletRequestModel addWalletRequest = new AddWalletRequestModel
                    {
                        SeedId = Preferences.Get(StringConstant.SeedId, string.Empty),
                        WalletName = WalletName
                    };
                    addWalletResponse = await apiService.AddWallet(addWalletRequest);
                    if (addWalletResponse != null)
                    {
                        if (addWalletResponse.Result && addWalletResponse.Status == 200)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessful, addWalletResponse.Message, Resource.txtOk);
                            IsWalletExist = true;
                            Preferences.Set(StringConstant.WalletExist, IsWalletExist);
                            Preferences.Set(StringConstant.EncryptedPrivateKey, addWalletResponse.EncryptedPrivateKey);
                            WalletModel wallet = new WalletModel
                            {
                                WalletAdress = addWalletResponse.WalletAddress,
                                Qrcode = addWalletResponse.Qrcode,
                                Balance = addWalletResponse.Balance,
                                WalletName = addWalletResponse.WalletName,
                                Image = AppIcon,
                                PrivateKey = addWalletResponse.PrivateKey,
                            };
                            Wallets.Add(wallet);
                            HideAddwalletButton();
                            _ = GetWalletList();
                            CloseWallet();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, addWalletResponse.Message, Resource.txtOk);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
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
        /// This method is used to show the list of wallets.
        /// </summary>
        /// <returns></returns>
        private async Task GetWalletList()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    IsLoading = true;
                    Wallets = new ObservableCollection<WalletModel>();

                    GetWalletRequestModel getWalletRequest = new GetWalletRequestModel
                    {
                        SeedId = Preferences.Get(StringConstant.SeedId , string.Empty),
                    };

                    getWalletResponse = await apiGetService.GetWallet(getWalletRequest);
                    if (getWalletResponse != null)
                    {
                        if (getWalletResponse.Result && getWalletResponse.Status == 200)
                        {
                            if (getWalletResponse.Data != null)
                            {
                                foreach (AddWalletResponseModel item in getWalletResponse.Data)
                                {
                                    BalanceFormatted = item.Balance.ToString("n2");
                                    WalletModel wallet = new WalletModel
                                    {
                                        WalletAdress = item.WalletAddress,
                                        Qrcode = item.Qrcode,
                                        WalletFormattedBalance = BalanceFormatted,
                                        WalletName = item.WalletName,
                                        Image = AppIcon,
                                        PrivateKey = item.PrivateKey,
                                    };
                                    Wallets.Add(wallet);
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

        #endregion

        #region Public method
        /// <summary>
        /// (Class CONSTRUCTOR)This method is used to add, create new wallets and show wallets.
        /// </summary>
        public AddWalletVM()
        {
            try
            {
                apiService = new AddWalletService();
                apiGetService = new GetWalletService();
                IsCreateWalletVisible = false;
                AddWalletCommand = new Command(AddWallet);
                CreateWalletCommand = new Command(CreateWallet);
                CloseCreateWalletCommand = new Command(CloseWallet);
                HideAddwalletButton();               
                Preferences.Set(StringConstant.AcountFlag, true);
                _ = GetWalletList();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method use to hide addwallet button for user having on walet already on add wallet page
        /// </summary>
        public void HideAddwalletButton()
        {
            WalleteNotExist = !Preferences.Get(StringConstant.IstoreAccountHideButton, false) && !Preferences.Get(StringConstant.WalletExist, false);
        }
        #endregion
    }
}
