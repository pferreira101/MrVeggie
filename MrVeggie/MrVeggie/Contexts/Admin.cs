using MrVeggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Admin {

        private IngredienteContext _context_i;

        public Admin(IngredienteContext context_i) {
            _context_i = context_i;
        }

        public void registaIngrediente(string nome, string url) {
            Ingrediente ing = new Ingrediente(); // passar logo no construtor ?
            ing.nome = nome; 
            ing.url_imagem = url;

            _context_i.Ingrediente.Add(ing);
            _context_i.SaveChanges();
        }
    }
}
