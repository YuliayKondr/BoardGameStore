using System;
using System.Collections.Generic;
using LibraryBoardGameStore.Entites;
namespace LibraryBoardGameStore.Abstract
{
    /// <summary>
    /// Интрерфейс для работы с БД
    /// </summary>
    public interface IBoardGameRepository
    {
        IEnumerable<BoardGame> Games { get; }
        void SaveGame(BoardGame game);
        BoardGame DeleteGame(int BoardGameId);
    }
}
