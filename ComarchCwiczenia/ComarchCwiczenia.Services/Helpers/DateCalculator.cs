using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Services.Helpers
{
    public class DateCalculator
    {
        public DateTime GetNextBusinessDay(DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            } while (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday);

            return date;
        }
    }
}
