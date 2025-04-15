using Beyond.Domain.Base;
using Beyond.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Aggregates.TodoList
{
    /// <summary>
    /// Aggregate for TodoList
    /// </summary>
    public class TodoList : ITodoList, IEntity<Guid>
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; }

        private readonly Dictionary<int, TodoItem> _items = new();

        /// <summary>
        /// List of todoItems, ordered by their ID
        /// </summary>
        public IReadOnlyCollection<TodoItem> Items => _items.Values.OrderBy(x => x.Id).ToList().AsReadOnly();

        /// <summary>
        /// Initializes a new instance of TodoList
        /// </summary>
        public TodoList()
        {
            Id = Guid.NewGuid();
        }

        /// <inheritdoc/>
        public void AddItem(int id, string title, string description, string category)
        {
            if (_items.ContainsKey(id))
                throw new ArgumentException($"Already exists an item with id {id}.");

            _items[id] = new TodoItem(id, title, description, category);
        }

        /// <inheritdoc/>
        public void UpdateItem(int id, string description)
        {
            if (!_items.ContainsKey(id))
                throw new ArgumentException($"No items with id {id}.");

            _items[id].SetDescription(description);
        }

        /// <inheritdoc/>
        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ArgumentException($"No items with id {id}.");

            if (!_items[id].CanBeUpdatedOrRemoved())
                throw new InvalidOperationException("The item cannot be removed.");

            _items.Remove(id);
        }

        /// <inheritdoc/>
        public void RegisterProgression(int id, DateTime dateTime, decimal percent)
        {
            if (!_items.ContainsKey(id))
                throw new ArgumentException($"No items with id {id}.");

            _items[id].AddProgression(new Progression(dateTime, percent));
        }

        /// <inheritdoc/>
        public void PrintItems()
        {
            foreach (var item in Items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
