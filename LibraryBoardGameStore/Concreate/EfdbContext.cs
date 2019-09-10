using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBoardGameStore.Entites;
using System.Data.Entity;
namespace LibraryBoardGameStore.Concreate
{
    public class EfdbContext : DbContext
    {
        public EfdbContext(string conn) : base(conn) { }
        public DbSet<BoardGame> BoardGames {get;set;}
       // public DbSet<CartLine> Carts { get; set; }
        //public DbSet<ShippingDetails> ShippingDetails { get; set; }
    }
}
