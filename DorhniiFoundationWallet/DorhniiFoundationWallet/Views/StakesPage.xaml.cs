using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StakesPage : ContentPage
    {
        public StakesPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var res = await this.DisplayAlert("Exit ?", "Do you really want to exit the application?", "Yes", "No").ConfigureAwait(false);

                if (res) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            });
            return true;
        }
    }
}