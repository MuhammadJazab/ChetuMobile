using System.Windows.Input;
namespace DorhniiFoundationWallet.Models
{
    public class SeedPhraseListModel
    {
        public string LabelNumber { get; set; }
        public string EntryText { get; set; }
        public int EntryId { get; set; }
        public ICommand OnLabelTap { get; set; }
        public ICommand OnUpperLabelTap { get; set; }
    }
}
