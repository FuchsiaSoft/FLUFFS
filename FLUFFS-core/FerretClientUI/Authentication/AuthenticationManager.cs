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
        public User CurrentUser { get; private set; }

        public bool Authenticate(string login, string password)
        {
            throw new NotImplementedException();
        }


    }
}
