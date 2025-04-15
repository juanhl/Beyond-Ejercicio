using Beyond.Domain.Aggregates.TodoList;
using Beyond.Domain.Contracts;
using Beyond.Infrastructure.Repositories;
using Beyond.Web.Server.Request;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Web.Server.Controllers
{
    /// <summary>
    /// Controller for TodoList endpoints
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        /// <summary>
        /// Single instance for all application. (This is only for testing purposes)
        /// </summary>
        private static readonly TodoList _todoListInstance = new();

        /// <summary>
        /// Respository
        /// </summary>
        private readonly ITodoListRepository _todoListRepository;

        /// <summary>
        /// Initializes a new instance of TodoListController
        /// </summary>
        /// <param name="todoListRepository">todoListRepository</param>
        public TodoListController(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        // GET /todolist/items
        [HttpGet("items")]
        public IActionResult GetItems()
        {
            return Ok(_todoListInstance.Items);
        }

        // GET /todolist/categories
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _todoListRepository.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /todolist/items
        [HttpPost("items")]
        public IActionResult AddItem([FromBody] AddItemRequest request)
        {
            try
            {
                // Checks whether category is valid
                var categories = _todoListRepository.GetAllCategories();
                if (!categories.Contains(request.Category))
                {
                    throw new ArgumentException("Category is not valid.", nameof(request.Category));
                }

                var itemId = _todoListRepository.GetNextId();
                _todoListInstance.AddItem(itemId, request.Title, request.Description, request.Category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /todolist/items/{id}
        [HttpPut("items/{id}")]
        public IActionResult UpdateItem(int id, [FromBody] UpdateItemRequest request)
        {
            try
            {
                _todoListInstance.UpdateItem(id, request.Description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /todolist/items/{id}
        [HttpDelete("items/{id}")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                _todoListInstance.RemoveItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /todolist/items/{id}/progress
        [HttpPost("items/{id}/progress/")]
        public IActionResult AddProgression(int id, [FromBody] AddProgressionRequest request)
        {
            try
            {
                _todoListInstance.RegisterProgression(id, DateTime.UtcNow, request.Percent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
