using System;
using System.Collections.Generic;
using System.Linq;
namespace LibraryBoardGameStore.Entites
{
    /// <summary>
    /// класс по работе с корзиной
    /// </summary>
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        /// <summary>
        /// добавление в корзину
        /// </summary>
        /// <param name="game">игра</param>
        /// <param name="quantity">количество</param>
        public void AddItem(BoardGame game, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.BoardGame.BoardGameId == game.BoardGameId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    BoardGame = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        /// <summary>
        /// удаление с корзині
        /// </summary>
        /// <param name="game"></param>
        public void RemoveLine(BoardGame game)
        {
            lineCollection.RemoveAll(l => l.BoardGame.BoardGameId == game.BoardGameId);
        }
        /// <summary>
        /// сумма корзины
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.BoardGame.Price * e.Quantity);

        }
        /// <summary>
        /// очистка корзины
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        
        public BoardGame BoardGame { get; set; }
        public int Quantity { get; set; }

    }
}

