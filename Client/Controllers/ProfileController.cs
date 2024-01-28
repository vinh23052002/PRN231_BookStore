using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ProfileController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var userId = User.FindFirst(p => p.Type.Equals("user_id")).Value;
            LoginResponse user;
            using (HttpClient client = new HttpClient())
            {
                var url = "http://localhost:5069/api/Users/" + userId;
                using (HttpResponseMessage res = await client.GetAsync(url))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            user = JsonConvert.DeserializeObject<LoginResponse>(data);
                            return View(user);
                        }
                    }
                }
            }
            return View();
        }
    }
}
