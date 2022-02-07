using Android.Content;
using DorhniiFoundationWallet.CustomRenderers;
using DorhniiFoundationWallet.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RendererEntry), typeof(EntryCustomerRenderer))]
namespace DorhniiFoundationWallet.Droid.CustomRenderers
{
    /// <summary>
    /// 
    /// </summary>
    public class EntryCustomerRenderer : EntryRenderer
    {
        public EntryCustomerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.Background = null;
        }
    }
}