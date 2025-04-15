using Beyond.Domain.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Aggregates.TodoList
{
    /// <summary>
    /// Entity for TodoItem
    /// </summary>
    public class TodoItem : IEntity<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; }

        private readonly List<Progression> _progressions = [];

        /// <summary>
        /// List of progressions
        /// </summary>
        public IReadOnlyCollection<Progression> Progressions => _progressions.AsReadOnly();

        /// <summary>
        /// Flag that indicates whether the TodoItem is completed
        /// </summary>
        public bool IsCompleted => TotalPercent >= 100;

        private decimal TotalPercent => _progressions.Sum(x => x.Percent);

        /// <summary>
        /// Initializes a new instance of TodoItem
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="title">title</param>
        /// <param name="description">description</param>
        /// <param name="category">category</param>
        public TodoItem(int id, string title, string description, string category)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
        }

        /// <summary>
        /// Set the description
        /// </summary>
        /// <param name="description">description</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetDescription(string description)
        {
            if (!CanBeUpdatedOrRemoved())
                throw new InvalidOperationException("TodoItem cannot be updated if the total progress is greater than 50.");

            Description = description;
        }

        /// <summary>
        /// Indicates whether the instance can be updated or removed based on the total progress percentage
        /// </summary>
        /// <returns>true if the item can be updated or removed; otherwise, false</returns>
        public bool CanBeUpdatedOrRemoved() => TotalPercent <= 50;

        /// <summary>
        /// Add a progression
        /// </summary>
        /// <param name="progression">progression</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddProgression(Progression progression)
        {
            if (_progressions.Count > 0 && progression.DateTime <= _progressions.Max(x => x.DateTime))
                throw new ArgumentException("The progression date must be later than the last recorded progression date.", nameof(progression));

            if ((progression.Percent + TotalPercent) > 100)
                throw new ArgumentException("Total progression cannot exceed 100%.", nameof(progression));

            _progressions.Add(progression);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Id}) {Title} - {Description} ({Category}) Completed:{IsCompleted}");

            // Iterate progressions already ordered by datetime
            decimal totalPercent = 0;
            foreach (var progression in _progressions)
            {
                totalPercent += progression.Percent;
                int progressLength = (int)Math.Round(totalPercent / 100 * 50);
                string formattedDate = progression.DateTime.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                sb.Append($"\r\n{formattedDate} - {totalPercent}%\t\t |{new string('O', progressLength).PadRight(50)}|");
            }

            return sb.ToString();
        }
    }
}
