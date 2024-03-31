using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1
{
    public class SeedData
    {
        public static Workday GetFullyInitializedWorkday()
        {
            // Попереднє створення об'єктів, які будуть використовуватися для ініціалізації
            var company = new Company { Id = Guid.NewGuid(), Name = "Company #1", Cinemas = new List<Cinema>(), Users = new List<User>() };
            var cinema = new Cinema { Id = Guid.NewGuid(), Name = "Cinema #1", Adress = "1 street", Company = company, Products = new List<Product>(), Halls = new List<Hall>(), Films = new List<Film>(), WorkDays = new List<Workday>() };
            var hall = new Hall { Id = Guid.NewGuid(), Name = "Hall #1", Cinema = cinema, Seats = new List<HallSeat>(), Sessions = new List<Session>(), IsDeleted = false };
            var film = new Film { Id = Guid.NewGuid(), Name = "Film #1", Duration = TimeSpan.FromHours(2), Cinema = cinema, Sessions = new List<Session>(), IsDeleted = false };
            var session = new Session { Id = Guid.NewGuid(), StartDateTime = DateTime.UtcNow, Hall = hall, Film = film, Tickets = new List<Ticket>() };
            var seat = new HallSeat { Id = Guid.NewGuid(), Row = 1, Column = 1, Status = HallSeatStatus.Available, Hall = hall, Tickets = new List<Ticket>() };
            var ticket = new Ticket { Id = Guid.NewGuid(), CreationTime = DateTime.UtcNow, Status = TicketStatus.Booked, Session = session, Seat = seat };
            var user = new User { Id = Guid.NewGuid(), FisrtName = "John", LastName = "Doe", Company = company, WorkDays = new List<Workday>() };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product #1", Price = 10m, Amount = 100, SoldAmount = 5, IncomeAmount = 50, IsDeleted = false, Cinema = cinema };
            var workday = new Workday { Id = Guid.NewGuid(), StartDateTime = DateTime.UtcNow, Cinema = cinema, User = user, Report = new Report() };

            // Встановлення зворотніх зв'язків та додавання об'єктів до списків
            cinema.Company = company;
            company.Cinemas.Add(cinema);
            company.Users.Add(user);

            cinema.Products.Add(product);
            cinema.Halls.Add(hall);
            cinema.Films.Add(film);
            cinema.WorkDays.Add(workday);

            hall.Cinema = cinema;
            hall.Sessions.Add(session);
            hall.Seats.Add(seat);

            film.Cinema = cinema;
            film.Sessions.Add(session);

            session.Hall = hall;
            session.Film = film;
            session.Tickets.Add(ticket);

            seat.Hall = hall;
            seat.Tickets.Add(ticket);

            ticket.Session = session;
            ticket.Seat = seat;

            workday.Cinema = cinema;
            workday.User = user;
            workday.Report = new Report { Id = Guid.NewGuid(), Status = RepostStatus.NotReviewed, WorkDay = workday };

            user.Company = company;
            user.WorkDays.Add(workday);

            return workday;
        }
        public static Workday GetExtendedInitializedWorkday2()
        {
            var company = new Company { Id = Guid.NewGuid(), Name = "Company #1", Cinemas = new List<Cinema>(), Users = new List<User>() };
            var cinema = new Cinema { Id = Guid.NewGuid(), Name = "Cinema #1", Adress = "Address 1", Company = company, Products = new List<Product>(), Halls = new List<Hall>(), Films = new List<Film>(), WorkDays = new List<Workday>() };
            company.Cinemas.Add(cinema);


            var products = new List<Product> {
                new Product { Id = Guid.NewGuid(), Cinema = cinema, Name = "Beer", Price = 60, Amount = 5, SoldAmount = 4, IncomeAmount = 10, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Cinema = cinema, Name = "Super Ham and Cheese Sandwich XXXL + Double Extra Meat", Price = 150, Amount = 3, SoldAmount = 8, IncomeAmount = 15, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Cinema = cinema, Name = "Popcorn", Price = 100, Amount = 6, SoldAmount = 7, IncomeAmount = 5, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Cinema = cinema, Name = "Cola", Price = 25, Amount = 6, SoldAmount = 15, IncomeAmount = 20, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Cinema = cinema, Name = "Hot Dog", Price = 125, Amount = 6, SoldAmount = 3, IncomeAmount = 0, IsDeleted = false }
            };
            foreach (var product in products)
                cinema.Products.Add(product);

            var films = new List<Film>
            {
                new Film { Id = Guid.NewGuid(), Name = "The Shawshank Redemption", Duration = TimeSpan.FromHours(2), Cinema = cinema, Sessions = new List<Session>(), IsDeleted = false },
                new Film { Id = Guid.NewGuid(), Name = "Fight Club", Duration = TimeSpan.FromHours(1.5), Cinema = cinema, Sessions = new List<Session>(), IsDeleted = false }
            };
            foreach (var film in films)
                cinema.Films.Add(film);

            var halls = new List<Hall>
            {
                new Hall { Id = Guid.NewGuid(), Name = "Hall #1", Cinema = cinema, Seats = new List<HallSeat>(), Sessions = new List<Session>(), IsDeleted = false },
                new Hall { Id = Guid.NewGuid(), Name = "Hall #2", Cinema = cinema, Seats = new List<HallSeat>(), Sessions = new List<Session>(), IsDeleted = false }
            };
            foreach (var hall in halls)
                cinema.Halls.Add(hall);

            var sessions = new List<Session>
            {
                new Session { Id = Guid.NewGuid(), StartDateTime = DateTime.UtcNow, Hall = halls[0], Film = films[0], Tickets = new List<Ticket>(), Price = 125 },
                new Session { Id = Guid.NewGuid(), StartDateTime = DateTime.UtcNow.AddHours(3), Hall = halls[1], Film = films[1], Tickets = new List<Ticket>(), Price = 145 }
            };
            halls[0].Sessions.Add(sessions[0]);
            halls[1].Sessions.Add(sessions[1]);
            films[0].Sessions.Add(sessions[0]);
            films[1].Sessions.Add(sessions[1]);

            for (int i = 0; i < halls.Count; i++)
            {
                for (int row = 0; row < 2; row++)
                {
                    for (int col = 0; col < 2; col++)
                    {
                        var seat = new HallSeat { Id = Guid.NewGuid(), Hall = halls[i], Row = row, Column = col, Status = HallSeatStatus.Available, Tickets = new List<Ticket>() };
                        halls[i].Seats.Add(seat);

                        var ticket = new Ticket {
                            Id = Guid.NewGuid(),
                            CreationTime = DateTime.UtcNow,
                            Status = new Random().Next(0, 2) == 0 ? TicketStatus.Sold : TicketStatus.Booked,
                            Session = sessions[i],
                            Seat = seat
                        };
                        sessions[i].Tickets.Add(ticket);
                        seat.Tickets.Add(ticket);
                    }
                }
            }

            var user = new User { Id = Guid.NewGuid(), FisrtName = "Dmytro", LastName = "Kotenko", Company = company, WorkDays = new List<Workday>() };
            company.Users.Add(user);

            var workday = new Workday { Id = Guid.NewGuid(), StartDateTime = DateTime.UtcNow, EndDateTime = DateTime.UtcNow.AddHours(8.2), Cinema = cinema, User = user };
            user.WorkDays.Add(workday);
            cinema.WorkDays.Add(workday);

            return workday;
        }
    }
}
