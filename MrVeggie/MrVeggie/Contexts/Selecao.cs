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

        public Selecao(ReceitaContext context_r, IngredienteContext context_ing, IngredientesPassoContext context_ip) {
            _context_r = context_r;
            _context_ing = context_ing;
            _context_ip = context_ip;
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

        

    }
}
