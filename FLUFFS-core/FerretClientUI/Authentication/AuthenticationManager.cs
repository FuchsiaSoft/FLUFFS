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


    }
}
