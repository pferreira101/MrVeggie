using MrVeggie.Models;
using MrVeggie.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Autenticacao {

        private readonly UtilizadorContext _context_u;


        public bool validaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            var returnedUser = _context_u.Utilizador.Where(b => b.email == u.email && b.password == u.password).FirstOrDefault();

            if (returnedUser == null) return false;

            return true;
        }

        public bool RegistaUtilizador(Utilizador u) {
            var userList = _context_u.Utilizador
                           .Where(s => s.email == u.email)
                           .ToList();

            if (((List<Utilizador>)userList).Count != 0) return false;

            u.password = MyHelpers.HashPassword(u.password);
            _context_u.Add(u);
            _context_u.SaveChanges();

            return true;
        }

        public Utilizador getUtilizador(string email) {

            return null;
        }
    }
}
