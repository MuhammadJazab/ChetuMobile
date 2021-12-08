using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Helpers
{
    public class StringConstant
    {
        #region API Constant Values
        public const string API_KEY_URL = "https://dohrniifoundationi2-qa.chetu.com/v1/";
        public const string API_KEY_HttpAuthorization = "Authorization";
        public const string API_KEY_BEARER = "bearer";
        public const string HttpContentType = "Application/json";
        public const string JSONContentType = "application/json";

        public const string SaveSeedsAPI = "authentication/seed";
        public const string VerifySeedAPI = "authentication/validateSeed";
        public const string CreateWalletAPI = "wallet/create";
        public const string TransactionHistoryAllAPI = "wallet/transactionhistory";
        public const string AddWalletAPI = "wallet/walletlist";
        public const string WalletDetailsAPI = "wallet/walletdetail";
        #endregion

        #region String Constants
        public const string TabKeyId = "TabKeyId";
        public const string SeedId = "SeedId";
        public const string DevicePassword = "DevicePassword";
        #endregion

        #region Image Constants
        public const string AppIcon = "AppIconImage.png";
        public const string PasswordIcon = "PasswordLockIcon.png";
        public const string CrossEyeCut = "eye.png";
        public const string CrossEyeUnCut = "eye_cut.png";
        public const string EditIcon = "editIcon.png";
        public const string SaveIcon = "saveIcon.png";
        public const string RightArrowClickedIcon = "rightArrowGray.png";
        public const string SendTransactionIcon = "SendTransaction.png";
        public const string ReceiveTransactionIcon = "ReceiveTransaction.png";
        public const string SupportIcon = "supportIcon.png";
        public const string PrivacyIcon = "privacyPolicyIcon.png";
        public const string TermsIcon = "termIcon.png";
        public const string AccountsIcon = "walletIcon.png";
        public const string LanguageIcon = "languageIcon.png";
        public const string FingerprintIcon = "fingerprint.png";
        public const string ShowSeedIcon = "eyecutIcons.png";
        public const string LogoutIcon = "logoutIcon.png";
        public const string SettingsBlack = "settingsblack.png";
        public const string SettingsGray = "settingsgray.png";
        public const string StakesBlack = "stakesblack.png";
        public const string StakesGray = "stakesgray.png";
        public const string TransactionBlack = "transactionblack.png";
        public const string TransactionGray = "transactiongray.png";
        public const string WalletBlack = "walletblack.png";
        public const string WalletGray = "walletgray.png";
     

        #endregion

        #region String Constants Values
        public const string passwordRegex = "^(?=.*[A - Za - z])(?=.* d)[A - Za - zd]{8,}$";
        #endregion

      

    }
}
