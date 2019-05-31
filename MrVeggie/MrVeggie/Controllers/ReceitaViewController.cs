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
        

        public ReceitaViewController(ReceitaContext context_r, PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i, AgendaContext context_a, UtilizadorContext context_u) {
            selecao = new Selecao(context_r, context_i, context_ip, context_u, context_a);
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
        public IActionResult showReceitasSearch(string searchBar){
            List<Receita> receitas = new List<Receita>();
            Ingrediente[] ingredientes = selecao.getIngredientes();

            foreach(int i in selecao.getReceitas(searchBar)){
                receitas.Add(preparacao.getIngredientes(i));
            }
            
           
           
           return View("ShowReceitas",new ReceitaAndIngredienteViewModel { Ingredientes = ingredientes, receitas = receitas.ToArray() });
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





        [HttpPost]
        public bool VerificaAgenda([FromBody] string[] data) {
            return selecao.verificaAgenda(Int32.Parse(data[0]), (data[1].ToCharArray())[0], User.Identity.Name);
        }

        [HttpPost]
        public void MarcaAgenda([FromBody] string[] data) {
            selecao.marcaAgenda(Int32.Parse(data[0]), (data[1].ToCharArray())[0], Int32.Parse(data[2]), User.Identity.Name);
        }


        [HttpPost]
        public void AdicionaReceitaFavoritos([FromBody] int id_receita) {
            selecao.adicionaReceitaFavoritos(id_receita, User.Identity.Name);
        }
    }
}

