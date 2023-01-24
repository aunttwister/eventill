using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Helpers
{
    public static class DateTimeHelpers
    {
        public static string RemoveYear(this DateTime fullDateTime)
        {
            CultureInfo frCulture = new CultureInfo("fr-FR");
            string frDate = frCulture.DateTimeFormat.ShortDatePattern;
            var removeYear = fullDateTime.ToString(frDate).Split('/').SkipLast(1);
            return string.Join('.', removeYear) + " " + fullDateTime.ToString("HH:mm"); ;
        }
    }
}
