using Beyond.Domain.Aggregates.TodoList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Beyond.Domain.Test.Aggregates.TodoList
{
    public class TodoListTests
    {
        [Fact]
        public void AddItem_WithNewItem_ShouldAdd()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Test Desc", "Work");

            Assert.Single(todoList.Items);
            Assert.Equal("Test", todoList.Items.First().Title);
        }

        [Fact]
        public void AddItem_WithDuplicatedItem_ShouldThrow()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Test Desc", "Work");

            var ex = Assert.Throws<ArgumentException>(() =>
                todoList.AddItem(1, "Another", "Desc", "Work")
            );

            Assert.Contains("Already exists an item with id", ex.Message);
        }

        [Fact]
        public void UpdateItem_WithValidId_ShouldUpdate()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Initial", "Work");
            todoList.UpdateItem(1, "Updated Desc");

            Assert.Equal("Updated Desc", todoList.Items.First().Description);
        }

        [Fact]
        public void UpdateItem_WithInvalidId_ShouldThrow()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();

            var ex = Assert.Throws<ArgumentException>(() =>
                todoList.UpdateItem(99, "Update")
            );

            Assert.Contains("No items with id", ex.Message);
        }

        [Fact]
        public void RemoveItem_WithValidId_ShouldRemove()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Desc", "Work");

            todoList.RemoveItem(1);
            Assert.Empty(todoList.Items);
        }

        [Fact]
        public void RemoveItem_WithInvalidValidId_ShouldThrow()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();

            var ex = Assert.Throws<ArgumentException>(() =>
                todoList.RemoveItem(99)
            );

            Assert.Contains("No items with id", ex.Message);
        }

        [Fact]
        public void RemoveItem_WhenProgressOver50_ShouldThrow()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Desc", "Work");

            todoList.RegisterProgression(1, new DateTime(2025, 4, 1), 60);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                todoList.RemoveItem(1)
            );

            Assert.Equal("The item cannot be removed.", ex.Message);
        }

        [Fact]
        public void RegisterProgression_WithValidId_ShouldAdd()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();
            todoList.AddItem(1, "Test", "Desc", "Work");
            todoList.RegisterProgression(1, new DateTime(2025, 4, 1), 30);

            var item = todoList.Items.First();
            Assert.Single(item.Progressions);
        }

        [Fact]
        public void RegisterProgression_WithInvalidId_ShouldThrow()
        {
            var todoList = new Domain.Aggregates.TodoList.TodoList();

            var ex = Assert.Throws<ArgumentException>(() =>
                todoList.RegisterProgression(99, DateTime.Now, 10)
            );

            Assert.Contains("No items with id", ex.Message);
        }
    }
}
