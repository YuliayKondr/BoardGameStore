using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameStore.Infrastructure.Abstract;
using BoardGameStore.Models;
using System.Web.Security;
using BoardGameStore.Filters;
namespace BoardGameStore.Controllers
{
    /// <summary>
    /// контроллер для входа в систему создан для авторизации админа
    /// </summary>
    public class AccountController : Controller
    {
        
        IAuthProvider authProvider;
        
        public ViewResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {                
                if(FormsAuthentication.Authenticate(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}