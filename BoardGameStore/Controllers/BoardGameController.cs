using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBoardGameStore.Entites;
using LibraryBoardGameStore.Abstract;
using BoardGameStore.Models;
namespace BoardGameStore.Controllers

{
    /// <summary>
    /// первый контроллер (главный)
    /// </summary>
    public class BoardGameController : Controller
    {
        private IBoardGameRepository repository;
        public int pageSize = 3;//3 игры на странице
        public BoardGameController(IBoardGameRepository repo)
        {
            repository = repo;
        }
        /// <summary>
        /// метод действия для вывода всех игр
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ViewResult List(string category, int page=1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = repository.Games.Where(p => category == null || String.Compare(p.Category, category) == 0).OrderBy(game => game.BoardGameId)
                .Skip((page - 1) * pageSize).Take(pageSize),
                GetPagingInfo = new PagingInfo
                {
                    CurrentPage = page,                    
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?repository.Games.Count() :
                        repository.Games.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        /// <summary>
        /// получение картинки с БД
        /// </summary>
        /// <param name="BoardGameId"></param>
        /// <returns></returns>
        public FileContentResult GetImage(int BoardGameId)
        {
            BoardGame game = repository.Games
                .FirstOrDefault(g => g.BoardGameId == BoardGameId);

            if (game != null)
            {
                return File(game.Piccher,game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}