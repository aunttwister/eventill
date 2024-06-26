﻿namespace Reservations.Domain
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
        public long EventOccurrenceId { get; set; }
        public EventOccurrence EventOccurrence { get; set; }
        public bool PaymentCompleted { get; set; }
    }
}