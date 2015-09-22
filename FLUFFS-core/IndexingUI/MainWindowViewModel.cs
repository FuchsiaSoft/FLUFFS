using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexingUI
{
    class MainWindowViewModel : ViewModelBase
    {

        private int _FileCount = 0;

        public int FileCount
        {
            get { return _FileCount; }
            set
            {
                _FileCount = value;
                RaisePropertyChanged("FileCount");
            }
        }

        private int _SecondsElapsed = 0;

        public int SecondsElapsed
        {
            get { return _SecondsElapsed; }
            set
            {
                _SecondsElapsed = value;
                RaisePropertyChanged("SecondsElapsed");
            }
        }

        public DateTime TimeElapsed
        {
            get
            {
                return new DateTime(0, 0, 0, 0, 0, _SecondsElapsed);
            }
        }


    }
}
