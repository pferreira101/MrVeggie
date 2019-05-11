using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Shared { 

    public class UtilizadorHandling {

        private readonly UtilizadorContext _context;

        public UtilizadorHandling(UtilizadorContext context) {
            _context = context;
        }

        public Utilizador[] getUtilizadores() {
            return _context.Utilizador.ToArray();
        }


        public bool RegistaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            _context.Utilizador.Add(u);
            _context.SaveChanges();

            return true;
        }

        public bool validaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            var returnedUser = _context.Utilizador.Where(b => b.email == u.email && b.password == u.password).FirstOrDefault();

            if (returnedUser == null) return false;

            return true;
        }


        public Utilizador getUtilizadorIngredientesPref(int id) {

            Utilizador utilizador = _context.Utilizador.Find(id);

            List<UtilizadorIngredientePref> ings_ids = _context.UtilizadorIngredientePref.Where(uip => uip.utilizador_id == id).ToList();

            foreach (var uip in ings_ids) {
                utilizador.ingredientes_pref.Add(_context.Ingrediente.Find(uip.ingrediente_id));
            }

            return utilizador;

        }
    }
}
