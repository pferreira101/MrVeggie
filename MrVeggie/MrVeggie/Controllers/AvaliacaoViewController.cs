using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;

namespace MrVeggie.Controllers{

    [Route("[controller]/[action]")]
    public class AvaliacaoViewController : Controller {

        private Avaliacao avaliacao;

        public AvaliacaoViewController(ReceitaContext context) {
            avaliacao = new Avaliacao(context);
        }



        [HttpGet("{id}")]
        public IActionResult AvaliarReceita(int id) {
            Receita receita = avaliacao.getReceita(id);

            return View(receita);
        }


        [HttpPost("{id}")]
        public IActionResult AvaliarReceita() {
            int pontuacao = Int32.Parse(HttpContext.Request.Form["rate"]);
            int id_utilizador = 1;

            avaliacao.avalia(1, id_utilizador, pontuacao);

            return RedirectToAction("Index", "Home");
        }
    }
}