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
    public class AddWalletVM : ObservableObject
    {

        #region Properties
        IAddWalletService apiService;
        IGetWalletService apiGetService;
        AddWalletResponseModel addWalletResponse = null;
        GetWalletResponseModel getWalletResponse = null;
        #region Private Properties
        private string walletname;
        #endregion

        #region Public Blindable Properties
        /// <summary>
        /// This property gets or set the name of the wallet.
        /// </summary>
        public string WalletName
        {
            get
            {
                return walletname;
            }
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

        #region Image Properties
        /// <summary>
        /// This property gets or sets the image of the wallet lists.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        #endregion

        #region List Properties
        /// <summary>
        /// This private property gets or sets the list of wallets.
        /// </summary>
        private ObservableCollection<WalletModel> wallets;

        /// <summary>
        /// This public property gets or sets the list of wallets.
        /// </summary>
        public ObservableCollection<WalletModel> Wallets
        {
            get { return wallets ?? (wallets = new ObservableCollection<WalletModel>()); }
            set
            {
                wallets = value;
                OnPropertyChanged(nameof(Wallets));
            }
        }
        #endregion

        #region Boolean Properties
        /// <summary>
        /// This property gets and sets the visibilty of Create Wallet Popup
        /// </summary>
        private bool isCreateWalletVisible { get; set; }

        /// <summary>
        /// This property gets and sets the visibilty of Create Wallet Popup
        /// </summary>
        public bool IsCreateWalletVisible
        {
            get
            {
                return isCreateWalletVisible;
            }
            set
            {
                if (isCreateWalletVisible != value)
                {
                    isCreateWalletVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Command Properties
        /// <summary>
        /// This property gets or sets the command for Adding new wallet.
        /// </summary>
        public ICommand AddWalletCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for Creating new wallet.
        /// </summary>
        public ICommand CreateWalletCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for closing or opening create wallet popup.
        /// </summary>
        public ICommand CloseCreateWalletCommand { get; set; }

        /// <summary>
        /// This command property is used for click event on list item.
        /// </summary>
        private Command selectedItemCommand;
        #endregion

        #endregion
        
        #region Methods

        /// <summary>
        /// This method is used to add, create new wallets and show wallets.
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
                GetWalletList();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is used to add new wallet.
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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });

                    AddWalletRequestModel addWalletRequest = new AddWalletRequestModel
                    {
                        seedId = "618eb994ee815937fd53f919",                       
                        walletName= WalletName
                    };

                    addWalletResponse = await apiService.AddWallet(addWalletRequest);
                    if (addWalletResponse != null)
                    {
                        if (addWalletResponse.result && addWalletResponse.status == 200)
                        {
                            GetWalletList();
                            CloseWallet();
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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    Wallets = new ObservableCollection<WalletModel>();

                    GetWalletRequestModel getWalletRequest = new GetWalletRequestModel
                    {
                        seedId = "618eb994ee815937fd53f919"
                    };

                    getWalletResponse = await apiGetService.GetWallet(getWalletRequest);
                    if (getWalletResponse != null)
                    {
                        if (getWalletResponse.result && getWalletResponse.status == 200)
                        {
                            if (getWalletResponse.data != null)
                            {
                                foreach (var item in getWalletResponse.data)
                                {
                                    WalletModel wallet = new WalletModel
                                    {
                                        WalletAdress = item.WalletAddress,
                                        Qrcode = item.Qrcode,
                                        Balance = item.Balance.ToString(),
                                        WalletName = item.WalletName, 
                                        Image= AppIcon
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }


        /// <summary>
        /// This command method is used for click event on list item.
        /// </summary>
        public Command SelectedItemCommand
        {
            get
            {
                if (selectedItemCommand==null)
                {
                    selectedItemCommand = new Command(async (selectedItem) =>
                    {
                        if(selectedItem!=null)
                        {
                            var item = selectedItem as WalletModel;
                            Preferences.Set("WalletAddress", item.WalletAdress);
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
        #endregion
    }
}
