using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryBoardGameStore.Entites
{
    public class BoardGame
    {
        [Key]
        public int BoardGameId { get; set; }
        [Display(Name = "Название")]
        [MaxLength(100,ErrorMessage ="Превышение допустимой длины")]
        [Required(ErrorMessage = "Пожалуйста, введите название игры")]
        public string NameGame { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для игры")]
        public string Description { get; set; }
        [Display(Name = "Цена (грн)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }
        public byte[] Piccher { get; set; }        
        [Display(Name = "Категория")]
        [MaxLength(15,ErrorMessage ="Превышение допустимой длины")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для игры")]
        public string Category { get; set; }
        [MaxLength(20)]
        public string ImageMimeType { get; set; }

    }
}
