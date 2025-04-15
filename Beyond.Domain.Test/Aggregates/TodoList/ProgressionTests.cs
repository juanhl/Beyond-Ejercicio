using Beyond.Domain.Aggregates.TodoList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Beyond.Domain.Test.Aggregates.TodoList
{
    public class ProgressionTests
    {
        [Fact]
        public void Constructor_WithValidValues_ShouldCreateProgression()
        {
            var date = new DateTime(2025, 4, 15);
            var percent = 50m;

            var progression = new Progression(date, percent);

            Assert.Equal(date, progression.DateTime);
            Assert.Equal(percent, progression.Percent);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(100.01)]
        [InlineData(150)]
        public void Constructor_WithInvalidPercent_ShouldThrowArgumentOutOfRangeException(decimal invalidPercent)
        {
            var date = DateTime.UtcNow;

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Progression(date, invalidPercent));

            Assert.Contains("The percent must be greater than 0 and less than 100", ex.Message);
        }
    }
}
