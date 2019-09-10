using System.Web.Mvc;
using LibraryBoardGameStore.Abstract;
using LibraryBoardGameStore.Entites;
using System.Web;
using System.Linq;
namespace BoardGameStore.Controllers
{
    /// <summary>
    /// контроллер для админа, может зайти только админ
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        IBoardGameRepository repository;

        public AdminController(IBoardGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }
        /// <summary>
        /// все игры
        /// </summary>
        /// <param name="BoardGameId"></param>
        /// <returns></returns>
        public ViewResult Edit(int BoardGameId)
        {
            BoardGame game = repository.Games
                .FirstOrDefault(g => g.BoardGameId == BoardGameId);
            return View(game);
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(BoardGame game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.Piccher = new byte[image.ContentLength];
                    image.InputStream.Read(game.Piccher, 0, image.ContentLength);
                }
                repository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.NameGame);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(game);
            }
        }
        /// <summary>
        /// добавление игры
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("Edit", new BoardGame());
        }
        /// <summary>
        /// удаление
        /// </summary>
        /// <param name="BoardGameId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int BoardGameId)
        {
            BoardGame deletedGame = repository.DeleteGame(BoardGameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedGame.NameGame);
            }
            return RedirectToAction("Index");
        }
    }
}
