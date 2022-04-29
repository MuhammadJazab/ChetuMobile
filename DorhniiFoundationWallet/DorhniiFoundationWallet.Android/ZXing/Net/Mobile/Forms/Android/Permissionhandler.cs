using Android.Content.PM;

namespace ZXing.Net.Mobile.Forms.Android
{
    internal class Permissionhandler
    {
        internal static void OnRequestPermissionresult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            var permision = Permission.Granted;
        }
    }
}