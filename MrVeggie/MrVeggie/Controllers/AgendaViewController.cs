using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Pages;

namespace MrVeggie.Controllers {


    public class AgendaViewController : Controller {


        private Sugestao sugestao { get; set; }


        public AgendaViewController(ReceitaContext context_r, UtilizadorContext context_u, AgendaContext context_a, IngredienteContext context_i) {
            sugestao = new Sugestao(context_r, context_u, null, context_a);
        }


        public IActionResult ShowAgenda() {
            
            List<Agenda> agenda = sugestao.getAgenda(User.Identity.Name);
            List<Receita> receitas = sugestao.getReceitas();

            AgendaReceitas ai = new AgendaReceitas {
                agenda = agenda,
                receitas = receitas
            };

            return View(ai);
        }
    }
}