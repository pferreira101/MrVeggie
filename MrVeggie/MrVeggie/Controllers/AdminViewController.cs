using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;

namespace MrVeggie.Controllers {


    public class AdminViewController : Controller {

        private Admin admin;

        public AdminViewController(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, UtensilioContext context_uten ,OperacaoContext context_op) {
            admin = new Admin(context_i, context_u, context_r, context_op, context_uten);

        }


            public IActionResult Index() {
            return View();
        }

        public IActionResult NewIngrediente() {
            return View();
        }

        public IActionResult Estatisticas() {
            Estatistica est = admin.getEstatistica();

            return View(est);
        }

        public IActionResult NewReceita() {
            List<Utensilio> utensilios = admin.getUtensilios();

            return View(new NewReceita { receita = new Receita(), utensilios = utensilios } );
        }


        public IActionResult registaIngrediente(string nome, string url_imagem) {

            admin.registaIngrediente(nome, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }




        [HttpPost]
        public IActionResult registaReceita(NewReceita n) {

            admin.registaReceita(n.receita, n.utensilios);

            return View("NewPasso",new NewPasso
            {
                passo = new Passo(),
                id_receita = admin.getNewReceitaID(n.receita.nome),
                ingredientes = admin.getIngredientes(),
                nPasso = 1,
                operacoes = admin.getOperacoes(),
                receitas = admin.getReceitas()
            });
        }


        [HttpPost]
        public IActionResult registaPasso(Tuple<Passo, List<Operacao>, List<Receita>, int, int> t)
        {

            //admin.registaPasso();

            return RedirectToAction("NewPasso", "AdminView", t );
        }

        public IActionResult NewPasso(Tuple<Passo, List<Operacao>, List<Receita>, int, int> t){
          
           return View(new Tuple<Passo, List<Operacao>, List<Receita>,int,int>(new Passo(), admin.getOperacoes(), admin.getReceitas(), t.Item4, t.Item5));
        }
    }
}