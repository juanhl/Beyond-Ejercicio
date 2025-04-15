using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Contracts
{
    /// <summary>
    /// TodoListRepository interface
    /// </summary>
    public interface ITodoListRepository
    {
        /// <summary>
        /// Get the next TodoItem ID
        /// </summary>
        /// <returns>The next TodoItem ID</returns>
        int GetNextId();

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>The list of categories</returns>
        List<string> GetAllCategories();
    }
}
