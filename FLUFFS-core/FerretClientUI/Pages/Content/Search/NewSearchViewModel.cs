using EntityModel;
using FerretClientUI.Utils;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Pages.Content.Search
{
    class NewSearchViewModel : ViewModelBase
    {
        public NewSearchViewModel()
        {
            //TODO: Dummy data for UI testing, get rid
            Random rnd = new Random();
            WorkingSets = new ObservableCollection<WorkingSet>();
            for (int i = 0; i < 10; i++)
            {
                WorkingSet thisSet = new WorkingSet()
                {
                    Name = "Test working set: " + i.ToString("000"),
                    Description = RandomWordGenerator.GenerateRandomText(rnd.Next(5,10), CaseOption.LowerCase, NumberOption.NoNumbers)
                };

                int fileCount = rnd.Next(1000);
                for (int j = 0; j < fileCount; j++)
                {
                    thisSet.TrackedFiles.Add(new TrackedFile());
                }
                WorkingSets.Add(thisSet);
            }

            AvailableRegExTemplates = new ObservableCollection<RegExTemplate>();
            SelectedRegExTemplates = new ObservableCollection<RegExTemplate>();
            SelectedRegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(\b(07[0-9]{9}|0[1-3][0-9]{9})\b)",
                Title = "UK phones",
                Description = "UK landline or mobile numbers with no spaces, as a distinct word"
            });
            AvailableRegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(?i)(\b[a-z]{5}\d{2}\b)",
                Title = "Ref number on its own",
                Description = "5 letters and 2 numbers, as a distinct word"
            });
            AvailableRegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"(?i)([a-z]{5}\d{2})",
                Title = "Ref number within other words",
                Description = "5 letters and 2 numbers"
            });
            AvailableRegExTemplates.Add(new RegExTemplate()
            {
                Syntax = @"([A-Z])",
                Title = "All uppercase letters",
                Description = "Just all uppercase letters"
            });
        }

        private ObservableCollection<WorkingSet> _WorkingSets;

        public ObservableCollection<WorkingSet> WorkingSets
        {
            get { return _WorkingSets; }
            set
            {
                _WorkingSets = value;
                RaisePropertyChanged("WorkingSets");
            }
        }

        private ObservableCollection<RegExTemplate> _AvailableRegExTemplates;

        public ObservableCollection<RegExTemplate> AvailableRegExTemplates
        {
            get { return _AvailableRegExTemplates; }
            set
            {
                _AvailableRegExTemplates = value;
                RaisePropertyChanged("AvailableRegExTemplates");
            }
        }

        private ObservableCollection<RegExTemplate> _SelectedRegExTemplates;

        public ObservableCollection<RegExTemplate> SelectedRegExTemplates
        {
            get { return _SelectedRegExTemplates; }
            set
            {
                _SelectedRegExTemplates = value;
                RaisePropertyChanged("SelectedRegExTemplates");
            }
        }



    }
}
