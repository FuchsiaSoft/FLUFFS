using EntityModel;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FerretClientUI.DataEntry.DataEntryWindows
{
    class UserViewModel : DataEntryViewModelBase
    {
        private User _User;

        public UserViewModel(DataEntryMode mode, User user,
            Window activeWindow, Action exitAction)
        {
            _Mode = mode;
            _User = user;
            _ActiveWindow = activeWindow;
            _ExitAction = exitAction;

            FetchExisting();
        }

        #region observable properties

        private string _Firstname;
        [Required(ErrorMessage ="Must provide a first name for the user")]
        public string Firstname
        {
            get { return _Firstname; }
            set
            {
                _Firstname = value;
                RaisePropertyChanged("Firstname");
            }
        }

        private string _Surname;
        [Required(ErrorMessage ="Must provide a last name for the user")]
        public string Surname
        {
            get { return _Surname; }
            set
            {
                _Surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        private string _Login;
        [Required(ErrorMessage ="Must provide a login for the user")]
        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                RaisePropertyChanged("Login");
            }
        }

        private bool _SysAdmin;

        public bool SysAdmin
        {
            get { return _SysAdmin; }
            set
            {
                _SysAdmin = value;
                RaisePropertyChanged("SysAdmin");
            }
        }

        private bool _NewPassword;

        public bool NewPassword
        {
            get { return _NewPassword; }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        private string _TempPassword;

        public string TempPassword
        {
            get { return _TempPassword; }
            set
            {
                _TempPassword = value;
                RaisePropertyChanged("TempPassword");
            }
        }


        private ObservableCollection<Index> _AvailableIndices;

        public ObservableCollection<Index> AvailableIndices
        {
            get { return _AvailableIndices; }
            set
            {
                _AvailableIndices = value;
                RaisePropertyChanged("AvailableIndices");
            }
        }


        #endregion

        /**In this viewmodel we need to override the normal
        dovalidationthensave because we need to check for uniqueness
        in usernames, and also to handle adding access rights to
        various indices after the user record has been updated. **/
        protected override void DoValidationThenSave()
        {
            if (CheckUnique() == false)
            {
                ModernDialog.ShowMessage
                    ("Username is not unique", "ERROR", MessageBoxButton.OK);
                return;
            }

            if (_Mode == DataEntryMode.New && NewPassword == false)
            {
                ModernDialog.ShowMessage
                    ("New users must have a temporary password", "ERROR", MessageBoxButton.OK);
                return;
            }

            if (NewPassword && string.IsNullOrEmpty(TempPassword))
            {
                ModernDialog.ShowMessage
                    ("Must enter a temporary password if the user is being forced to change it",
                     "ERROR", MessageBoxButton.OK);
                return;
            }

            base.DoValidationThenSave();
        }

        private bool CheckUnique()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                int currentUserId = (_User == null ? 0 : _User.Id);

                return db.Users
                    .Where(u => u.Login == Login
                        && u.Id != currentUserId).Count() == 0;
            }
        }

        protected override void FetchExisting()
        {
            if (_Mode == DataEntryMode.Edit)
            {
                using (DbModelContainer db = new DbModelContainer())
                {
                    User user = db.Users.Find(_User.Id);
                    Firstname = user.Firstname;
                    Surname = user.Surname;
                    Login = user.Login;
                    SysAdmin = user.IsSysAdmin;
                    NewPassword = false;

                    ObservableCollection<Index> indices = 
                        new ObservableCollection<Index>(db.Indices);

                    foreach (Index index in indices)
                    {
                        if (user.Indices.Where(i => i.Id == index.Id).Count() != 0)
                        {
                            index.IsSelected = true;
                        }
                    }

                    AvailableIndices = indices;
                }
            } 
            else
            {
                using (DbModelContainer db = new DbModelContainer())
                {
                    AvailableIndices = new ObservableCollection<Index>(db.Indices);
                }
            }


        }

        protected override void SaveExisting()
        {
            throw new NotImplementedException();
        }

        protected override void SaveNew()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                User user = new User()
                {
                    Firstname = this.Firstname,
                    Surname = this.Surname,
                    Login = this.Login,
                    IsSysAdmin = this.SysAdmin,
                    Salt = "salt",
                    Hash = "hash"
                };

                db.Users.Add(user);

                db.SaveChanges();

                user.ChangePassword(TempPassword);

                user.NewPasswordDue = true;

                db.SaveChanges();

                //access rights
                foreach (Index index in AvailableIndices.Where(i=>i.IsSelected))
                {
                    Index chosenIndex = db.Indices.Find(index.Id);
                    user.Indices.Add(chosenIndex);
                }

                db.SaveChanges();
            }
        }
    }
}
