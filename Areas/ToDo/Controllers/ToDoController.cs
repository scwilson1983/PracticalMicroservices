using PracticalMicroservices.Areas.ToDo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PracticalMicroservices.Areas.ToDo.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        public static List<TodoItem> Items { get; set; } = new List<TodoItem> { new TodoItem { Title = "Take out trash", IsDone = false } };

        [HttpGet]
        public ActionResult Get() => Ok(Items);

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            Items.Add(item);
            return Ok();
        }
    }
}
