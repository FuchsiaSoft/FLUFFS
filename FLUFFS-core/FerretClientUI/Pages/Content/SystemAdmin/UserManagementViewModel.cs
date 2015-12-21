using EntityModel;
using FerretClientUI.DataEntry;
using FerretClientUI.DataEntry.DataEntryWindows;
using MVVM;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FerretClientUI.Pages.Content.SystemAdmin
{
    class UserManagementViewModel : ViewModelBase
    {
        public UserManagementViewModel()
        {
            RefreshAsync();
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

        public RelayCommand EditUserCommand { get { return new RelayCommand(EditUser); } }

        private void EditUser(object obj)
        {
            UserView view = new UserView();
            view.DataContext = new UserViewModel
                (DataEntryMode.Edit, (User)obj, view, RefreshAsync);
            view.Show();
        }

        public RelayCommand NewUserCommand { get { return new RelayCommand(NewUser); } }

        private void NewUser(object obj)
        {
            UserView view = new UserView();
            view.DataContext = new UserViewModel
                (DataEntryMode.New, null, view, RefreshAsync);
            view.Show();
        }

        private async void RefreshAsync()
        {
            await Task.Run(() =>
            {
                MarkBusy();

                Refresh();

                MarkFree();
            });
        }

        private void Refresh()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                Users = new ObservableCollection<User>
                    (db.Users.Include(i => i.Indices));
            }
        }
    }
}
