using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Sugestao {

        private readonly ReceitaContext _context_r;
        private readonly UtilizadorContext _context_u;

        public Sugestao(ReceitaContext context_r, UtilizadorContext context_u) {
            _context_r = context_r;
            _context_u = context_u;
        }

        public List<Receita> getSugestoes() {
            List<Receita> r = new List<Receita>();

            r.Add(_context_r.Receita.Find(1));
            Console.WriteLine("********************************************************* {0}", r.Count());
            return r;
        }


        public List<Receita> getSugestoes(string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email == email).First().id_utilizador;
            List<Receita> r = new List<Receita>(4);

            //r.Add(getReceitaSugeridaPorHistorico(id_utilizador));
            r.Add(getReceitaSugeridaPorReceitasFav(id_utilizador));
            //r.Add(getReceitaSugeridaPorIngredientesFav(id_utilizador));
            //r.Add(getReceitaSugeridaPorHistorico(id_utilizador));



            return r;
        }

        private Receita getReceitaSugeridaPorReceitasFav(int id_utilizador) {
            List<int> rIds = _context_u.UtilizadorReceitasPref.Where(u => u.utilizador_id == id_utilizador).Select(u => u.receita_id).ToList();

            List<Receita> receitas = _context_r.Receita.Where(r => rIds.Contains(r.id_receita)).ToList();

            Receita x = receitas.First();

            return x;
        }

        private Receita getReceitaSugeridaPorIngredientesFav(int id_utilizador) {
            throw new NotImplementedException();
        }

        private Receita getReceitaSugeridaPorHistorico(int id_utilizador) {
            
            throw new NotImplementedException();
        }
    }
}
