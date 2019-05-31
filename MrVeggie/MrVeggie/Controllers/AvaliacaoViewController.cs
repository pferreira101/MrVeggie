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

        public AvaliacaoViewController(ReceitaContext context_r, UtilizadorContext context_u, HistoricoUtilizadorContext context_hu) {
            avaliacao = new Avaliacao(context_r, context_u, context_hu);
        }



        [HttpGet("{id_receita}")]
        public IActionResult AvaliarReceita(int id_receita) {
            Receita receita = avaliacao.getReceita(id_receita);

            avaliacao.addToHistorico(id_receita, User.Identity.Name);

            return View(receita);
        }


        [HttpPost("{id_receita}")]
        public IActionResult AvaliarReceitaUtilizador(int id_receita) { 
            int pontuacao = Int32.Parse(HttpContext.Request.Form["rate"]);

            avaliacao.avalia(id_receita, User.Identity.Name, pontuacao);

            return RedirectToAction("Index", "Home");
        }
    }
}