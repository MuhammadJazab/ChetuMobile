using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is for browse features
    /// </summary>
    public class BrowsePageViewModel : BaseViewModel
    {

        #region Single Property


        #endregion

        #region full Property
        private string enteredUrl;
        public string EnteredUrl
        {
            get => enteredUrl;
            set
            {
                enteredUrl = value;
                OnPropertyChanged(nameof(EnteredUrl));
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// This constructor method used to work for browse functionality
        /// </summary>
        public BrowsePageViewModel()
        {
            EnteredUrl = StringConstant.CommonURLpart + Preferences.Get(StringConstant.UserEnteredUrl, string.Empty);
        }


        #endregion

        #region Method


        #endregion

        #region Command
        private Command backCommand1;
        /// <summary>
        /// This command is used for navigate to back Page
        /// </summary>
        public ICommand BackCommand
        {
            get
            {
                if (backCommand1 == null)
                {
                    backCommand1 = new Command(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new SwapPage());
                    });
                }

                return backCommand1;
            }
        }

        #endregion
    }

}
