using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(long? id)
        {
            //IndexViewModel indexViewModel = await FetchData(id);//Typical usecase - fetch data from a webapi backend
            await Task.CompletedTask;

            return View();
        }

        /*private async Task<IndexViewModel> FetchData(long? id)
        {
            using HttpClient client = _clientFactory.CreateClient(Startup.WebapiBackendClient);

            string url = $"{Startup.WebapiBackendUrl}{(id.HasValue ? $"/{id}" : string.Empty)}";
            using HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string jsonData = await response.Content.ReadAsStringAsync();

            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);
            
            return indexViewModel;
        }*/
    }
}