using EntityModel;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace IndexingUI
{
    class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            MarkFree();
        }

        public int FileCount
        {
            get
            {
                if (RunningIndex == null) return 0;

                return RunningIndex.RunningFileCount;
            }
        }

        public int SecondsElapsed
        {
            get
            {
                if (_StartTime == null) return 0;
                TimeSpan span = (TimeSpan)(DateTime.Now - _StartTime);
                return (int)span.TotalSeconds;
            }
        }

        private DateTime? _StartTime = null;
        public DateTime TimeElapsed
        {
            get
            {
                if (_StartTime == null) return new DateTime(0);
                TimeSpan span = (TimeSpan)(DateTime.Now - _StartTime);
                return new DateTime(2015, 1, 1, span.Hours, span.Minutes, span.Seconds);
            }
        }

        private string _IndexName;

        public string IndexName
        {
            get { return _IndexName; }
            set
            {
                _IndexName = value;
                RaisePropertyChanged("IndexName");
            }
        }

        private string _Root;

        public string Root
        {
            get { return _Root; }
            set
            {
                _Root = value;
                RaisePropertyChanged("Root");
            }
        }


        public double FilesPerSecond
        {
            get
            {
                if (FileCount == 0 || SecondsElapsed == 0) return 0;
                return (double)FileCount / SecondsElapsed;
            }
        }

        private Index _RunningIndex;

        public Index RunningIndex
        {
            get { return _RunningIndex; }
            set
            {
                _RunningIndex = value;
                RaisePropertyChanged("RunningIndex");
            }
        }


        public RelayCommand BrowseCommand { get { return new RelayCommand(Browse, CanBrowse); } }

        private bool CanBrowse(object obj)
        {
            if (_RunningIndex == null) return true;
            return _RunningIndex.IsRunning ? false : true;
        }

        private void Browse(object obj)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            Root = dialog.SelectedPath;
        }

        public RelayCommand StartCommand { get { return new RelayCommand(StartIndex, CanStartIndex); } }

        private bool CanStartIndex(object obj)
        {
            if (_RunningIndex == null) return true;
            return _RunningIndex.IsRunning ? false : true;
        }

        private async void StartIndex(object obj)
        {
            MarkBusy();
            _StartTime = DateTime.Now;
            RunningIndex = new Index();
            RunningIndex.IsRunning = true;
            RunningIndex.BuildIndexAsync(Root, IndexName);
            CommandManager.InvalidateRequerySuggested();
            TrackProgress();
        }

        private void TrackProgress()
        {
            Thread thread = new Thread(() =>
            {
                do
                {
                    RaisePropertyChanged("FileCount");
                    RaisePropertyChanged("TimeElapsed");
                    RaisePropertyChanged("FilesPerSecond");

                    Thread.Sleep(400);

                } while (RunningIndex.IsRunning);

                MarkFree();
            });
            thread.Start();
        }
    }
}
