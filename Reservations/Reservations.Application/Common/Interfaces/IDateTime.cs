using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Interfaces
{
    public interface IDateTime
    {
        public DateTime UtcNow { get; }
        public DateTime Today { get; }
        public DateTime Tomorrow { get; }

        public string DateTimeOffset => new DateTimeOffset(UtcNow).ToUnixTimeSeconds().ToString();
    }
}
