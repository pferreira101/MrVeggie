using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Shared {
    public class IngredienteHandling {

        private readonly IngredienteContext _context;

        public IngredienteHandling(IngredienteContext context) {
            _context = context;
        }

        public Ingrediente[] getIngredientes() {
            return _context.ingrediente.ToArray();
        }
    }
}
