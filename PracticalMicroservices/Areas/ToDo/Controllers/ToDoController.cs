using PracticalMicroservices.Areas.ToDo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PracticalMicroservices.Events.Services;
using PracticalMicroservices.Events.Entities;
using System.Text.Json;
using System;
using System.Threading.Tasks;

namespace PracticalMicroservices.Areas.ToDo.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        public static List<TodoItem> Items { get; set; } = new List<TodoItem> { new TodoItem { Title = "Take out trash", IsDone = false } };
        readonly IMessageStore _messageStore;

        public ToDoController(IMessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        [HttpGet]
        public ActionResult Get() => Ok(Items);

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            Items.Add(item);
            var version = _messageStore.WriteMessage(item, Guid.NewGuid());
            return Ok();
        }
    }
}
