using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Avaliacao {


        private readonly ReceitaContext _context;

        public Avaliacao(ReceitaContext context) {
            _context = context;
        }


        public void avalia(int id_receita, int id_utilizador, int pontuacao) {
            Console.WriteLine("*********** {0} ************* {1} ************* {2}", id_receita, id_utilizador, pontuacao);

            Receita r = getReceita(id_receita);

            r.avaliacao = ((r.avaliacao * r.n_avaliacoes) + pontuacao) / (r.n_avaliacoes+1);
            r.n_avaliacoes++;

            _context.Update<Receita>(r);
            _context.SaveChanges();
        }


        public Receita getReceita(int id) {
            Receita receita = _context.Receita.Find(id);

            var passos = _context.Passo.Where(p => p.receita_id == id);

            foreach (Passo p in passos) {
                receita.passos.Add(p);
            }

            return receita;
        }
    }
}
