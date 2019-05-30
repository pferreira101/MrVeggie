using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Avaliacao {

        private readonly ReceitaContext _context_r;
        private readonly UtilizadorContext _context_u;
        private readonly HistoricoUtilizadorContext _context_hu;

        public Avaliacao(ReceitaContext context_r, UtilizadorContext context_u, HistoricoUtilizadorContext context_hu) {
            _context_r = context_r;
            _context_u = context_u;
            _context_hu = context_hu;
        }


        public void avalia(int id_receita, string email_utilizador, int pontuacao) {
            Receita r = _context_r.Receita.Find(id_receita);
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email_utilizador)).First().id_utilizador; // SERÁ ASSIM QUE SE VAI BUSCAR?? METER ID NA COOKIE? COMO?! 

            r.avaliacao = ((r.avaliacao * r.n_avaliacoes) + pontuacao) / (r.n_avaliacoes+1);
            r.n_avaliacoes++;

            HistoricoUtilizador historico = _context_hu.HistoricoUtilizador.Where(hu => hu.utilizador_id == id_utilizador && hu.receita_id == id_receita).OrderByDescending(hu => hu.data_conf).First();
            historico.avaliacao = pontuacao;

            _context_r.Update<Receita>(r);
            _context_r.SaveChanges();

            _context_hu.Update<HistoricoUtilizador>(historico);
            _context_hu.SaveChanges();
        }

        public Receita getReceita(int id_receita) {
            return _context_r.Receita.Find(id_receita);
        }

        public void addToHistorico(int id_receita, string email_utilizador) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email_utilizador)).First().id_utilizador; // SERÁ ASSIM QUE SE VAI BUSCAR?? METER ID NA COOKIE? COMO?! 

            HistoricoUtilizador hu = new HistoricoUtilizador();
            hu.avaliacao = 0;
            hu.data_conf = DateTime.Now;
            hu.receita_id = id_receita;
            hu.utilizador_id = id_utilizador;

            _context_hu.HistoricoUtilizador.Add(hu);
            _context_hu.SaveChanges();
        }
    }
}
