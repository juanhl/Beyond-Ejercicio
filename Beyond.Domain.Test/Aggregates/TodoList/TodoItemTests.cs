using Beyond.Domain.Aggregates.TodoList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Beyond.Domain.Test.Aggregates.TodoList
{
    public class TodoItemTests
    {
        private TodoItem CreateTodoItem() =>
            new TodoItem(1, "Title", "Initial description", "Work");

        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var item = CreateTodoItem();

            Assert.Equal(1, item.Id);
            Assert.Equal("Title", item.Title);
            Assert.Equal("Initial description", item.Description);
            Assert.Equal("Work", item.Category);
            Assert.Empty(item.Progressions);
            Assert.False(item.IsCompleted);
        }

        [Fact]
        public void SetDescription_WhenProgressUnder50_ShouldUpdate()
        {
            var item = CreateTodoItem();
            item.SetDescription("Updated description");

            Assert.Equal("Updated description", item.Description);
        }

        [Fact]
        public void SetDescription_WhenProgressOver50_ShouldThrow()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.UtcNow.AddDays(-1), 60));

            var ex = Assert.Throws<InvalidOperationException>(() => item.SetDescription("New"));
            Assert.Equal("TodoItem cannot be updated if the total progress is greater than 50.", ex.Message);
        }

        [Fact]
        public void CanBeUpdatedOrRemoved_WhenProgressUnder50_ShouldBeTrue()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.UtcNow, 40));

            var isEditable = item.CanBeUpdatedOrRemoved();

            Assert.True(isEditable);
        }


        [Fact]
        public void CanBeUpdatedOrRemoved_WhenProgressOver50_ShouldBeFalse()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.UtcNow, 60));

            var isEditable = item.CanBeUpdatedOrRemoved();

            Assert.False(isEditable);
        }

        [Fact]
        public void AddProgression_ValidProgression_ShouldAdd()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.UtcNow, 40));

            Assert.Single(item.Progressions);
            Assert.False(item.IsCompleted);
        }

        [Fact]
        public void AddProgression_WhenDateNotLater_ShouldThrow()
        {
            var item = CreateTodoItem();
            var now = DateTime.Now;

            item.AddProgression(new Progression(now, 30));

            var ex = Assert.Throws<ArgumentException>(() =>
                item.AddProgression(new Progression(now, 20))
            );

            Assert.Contains("The progression date must be later", ex.Message);
        }

        [Fact]
        public void AddProgression_WhenTotalPercentExceeds100_ShouldThrow()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.Now.AddDays(-2), 70));

            var ex = Assert.Throws<ArgumentException>(() =>
                item.AddProgression(new Progression(DateTime.Now, 40))
            );

            Assert.Contains("Total progression cannot exceed 100%", ex.Message);
        }

        [Fact]
        public void IsCompleted_WhenTotalIs100_ShouldBeTrue()
        {
            var item = CreateTodoItem();
            item.AddProgression(new Progression(DateTime.Now.AddDays(-2), 30));
            item.AddProgression(new Progression(DateTime.Now.AddDays(-1), 70));

            Assert.True(item.IsCompleted);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedProgress()
        {
            var item = new TodoItem(1, "Complete Project Report", "Finish the final report for the project", "Work");
            item.AddProgression(new Progression(new DateTime(2025, 3, 18), 30));
            item.AddProgression(new Progression(new DateTime(2025, 3, 19), 50));
            item.AddProgression(new Progression(new DateTime(2025, 4, 20), 20));

            var output = item.ToString();

            Assert.Equal("1) Complete Project Report - Finish the final report for the project (Work) Completed:True\r\n3/18/2025 12:00:00 AM - 30%\t\t |OOOOOOOOOOOOOOO                                   |\r\n3/19/2025 12:00:00 AM - 80%\t\t |OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO          |\r\n4/20/2025 12:00:00 AM - 100%\t\t |OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO|", output);
        }
    }
}
