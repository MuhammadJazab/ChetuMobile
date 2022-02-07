using Android.Content;
using DorhniiFoundationWallet.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(Editor), typeof(EditorCustomRenderer), new[] { typeof(VisualMarker.DefaultVisual) })]
namespace DorhniiFoundationWallet.Droid.CustomRenderers
{
    public class EditorCustomRenderer : EditorRenderer
    {
        public EditorCustomRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}