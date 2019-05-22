using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class ReceitaViewController : Controller {

        private Selecao selecao;
        private Preparacao preparacao;
        

        public ReceitaViewController(ReceitaContext context_r, PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            selecao = new Selecao(context_r, context_i, context_ip, null);
            preparacao = new Preparacao(context_r, context_p, context_ip, context_i);
        }


        [HttpGet]
        public IActionResult showReceitas() {
            Receita[] receitas = selecao.getReceitas();
            Ingrediente[] ingredientes = selecao.getIngredientes();

            return View(new ReceitaAndIngredienteViewModel { Ingredientes = ingredientes, receitas = receitas });
        }

        [HttpGet("{id}")]
        public IActionResult showReceita(int id) {
            Receita receita = preparacao.getIngredientes(id);

            if (receita == null) return NotFound();

            return View(receita);
        }
        
        [HttpPost]
        public IActionResult showReceitas(int[] checkboxes)
        {

            if (ModelState.IsValid && checkboxes.Length != 0)
            {
                ReceitaAndIngredienteViewModel m = new ReceitaAndIngredienteViewModel { Ingredientes = selecao.getIngredientes(), receitas = selecao.getReceitas(checkboxes)};
                Console.WriteLine("modelo valido");
                return View(m);
            }
            Console.WriteLine("modelo invalido");
            return View(new ReceitaAndIngredienteViewModel { Ingredientes = selecao.getIngredientes(), receitas = selecao.getReceitas() });
        }
    }
}

