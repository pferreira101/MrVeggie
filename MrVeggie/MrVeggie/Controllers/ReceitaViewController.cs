using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class ReceitaViewController : Controller {

        private Selecao selecao;
        private Preparacao preparacao;
        

        public ReceitaViewController(ReceitaContext context_r, PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            selecao = new Selecao(context_r, context_i);
            preparacao = new Preparacao(context_r, context_p, context_ip, context_i);
        }


        [HttpGet]
        public IActionResult showReceitas() {
            Receita[] receitas = selecao.getReceitas();

            return View(receitas);
        }

        [HttpGet("{id}")]
        public IActionResult showReceita(int id) {
            Receita receita = preparacao.getIngredientes(id);

            if (receita == null) return NotFound();

            return View(receita);
        }        
    }
}