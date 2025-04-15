using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Contracts
{
    /// <summary>
    /// TodoList interface
    /// </summary>
    public interface ITodoList
    {
        /// <summary>
        /// Add an item to the list.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="title">title</param>
        /// <param name="description">description</param>
        /// <param name="category">category</param>
        void AddItem(int id, string title, string description, string category);

        /// <summary>
        /// Update an item from the list.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="description">description</param>
        void UpdateItem(int id, string description);

        /// <summary>
        /// Remove an item from the list.
        /// </summary>
        /// <param name="id">id</param>
        void RemoveItem(int id);

        /// <summary>
        /// Register a progression of an item from the list.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dateTime">dateTime</param>
        /// <param name="percent">percent</param>
        void RegisterProgression(int id, DateTime dateTime, decimal percent);

        /// <summary>
        /// Print the items
        /// </summary>
        void PrintItems();
    }
}
