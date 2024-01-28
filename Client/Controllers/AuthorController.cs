using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize(Roles = "1")]
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AuthorResponse> listA;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync("http://localhost:5069/api/Authors"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        listA = JsonConvert.DeserializeObject<List<AuthorResponse>>(data);
                    }
                }
            }
            return View(listA);
        }
    }
}
