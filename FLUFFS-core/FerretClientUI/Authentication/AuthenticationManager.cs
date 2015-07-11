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
            throw new NotImplementedException();
        }


    }
}
