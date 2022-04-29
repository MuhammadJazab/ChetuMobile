namespace DorhniiFoundationWallet.Helpers
{
    public class StringConstant
    {
        #region API Constant Values        
        //public const string API_KEY_URL = "https://dohrniifoundationi2-qa.chetu.com/v1/";
        public const string API_KEY_URL = "https://api.dohrniiwallet.ch/v1/";
        public const string API_KEY_HttpAuthorization = "Authorization";
        public const string API_KEY_BEARER = "bearer";
        public const string HttpContentType = "Application/json";
        public const string JSONContentType = "application/json";
        public const string SaveSeedsAPI = "authentication/seed";
        public const string VerifySeedAPI = "authentication/validateSeed";
        public const string CreateWalletAPI = "wallet/create";
        public const string TransactionHistoryAllAPI = "wallet/transactionhistory";
        public const string WalletAPI = "wallet/walletlist";
        public const string WalletDetailsAPI = "wallet/walletdetail";
        public const string TransferTokenAPI = "wallet/transfertoken";
        public const string SupportPageAPI = "setting/supportmail";
        public const string RestoreWalletAPI = "wallet/restore";
        public const string GasFeeAPI = "wallet/gas/estimate";
        public const string CreatePasswordAPI = "authentication/create/password";
        public const string LoginApi = "authentication/login";
        public const string ChangePasswordApi = "authentication/change/password";
        public const string StakeTokenOverViewAPI = "wallet/stake/token/overview  ";
        public const string StakingAvailableBalanceAPI = "wallet/stake/available/token";
        public const string TakeStakeAPI = "wallet/stake";
        public const string GetStakedDetailAPI = "wallet/stake/token/detail";
        public const string UnStakeTokenAPI = "wallet/unstake";       
        #endregion
        #region Used URL link
        public const string VexchanageLink = "Vexchange.io";
        public const string VeRocketLink = "Verocket.com";
        public const string VeChainStatLink = " https://vechainstats.com/transaction/";
        //public const string VeChainStatLink = "https://explore-testnet.vechain.org/transactions/";
        #endregion
        #region touch face id variables
        public const string KEY_IS_TOUCH_FACE_ID_ENABLED = "IsTouchFaceIDEnable";
        public const string Face_ID = "Face ID";
        public const string Touch_ID = "Touch ID";
        public const string Passcode = "Passcode";
        #endregion
        #region String Constants
        public const string TabKeyId = "TabKeyId";
        public const string SeedId = "SeedId";
        public const string DevicePassword = "Password";
        public const string BackButton = "Back";
        public const string walletAddress = "WalletAdress";
        public const string TransactionTypeSend = "Send";
        public const string TransactionTypeAll = "All";
        public const string SeedIDValue = "618eb994ee815937fd53f919";
        public const string IsRestoreValidate = "IsRestoreValidate";
        public const string PrivateKey = "PrivateKey";
        public const string EncryptedPrivateKey = "EncryptedPrivateKey";
        public const string AcountFlag = "IsAcountExist";
        public const string CoinShortname = "CoinShortname";
        public const string Csv = ".csv";
        public const string SeedPhrase = "SeedPhrase.";
        public const string CsvFile = "csvfile";
        public const string TextCsv = "text/csv";
        public const string UserEnteredUrl = "public const string";
        public const string CommonURLpart = "https://www.";
        public const string HTTP = "https://";
        public const string ETH = "ETH";
        public const string VECHAIN = "VECHAIN";
        public const string ETHEREUM = "ETHEREUM";
        public const string SEED_PHRASE = "SEED_PHRASE";
        public const string PRIVATE_kEY = "PRIVATE_KEY";
        public const string scanAddress = "scanAdress";
        public const string UserID = "userid";
        public const string WalletExist = "WalletExist";
        public const string IstoreAccountHideButton = "IstoreAccountHideButton";
        public const string StakeDbId = "StakeDbId";
        public const string Insufficientbalance = "Insufficient balance";
        #endregion
        #region Image Constants
        public const string AppIcon = "AppIconImage.png";
        public const string PasswordIcon = "PasswordLockIcon.png";
        public const string CrossEyeCut = "eye.png";
        public const string CrossEyeUnCut = "eye_cut.png";
        public const string PlusAppIcon = "PlusApp.png";
        public const string SaveIcon = "SaveWhiteGrayBack.png";
        public const string SendTransactionIcon = "SendTransaction.png";
        public const string ReceiveTransactionIcon = "ReceiveTransaction.png";
        public const string ProfileIcon = "ProfileIcon.png";
        public const string BiometricICon = "ProfileIcon.png";
        public const string SupportIcon = "supportIcon.png";       
        public const string PrivacyIcon = "privacyPolicyIcon.png";
        public const string TermsIcon = "termIcon.png";
        public const string AccountsIcon = "walletIcon.png";
        public const string LanguageIcon = "languageIcon.png";
        public const string FingerprintIcon = "FingerPrintIcon.png";
        public const string PasswordchangeIcon = "PasswordChangeIcon.png";
        public const string ShowSeedIcon = "eyecutIcons.png";
        public const string LogoutIcon = "logoutIcon.png";
        public const string LockIcon = "Group.png";
        public const string SendAppIcon = "Pointingup.png";
        public const string ReceiveAppIcon = "Pointingdown.png";
        public const string CopyAppIcon = "CopyAppIcon.png";       
        public const string ShareApp48Icon = "ShareApp48Icon.png";
        public const string ScanAppIcon = "ScanAppIcon.png";
        public const string ForwardAppIcon = "ForwardIcon";
        public const string SaveSeedAppIcon = "DownloadSeed";
        public const string BackwardPasswordPage = "leftarrowWhite";
        public const string BackwardAppIcon = "BackwardIcon";
        public const string SettingsBlack = "SettingGroup3.png";
        public const string SettingsGray = "SettingSmall.png";       
        public const string StakesBlack = "Stack.png";
        public const string StakesGray = "StackSmall.png";        
        public const string SwapIcon = "SwapAppIcon.png";
        public const string TransactionBlack = "History.png";
        public const string TransactionGray = "HistorySmall.png";        
        public const string WalletBlack = "WalletGroup1.png";
        public const string WalletGray = "Walletblack3.png";       
        public const string EditWhite = "EditIconGrayBack.png";
        public const string Vexchange = "Vexchange.png";
        public const string VeRocket = "VerocketImage.png";
        //New Image constant
        public const string DohrniiTextLogo = "DohrniiTextLogo.png";
        public const string EntryLockICon = "Group.png";
        public const string YOURNEWSEEDPHRASE = "YOURNEWSEEDPHRASE.png";
        public const string SEEDPHRASE = "SEEDPHRASE.png";
        public const string CrossSign = "CrossSign.png";// it was already
        public const string ScanImageButton = "Group1.png";
        #endregion
        #region String Constants Values
        public const string PasswordRegex = "^[0-9a-zA-Z]{8,16}$";
        public const string AddWalletEntryRegex = "^[a-zA-Z\\s]+$";
        public const string AddWalletEntryRegex2 = "^[0-9a-zA-Z]+$";
        #endregion



    }
}
