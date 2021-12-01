using Microsoft.AspNetCore.Components;
using PracticalMicroservices.Areas.VideoViews.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PracticalMicroservices.Areas.VideoViews.Pages
{
    public partial class VideoViews
    {
        [Inject]
        protected IHttpClientFactory HttpClientFactory { get; set; }
        private VideoView videoViewsVm = new();

        protected async override Task OnInitializedAsync()
        {
            videoViewsVm = await LoadViewings();
        }

        private async Task IncrementViewings()
        {
            var client = HttpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            var json = JsonSerializer.Serialize(videoViewsVm);
            await client.PostAsync("videoviews", new StringContent(json, Encoding.UTF8, "application/json"));
            videoViewsVm = await LoadViewings();
        }

        private async Task<VideoView> LoadViewings()
        {
            var client = HttpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            var response = await client.GetAsync("videoviews");
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VideoView>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
