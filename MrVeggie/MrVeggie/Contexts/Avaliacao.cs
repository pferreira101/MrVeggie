using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Avaliacao {

        private readonly ReceitaContext _context_r;
        private readonly UtilizadorContext _context_u;

        public Avaliacao(ReceitaContext context_r, UtilizadorContext context_u) {
            _context_r = context_r;
            _context_u = context_u;
        }


        public void avalia(int id_receita, string email_utilizador, int pontuacao) {
            Receita r = getReceita(id_receita);
            int id = _context_u.Utilizador.Where(u => u.email.Equals(email_utilizador)).First().id_utilizador; // SERÁ ASSIM QUE SE VAI BUSCAR?? METER ID NA COOKIE? COMO?! 

            r.avaliacao = ((r.avaliacao * r.n_avaliacoes) + pontuacao) / (r.n_avaliacoes+1);
            r.n_avaliacoes++;

            _context_r.Update<Receita>(r);
            _context_r.SaveChanges();
        }


        public Receita getReceita(int id) {
            Receita receita = _context_r.Receita.Find(id);

            /* NÃO É PRECISO O DETALHE DOS PASSES NESTE PONTO
            var passos = _context.Passo.Where(p => p.receita_id == id);

            foreach (Passo p in passos) {
                receita.passos.Add(p);
            }
            */
            return receita;
        }
    }
}
