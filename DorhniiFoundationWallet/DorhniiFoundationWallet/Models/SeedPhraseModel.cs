using DorhniiFoundationWallet.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models
{
    public class SeedPhraseModel : ObservableObject
    {
        public string SeedLabel { get; set; }
        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
