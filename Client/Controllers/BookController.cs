using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize(Roles = "1")]
    public class BookController : Controller
    {

        public async Task<IActionResult> IndexAsync(int type = 0, string searchString = "")
        {
            if (String.IsNullOrEmpty(searchString))
            {
                searchString = string.Empty;
            }
            List<BookResponse> listB;
            ViewData["searhString"] = searchString;
            using (HttpClient client = new HttpClient())
            {
                var url = string.Empty;
                if (type == 1 && !String.IsNullOrEmpty(searchString))
                {
                    url = "http://localhost:5069/api/Books/Title/" + searchString;
                }
                else if (type == 2 && !String.IsNullOrEmpty(searchString))
                {
                    url = "http://localhost:5069/api/Books/Price/" + searchString;
                }
                else
                {
                    url = "http://localhost:5069/api/Books";
                }

                using (HttpResponseMessage res = await client.GetAsync(url))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            listB = JsonConvert.DeserializeObject<List<BookResponse>>(data);
                        }
                    }
                    else
                    {
                        throw new Exception("Error in Search Book API");
                    }

                }
            }
            return View(listB);
        }

    }
}
