namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to fingerPrint Popup
    /// </summary>
    public interface IFingerPrintPopup
    {
        void ShowPopup();
        void HidePopup();
        void IsFingerPrintCanAuthenticate();

    }
}
