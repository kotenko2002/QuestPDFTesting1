using System;

namespace QuestPDFTesting1.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
    public class Workday : BaseEntity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Report Report { get; set; }
    }
    public class User : BaseEntity
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsFired { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Workday> WorkDays { get; set; }
    }
    public enum TicketStatus
    {
        Booked = 3,
        Sold = 4
    }
    public class Ticket : BaseEntity
    {
        public DateTime CreationTime { get; set; }
        public TicketStatus Status { get; set; }

        public Guid SessionId { get; set; }
        public virtual Session Session { get; set; }
        public Guid SeatId { get; set; }
        public virtual HallSeat Seat { get; set; }
    }
    public class Session : BaseEntity
    {
        public DateTime StartDateTime { get; set; }
        public decimal Price { get; set; }

        public Guid HallId { get; set; }
        public virtual Hall Hall { get; set; }
        public Guid FilmId { get; set; }
        public virtual Film Film { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
    public enum RepostStatus
    {
        NotReviewed = 1,
        Approved = 2,
        Rejected = 3
    }
    public class Report : BaseEntity
    {
        public RepostStatus Status { get; set; }

        public Guid WorkDayId { get; set; }
        public virtual Workday WorkDay { get; set; }
    }
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Amount { get; set; }
        public int SoldAmount { get; set; }
        public int IncomeAmount { get; set; }

        public bool IsDeleted { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
    public enum HallSeatStatus
    {
        NotExists = 0,
        Available = 1,
        Unavailable = 2
    }
    public class HallSeat : BaseEntity
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public HallSeatStatus Status { get; set; }

        public Guid HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
    public class Hall : BaseEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }

        public virtual ICollection<HallSeat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
    public class Film : BaseEntity
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }

        public bool IsDeleted { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
    public class Cinema : BaseEntity
    {
        public string Name { get; set; }
        public string Adress { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Workday> WorkDays { get; set; }
    }
}
