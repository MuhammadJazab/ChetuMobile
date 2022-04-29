using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DorhniiFoundationWallet.CustomRenderers;
using DorhniiFoundationWallet.iOS.CustomRender;
using Microsoft.AppCenter.Crashes;
using UIKit;

[assembly: ExportRenderer(typeof(RendererEntry), typeof(EntryCustomerRenderer))]
namespace DorhniiFoundationWallet.iOS.CustomRender
{
    public class EntryCustomerRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.Layer.BorderWidth = 0;
                    Control.BorderStyle = UIKit.UITextBorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
