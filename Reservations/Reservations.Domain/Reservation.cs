namespace Reservations.Domain
{
    public class Reservation : AuditableEntity
    {
        public Reservation()
        {
            Tickets = new HashSet<Ticket>();
        }
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}