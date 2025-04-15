using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Aggregates.TodoList
{
    /// <summary>
    /// Value object for Progression
    /// </summary>
    public struct Progression
    {
        /// <summary>
        /// DateTime of the progression
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Percent of the progression
        /// </summary>
        public decimal Percent { get; }

        /// <summary>
        /// Initializes a new instance of Progression
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        /// <param name="percent">percent</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Progression(DateTime dateTime, decimal percent)
        {
            if (percent <= 0 || percent > 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "The percent must be greater than 0 and less than 100.");

            DateTime = dateTime;
            Percent = percent;
        }
    }
}
