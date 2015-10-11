using FirstFloor.ModernUI.Windows.Controls;
using MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FerretClientUI.DataEntry
{
    public enum DataEntryMode
    {
        New, Edit
    }

    public abstract class DataEntryViewModelBase : ViewModelBase
    {
        protected DataEntryMode? _Mode;

        public virtual void SetMode(DataEntryMode mode)
        {
            _Mode = mode;
        }

        protected virtual IEnumerable<ValidationResult> DoValidation()
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            List<PropertyInfo> properties = this.GetType().GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                Validator.TryValidateProperty
                    (property.GetValue(this),
                    new ValidationContext(this) { MemberName = property.Name },
                    validationResults);
            }

            return validationResults;
        }

        public virtual ICommand SaveCommand { get { return new DelegateCommand(DoValidationThenSave); } }

        protected virtual void DoValidationThenSave()
        {
            if (_Mode == null)
            {
                throw new InvalidOperationException
                    ("The DataEntryMode was not set for the derived viewmodel");
            }

            IEnumerable<ValidationResult> validationResults = DoValidation();

            if (validationResults.Count() == 0)
            {
                if (_Mode == DataEntryMode.New)
                {
                    SaveNew();
                }

                if (_Mode == DataEntryMode.Edit)
                {
                    SaveExisting();
                }
                CloseWindow();

                return;
            }

            StringBuilder builder = new StringBuilder();

            foreach (ValidationResult result in validationResults)
            {
                builder.AppendLine(result.ErrorMessage);
            }

            ModernDialog.ShowMessage
                (builder.ToString(), "VALIDATION ERRORS", MessageBoxButton.OK);

        }

        protected abstract void FetchExisting();
        protected abstract void SaveNew();
        protected abstract void SaveExisting();
    }
}
