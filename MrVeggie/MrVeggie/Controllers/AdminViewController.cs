using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Models.Pages;

namespace MrVeggie.Controllers {


    public class AdminViewController : Microsoft.AspNetCore.Mvc.Controller
    {

        private Admin admin;

        public AdminViewController(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, UtensilioContext context_uten ,OperacaoContext context_op, IngredientesPassoContext context_ip) {
            admin = new Admin(context_i, context_u, context_r, context_op, context_uten, context_ip);

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
            

            return View(new NewReceita { receita = new Receita(), utensilios = utensilios} );
        }


        public IActionResult registaIngrediente(string nome, string url_imagem) {

            admin.registaIngrediente(nome, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }




        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult registaReceita(NewReceita n) {
            n.utensilios = admin.getUtensilios(Request.Form["uts"]);
            admin.registaReceita(n.receita, n.utensilios);
          
            
            return View("NewPasso", new NewPasso {
                passo = new Passo(),
                id_receita = admin.getNewReceitaID(n.receita.nome),
                ingredientes = admin.getIngredientes(),
                nPasso = 1,
                operacoes = admin.getOperacoes(),
                receitas = admin.getReceitas(),
                unidades = admin.getUnidades(),

            });
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult registaPasso(NewPasso model)
        {
            
            model.ingredientes = admin.getIngredientes(Request.Form["ings"]);
            if (Request.Form["recs"].Count != 0)
            {
                model.passo.sub_receita_id = int.Parse(Request.Form["recs"].First());
            }

            model.passo.nr = model.nPasso;    
            model.passo.receita_id = model.id_receita;
            model.passo.operacao_id = int.Parse(Request.Form["ops"].First());

           
            admin.registaPasso(model, Request.Form["unds"], Request.Form["quantidades"]);

            model.nPasso++;
            model.operacoes = admin.getOperacoes();
            model.receitas = admin.getReceitas();
            model.unidades = admin.getUnidades();
            model.ingredientes = admin.getIngredientes();

            return View("NewPasso", model);
        }

        [HttpPost]
        public IActionResult finalizaReceita(NewPasso model)
        {
            admin.finalizaReceita(model.id_receita, model.nPasso);

            return View("Index");
        }

        public IActionResult NewOperacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult registaOperacao(Operacao model)
        {
            admin.registaOperacao(model);

            return View("Index");
        }
    }
}