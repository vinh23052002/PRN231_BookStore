using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize(Roles = "1")]
    public class PublisherController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<PublisherResponse> listP;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync("http://localhost:5069/api/Publishers"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        listP = JsonConvert.DeserializeObject<List<PublisherResponse>>(data);
                    }
                }
            }
            return View(listP);
        }
    }
}
