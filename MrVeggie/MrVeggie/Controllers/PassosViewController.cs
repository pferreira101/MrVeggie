using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;
using MrVeggie.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class PassosViewController : Controller {

        private PassoHandling passo_handling;

        public PassosViewController(PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            passo_handling = new PassoHandling(context_p, context_ip, context_i);
        }

        [HttpGet("{id_receita}-{nr_passo}")]
        public IActionResult ShowPasso(int id_receita, int nr_passo) {
            Passo passo = passo_handling.getDetalhesPasso(id_receita, nr_passo);

            if (passo == null) return NotFound();


            return View(passo);
        }
    }
}
