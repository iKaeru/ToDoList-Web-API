using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Errors;
using Database = Models.DataBase;
using Model = Models.ToDoItems;
using View = Client.Models.ToDoItems;
using Converter = Converters;

namespace ToDoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class ToDoController : Controller
    {
        private readonly Database.TodoContext context;

        public ToDoController(Database.TodoContext context)
        {
            this.context = context;

            if (!this.context.TodoItems.Any())
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                this.context.TodoItems.Add(new Model.TodoItem {Name = "Item1"});
                this.context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.TodoItem>>> GetTodoItems()
        {
            return await context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.TodoItem>> GetTodoItem(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var todoItem = await context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                var error = ServiceErrorResponses.ItemNotFound(id.ToString());
                return this.NotFound(error);
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Model.TodoItem>> PostTodoItem([FromBody] View.ToDoItemBuildInfo item)
        {
            var modelBuildInfo = Converter.ToDoItemBuildInfoConverter.Convert(item);
            var modelItem = new Model.TodoItem
            {
                Name = modelBuildInfo.Name,
                IsComplete = modelBuildInfo.IsComplete
            };
            context.TodoItems.Add(modelItem);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new {id = modelItem.Id}, modelItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid? id, [FromBody] View.ToDoItemPatchInfo item)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var modelPatchInfo = Converter.ToDoItemPatchInfoConverter.Convert(item);
            var modelItem = new Model.TodoItem
            {
                Id = id.Value,
                Name = modelPatchInfo.Name,
                IsComplete = modelPatchInfo.IsComplete
            };

            if (id != modelItem.Id)
            {
                var error = ServiceErrorResponses.ItemNotFound(id.ToString());
                return this.NotFound(error);
            }

            context.Entry(modelItem).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                var error = ServiceErrorResponses.ItemNotFound(id.ToString());
                return this.NotFound(error);
            }

            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}