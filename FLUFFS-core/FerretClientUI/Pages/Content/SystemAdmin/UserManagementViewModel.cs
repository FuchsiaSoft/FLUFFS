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
            //TODO: delete this, temp data for testing UI out

            Users = new ObservableCollection<User>();

            Users.Add(new User()
            {
                Firstname = "Chris",
                Surname = "Wilson",
                Login = "WILSC01"
            });

            Users.Add(new User()
            {
                Firstname = "Davey",
                Surname = "Jones",
                Login = "JONED01"
            });

            Users.Add(new User()
            {
                Firstname = "John",
                Surname = "Smith",
                Login = "SMITJ01"
            });

            Users.Add(new User()
            {
                Firstname = "Doctor",
                Surname = "Who",
                Login = "WHOD01"
            });
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
