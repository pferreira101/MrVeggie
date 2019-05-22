using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Selecao {

        private readonly ReceitaContext _context_r;
        private readonly IngredienteContext _context_ing;
        private readonly IngredientesPassoContext _context_ip;
        private readonly UtilizadorContext _context_u;

        public Selecao(ReceitaContext context_r, IngredienteContext context_ing, IngredientesPassoContext context_ip, UtilizadorContext context_u) {
            _context_r = context_r;
            _context_ing = context_ing;
            _context_ip = context_ip;
            _context_u = context_u;
        }

        public Receita[] getReceitas() {
            return _context_r.Receita.ToArray();
        }

        public Receita[] getReceitas(IEnumerable<int> s)
        {
            List<Receita> receitas = _context_r.Receita.ToList();
            List<Receita> result = new List<Receita>(receitas);



            foreach (Receita r in receitas)
            {

                List<Passo> passos = _context_r.Passo.Where(p => p.receita_id == r.id_receita).ToList();
                List<int> ings = new List<int>();
                foreach (Passo p in passos)
                {
                    ings.AddRange(_context_ip.IngredientesPasso.Where(ip => ip.passo_id == p.id_passo).Select( i => i.ingrediente_id).ToList());                    
                }
                
                if (ings.Intersect(s).Count() == 0)
                {
                    result.Remove(r);
                }
            }

            return result.ToArray();
        }

        public Ingrediente[] getIngredientes() {
            return _context_ing.Ingrediente.ToArray();
        }





        // fazer este caso para quando não está ninguem logado
        public Receita[] getSugestoes() {
            List<Receita> receitas = new List<Receita>();

            Random random = new Random();
            
   

            for (int i = 0; i < 5; i++) {
                
            }

            _context_r.Receita.Find(1);

            return receitas.ToArray();
        }

        public Receita[] getSugestoes(string email) {
            int id = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            List<Receita> receitas_pref = getUtilizadorReceitasPref(id);
            /*
            List<Receita> receitas = new List<Receita>();

            Random random = new Random();

            for (int i = 0; i < 5; i++) {
                receitas.Add(receitas_pref.ElementAtOrDefault(random.Next(receitas_pref.Count())));
            }
            
            return receitas.ToArray(); */
            return receitas_pref.ToArray();
        }

        public List<Receita> getUtilizadorReceitasPref(int id) {
            List<Receita> receitas = new List<Receita>();

            List<UtilizadorReceitasPref> receitas_ids = _context_u.UtilizadorReceitasPref.Where(uip => uip.utilizador_id == id).ToList();

            foreach (var uip in receitas_ids) {
                receitas.Add(_context_r.Receita.Find(uip.receita_id));
            }

            return receitas;
        }
        

    }
}
