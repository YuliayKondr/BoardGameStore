using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBoardGameStore.Abstract;
using LibraryBoardGameStore.Entites;
using BoardGameStore.Models;
namespace BoardGameStore.Controllers
{
    /// <summary>
    /// класс-контроллекр корзина
    /// </summary>
    public class CartController : Controller
    {
        // GET: Cart
        private IBoardGameRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IBoardGameRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        /// <summary>
        /// метод действия перехода в корзину
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        /// <summary>
        /// добавление 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="BoardGameId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart,int BoardGameId, string returnUrl)
        {
            BoardGame game = repository.Games
                .FirstOrDefault(g => g.BoardGameId == BoardGameId);

            if (game != null)
            {
                cart.AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /// <summary>
        /// удаление с корзины
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="BoardGameId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart,int BoardGameId, string returnUrl)
        {
            BoardGame game = repository.Games
                .FirstOrDefault(g => g.BoardGameId == BoardGameId);

            if (game != null)
            {
                cart.RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /// <summary>
        /// возвращает частичное представление суммы карзины
        /// для отображения на главной страннице
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        [HttpGet]
        public ViewResult Checkout(Cart cart)
        {
            return View(new ShippingDetails());
        }
        /// <summary>
        /// отправка заказа
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }


    }
}
