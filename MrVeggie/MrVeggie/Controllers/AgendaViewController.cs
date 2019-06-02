using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Models.Pages;
using System.Net.Mail;

namespace MrVeggie.Controllers {


    public class AgendaViewController : Controller {


        private Sugestao sugestao { get; set; }
        private Preparacao preparacao { get; set; }
        private Selecao selecao { get; set; }


        public AgendaViewController(ReceitaContext context_r, UtilizadorContext context_u, AgendaContext context_a, IngredienteContext context_i, PassoContext context_p, IngredientesPassoContext context_ip) {
            sugestao = new Sugestao(context_r, context_u, null, context_a);
            preparacao = new Preparacao(context_r, context_p, context_ip, context_i);
            selecao = new Selecao(context_r, context_i, context_ip, context_u, context_a);
        }


        public IActionResult ShowAgenda() {

            List<Agenda> agenda = sugestao.getAgenda(User.Identity.Name);
            List<Receita> receitas = sugestao.getReceitas();

            AgendaReceitas ar = new AgendaReceitas {
                agenda = agenda,
                receitas = receitas
            };

            return View(ar);
        }


        [HttpPost]
        public void MarcaAgenda([FromBody] string[] data) {
            int id_receita = Int32.Parse(data[0]);
            int dia = Int32.Parse(data[1]);
            char refeicao = data[2].ToCharArray()[0];

            selecao.marcaAgenda(dia, refeicao, id_receita, User.Identity.Name);
        }


        public IActionResult ShowIngredientesAgenda() {

            List<Agenda> agenda = sugestao.getAgenda(User.Identity.Name);
            List<Receita> receitas = new List<Receita>();

            foreach(Agenda a in agenda) {
                if(a.receita_id != -1) receitas.Add(preparacao.getIngredientes(a.receita_id));
            }

            Dictionary<Ingrediente, Quantidade> ingredientes = new Dictionary<Ingrediente, Quantidade>();

            foreach (Receita r in receitas) {
                foreach (Passo p in r.passos) {
                    p.ingredientes = preparacao.getIngredientesPasso(p.id_passo);
                    foreach (var i in p.ingredientes) {
                        if (!ingredientes.ContainsKey(i.Key)) {
                            Quantidade q = new Quantidade(0, i.Value.unidade);
                            ingredientes.Add(i.Key, q);
                        }
                        ingredientes[i.Key].add(i.Value.quantidade);

                    }
                }
            }
            

            return View(ingredientes);
        }





        [HttpPost]
        public void sendEmail([FromBody] string ingredientes) {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            string init = "<p>Olá!</p> <p>Lista semanal dos ingredientes:</p>";
            string end = "<p>Boa semana,</p> <p>Mr. Veggie</p>";
            mail.From = new MailAddress("mr.veggiept@gmail.com");
            mail.To.Add(User.Identity.Name);
            mail.Subject = "Lista de ingredientes semanal";
            mail.Body = init + ingredientes + end;
            mail.IsBodyHtml = true;


            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("mr.veggiept@gmail.com", "li4grupo25");
            client.EnableSsl = true;
            
            client.Send(mail);
        }

    }
}