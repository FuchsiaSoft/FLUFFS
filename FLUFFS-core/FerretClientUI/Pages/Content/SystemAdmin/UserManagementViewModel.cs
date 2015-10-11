using EntityModel;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Pages.Content.SystemAdmin
{
    class UserManagementViewModel : ViewModelBase
    {
        public UserManagementViewModel()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                Users = new ObservableCollection<User>(db.Users);
            }
        }


        private ObservableCollection<User> _Users;

        public ObservableCollection<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");
            }
        }

        private User _SelectedUser;

        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }

    }
}
