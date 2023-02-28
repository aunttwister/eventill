using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Services
{
    public class TicketService : ITicketService
    {
        public void ResetTicketState(List<Ticket> tickets, TicketState ticketState)
        {
            if (!tickets.Any())
                throw new NotFoundException(nameof(Ticket), "multiple");

            switch (ticketState)
            {
                case TicketState.Available:
                    tickets.ForEach(t => 
                    {
                        t.TicketState = ticketState;
                        t.ReservationId = null;
                        t.Reservation = null;
                    });
                    break;
                case TicketState.Reserved:
                    tickets.ForEach(t => t.TicketState = ticketState);
                    break;
                case TicketState.Unavailable:
                    tickets.ForEach(t =>
                    {
                        t.TicketState = ticketState;
                        t.ReservationId = null;
                        t.Reservation = null;
                    });
                    break;
                case TicketState.Sold:
                    tickets.ForEach(t => t.TicketState = ticketState);
                    break;
                default:
                    break;
            }
        }
    }
}
