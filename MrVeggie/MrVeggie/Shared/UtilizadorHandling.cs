using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Shared { 

    public class UtilizadorHandling {

        private readonly UtilizadorContext _context;
        private readonly IngredienteContext _context_ing;
        private readonly UtilizadorIngredientesPrefContext _context_uip;
        private readonly UtilizadorReceitasPrefContext _context_urp;

        public UtilizadorHandling(UtilizadorContext context, UtilizadorIngredientesPrefContext context_uip, UtilizadorReceitasPrefContext context_urp) {
            _context = context;
            _context_uip = context_uip;
            _context_urp = context_urp;
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

            List<UtilizadorIngredientesPref> ings_ids = _context.UtilizadorIngredientesPref.Where(uip => uip.utilizador_id == id).ToList();

            utilizador.ingredientes_pref = new List<Ingrediente>();

            foreach (var uip in ings_ids) {
                utilizador.ingredientes_pref.Add(_context_uip.Ingrediente.Find(uip.ingrediente_id));
            }

            return utilizador;

        }

        public Utilizador getUtilizadorReceitasPref(int id) {
            Utilizador utilizador = _context.Utilizador.Find(id);

            List<UtilizadorReceitasPref> receitas_ids = _context.UtilizadorReceitasPref.Where(uip => uip.utilizador_id == id).ToList();

            utilizador.receitas_pref = new List<Receita>();

            foreach (var uip in receitas_ids) {
                utilizador.receitas_pref.Add(_context.Receita.Find(uip.receita_id));
            }

            return utilizador;
        }
    }
}
