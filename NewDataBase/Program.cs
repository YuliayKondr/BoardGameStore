using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBoardGameStore.Entites;
using LibraryBoardGameStore.Concreate;
using LibraryBoardGameStore.WorkImage;
namespace NewDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (EfdbContext context = new EfdbContext("DbStore"))
                {
                    if(!context.Database.Exists())
                    {
                        context.Database.Create();
                        Console.WriteLine("Dadatabase create");
                    }
                    BoardGame game1 = new BoardGame()
                    {
                        NameGame = "Uno",
                        Price = 235M,
                        Category = "Карты",
                        Description = "Колода для игры в Уно содержит карты с цифрами от 0 до 9 четырёх цветов, а также – специальные: «Пропусти ход», «Наоборот», «Возьми две», «Закажи цвет» и «Закажи цвет и возьми четыре».\nКаждому раздаётся по 7 карт, после чего закладывается одна для сброса. Цель игры – первым избавиться от всех карт. Победитель получает очки, согласно картам, оставшимся на руках у остальных.",
                        Piccher = WImage.CreateCopy(@"D:\Программирование\ШАГ Юля\DZ-ASP.Net\Контрольная2\BoardGameStore\NewDataBase\Image\Uno.jpg"),
                        ImageMimeType="jpg"
                    };

                    context.BoardGames.Add(game1);
                    BoardGame game2 = new BoardGame()
                    {
                        Category="Карты",
                        NameGame = "Саботер",
                        Price = 320M,
                        Description = "Игра для 4-10 человек , в не входят: 44 карты туннелей,27 карт действий,28 карт золотых самородков,7 карт золотоискателей,4 карты вредителей,Правила игры",
                        Piccher = WImage.CreateCopy(@"D:\Программирование\ШАГ Юля\DZ-ASP.Net\Контрольная2\BoardGameStore\NewDataBase\Image\saboteur.jpg"),
                        ImageMimeType="jpg"
                    };

                    BoardGame game3 = new BoardGame()
                    {
                        NameGame = "Стальная арена",
                        Category="Фишки",
                        Price = 360M,
                        Description = "Рекомендована обладателям базовой игры и любителям активного взаимодействия между игроками. Каждый раунд в игре предстоит быть готовым к удару противника и самому стараться нанести его. Для тех, кто хочет вновь померяться силами на Стальной Арене с новым арсеналом оружия и приемов. \nИгра для 2-4 человек, в нее входят: 14 новых модулей атаки ,4 карточки способностей, 8 жетонов мин,4 новых тайла местности,8 дополнительных модулей поворота,4 памятки,4 подсказки",
                        Piccher = WImage.CreateCopy(@"D:\Программирование\ШАГ Юля\DZ-ASP.Net\Контрольная2\BoardGameStore\NewDataBase\Image\ksercs.jpg"),
                        ImageMimeType="jpg"
                    };
                    BoardGame game4 = new BoardGame()
                    {
                        Category="Карты и фишки",
                        NameGame = "Metro 2033",
                        Description = "Комплектация: игровое поле,6 карт фракций,6 карт героев, 6 пластиковых фигурок героев,7 карт законов,36 карт угроз(по 18 карт для первого и второго этапов партии),18 карт снаряжения,18 карт заданий,7 боевых карт, жетоны ресурсов: по 35 свиней, грибов и патронов, жетоны фракций: по 13 каждой, жетон первого игрока, жетон раунда, правила игры",
                        Price = 950M,
                        Piccher = WImage.CreateCopy(@"D:\Программирование\ШАГ Юля\DZ-ASP.Net\Контрольная2\BoardGameStore\NewDataBase\Image\metro.jpg"),
                        ImageMimeType="jpg"
                    };
                    BoardGame game5 = new BoardGame()
                    {
                        NameGame = "Цитадели",
                        Category="Карты",
                        Price = 480M,
                        Description = " настольная игра немецкого стиля, разработанная Бруно Файдутти (англ. Bruno Faidutti). Впервые она была опубликована во Франции в 2000 под именем «Citadelles», позже в Германии под именем «Ohne Furcht und Adel» (нем., «Без страха и благородства»). Игра входит в топ-200 лучших настольных игр по рейтингу сайта «BoardGameGeek»\nВ игру могут играть от двух до восьми человек. Цель игры - заработать как можно больше очков за построенные кварталы. Побеждает тот, у кого будет наибольшее количество очков, когда любой из игроков построит восемь кварталов.\nВ комплект входят: правила игры,8 карт персонажей,65 карт городских строений,8 информационных карт для каждого играющего,10 карт персонажей из дополнения \"The Dark City\",14 карт дополнительных сооружений из дополнения \"The Dark City\",1 фишка короны,30 пластиковых фишек монет",
                        Piccher = WImage.CreateCopy(@"D:\Программирование\ШАГ Юля\DZ-ASP.Net\Контрольная2\BoardGameStore\NewDataBase\Image\tsitadeli-delyuks-nastolnaya.jpg"),
                        ImageMimeType="jpg"
                    };
                    context.BoardGames.Add(game2);
                    context.BoardGames.Add(game3);
                    context.BoardGames.Add(game4);
                    context.BoardGames.Add(game5);
                    context.SaveChanges();


                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
