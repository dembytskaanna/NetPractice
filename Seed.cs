using Cinema.Data;
using Cinema.Models;

namespace Cinema
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Halls.Any())
            {
                var halls = new List<Hall>
                {
                    new Hall { HallId = 1, Name = "Зал 1", Capacity = 150 },
                    new Hall { HallId = 2, Name = "Зал 2", Capacity = 200 },
                    new Hall { HallId = 3, Name = "Зал 3", Capacity = 120 },
                    new Hall { HallId = 4, Name = "Зал 4", Capacity = 180 },
                    new Hall { HallId = 5, Name = "Зал 5", Capacity = 250 },
                    new Hall { HallId = 6, Name = "Зал 6", Capacity = 100 },
                    new Hall { HallId = 7, Name = "Зал 7", Capacity = 160 }
                };

                dataContext.Halls.AddRange(halls);
                dataContext.SaveChanges();
            }

            if (!dataContext.Films.Any())
            {
                var films = new List<Film>
                {
                    new Film
                    {
                        FilmId = 1,
                        Title = "Каскадер",
                        Description = "Колт — каскадер і, як і кожному каскадеру, йому доводиться виконувати складні трюки, стрибати з найвищої висоти та постійно ризикувати життям для ефектних кіносцен. Переживши нещасний випадок на майданчику, який майже знищив його кар’єру, Колту знову доведеться повернутися до роботи. Він повинен розшукати зниклу кінозірку, розгадати підлу змову та ще й повернути кохання усього життя. Хіба щось може піти не так?",
                        Duration = 114,
                        Genre = "комедія, екшн",
                        Rating = 7.4f,
                        Trailer = "https://youtu.be/nAgv9APE2Iw?si=D5tdzYXrDjSoUBF6",
                        Cast = "Раян Ґослінг, Емілі Блант, Аарон Тейлор-Джонс"
                    },
                    new Film
                    {
                        FilmId = 2,
                        Title = "Ебіґейл",
                        Description = "Після викрадення 12-річної доньки впливової кримінальної фігури викрадачі намагаються отримати викуп у розмірі 50 мільйонів доларів. Усе, що їм потрібно, — простежити за дитиною протягом ночі. Проте замкнені в будинку грабіжники починають таємниче зникати. На свій жах вони виявляють, що залишилися сам-на-сам не зі звичайною дівчинкою…",
                        Duration = 90,
                        Genre = "трилер, жахи",
                        Rating = 7.1f,
                        Trailer = "https://youtu.be/jSdoJ5-PKx4",
                        Cast = "Мелісса Баррера, Ден Стівенс, Кетрін Ньютон"
                    },
                    new Film
                    {
                        FilmId = 3,
                        Title = "Манкімен",
                        Description = "Жорстокі вулиці Мумбаї зробили з нього бійця, який не зупиниться ні перед чим, щоб помститися за свою сім'ю. Відтепер його життя ділиться між підпільними боями за гроші, де люди в масках б'ються до останнього вдиху, і роботою в елітному нічному клубі для найбагатших і найпорочніших. Все більше занурюючись у таємний світ, він зводить рахунки з кримінальними авторитетами та корумпованими політиками, які забирають у людей все.",
                        Duration = 113,
                        Genre = "бойовик, кримінал, трилер",
                        Rating = 7.0f,
                        Trailer = "https://youtu.be/635PCMfvjHo",
                        Cast = "Дев Патель, Шарлто Коплі, Собхіта Дхуліпала"
                    },
                    new Film
                    {
                        FilmId = 4,
                        Title = "Пухнасті сепергерої",
                        Description = "Злий технічний геній загрожує перетворити місто на симуляцію метавсесвіту.",
                        Duration = 81,
                        Genre = "анімація, пригоди",
                        Rating = 5.2f,
                        Trailer = "https://youtu.be/eNermFuJ448",
                        Cast = ""
                    },
                    new Film
                    {
                        FilmId = 5,
                        Title = "Омен: початок",
                        Description = "Молоду американку відправляють до Риму, щоб розпочати служіння церкви, але вона стикається з темрявою, яка змушує її засумніватися у своїй вірі, та розкриває жахливу змову, яка може викликати народження втілення зла.",
                        Duration = 120,
                        Genre = "жахи",
                        Rating = 6.8f,
                        Trailer = "https://youtu.be/VMiz44tOs0I",
                        Cast = "Нелл Тайґер Фрі, Ніколь Сорас, Ралф Айнесон"
                    },
                    new Film
                    {
                        FilmId = 6,
                        Title = "Ґоділла та Конґ",
                        Description = "Епічна битва триває! Кінематографічний Монстерверс від Legendary Pictures розказує про події після вибухового протистояння у стрічці «Ґодзілла проти Конґа». Цього разу нова небачена сила протистоїть могутньому Конґу та безстрашному Ґодзіллі і загрожує їхньому існуванню та…існуванню світу. «Ґодзілла та Конґ: Нова імперія» заглибиться в історію походження цих Титанів, таємниці Острова Черепа, а також міфічну битву, яка допомогла створити цих неймовірних істот і повʼязала їх навіки із людством.",
                        Duration = 120,
                        Genre = "Drama",
                        Rating = 6.5f,
                        Trailer = "https://youtu.be/qdOAhIeX79Q",
                        Cast = "Ребекка Голл, Ден Стівенс, Браян Тайрі Генрі"
                    },
                    new Film
                    {
                        FilmId = 7,
                        Title = "Пробивний чувак",
                        Description = "Рейтинги шоу, де люди вбивають одне одного у найвигадливіші способи, стрімко злетіли до небес! Однак організатори цього проєкту перейшли межу, коли вбили родину 9-річного хлопчика. Тепер він готовий на все заради помсти. Його план доволі простий. Спочатку вирости і стати сильнішим. Потім пройти тренування у божевільного майстра бойових мистецтв. І, нарешті, грандіозно повернутися на криваве шоу, щоб влаштувати ефектну розплату!",
                        Duration = 115,
                        Genre = "Drama",
                        Rating = 6.7f,
                        Trailer = "https://youtu.be/vULvPRU2W1g",
                        Cast = "Білл Скашґорд, Фамке Янссен, Джессіка Рот"
                    }
                };

                dataContext.Films.AddRange(films);
                dataContext.SaveChanges();
            }

            if (!dataContext.Screenings.Any())
            {
                var screenings = new List<Screening>
                {
                    new Screening
                    {
                        ScreeningId = 1,
                        StartTime = new DateTime(2023, 5, 1, 18, 0, 0),
                        EndTime = new DateTime(2023, 7, 2, 20, 22, 0),
                        Date = new DateTime(2024, 5, 1),
                        TicketPrice = 120,
                        FilmId = 1,
                        HallId = 1
                    },
                    new Screening
                    {
                        ScreeningId = 2,
                        StartTime = new DateTime(2023, 6, 2, 20, 30, 0),
                        EndTime = new DateTime(2023, 9, 30, 23, 35, 0),
                        Date = new DateTime(2024, 6, 2),
                        TicketPrice = 140,
                        FilmId = 2,
                        HallId = 2
                    },
                    new Screening
                    {
                        ScreeningId = 3,
                        StartTime = new DateTime(2023, 6, 3, 15, 0, 0),
                        EndTime = new DateTime(2023, 10, 21, 17, 32, 0),
                        Date = new DateTime(2024, 6, 3),
                        TicketPrice = 100,
                        FilmId = 3,
                        HallId = 3
                    },
                    new Screening
                    {
                        ScreeningId = 4,
                        StartTime = new DateTime(2023, 6, 4, 19, 0, 0),
                        EndTime = new DateTime(2023, 8, 19, 21, 34, 0),
                        Date = new DateTime(2024, 6, 4),
                        TicketPrice = 120,
                        FilmId = 4,
                        HallId = 4
                    },
                    new Screening
                    {
                        ScreeningId = 5,
                        StartTime = new DateTime(2023, 7, 5, 21, 0, 0),
                        EndTime = new DateTime(2023, 11, 28, 23, 28, 0),
                        Date = new DateTime(2024, 7, 5),
                        TicketPrice = 140,
                        FilmId = 5,
                        HallId = 1
                    },
                    new Screening
                    {
                        ScreeningId = 6,
                        StartTime = new DateTime(2023, 7, 6, 16, 0, 0),
                        EndTime = new DateTime(2023, 11, 28, 18, 22, 0),
                        Date = new DateTime(2024, 7, 6),
                        TicketPrice = 110,
                        FilmId = 6,
                        HallId = 2
                    },
                    new Screening
                    {
                        ScreeningId = 7,
                        StartTime = new DateTime(2023, 7, 7, 18, 30, 0),
                        EndTime = new DateTime(2023, 10, 23, 20, 49, 0),
                        Date = new DateTime(2024, 7, 7),
                        TicketPrice = 130,
                        FilmId = 7,
                        HallId = 3
                    }
                };

                dataContext.Screenings.AddRange(screenings);
                dataContext.SaveChanges();
            }

            if (!dataContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User { UserId = 1, FirstName = "Іван", LastName = "Петренко", Email = "ivan.petrenko@email.com", Password = "password123" },
                    new User { UserId = 2, FirstName = "Ольга", LastName = "Корольова", Email = "olga.korolova@email.com", Password = "qwerty123" },
                    new User { UserId = 3, FirstName = "Максим", LastName = "Сидоренко", Email = "maksym.sydorenko@email.com", Password = "securepass" },
                    new User { UserId = 4, FirstName = "Анна", LastName = "Кравчук", Email = "anna.kravchuk@email.com", Password = "pass456" },
                    new User { UserId = 5, FirstName = "Дмитро", LastName = "Ковальчук", Email = "dmytro.kovalchuk@email.com", Password = "mypassword" },
                    new User { UserId = 6, FirstName = "Софія", LastName = "Бондаренко", Email = "sofia.bondarenko@email.com", Password = "password789" },
                    new User { UserId = 7, FirstName = "Андрій", LastName = "Мельник", Email = "andrii.melnyk@email.com", Password = "pass123xyz" }
                };

                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();
            }

            if (!dataContext.Bookings.Any())
            {
                var bookings = new List<Booking>
                {
                    new Booking { BookingId = 1, Date = new DateTime(2023, 5, 1), UserId = 1, ScreeningId = 1, TicketCount = 2 },
                    new Booking { BookingId = 2, Date = new DateTime(2023, 6, 2), UserId = 2, ScreeningId = 2, TicketCount = 3 },
                    new Booking { BookingId = 3, Date = new DateTime(2023, 6, 3), UserId = 3, ScreeningId = 3, TicketCount = 1 },
                    new Booking { BookingId = 4, Date = new DateTime(2023, 6, 4), UserId = 4, ScreeningId = 4, TicketCount = 4 },
                    new Booking { BookingId = 5, Date = new DateTime(2023, 7, 5), UserId = 5, ScreeningId = 5, TicketCount = 2 },
                    new Booking { BookingId = 6, Date = new DateTime(2023, 7, 6), UserId = 6, ScreeningId = 6, TicketCount = 1 },
                    new Booking { BookingId = 7, Date = new DateTime(2023, 7, 7), UserId = 7, ScreeningId = 7, TicketCount = 3 }
                };

                dataContext.Bookings.AddRange(bookings);
                dataContext.SaveChanges();
            }
        }
    }
}
