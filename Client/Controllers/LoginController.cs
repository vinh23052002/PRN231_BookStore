using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string err = "")
        {
            ModelState.AddModelError(string.Empty, err);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string email, string pass)
        {
            LoginResponse loginResponse;
            using (HttpClient client = new HttpClient())
            {
                var url = $"http://localhost:5069/Login?email={email}&password={pass}";
                using (HttpResponseMessage res = await client.GetAsync(url))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(data);
                            var fullName = (loginResponse.first_name ?? "") + (" " + loginResponse.middle_name ?? "") + " " + (loginResponse.last_name ?? "");
                            var claims = new List<Claim>
                            {
                                new Claim("user_id",loginResponse.user_id.ToString()),
                                new Claim(ClaimTypes.Role,loginResponse.role_id.ToString()),
                                new Claim("fullName",fullName),
                            };
                            var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                            };

                            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        var routeValues = new RouteValueDictionary
                        {
                            { "err", "User or Password not Correct!!" }
                        };
                        return RedirectToAction("Index", routeValues);
                    }

                }
            }

        }
    }
}
