using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for sending and receiving coins
    /// </summary>
    public class CoinVM : ObservableObject
    {
        #region Properties

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

        #region Command Properties
        /// <summary>
        /// This command property gets or sets the back button command.
        /// </summary>
        public ICommand BackButtonCommand { get; set; }

        /// <summary>
        /// This command property gets or sets the send button command.
        /// </summary>
        public ICommand SendButtonCommand { get; set; }

        /// <summary>
        /// This command property gets or sets the receive button command.
        /// </summary>
        public ICommand ReceiveButtonCommand { get; set; }
        #endregion

        #region List Properties
        /// <summary>
        /// This private property gets or sets the list of transactions.
        /// </summary>
        public ObservableCollection<TransactionListModel> TransactionList { get; set; }
        #endregion
        #endregion

        #region Methods

        /// <summary>
        /// This method is used for sending and receiving coins
        /// </summary>
        public CoinVM()
        {
            GetTransactionList();
            BackButtonCommand = new Command(BackButtonClick);
            SendButtonCommand = new Command(SendButtonClick);
            ReceiveButtonCommand = new Command(ReceiveButtonClick);
        }

        /// <summary>
        /// This method gets the list of all coin transactions
        /// </summary>
        public void GetTransactionList()
        {
            #region commented
            //TransactionList = new ObservableCollection<TransactionListModel>
            //{
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="12,456.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="5th November 2021 7.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,960.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="5th November 2021 9.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="14,563.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="6th November 2021 11.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="75,891.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="5th November 2021 12.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="56,223.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="7th November 2021 01.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="86,557.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="8th November 2021 06.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="96,667.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="9th November 2021 04.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,556.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="10th November 2021 09.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="12,456.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="11th November 2021 05.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,960.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="12th November 2021 07.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="14,563.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="13th November 2021 04.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="75,891.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="14th November 2021 03.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="56,223.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="15th November 2021 04.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="86,557.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="16th November 2021 05.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="96,667.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="17th November 2021 06.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,556.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="18th November 2021 08.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="12,456.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="19th November 2021 10.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,960.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="20th November 2021 12.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="14,563.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="24th November 2021 07.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="75,891.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="5th November 2021 09.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="56,223.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="25th November 2021 08.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=SendTransactionIcon, TransactionDescription="Send", TransactionAmount="86,557.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="26th November 2021 01.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="96,667.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="27th November 2021 03.00PM" },
            //     new TransactionListModel() { SendReceiveTransactionImage=ReceiveTransactionIcon, TransactionDescription="Receive", TransactionAmount="78,556.00 $", TransactionFee="(Fee : 1 DHN)", DateAndTime="28th November 2021 04.00PM" }
            //};
            #endregion
        }

        /// <summary>
        /// Method to click on Back Button
        /// </summary>
        public async void BackButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Send Button
        /// </summary>
        public async void SendButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SendCoinPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Receive Button
        /// </summary>
        public async void ReceiveButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ReceiveCoinPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
