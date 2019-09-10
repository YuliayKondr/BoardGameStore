using System;
using System.Collections.Generic;
using LibraryBoardGameStore.Entites;
using LibraryBoardGameStore.Abstract;
namespace LibraryBoardGameStore.Concreate
{
    /// <summary>
    /// Класс реализующий саму работу с БД
    /// </summary>
    public class EFGameRepository: IBoardGameRepository
    {
        EfdbContext context = new EfdbContext("DbStore");
        public IEnumerable<BoardGame> Games
        {
            get { return context.BoardGames; }
        }
        public void SaveGame(BoardGame game)
        {
            if (game.BoardGameId == 0)
                context.BoardGames.Add(game);
            else
            {
                BoardGame dbEntry = context.BoardGames.Find(game.BoardGameId);
                if (dbEntry != null)
                {
                    dbEntry.NameGame = game.NameGame;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                    dbEntry.Piccher = game.Piccher;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public BoardGame DeleteGame(int BoardGameId)
        {
            BoardGame dbEntry = context.BoardGames.Find(BoardGameId);
            if (dbEntry != null)
            {
                context.BoardGames.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
