using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LibraryBoardGameStore.Abstract;
using LibraryBoardGameStore.Entites;
using BoardGameStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
            {
                new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
                new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
                new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
                new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
                new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<BoardGame> result = ((IEnumerable<BoardGame>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Игра1", result[0].NameGame);
            Assert.AreEqual("Игра2", result[1].NameGame);
            Assert.AreEqual("Игра3", result[2].NameGame);
        }
        [TestMethod]
        public void Can_Edit_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
        new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            BoardGame game1 = controller.Edit(1).ViewData.Model as BoardGame;
            BoardGame game2 = controller.Edit(2).ViewData.Model as BoardGame;
            BoardGame game3 = controller.Edit(3).ViewData.Model as BoardGame;

            // Assert
            Assert.AreEqual(1, game1.BoardGameId);
            Assert.AreEqual(2, game2.BoardGameId);
            Assert.AreEqual(3, game3.BoardGameId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
        new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            BoardGame result = controller.Edit(6).ViewData.Model as BoardGame;

            // Assert
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            BoardGame game = new BoardGame { NameGame = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(game);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveGame(game));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            BoardGame game = new BoardGame { NameGame = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(game);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveGame(It.IsAny<BoardGame>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // Организация - создание объекта Game
            BoardGame game = new BoardGame { BoardGameId = 2, NameGame = "Игра2" };

            // Организация - создание имитированного хранилища данных
            Mock<IBoardGameRepository> mock = new Mock<IBoardGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<BoardGame>
    {
        new BoardGame { BoardGameId = 1, NameGame = "Игра1"},
        new BoardGame { BoardGameId = 2, NameGame = "Игра2"},
        new BoardGame { BoardGameId = 3, NameGame = "Игра3"},
        new BoardGame { BoardGameId = 4, NameGame = "Игра4"},
        new BoardGame { BoardGameId = 5, NameGame = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(game.BoardGameId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Game
            mock.Verify(m => m.DeleteGame(game.BoardGameId));
        }
    }
    
}