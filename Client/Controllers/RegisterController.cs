using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index(string err = "")
        {
            ModelState.AddModelError(string.Empty, err);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(string email, string password, string confirmPassword)
        {
            LoginResponse loginResponse;
            string message = string.Empty;
            var requestData = new RegisterRequest
            {
                email = email,
                password = password,
                confirmPassword = confirmPassword
            };

            if (!password.Equals(confirmPassword))
            {
                message = "Password and Confirm PassWord not equal";
                var routeValues = new RouteValueDictionary
                        {
                            { "err", message }
                        };
                return RedirectToAction("Index", routeValues);
            }
            using (HttpClient client = new HttpClient())
            {
                var url = $"http://localhost:5069/Resgister";
                using (HttpResponseMessage res = await client.PostAsJsonAsync(url, requestData))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(data);
                            var fullName = loginResponse.first_name ?? "" + " " + loginResponse.middle_name ?? "" + " " + loginResponse.last_name ?? "";
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
                            { "err", "This email already exist" }
                        };
                        return RedirectToAction("Index", routeValues);
                    }

                }
            }

        }
    }
}
