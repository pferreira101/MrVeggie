using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;
using MrVeggie.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MrVeggie.Models.Auxiliary;

namespace MrVeggie.Controllers {


    [Route("[controller]/[action]")]
    public class UserViewController : Controller {

        private UtilizadorHandling utilizador_handling;

        public UserViewController(UtilizadorContext context, UtilizadorIngredientesPrefContext context_uip, UtilizadorReceitasPrefContext context_urp) {
            //_context = context;
            utilizador_handling = new UtilizadorHandling(context, context_uip, context_urp);
        }


        [Authorize]
        public IActionResult getUtilizadores() {
            Utilizador[] users = utilizador_handling.getUtilizadores();

            return View(users);
        }


        [HttpGet]
        public IActionResult RegistaUtilizador() {

            return View();
        }

        [HttpPost]
        public IActionResult RegistaUtilizador([Bind] Utilizador u) {

            if (ModelState.IsValid) {
                bool RegistrationStatus = this.utilizador_handling.RegistaUtilizador(u);

                if (RegistrationStatus) {
                    ModelState.Clear();
                    TempData["Success"] = "Registration Successful!";
                }
                else {
                    TempData["Fail"] = "This User ID already exists. Registration Failed.";
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult LoginUtilizador() {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUtilizador([Bind] Utilizador u) {
            ModelState.Remove("nome");
            ModelState.Remove("email");

            if (ModelState.IsValid) {
                var LoginStatus = this.utilizador_handling.validaUtilizador(u);
                if (LoginStatus) {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, u.email)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");

                }
                else {
                    TempData["UserLoginFailed"] = "Login Failed. Please enter correct credentials";
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult IngredientesPref() {
            int id = 1; // passar para argumento

            Utilizador utilizador = utilizador_handling.getUtilizadorIngredientesPref(2);

            return View(utilizador);
        }

        [HttpGet]
        public IActionResult ReceitasPref() {
            int id = 1; // passar para argumento

            Utilizador utilizador = utilizador_handling.getUtilizadorReceitasPref(id);

            return View(utilizador);
        }

    }
}