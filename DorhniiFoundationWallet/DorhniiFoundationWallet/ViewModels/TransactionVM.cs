using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for displaying all transactions.
    /// </summary>
    public class TransactionVM : ObservableObject
    {
        #region Properties
        ITransactionHistoryService apiService;
        TransactionHistoryResponseModel transactionResponse = null;

        #region List Properties
        private ObservableCollection<TransactionListModel> transactionList;
        /// <summary>
        /// This private property gets or sets the list of all transactions.
        /// </summary>
        public ObservableCollection<TransactionListModel> TransactionList
        {
            get { return transactionList ?? (transactionList = new ObservableCollection<TransactionListModel>()); }
            set
            {
                transactionList = value;
                OnPropertyChanged(nameof(TransactionList));
            }
        }
        #endregion

        #region Image Properties
        /// <summary>
        /// This property gets or sets the image of the send transactions.
        /// </summary>
        public string SendTransactionIcon { get; set; } = StringConstant.SendTransactionIcon;

        /// <summary>
        /// This property gets or sets the image of the receive transactions.
        /// </summary>
        public string ReceiveTransactionIcon { get; set; } = StringConstant.ReceiveTransactionIcon;
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// This method is used to display the list of send and receive transactions.
        /// </summary>
        public TransactionVM()
        {
            apiService = new TransactionHistoryService();
            GetTransactionList();
        }

        /// <summary>
        /// This method is used to get the list of all trancsactions.
        /// </summary>
        private async void GetTransactionList()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });

                    TransactionHistoryRequestModel transactionHistoryRequest = new TransactionHistoryRequestModel
                    {
                        WalletAddress = "",
                        TransactionType = "All"
                    };

                    transactionResponse = await apiService.GetAllTransaction(transactionHistoryRequest);
                    if (transactionResponse != null)
                    {
                        if (transactionResponse.result && transactionResponse.status == 200)
                        {
                            if (transactionResponse.data != null)
                            {
                                foreach (var item in transactionResponse.data)
                                {
                                    TransactionListModel transaction = new TransactionListModel();
                                    transaction.TransactionType = item.TransactionType;
                                    transaction.TransactionAmount = item.Amount.ToString();
                                    transaction.TransactionDateTime = item.Date.ToString();
                                    transaction.CoinName = item.CoinName;
                                    transaction.FeeDetails = "(Fee : 1 DHN)";
                                    transaction.TransactionTypeImage = transaction.TransactionType == "Send" ? SendTransactionIcon : ReceiveTransactionIcon;
                                    TransactionList.Add(transaction);
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
        #endregion
    }
}
