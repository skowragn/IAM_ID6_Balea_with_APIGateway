﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Management.Web.Controllers;

public class AccountController : Controller
{
    [Authorize]
    public IActionResult Login()
    {
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public ActionResult Logout()
    {
        return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme });
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}

