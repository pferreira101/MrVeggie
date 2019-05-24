using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;

namespace MrVeggie.Controllers {


    public class HistoricoViewController : Controller {

        private Sugestao sugestao;


        public HistoricoViewController(ReceitaContext context_r, UtilizadorContext context_u) {
            sugestao = new Sugestao(context_r, context_u, null, null);
        }


        [HttpGet]
        public IActionResult ShowHistorico() {
            List<Receita> receitas = new List<Receita>();

            receitas = sugestao.getHistorico(User.Identity.Name);

            return View(receitas);
        }
    }
}