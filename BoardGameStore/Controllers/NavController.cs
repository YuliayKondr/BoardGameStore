using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBoardGameStore.Abstract;
namespace BoardGameStore.Controllers
{
/// <summary>
/// контроллер для меню
/// </summary>
    public class NavController : Controller
    {
        // GET: Nav
        IBoardGameRepository repository;
        public NavController(IBoardGameRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string category= null,bool horizontalNav = false)
        {
            ViewBag.Cat = category;
            IEnumerable<string> categories = repository.Games
                .Select(g => g.Category).Distinct().OrderBy(x => x);
            string viewName = horizontalNav ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
    }
}