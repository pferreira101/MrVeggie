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



        [HttpGet("{id_receita}")]
        public IActionResult AvaliarReceita(int id_receita) {
            Receita receita = avaliacao.getReceita(id_receita);

            return View(receita);
        }


        [HttpPost("{id_receita}")]
        public IActionResult AvaliarReceitaUtilizador(int id_receita) {  ///// PASSAR O UTILIZADOR COMO ARGUMENTO MAS COMO??
            int pontuacao = Int32.Parse(HttpContext.Request.Form["rate"]);
            int id_utilizador = 1;

            avaliacao.avalia(id_receita, id_utilizador, pontuacao);

            return RedirectToAction("Index", "Home");
        }
    }
}