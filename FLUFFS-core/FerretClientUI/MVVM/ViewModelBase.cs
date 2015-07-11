using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FerretClientUI.MVVM
{
    abstract class ViewModelBase : ObservableObject
    {
        private Window _ActiveWindow = null;
        private Action _ExitAction = null;
        private bool _HasExecuted = false;

        private bool _EnableControls;
        public bool EnableControls
        {
            get { return _EnableControls; }
            set
            {
                _EnableControls = value;
                RaisePropertyChanged("EnableControls");
            }
        }

        private double _ControlOpacity;
        public double ControlOpacity
        {
            get { return _ControlOpacity; }
            set
            {
                _ControlOpacity = value;
                RaisePropertyChanged("ControlOpacity");
            }
        }

        protected void MarkBusy()
        {
            MarkBusy(0.5);
        }

        protected void MarkBusy(double opacity)
        {
            EnableControls = false;
            ControlOpacity = opacity;
        }

        protected void MarkFree()
        {
            EnableControls = true;
            ControlOpacity = 1;
        }

        ~ViewModelBase()
        {
            if (_ExitAction != null &&
                _HasExecuted == false)
            {
                _ExitAction.Invoke();
            }
        }
    }
}
