using FerretClientUI.MVVM;
using FerretClientUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Pages.Content.Search
{
    class RegexLibraryViewModel : ViewModelBase
    {
        public RegexLibraryViewModel()
        {
            RandomText = RandomWordGenerator.GenerateRandomText
                (1000, CaseOption.LowerCase, NumberOption.NumbersAsWords);
        }


        private string _RandomText;

        public string RandomText
        {
            get { return _RandomText; }
            set
            {
                _RandomText = value;
                RaisePropertyChanged("RandomText");
            }
        }

        
    }
}
