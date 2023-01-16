using Microsoft.AspNetCore.Mvc;
using WebAPIDay01.Models;
using WebAPIDay01.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIDay01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly TodoContext _todoContext;

        public ToDoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public IActionResult GetAllToDos()
        {
            ToDoRepository toDoRepository = new ToDoRepository(_todoContext);
            var todos = toDoRepository.GetAllToDos();

            return Ok(todos);
        }

        [HttpGet("{toDoId}")]
        public IActionResult GetToDo(int toDoId)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_todoContext);

            var todo = toDoRepository.GetToDo(toDoId);

            if(todo == null)
            {
                return NotFound("Id is not found");
            }

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult CreateToDo(ToDo createdToDo)
        {
            if (createdToDo == null)
            {
                return BadRequest(createdToDo);
            }
            ToDoRepository toDoRepository = new ToDoRepository(_todoContext);
            toDoRepository.AddToDo(createdToDo);
            return Ok(createdToDo);
        }

        [HttpPut]
        public IActionResult UpdateToDo(ToDo updatedToDo)
        {
          
            ToDoRepository toDoRepository = new ToDoRepository(_todoContext);

           
            bool validateUpdatedToDo = toDoRepository.UpdateToDo(updatedToDo);

            if (!validateUpdatedToDo)
            {
                return BadRequest("Something went wrong updating to do list");
            }

            var todo = toDoRepository.GetToDo(updatedToDo.Id);


            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_todoContext);


            bool validatedToDo = toDoRepository.DeleteToDo(id);

            if (!validatedToDo)
            {
                return BadRequest("Something went wrong deleting to do");

            }

            var todos = toDoRepository.GetAllToDos();


            return Ok(todos);
        }

    }
}