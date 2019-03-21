using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_with_EF_lecture6.Models;
using SPA.DAL;
using SPA.Models;

namespace MVC_with_EF_lecture6.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameContext _context;
        PlayerAccessLayer ual = new PlayerAccessLayer();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("PlayerId,PlayerEmail,PlayerPassword,PlayerConfirmedPassword,PlayerProviderKey,PlayerName")] Player player)
        {
            if (ModelState.IsValid)
            {

                var accountSalt = BCrypt.Net.BCrypt.GenerateSalt(10, SaltRevision.Revision2A);
                var password = BCrypt.Net.BCrypt.HashPassword(player.PlayerPassword, accountSalt);

                player.PlayerProviderKey = accountSalt;
                player.PlayerPassword = password;
                player.PlayerConfirmedPassword = password;


                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login","Home");
            }
            return View(player);
        }

        [HttpGet, ActionName("Login")]
        public IActionResult Login()
        {
            return View(new Player());
        }

        [HttpPost, ActionName("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogInAsync(Player playerModel)
        {
            try
            {
                bool ingelogd = await ual.SignIn(HttpContext, playerModel);
                if (ingelogd) return RedirectToAction("Ingelogd", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("summary", ex.Message);
                return View(playerModel);

            }
            return View(playerModel);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            ual.SignOut(HttpContext);
            return RedirectToAction("Uitgelogd", "Home");
        }

        public IActionResult Ingelogd()
        {
            return View();
        }

        public IActionResult Uitgelogd()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}