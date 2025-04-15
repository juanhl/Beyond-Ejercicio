using Beyond.Domain.Aggregates.TodoList;
using Beyond.Domain.Contracts;
using Beyond.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Create the host configuring the services
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<ITodoListRepository, InMemoryTodoListRepository>();
    })
    .Build();

// Create a scope
using IServiceScope scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

var todoListRepository = services.GetRequiredService<ITodoListRepository>();
var categories = todoListRepository.GetAllCategories();
var todoList = new TodoList();

// Logic to create a Random TodoList
for (var i = 0; i < Random.Shared.Next(2, 4); i++)
{
    var itemId = todoListRepository.GetNextId();
    todoList.AddItem(itemId, $"Title of item {itemId}", $"Description of item {itemId}", categories[Random.Shared.Next(0, categories.Count - 1)]);

    var dateTime = DateTime.Now;
    int totalPercent = 0;
    int count = 0;
    do
    {
        int percent = Random.Shared.Next(1, 40);

        if ((percent + totalPercent) > 100) 
            percent = 100 - totalPercent;

        totalPercent += percent;

        dateTime = dateTime.AddMinutes(Random.Shared.Next(100, 1000));
        todoList.RegisterProgression(itemId, dateTime, percent);

        count++;
    } while (totalPercent < 100 && count < 5);
}

// Print TodoList
todoList.PrintItems();
