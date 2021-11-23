using PracticalMicroservices.Areas.ToDo.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PracticalMicroservices.Areas.ToDo.Pages
{
    public partial class ToDo
    {
        private List<TodoItem> todos = new();
        private string newTodo;
        [Inject]
        protected IHttpClientFactory HttpClientFactory { get; set; }

        protected async override Task OnInitializedAsync()
        {
            todos = await LoadToDos();
        }

        private async Task<List<TodoItem>> LoadToDos()
        {
            var client = HttpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            var response = await client.GetAsync("todo");
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TodoItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodo))
            {
                var client = HttpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://localhost:44382/api/");
                var json = JsonSerializer.Serialize(new TodoItem { Title = newTodo });
                await client.PostAsync("todo", new StringContent(json, Encoding.UTF8, "application/json"));
                todos = await LoadToDos();
                newTodo = string.Empty;
            }
        }
    }
}
