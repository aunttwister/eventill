using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Interfaces
{
    public interface IEventSetupService
    {
        public List<Ticket> InitializeTickets(int ticketCount, decimal ticketPrice);
        public Task<List<Question>> FilterQuestionsAsync(
            IEnumerable<Question> questions, 
            CancellationToken cancellationToken);
        public Task<List<EventOccurrence>> FilterEventOccurrencesAsync(
            IEnumerable<EventOccurrence> eventOccurrences,
            CancellationToken cancellationToken);
    }
}
