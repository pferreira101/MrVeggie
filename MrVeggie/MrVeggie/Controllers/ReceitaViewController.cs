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
        private PassoHandling passo_handling;
        
        
        public ReceitaViewController(ReceitaContext context_r, PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            receita_handling = new ReceitaHandling(context_r);
            passo_handling = new PassoHandling(context_p, context_ip, context_i);
        }

        [HttpGet]
        public IActionResult showReceitas() {
            Receita[] receitas = receita_handling.getReceitas();

            return View(receitas);
        }

        [HttpGet("{id}")]
        public IActionResult showReceita(int id) {
            Receita receita = receita_handling.getReceita(id);

            if (receita == null) return NotFound();

            Dictionary<Ingrediente, int> ingredientes = new Dictionary<Ingrediente, int>();

            foreach(var p in receita.passos) {
                Dictionary<Ingrediente, int> ings_passo = passo_handling.getIngredientes(p.id_passo);

                foreach (var i in ings_passo) {
                    if (!ingredientes.ContainsKey(i.Key)) ingredientes.Add(i.Key, 0);
                    ingredientes[i.Key] += i.Value;

                    Console.WriteLine("****** LISTA TOTAL : INGREDIENTE {0} - QUANTIDADE {1} **************", i.Key.id_ingrediente, i.Value);
                }
            }
            receita.ingredientes = ingredientes;

            return View(receita);
        }

        
    }
}