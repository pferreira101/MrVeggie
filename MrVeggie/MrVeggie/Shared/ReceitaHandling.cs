using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Shared {

    public class ReceitaHandling {

        private readonly ReceitaContext _context;

        public ReceitaHandling(ReceitaContext context) {
            _context = context;
        }

        public Receita[] getReceitas() {
            return _context.receita.ToArray();
        }


        public Receita getReceita(int id) {
            return _context.receita.Find(id);
        }

    }
}
