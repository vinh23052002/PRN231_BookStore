﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LogoutController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            await HttpContext.SignOutAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
