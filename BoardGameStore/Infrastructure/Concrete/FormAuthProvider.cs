using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BoardGameStore.Infrastructure.Abstract;
namespace BoardGameStore.Infrastructure.Concrete
{
    public class FormAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}