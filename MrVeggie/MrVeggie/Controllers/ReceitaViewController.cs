using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class ReceitaViewController : Controller {

        private ReceitaHandling receita_handling;

        
        public ReceitaViewController(ReceitaContext context) {
            receita_handling = new ReceitaHandling(context);
        }

        [HttpGet]
        public IActionResult showReceitas() {
            Receita[] receitas = receita_handling.getReceitas();

            Console.WriteLine("***********************RECEITAS****************************");

            return View(receitas);
        }

        [HttpGet("id={id_receita}")]
        public IActionResult showReceita(int id_receita) {
            Receita receita = receita_handling.getReceita(id_receita);

            Console.WriteLine("RECEITA ID = {0}", receita.id_receita);

            return View(receita);
        }
    }
}