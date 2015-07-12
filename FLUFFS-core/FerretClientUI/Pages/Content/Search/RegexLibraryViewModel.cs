using EntityModel;
using FerretClientUI.MVVM;
using FerretClientUI.Utils;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FerretClientUI.Pages.Content.Search
{
    class RegexLibraryViewModel : ViewModelBase
    {
        public RegexLibraryViewModel()
        {
            NumbersReplaceChecked = true;
            LowerCaseChecked = true;
            WordCount = 500;
            Reload();

            //TODO: testing a listview itemtemplate, delete this stuff
            RegExTemplates = new ObservableCollection<RegExTemplate>();
            RegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(\b(07[0-9]{9}|0[1-3][0-9]{9})\b)",
                Title = "UK phones",
                Description = "UK landline or mobile numbers with no spaces, as a distinct word"
            });
            RegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(?i)(\b[a-z]{5}\d{2}\b)",
                Title = "Ref number on its own",
                Description = "5 letters and 2 numbers, as a distinct word"
            });
            RegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(?i)([a-z]{5}\d{2})",
                Title = "Ref number within other words",
                Description = "5 letters and 2 numbers, as a distinct word"
            });
            RegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"([A-Z])",
                Title = "All uppercase letters",
                Description = "Just all uppercase letters"
            });
        }

        //TODO: testing a listview itemtemplate, delete this stuff
        private ObservableCollection<RegExTemplate> _RegExTemplates;

        public ObservableCollection<RegExTemplate> RegExTemplates
        {
            get { return _RegExTemplates; }
            set
            {
                _RegExTemplates = value;
                RaisePropertyChanged("RegExTemplates");
            }
        }

        private RegExTemplate _SelectedTemplate;

        public RegExTemplate SelectedTemplate
        {
            get { return _SelectedTemplate; }
            set
            {
                _SelectedTemplate = value;
                RaisePropertyChanged("SelectedTemplate");
                RegExText = SelectedTemplate.Syntax;
                RegExDescription =
                    "[b]" + SelectedTemplate.Title + "[/b]\n" +
                            SelectedTemplate.Description;
            }
        }



        private string _RegExText;

        public string RegExText
        {
            get { return _RegExText; }
            set
            {
                _RegExText = value;
                RaisePropertyChanged("RegExText");
            }
        }

        private string _RegExDescription;

        public string RegExDescription
        {
            get { return _RegExDescription; }
            set
            {
                _RegExDescription = value;
                RaisePropertyChanged("RegExDescription");
            }
        }



        private string _RandomText;

        public string RandomText
        {
            get
            {
                return GetBBCode(_RandomText, RegExText);
            }
            set
            {
                _RandomText = value;
                RaisePropertyChanged("RandomText");
            }
        }

        private static string GetBBCode(string text, string regexPattern)
        {
            if (string.IsNullOrEmpty(regexPattern) ||
                string.IsNullOrWhiteSpace(regexPattern)) return text;

            string color = "#" +
                    AppearanceManager.Current.AccentColor.R.ToString("X2") +
                    AppearanceManager.Current.AccentColor.G.ToString("X2") +
                    AppearanceManager.Current.AccentColor.B.ToString("X2");

            return System.Text.RegularExpressions.Regex.Replace
                (text, regexPattern, delegate(Match match)
            {
                string v = match.ToString();
                return "[b][u][color=" + color + "]" 
                    + v + 
                    "[/color][/u][/b]";
            });

        }

        private int _WordCount;

        public int WordCount
        {
            get { return _WordCount; }
            set
            {
                _WordCount = value;
                RaisePropertyChanged("WordCount");
            }
        }

        #region RadioButtons

        //TODO: do this properly with converters etc., this is way too
        //many bool obprops lying around.

        private bool _NoNumbersChecked;

        public bool NoNumbersChecked
        {
            get { return _NoNumbersChecked; }
            set
            {
                _NoNumbersChecked = value;
                RaisePropertyChanged("NoNumbersChecked");
            }
        }

        private bool _AllNumbersChecked;

        public bool AllNumbersChecked
        {
            get { return _AllNumbersChecked; }
            set
            {
                _AllNumbersChecked = value;
                RaisePropertyChanged("AllNumbersChecked");
            }
        }

        private bool _NumbersInsideChecked;

        public bool NumbersInsideChecked
        {
            get { return _NumbersInsideChecked; }
            set
            {
                _NumbersInsideChecked = value;
                RaisePropertyChanged("NumbersInsideChecked");
            }
        }

        private bool _NumbersReplaceChecked;

        public bool NumbersReplaceChecked
        {
            get { return _NumbersReplaceChecked; }
            set
            {
                _NumbersReplaceChecked = value;
                RaisePropertyChanged("NumbersReplaceChecked");
            }
        }

        private bool _UpperCaseChecked;

        public bool UpperCaseChecked
        {
            get { return _UpperCaseChecked; }
            set
            {
                _UpperCaseChecked = value;
                RaisePropertyChanged("UpperCaseChecked");
            }
        }

        private bool _LowerCaseChecked;

        public bool LowerCaseChecked
        {
            get { return _LowerCaseChecked; }
            set
            {
                _LowerCaseChecked = value;
                RaisePropertyChanged("LowerCaseChecked");
            }
        }

        private bool _TitleCaseChecked;

        public bool TitleCaseChecked
        {
            get { return _TitleCaseChecked; }
            set
            {
                _TitleCaseChecked = value;
                RaisePropertyChanged("TitleCaseChecked");
            }
        }

        private bool _RandomCaseChecked;

        public bool RandomCaseChecked
        {
            get { return _RandomCaseChecked; }
            set
            {
                _RandomCaseChecked = value;
                RaisePropertyChanged("RandomCaseChecked");
            }
        }

        #endregion

        public ICommand ReloadCommand { get { return new DelegateCommand(Reload); } }

        private void Reload()
        {
            CaseOption caseOption = CaseOption.LowerCase;
            if (UpperCaseChecked) caseOption = CaseOption.AllCaps;
            if (LowerCaseChecked) caseOption = CaseOption.LowerCase;
            if (TitleCaseChecked) caseOption = CaseOption.TitleCase;
            if (RandomCaseChecked) caseOption = CaseOption.RandomCase;

            NumberOption numberOption = NumberOption.NumbersAsWords;
            if (AllNumbersChecked) numberOption = NumberOption.AllNumbers;
            if (NoNumbersChecked) numberOption = NumberOption.NoNumbers;
            if (NumbersInsideChecked) numberOption = NumberOption.NumbersInWords;
            if (NumbersReplaceChecked) numberOption = NumberOption.NumbersAsWords;

            RandomText = RandomWordGenerator.GenerateRandomText
                (WordCount, caseOption, numberOption);
        }

        public ICommand CheckCommand { get { return new DelegateCommand(Check); } }

        private void Check()
        {
            RaisePropertyChanged("RandomText");
        }
    }
}
