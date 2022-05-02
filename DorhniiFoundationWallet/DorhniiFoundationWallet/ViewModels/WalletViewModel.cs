using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using DorhniiFoundationWallet.Views.Partials;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace DorhniiFoundationWallet.ViewModels
{ /// <summary>
  /// This class is used to display wallets , its details and send receive functionality.
  /// </summary>
    public class WalletViewModel : BaseViewModel
    {
        GetWalletDetailsResponseModel getWalletDetailsResponse = null;
        IUpdatedateWalletName apiUpdateWalletName;
        EditWalletNameResponseModel editWalletResponseModel = null;
        IGetWalletDetailsService apiService;
        IGasEstimateFeeService feeService;
        private static ITransferTokenService transferToken;
        private bool edit;
        
        private bool saveButton;
        private bool Optionscanner = false;
        private bool scanner = false;
        public string walletAdress;       
        private string walletName;
        private Command selectedItemCommand1;
        private Command coinPageCommand;
        private Command sendPageCommand;
        private Command receivePageCommand;
        private Command scanCommand;
        private Command closeSendPageCommand;
        private Command closeReceivePageCommand;
        private Command shareAdressCommand;
        private Command sendButtonCommand;
        private Command copyAdressCommand;
        private Command scanButtonCommand;
        public bool IsOpened { get; set; } = true;
        private ObservableCollection<WalletListModel> walletsList;
        #region Image Properties
        public string PlusAppIcon { get; set; } = StringConstant.PlusAppIcon;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        #endregion
        #region Command properties                           
        public ICommand AddNewWallet { get; set; }
        public ICommand EditWalletName { get; set; }       
        public ICommand SaveWalletName { get; set; }
        public ICommand TrasactionStatusCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public bool IsWalletnameClicked { get; set; } 
        public bool IsSavebuttonClick { get; set; }
        public WalletListModel SelectedWallet { get; set; }
        public Command SelectedItemCommand1
        {
            get
            {
                try
                {
                    if (selectedItemCommand1 == null)
                    {
                        selectedItemCommand1 = new Command(async (selectedItem) =>
                        {
                            try
                            {
                                if (selectedItem != null)
                                {
                                    IsLoading = true;
                                    if (WalletList != null)
                                    {
                                        if (WalletList.Count > 0)
                                        {
                                            WalletList.ToList().ForEach(x =>
                                            {
                                                x.IsCoinVisible = true;
                                                x.IsCoinDetailVisible = false;
                                                x.IsSendPageVisible = false;
                                                x.IsReceivePageVisible = false;
                                            });
                                        }
                                    }
                                    WalletListModel item = selectedItem as WalletListModel;
                                    SelectedWallet = item;
                                    if (item != null)
                                    {
                                        item.IsCoinVisible = false;
                                        item.IsCoinDetailVisible = true;
                                        item.IsSendPageVisible = false;
                                        item.IsReceivePageVisible = false;
                                        IsLoading = false;
                                    }
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return selectedItemCommand1;
            }
        }
        public Command CoinPageCommand
        {
            get
            {
                try
                {
                    if (coinPageCommand == null)
                    {
                        coinPageCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                WalletListModel item = selectedItem as WalletListModel;
                                item.IsCoinVisible = true;
                                item.IsCoinDetailVisible = false;
                                item.IsSendPageVisible = false;
                                item.IsReceivePageVisible = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return coinPageCommand;
            }
        }
        public Command SendPageCommand
        {
            get
            {
                if (sendPageCommand == null)
                {
                    sendPageCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            WalletListModel item = selectedItem as WalletListModel;
                            item.IsCoinVisible = false;
                            item.IsCoinDetailVisible = false;
                            item.IsSendPageVisible = true;
                            item.IsReceivePageVisible = false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return sendPageCommand;
            }
        }

        /// <summary>
        /// Method use to Open receive page
        /// </summary>
        public Command ReceivePageCommand
        {
            get
            {
                try
                {
                    if (receivePageCommand == null)
                    {
                        receivePageCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                WalletListModel item = selectedItem as WalletListModel;
                                item.IsCoinVisible = false;
                                item.IsCoinDetailVisible = true;
                                item.IsSendPageVisible = true;
                                item.IsReceivePageVisible = true;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return receivePageCommand;
            }
        }

        /// <summary>
        /// Method use to initiate to open receive page or scanner
        /// </summary>
        public Command ScanButtonCommand
        {
            get
            {
                try
                {
                    if (scanButtonCommand == null)
                    {
                        scanButtonCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                var IsStatus = await App.Current.MainPage.DisplayAlert(Resource.txtTransactTypeAlert, Resource.txtTransactTypeMessege, Resource.txtSENDCapital, Resource.txtRECEIVECapital);
                                if (IsStatus)
                                {
                                    IsOpened = false;
                                    ZXingScannerPage scanPage;
                                    scanPage = new ZXingScannerPage(null, new ScanCancel());
                                    scanPage.OnScanResult += (result) =>
                                    {
                                        scanPage.IsScanning = true;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await App.Current.MainPage.Navigation.PopModalAsync();
                                            WalletListModel content = selectedItem as WalletListModel;
                                            content.WalletAdressEntered = result.Text;
                                            walletAdress = result.Text;
                                            IsLoading = true;
                                            IsLoading = false;
                                            Optionscanner = true;
                                        });

                                    };
                                    await App.Current.MainPage.Navigation.PushModalAsync(scanPage);
                                    IsOpened = true;
                                    WalletListModel item = selectedItem as WalletListModel;
                                    item.IsCoinVisible = false;
                                    item.IsCoinDetailVisible = false;
                                    item.IsSendPageVisible = true;
                                    item.IsReceivePageVisible = false;
                                    item.WalletAdressEntered = Preferences.Get(StringConstant.scanAddress, string.Empty);
                                }
                                else
                                {
                                    WalletListModel item = selectedItem as WalletListModel;
                                    item.IsCoinVisible = false;
                                    item.IsCoinDetailVisible = false;
                                    item.IsSendPageVisible = false;
                                    item.IsReceivePageVisible = true;
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return scanButtonCommand;
            }
        }

        /// <summary>
        /// /// <summary>
        /// Method use to Scan wallet address
        /// </summary>
        /// </summary>
        public Command ScanCommand
        {
            get
            {
                if (scanCommand == null)
                {
                    scanCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null && IsOpened)
                        {
                            try
                            {
                                IsOpened = false;
                                ZXingScannerPage scanPage;
                                scanPage = new ZXingScannerPage(null, new ScanCancel());
                                scanPage.OnScanResult += (result) =>
                                    {
                                        scanPage.IsScanning = true;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await App.Current.MainPage.Navigation.PopModalAsync();
                                            WalletListModel item = selectedItem as WalletListModel;
                                            item.WalletAdressEntered = result.Text;
                                            walletAdress = result.Text;
                                            IsLoading = true;
                                            scanner = true;
                                            IsLoading = false;
                                        });
                                    };
                                await App.Current.MainPage.Navigation.PushModalAsync(scanPage);
                                IsOpened = true;
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return scanCommand;
            }
        }

        /// <summary>
        /// Method use to Close send page
        /// </summary>
        public Command CloseSendPageCommand
        {
            get
            {
                try
                {
                    if (closeSendPageCommand == null)
                    {
                        closeSendPageCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                WalletListModel item = selectedItem as WalletListModel;
                                item.IsCoinVisible = false;
                                item.IsCoinDetailVisible = true;
                                item.IsSendPageVisible = false;
                                item.IsReceivePageVisible = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return closeSendPageCommand;
            }
        }

        /// <summary>
        /// /// <summary>
        /// Method use to Close Receive Page
        /// </summary>
        /// </summary>
        public Command CloseReceivePageCommand
        {
            get
            {
                try
                {
                    if (closeReceivePageCommand == null)
                    {
                        closeReceivePageCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                WalletListModel item = selectedItem as WalletListModel;
                                item.IsCoinVisible = false;
                                item.IsCoinDetailVisible = true;
                                item.IsSendPageVisible = false;
                                item.IsReceivePageVisible = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return closeReceivePageCommand;
            }
        }

        /// <summary>
        /// /// <summary>
        /// Method use to copy wallet address
        /// </summary>
        /// </summary>
        public Command CopyAdressCommand
        {
            get
            {
                if (copyAdressCommand == null)
                {
                    copyAdressCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            try
                            {
                                var Copied = string.Copy(Preferences.Get(StringConstant.walletAddress, string.Empty));                               
                                await Clipboard.SetTextAsync(Copied);
                                DependencyService.Get<IMessage>().ShortAlert("Address Copied");
                                var text = await Clipboard.GetTextAsync();
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return copyAdressCommand;
            }
        }

        /// <summary>
        /// Method use to share wallet address
        /// </summary>
        public Command ShareAdressCommand
        {
            get
            {
                if (shareAdressCommand == null)
                {
                    shareAdressCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            try
                            {
                                await Share.RequestAsync(new ShareTextRequest
                                {
                                    Text = Preferences.Get(StringConstant.walletAddress, string.Empty),
                                    Title = "Share Text"
                                });
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return shareAdressCommand;
            }
        }

        /// <summary>
        /// Method use to initiate send method
        /// </summary>
        public Command SendButtonCommand
        {
            get
            {
                try
                {
                    if (sendButtonCommand == null)
                    {
                        sendButtonCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                try
                                {
                                    await SendCommandAsync();
                                }
                                catch (Exception ex)
                                {
                                    Crashes.TrackError(ex);
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return sendButtonCommand;
            }
        }
        #endregion
        #region public Properties
        private string gasFeeMessage;
        public string GasFeeMessage
        {
            get
            {
                gasFeeMessage = FeeUnit +"."+ " Please click “OK” to continue.";
                return gasFeeMessage;
            }
            set
            {
                if (gasFeeMessage != value)
                {
                    gasFeeMessage = value;
                    OnPropertyChanged(nameof(GasFeeMessage));
                }
            }
        }
        public string scanAdress;
        public string FeeAmount;
        public string FeeUnit;
        private string walletFormattedBalance;
        public string WalletFormattedBalance
        {
            get => walletFormattedBalance;
            set
            {
                if (walletFormattedBalance != value)
                {
                    walletFormattedBalance = value;
                    OnPropertyChanged(nameof(WalletFormattedBalance));
                }
            }
        }
        private string edItedWalletName;
        public string EdItedWalletName
        {
            get => edItedWalletName;
            set
            {
                if (edItedWalletName != value)
                {
                    edItedWalletName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string WalletName
        {
            get => walletName;
            set
            {
                if (walletName != value)
                {
                    walletName = value;
                    OnPropertyChanged();
                }
            }
        }
        public double txtWalletBalance;
        public bool exit;
        public bool error;
        public bool amountEntrty;
        private bool isPopup;
        public bool IsPopup
        {
            get => isPopup;
            set
            {
                isPopup = value;
                OnPropertyChanged(nameof(IsPopup));
            }
        }
        private string trasactionlink;
        public string Trasactionlink
        {
            get => trasactionlink;
            set
            {
                if (trasactionlink != value)
                {
                    trasactionlink = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public double WalletBalance
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
        #region Public methods
        /// <summary>
        /// This Constructor method is used to see the list of coins and coin amount.
        /// </summary>
        public WalletViewModel()
        {
            try
            {
                apiUpdateWalletName = new UpdateWalletNameService();
                feeService = new GetEstimateFeeService();
                transferToken = new TransferTokenService();
                apiService = new GetWalletDetailsService();
                AddNewWallet = new Command(AddNewWalletClick);
                EditWalletName = new Command(EditWalletClick);
                SaveWalletName = new Command(SaveWalletNameClickAsync);
                TrasactionStatusCommand = new Command(TrasactionStatusCommandClick);
                OKCommand = new Command(OKCommandClickAsync);
                SaveButton = true;
                Edit = false;
                _ = GetWalletDetails();
                exit = false;               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public async void TrasactionStatusCommandClick()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new TransactionDetailPage(Trasactionlink));
        }
        public async void OKCommandClickAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
        }


        // this method is used to edit wallet name 
        public void EditWalletClick()
        {
            try
            {
                Edit = true;
                SaveButton = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        // this method is used to save wallet name.
        public async void SaveWalletNameClickAsync()
        {
            try
            {
                SaveButton = true;
                Edit = false;
                if (!String.IsNullOrEmpty(EdItedWalletName))
                {
                    WalletName = EdItedWalletName;
                    try
                    {
                        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                        {
                            IsLoading = true;


                            EditWalletNameRequestModel editWalletNameRequestModel = new EditWalletNameRequestModel
                            {
                                WalletName = EdItedWalletName,
                                WalletAddress= Preferences.Get(StringConstant.walletAddress, string.Empty),
                            };

                            editWalletResponseModel = await apiUpdateWalletName.UpdateWalletName(editWalletNameRequestModel);
                            if (editWalletResponseModel != null)
                            {
                                if (editWalletResponseModel.Result && editWalletResponseModel.Status == 200)
                                {
                                    if (editWalletResponseModel != null)
                                    {
                                        WalletName = editWalletResponseModel.WalletName;
                                        await Application.Current.MainPage.DisplayAlert(string.Empty, editWalletResponseModel.Message, Resource.txtOk);
                                    }
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(string.Empty, editWalletResponseModel.Message, Resource.txtOk);
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
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method used to navigate AddWalletPage screen
        /// </summary>
        public async void AddNewWalletClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AddWalletPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
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
                    IsLoading = true;
                    WalletList = new ObservableCollection<WalletListModel>();
                    GetWalletsDetailsRequestModel getWalletDetailsRequest = new GetWalletsDetailsRequestModel
                    {
                        SeedId = Preferences.Get(StringConstant.SeedId, string.Empty)
                    };
                    getWalletDetailsRequest.WalletAddress = Preferences.Get(StringConstant.walletAddress, string.Empty);
                    getWalletDetailsResponse = await apiService.GetWalletDetails(getWalletDetailsRequest);
                    if (getWalletDetailsResponse != null)
                    {
                        if (getWalletDetailsResponse.Result && getWalletDetailsResponse.Status == 200)
                        {
                            if (getWalletDetailsResponse.Data != null)
                            {
                                Preferences.Set(StringConstant.walletAddress, getWalletDetailsResponse.WalletAddress);
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    WalletFormattedBalance = getWalletDetailsResponse.Balance.ToString("n2");
                                    WalletName = getWalletDetailsResponse.WalletName;
                                    foreach (WalletDetailsResponseModel item in getWalletDetailsResponse.Data)
                                    {
                                        WalletListModel wallet = new WalletListModel
                                        {
                                            CoinName = item.CoinName,
                                            CoinValue = Math.Round(item.CoinValue, 2),
                                            CoinValueString = item.CoinValue.ToString("n2"),
                                            CoinIcon = "https://" + item.CoinIcon,
                                            CoinShortName = item.CoinShortName,
                                            CoinUsdValue = Math.Round(item.CoinUsdValue, 2),
                                            CoinUsdValueString = item.CoinUsdValue.ToString("n2"),
                                            CoinStandard = item.CoinStandard,
                                            QrCode = getWalletDetailsResponse.QrCode,
                                            WalletAdress = getWalletDetailsResponse.WalletAddress,
                                            Walletbalance = Math.Round(getWalletDetailsResponse.Balance, 2),
                                            BlockChain = item.BlockChain,
                                        };
                                        Preferences.Set(StringConstant.CoinShortname, wallet.CoinShortName);
                                        WalletList.Add(wallet);
                                    }
                                    var index = 0;
                                    foreach (WalletListModel walletItem in WalletList)
                                    {
                                        walletItem.IsCoinVisible = true;
                                        walletItem.ListId = index;
                                        index++;
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
        /// Method use to send the coin to any wallet adress
        /// </summary>
        /// <returns></returns>
        private async Task SendCommandAsync()
        {
            await WalletFeeCalculationMethodAsync();
            if (!error)
            {
                await Application.Current.MainPage.DisplayAlert(Resource.txtGasFee, "This transaction will cost you " + FeeAmount + " " + GasFeeMessage, Resource.txtOk);
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
                                if (Convert.ToDouble(walletItem.Amount) != 0.00)
                                {
                                    if (scanner || Optionscanner)
                                    {
                                        transferTokenRequestModel.WalletAddressTo = walletAdress;
                                        transferTokenRequestModel.WalletAddressFrom = Preferences.Get(StringConstant.walletAddress, string.Empty);
                                        transferTokenRequestModel.Amount = Convert.ToDouble(walletItem.Amount);
                                        transferTokenRequestModel.CoinShortName = walletItem.CoinShortName;
                                        transferTokenRequestModel.BlockChain = walletItem.BlockChain;
                                        transferTokenRequestModel.Fee = FeeAmount.ToString();
                                        transferTokenRequestModel.EncryptedPrivateKey= Preferences.Get(StringConstant.EncryptedPrivateKey, string.Empty);
                                        walletAdress = null;
                                        scanner = false;
                                        Optionscanner = false;
                                        break;
                                    }
                                    else
                                    {
                                        transferTokenRequestModel.WalletAddressTo = walletItem.WalletAdressEntered;
                                        transferTokenRequestModel.WalletAddressFrom = Preferences.Get(StringConstant.walletAddress, string.Empty);
                                        transferTokenRequestModel.Amount = Convert.ToDouble(walletItem.Amount);
                                        transferTokenRequestModel.CoinShortName = walletItem.CoinShortName;
                                        transferTokenRequestModel.BlockChain = walletItem.BlockChain;
                                        transferTokenRequestModel.Fee = FeeAmount.ToString();
                                        transferTokenRequestModel.EncryptedPrivateKey = Preferences.Get(StringConstant.EncryptedPrivateKey, string.Empty);
                                        break;
                                    }
                                }
                            }
                            TransferTokenResponseModel response = await transferToken.TranferToken(transferTokenRequestModel);
                            if (response != null)
                            {
                                if (response.Status == 200 && response.Result)
                                {
                                    Trasactionlink = StringConstant.VeChainStatLink + response.TxId;
                                    IsPopup = true;
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, response.Message, Resource.txtOk);
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                                await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
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
            }
        }

        /// <summary>
        /// Gas Fee Calculation method  for transaction 
        /// </summary>
        /// <returns></returns>
        public async Task WalletFeeCalculationMethodAsync()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    IsLoading = true;
                    GasEstimateRequestModel feeCalculateRequestModel = new GasEstimateRequestModel();
                    foreach (WalletListModel walletItem in WalletList)
                    {
                        if (Convert.ToDouble(walletItem.Amount) != 0.00)
                        {
                            if (!string.IsNullOrEmpty(walletItem.WalletAdressEntered) && (Convert.ToDouble(walletItem.Amount) <= walletItem.CoinValue))

                            {
                                if (scanner || Optionscanner)
                                {
                                    feeCalculateRequestModel.WalletAddressTo = walletAdress;
                                    feeCalculateRequestModel.WalletAddressFrom = Preferences.Get(StringConstant.walletAddress, string.Empty);
                                    feeCalculateRequestModel.Amount = Convert.ToDouble(walletItem.Amount);
                                    feeCalculateRequestModel.CoinShortName = walletItem.CoinShortName;
                                    feeCalculateRequestModel.BlockChain = walletItem.BlockChain;
                                    amountEntrty = true;
                                    break;
                                }
                                else
                                {
                                    feeCalculateRequestModel.WalletAddressTo = walletItem.WalletAdressEntered;
                                    feeCalculateRequestModel.WalletAddressFrom = Preferences.Get(StringConstant.walletAddress, string.Empty);
                                    feeCalculateRequestModel.Amount = Convert.ToDouble(walletItem.Amount);
                                    feeCalculateRequestModel.CoinShortName = walletItem.CoinShortName;
                                    feeCalculateRequestModel.BlockChain = walletItem.BlockChain;
                                    amountEntrty = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(walletItem.Amount) <= walletItem.CoinValue)
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.txtWalletAddresAlert, Resource.txtOk);                                   
                                    exit = true;
                                    error = true;
                                    amountEntrty = true;
                                    break;
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, StringConstant.Insufficientbalance, Resource.txtOk);                                    
                                    error = true;
                                    amountEntrty = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!amountEntrty)
                    {
                        await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.txtEnterAmount, Resource.txtOk);                        
                        error = true;
                    }
                    GasFeeResponseModel response = await feeService.CalculateGasFee(feeCalculateRequestModel);
                    if (response != null && !exit)
                    {
                        if (response.Status == 200 && response.Result)
                        {
                            foreach (var item in WalletList)
                            {
                                item.GasAmount = response.GasConsumed.ToString();
                                item.GasUnit = response.GasUnit;
                                FeeAmount = response.GasConsumed;
                                FeeUnit = response.GasUnit;
                            }
                        }
                    }
                    else
                    {
                        if (!exit)
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);                            
                            error = true;
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);                    
                    error = true;
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
    }

}
