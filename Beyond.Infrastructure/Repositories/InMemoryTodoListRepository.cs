using Beyond.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Infrastructure.Repositories
{
    /// <summary>
    /// InMemoryTodoList Repository
    /// </summary>
    public class InMemoryTodoListRepository : ITodoListRepository
    {
        private static readonly List<string> _categories = new(["Work", "Entertainment", "Travel", "Family"]);

        private static int _sequence = 1;

        /// <inheritdoc/>
        public List<string> GetAllCategories()
        {
            return _categories.ToList();
        }
        
        /// <inheritdoc/>
        public int GetNextId()
        {
            return _sequence++;
        }
    }
}
