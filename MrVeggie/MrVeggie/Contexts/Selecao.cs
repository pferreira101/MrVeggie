using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Selecao {

        private readonly ReceitaContext _context_r;
        private readonly IngredienteContext _context_ing;

        public Selecao(ReceitaContext context_r, IngredienteContext context_ing) {
            _context_r = context_r;
            _context_ing = context_ing;
        }

        public Receita[] getReceitas() {
            return _context_r.Receita.ToArray();
        }


        public Ingrediente[] getIngredientes() {
            return _context_ing.Ingrediente.ToArray();
        }

    }
}
