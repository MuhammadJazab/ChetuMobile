using Android.App;
using Android.Widget;
using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Droid.DependencyServices;

[assembly:Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace DorhniiFoundationWallet.Droid.DependencyServices
{
    public class MessageAndroid : IMessage
    {
        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;
        
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}