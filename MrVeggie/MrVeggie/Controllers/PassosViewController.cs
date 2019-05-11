using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class PassosViewController : Controller {

        private Execucao execucao;


        public PassosViewController(PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            execucao = new Execucao(context_p, context_ip, context_i);
        }


        [HttpGet("{id_receita}-{nr_passo}")]
        public IActionResult ShowPasso(int id_receita, int nr_passo) {
            Passo passo = execucao.getDetalhesPasso(id_receita, nr_passo);

            if (passo == null) return NotFound();


            return View(passo);
        }
    }
}
