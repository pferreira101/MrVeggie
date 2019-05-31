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
                u.data_reg = DateTime.Now;
                bool RegistrationStatus = this.utilizador_handling.RegistaUtilizador(u);

                if (RegistrationStatus) {
                    ModelState.Clear();
                    TempData["Success"] = "Registado com sucesso.";
                }
                else {
                    TempData["Fail"] = "Email já registado. Tentativa de registo falhada.";
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
            ModelState.Clear();

            if (ModelState.IsValid) {
                var LoginStatus = this.utilizador_handling.validaUtilizador(u);
                if (LoginStatus) {

                    u.print();
                    u = utilizador_handling._context.Utilizador.Where(user => user.email.Equals(u.email)).First(); // so sabe o email e a password de u. assim fica completo. 
                    u.print();

                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, u.email)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);



                    if (u.admin) return RedirectToAction("Index", "AdminView") ;
                    else if(!u.config_inicial) return RedirectToAction("ConfigInicial", "IngredienteView");
                    else return RedirectToAction("Index", "Home");

                }
                else {
                    TempData["UserLoginFailed"] = "Por favor, insira as credenciais corretas.";
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
            int id = utilizador_handling._context.Utilizador.Where(u => u.email.Equals(User.Identity.Name)).First().id_utilizador; // SERÁ ASSIM QUE SE VAI BUSCAR?? METER ID NA COOKIE? COMO?! 

            Utilizador utilizador = utilizador_handling.getUtilizadorIngredientesPref(id);

            return View(utilizador);
        }

        [HttpGet]
        public IActionResult ReceitasPref() {
            int id = 1; // passar para argumento

            Utilizador utilizador = utilizador_handling.getUtilizadorReceitasPref(2);

            return View(utilizador);
        }


        public RedirectToActionResult registaConfigInicial() { // receber id como parametro??
            int id = utilizador_handling._context.Utilizador.Where(user => user.email.Equals(User.Identity.Name)).First().id_utilizador; // SERÁ ASSIM QUE SE VAI BUSCAR?? METER ID NA COOKIE? COMO?! 

            Utilizador u = utilizador_handling._context.Utilizador.Find(id);

            u.config_inicial = true;
            utilizador_handling._context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        
    }
}