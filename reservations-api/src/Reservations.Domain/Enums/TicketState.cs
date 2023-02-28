using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain.Enums
{
    public enum TicketState
    {
        Available, Reserved, Unavailable, Sold
    }
    public static class TicketStateExtensions
    {
        public static TicketState? StringToTicketState(this string value)
        {
            string lowerCase = value.ToLower();
            switch(lowerCase)
            {
                case "available":
                    return TicketState.Available;
                case "unavailable":
                    return TicketState.Unavailable;
                case "reserved":
                    return TicketState.Reserved;
                case "sold":
                    return TicketState.Sold;
                default:
                    return null;
            }
        }
    }
}
