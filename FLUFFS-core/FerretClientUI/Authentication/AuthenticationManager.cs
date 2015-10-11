using EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Authentication
{
    static class AuthenticationManager
    {
        public static User CurrentUser { get; private set; }

        public static bool Authenticate(string login, string password)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                User user = db.Users
                    .Where(u => u.Login == login)
                    .FirstOrDefault();

                if (user == null) return false;

                if (user.Authenticate(password))
                {
                    CurrentUser = user;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Creates the initial user record necessary to log in as
        /// sys admin and create subsequent records.  Only here as
        /// a convenience to be called with VS immediate window.
        /// </summary>
        /// <param name="username">The username for the new user</param>
        /// <param name="password">Password for the new user</param>
        /// <param name="firstname">First name of the new user</param>
        /// <param name="lastname">Last name of the new user</param>
        private static void CreateInitialUserRecord
            (string username, string password, string firstname,
             string lastname)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                User user = new User()
                {
                    Firstname = firstname,
                    Surname = lastname,
                    Login = username,
                    IsSysAdmin = true,
                    Salt = "salt",
                    Hash = "hash",
                    NewPasswordDue = true
                };

                db.Users.Add(user);
                db.SaveChanges();

                user.ChangePassword(password);

                user.NewPasswordDue = true;
                db.SaveChanges();
            }
        }


    }
}
