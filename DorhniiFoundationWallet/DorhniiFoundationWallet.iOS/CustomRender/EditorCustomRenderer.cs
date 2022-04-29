using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DorhniiFoundationWallet.CustomRenderers;
using DorhniiFoundationWallet.iOS.CustomRender;
using Microsoft.AppCenter.Crashes;
[assembly: ExportRenderer(typeof(RendererEditor), typeof(EditorCustomRenderer))]
namespace DorhniiFoundationWallet.iOS.CustomRender
{
    public class EditorCustomRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.BackgroundColor = null;                    
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
