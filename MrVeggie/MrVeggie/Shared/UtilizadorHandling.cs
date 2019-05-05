using MrVeggie.Models;
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
            return _context.utilizador.ToArray();
        }


        public bool RegistaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            _context.utilizador.Add(u);
            _context.SaveChanges();

            return true;
        }

        public bool validaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            var returnedUser = _context.utilizador.Where(b => b.email == u.email && b.password == u.password).FirstOrDefault();

            if (returnedUser == null) return false;

            return true;
        }
    }
}
