using System;
using System.Collections.Generic;
using LibraryBoardGameStore.Entites;
namespace BoardGameStore.Models
{
    public class GamesListViewModel
    {
        public IEnumerable<BoardGame> Games { get; set; }
        public PagingInfo GetPagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}