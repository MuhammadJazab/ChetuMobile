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
{ /// <summary>
  /// This class is used to display wallets , its details and send receive functionality.
  /// </summary>
    public class WalletViewModel : ObservableObject
    {
        GetWalletDetailsResponseModel getWalletDetailsResponse = null;
        IGetWalletDetailsService apiService;
        private static ITransferTokenService transferToken;
        #region Private Boolean Properties 
        private bool isSendPageVisible;
        private bool edit;
        private bool saveButton;
        private bool walletBlack;
        private bool walletgray;
        private bool stakesblack;
        private bool stakesgray;
        private bool transactionblack;
        private bool transactiongray;
        private bool settingsblack;
        private bool settingsgray;
        private string walletAdressEntered;
        private string coinValue;
        private string coinShortName;
        private string coinStandard;



        private int amount;
        //private bool isSendVisible;
        private bool isScannerPageVisible;
        private bool isWalletListPage;
        private ObservableCollection<WalletListModel> walletsList;
        #endregion
        #region Public Image Properties
        public string CoinValue
        {
            get => coinValue;
            set
            {
                if (coinValue != value)
                {
                    coinValue = value;
                    OnPropertyChanged(nameof(CoinValue));
                }
            }
        }
        public string CoinShortName
        {
            get => coinShortName;
            set
            {
                if (coinShortName != value)
                {
                    coinShortName = value;
                    OnPropertyChanged(nameof(CoinShortName));
                }
            }
        }

        public string CoinStandard
        {
            get => coinStandard;
            set
            {
                if (coinStandard != value)
                {
                    coinStandard = value;
                    OnPropertyChanged(nameof(CoinStandard));
                }
            }
        }
        public string ShareApp48Icon { get; set; } = StringConstant.ShareApp48Icon;
        public string PlusAppIcon { get; set; } = StringConstant.PlusAppIcon;       
        public string SaveIcon { get; set; } = StringConstant.SaveIcon;               
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string RightArrowClickedIcon { get; set; } = StringConstant.RightArrowClickedIcon;
        #endregion
        #region Command Properties 
        public ICommand ScanCommand { get; set; }
        public ICommand BackBtn_Clicked { get; set; }
        public ICommand SendBtn_Clicked { get; set; }
        public ICommand CoinNameCommand { get; set; }
        public ICommand AddNewWallet { get; set; }        
        public ICommand ArrowCommand { get; set; }      
        internal static void SendAppIconClick()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Bindable Public Properties        
        public string txtWalletBalance;

        public bool IsSendPageVisible
        {
            get => isSendPageVisible;
            set
            {
                if (isSendPageVisible != value)
                {
                    isSendPageVisible = value;
                    OnPropertyChanged(nameof(IsSendPageVisible));
                }
            }
        }
        public string WalletAdressEntered
        {
            get
            {
                return walletAdressEntered;
            }
            set
            {
                if (walletAdressEntered != value)
                {
                    walletAdressEntered = value;
                    OnPropertyChanged(nameof(WalletAdressEntered));
                }
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        //public bool IsSendVisible
        //{
        //    get => isSendVisible;
        //    set
        //    {
        //        isSendVisible = value;
        //        OnPropertyChanged(nameof(IsSendVisible));
        //    }
        //}       
        public bool SaveButton
        {
            get => saveButton;
            set
            {
                if (saveButton != value)
                {
                    saveButton = value;
                    OnPropertyChanged();
                }
            }
        }       
        public bool Edit
        {
            get => edit;
            set
            {
                if (edit != value)
                {
                    edit = value;
                    OnPropertyChanged();
                }
            }
        }
        public string WalletBalance
        {
            get => txtWalletBalance;
            set
            {
                if (txtWalletBalance != value)
                {
                    txtWalletBalance = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public bool IsScannerPageVisible
        {
            get => isScannerPageVisible;
            set
            {
                if (isScannerPageVisible != value)
                {
                    isScannerPageVisible = value;
                    OnPropertyChanged(nameof(IsScannerPageVisible));
                }
            }
        }
        public bool IsWalletListPage
        {
            get => isWalletListPage;
            set
            {
                if (isWalletListPage != value)
                {
                    isWalletListPage = value;
                    OnPropertyChanged(nameof(IsWalletListPage));
                }
            }
        }
        public ObservableCollection<WalletListModel> WalletList
        {
            get => walletsList ?? (walletsList = new ObservableCollection<WalletListModel>());
            set
            {
                walletsList = value;
                OnPropertyChanged(nameof(walletsList));
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// This Constructor method is used to see the list of coins and coin amount.
        /// </summary>
        public WalletViewModel()
        {
            try
            {
                transferToken = new TransferTokenService();
                apiService = new GetWalletDetailsService();                                                                    
                IsWalletListPage = true;                
                AddNewWallet = new Command(AddNewWalletClick);               
                BackBtn_Clicked = new Command(BackBtn_ClickedCommand);
                //SendBtn_Clicked = new Command(SendBtn_ClickedCommand);
                //ScanCommand = new Command(ScanCommandClick);

        _ = GetWalletDetails();
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion
        #region Public Methods

        public async void AddNewWalletClick()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
        }
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
                    WalletList = new ObservableCollection<WalletListModel>();
                    GetWalletsDetailsRequestModel getWalletDetailsRequest = new GetWalletsDetailsRequestModel
                    {
                        SeedId =Preferences.Get("Seedid", string.Empty)
                    };
                    getWalletDetailsRequest.WalletAddress = Preferences.Get("WalletAddress", string.Empty);
                    getWalletDetailsResponse = await apiService.GetWalletDetails(getWalletDetailsRequest);
                    if (getWalletDetailsResponse != null)
                    {
                        if (getWalletDetailsResponse.Result && getWalletDetailsResponse.Status == 200)
                        {
                            if (getWalletDetailsResponse.Data != null)
                            {
                                Preferences.Set("WalletAdress", getWalletDetailsResponse.WalletAddress);
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    WalletBalance = getWalletDetailsResponse.Balance.ToString();                                                                        
                                    foreach (WalletDetailsResponseModel item in getWalletDetailsResponse.Data)
                                    {
                                        WalletListModel wallet = new WalletListModel
                                        {
                                            CoinName = item.CoinName,
                                            CoinValue = item.CoinValue.ToString(),
                                            CoinIcon = item.CoinIcon.ToString(),
                                            CoinShortName = item.CoinShortName,
                                            CoinUsdValue = item.CoinUsdValue.ToString(),
                                            CoinStandard = item.CoinStandard,
                                            QrCode = getWalletDetailsResponse.QrCode,
                                            WalletAdress = getWalletDetailsResponse.WalletAddress,
                                            Walletbalance = getWalletDetailsResponse.Balance.ToString(),
                                        };
                                        Preferences.Set("CoinShortname", wallet.CoinShortName);
                                        Preferences.Set("QrCode", getWalletDetailsResponse.QrCode);
                                        WalletList.Add(wallet);
                                        foreach (WalletListModel walletItem in WalletList)
                                        {
                                            walletItem.IsCoinVisible = true;                                          
                                        }
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
        /// This command is used to Show CoinDetail and send Option
        /// </summary>
        /// <param name="walletLsitModel"></param>
        public void CoinNameClick(WalletListModel walletListModel)
        {
            try
            {
                walletListModel.IsCoinVisible = false;
                walletListModel.IsCoinDetailVisible = true;
                IsSendPageVisible = false;
                walletListModel.IsReceivePageVisible = false;
                //Preferences.Set("CoinShortname", walletListModel.CoinShortName);
                //Preferences.Set("coinValue", walletListModel.CoinValue);                
                //Preferences.Set("CoinStandard", walletListModel.CoinStandard);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This command is used come back on CoinPage 
        /// </summary>
        /// <param name="walletLsitModel"></param>
        public void CoiName2Click(WalletListModel walletListModel)
        {
            try
            {
                walletListModel.IsCoinVisible = true;
                walletListModel.IsCoinDetailVisible = false;
                IsSendPageVisible = false;
                walletListModel.IsReceivePageVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// this Command is used to Show the send Page
        /// </summary>
        /// <param name="walletLsitModel"></param>
        public void SendAppIconClick(WalletListModel walletListModel)
        {
            try
            {
                //Preferences.Set("CoinShortname", walletListModel.CoinShortName);
                //Preferences.Set("coinValue", walletListModel.CoinValue);
                //Preferences.Set("CoinStandard", walletListModel.CoinStandard);
                //CoinShortName = Preferences.Get("CoinShortname", " ");
                //CoinValue= Preferences.Get("coinValue", " ");
                //CoinStandard = Preferences.Get("CoinStandard", " ");
                walletListModel.IsCoinVisible = false;
                walletListModel.IsCoinDetailVisible = false;
                walletListModel.IsSendPageVisible = true;
                IsSendPageVisible = true;
                walletListModel.IsReceivePageVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// this method is used to show receive page
        /// </summary>
        /// <param name="walletLsitModel"></param>
        public void ReceiveAppIconlick(WalletListModel walletListModel)
        {
            try
            {
                walletListModel.IsCoinVisible = walletListModel.IsSendPageVisible = false;
                walletListModel.IsCoinDetailVisible = walletListModel.IsReceivePageVisible = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public  void BackBtn_ClickedCommand()
        {
            WalletListModel walletListModel = new WalletListModel();
            try
            {
                Preferences.Set("CoinShortname", walletListModel.CoinShortName);
                Preferences.Set("coinValue", walletListModel.CoinValue);
                Preferences.Set("CoinStandard", walletListModel.CoinStandard);
                CoinShortName = Preferences.Get("CoinShortname", " ");
                CoinValue = Preferences.Get("coinValue", " ");
                CoinStandard = Preferences.Get("CoinStandard", " ");
                IsSendPageVisible = false;
                Utilities.WalletList.IsCoinDetailVisible = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This command method  is used to Send coin to entered or scaned  walletAdress
        /// </summary>
        /// <param name="walletLsitModel"></param>
            
        //public async void SendBtn_ClickedCommand()
        public async Task SendCommandAsync()
        {
            try
            {

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    TransferTokenRequestModel transferTokenRequestModel = new TransferTokenRequestModel();
                    
                    foreach (WalletListModel walletItem in WalletList)
                    
                    {
                        if (walletItem.Amount != 0)
                        {
                            if (string.IsNullOrEmpty(walletItem.WalletAdressEntered))
                            {
                                if ( !string.IsNullOrEmpty(Utilities.Scannedtext))
                                {
                                    transferTokenRequestModel.BlockChain = "VECHAIN";
                                    transferTokenRequestModel.WalletAddressTo = Utilities.Scannedtext;
                                    transferTokenRequestModel.WalletAddressFrom = Preferences.Get("WalletAddress", string.Empty);
                                    transferTokenRequestModel.Amount = walletItem.Amount;
                                    transferTokenRequestModel.CoinShortName = walletItem.CoinShortName;
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, "please enter Valid Wallet Adress", Resource.txtOk);
                                }
                            }
                            else
                            {
                                transferTokenRequestModel.BlockChain = "VECHAIN";
                                transferTokenRequestModel.WalletAddressTo = walletItem.WalletAdressEntered;
                                transferTokenRequestModel.WalletAddressFrom = Preferences.Get("WalletAddress", string.Empty);
                                transferTokenRequestModel.Amount = walletItem.Amount;
                                transferTokenRequestModel.CoinShortName = walletItem.CoinShortName;
                            }
                        }
                    }
                    ResponseModel response = await transferToken.TranferToken(transferTokenRequestModel);
                    if (response != null)
                    {
                        if (response.Status == 200 && response.Result)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessAlert, response.Message, Resource.txtOk);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
                        }
                        
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, response.Message, Resource.txtOk);
                            Application.Current.MainPage.Navigation.PopAsync(IsSendPageVisible=false);
                            WalletPage.Loader = false;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, response.Message, Resource.txtOk);
                        Application.Current.MainPage.Navigation.PopAsync(IsSendPageVisible=false);
                        WalletPage.Loader = false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                    Application.Current.MainPage.Navigation.PopAsync(IsSendPageVisible=false);
                    WalletPage.Loader = false;
                }
             }
            catch (Exception ex)
             {
                Crashes.TrackError(ex);
             }
         }

        /// <summary>
        /// This method is used to close Receive coin screen
        /// </summary>
        /// <param name="walletLsitModel"></param>
        public async Task CloseReceivPageCommandAsync(WalletListModel walletListModel)
        {
            try
            {
                walletListModel.IsReceivePageVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        /// Method to click on Wallet Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        

        /// <summary>
        /// Method to click on Stakes Tab Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
       

       

       

        /// <summary>
        /// this command method is used to initiaize  QRcode  Scan method
        /// </summary>
        /// <param name="walletListModel"></param>
        /// <returns></returns>
        /// 
        
        //public  async void  ScanCommandClick()
       public async Task ScanQR(WalletListModel walletListModel)

        {
            try
            {
              await Application.Current.MainPage.Navigation.PushModalAsync(new ScanQRCode());
                //IsScannerPageVisible = true;
                //IsWalletListPage = false;                

            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
            
        }
    }
    #endregion
}
