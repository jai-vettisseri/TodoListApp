using Application.Common.Models;
using Application.TodoService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TodoListApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<TodoDto>>> GetTodoItems([FromQuery] bool includeCompleted = false)
        {
            return await Mediator.Send(new GetTodoList(includeCompleted));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetTodoItem(Guid id)
        {
            return await Mediator.Send(new GetTodo(id));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTodoItem command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTodoItem command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteTodoItem(id));

            return NoContent();
        }
    }
}
