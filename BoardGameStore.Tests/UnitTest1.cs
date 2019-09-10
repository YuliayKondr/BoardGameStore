using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGameStore.Controllers;
using LibraryBoardGameStore.Abstract;
using LibraryBoardGameStore.Entites;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using BoardGameStore.Models;
using BoardGameStore.HtmlHelpers;
namespace BoardGameStore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paging()
        {
            // Организация (arrange)
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
            {
                new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
                new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
                new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
                new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
                new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
            });
            BoardGameController controller = new BoardGameController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            GamesListViewModel result = (GamesListViewModel)controller.List(null,2).Model;

            // Утверждение (assert)
            List<BoardGame> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].NameGame, "Игра4");
            Assert.AreEqual(games[1].NameGame, "Игра5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
        new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
    });
            BoardGameController controller = new BoardGameController(mock.Object);
            controller.pageSize = 3;

            // Act
            GamesListViewModel result
                = (GamesListViewModel)controller.List(null,2).Model;

            // Assert
            PagingInfo pageInfo = result.GetPagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Games()
        {
            // Организация (arrange)
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1",Category="C1" },

        new BoardGame { BoardGameId = 2, NameGame = "Игра2",Category="C2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3",Category="C1"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4",Category="C2"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5",Category="C3"}
    });
            BoardGameController controller = new BoardGameController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<BoardGame> result = ((GamesListViewModel)controller.List("C2", 1).Model)
                .Games.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].NameGame == "Игра2" && result[0].Category == "C2");
            Assert.IsTrue(result[1].NameGame == "Игра4" && result[1].Category == "C2");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1",Category="C1" },

        new BoardGame { BoardGameId = 2, NameGame = "Игра2",Category="C2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3",Category="C1"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4",Category="C2"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5",Category="C3"}
    });
            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "C1");
            Assert.AreEqual(results[1], "C2");
            Assert.AreEqual(results[2], "C3");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new BoardGame[] {
            new BoardGame { BoardGameId = 1, NameGame = "Игра1", Category="Симулятор"},
            new BoardGame { BoardGameId = 2, NameGame = "Игра2", Category="Шутер"}});

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.Cat;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>{
                new BoardGame { BoardGameId = 1, NameGame = "Игра1", Category="Cat1"},
                new BoardGame { BoardGameId = 2, NameGame = "Игра2", Category="Cat2"},
                new BoardGame { BoardGameId = 3, NameGame = "Игра3", Category="Cat1"},
                new BoardGame { BoardGameId = 4, NameGame = "Игра4", Category="Cat2"},
                new BoardGame { BoardGameId = 5, NameGame = "Игра5", Category="Cat3"}});
                BoardGameController controller = new BoardGameController(mock.Object);
                controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((GamesListViewModel)controller.List("Cat1").Model).GetPagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Cat2").Model).GetPagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Cat3").Model).GetPagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).GetPagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
        

    }
}
