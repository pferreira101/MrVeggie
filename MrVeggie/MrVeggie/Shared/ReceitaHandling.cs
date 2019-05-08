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
            return _context.Receita.ToArray();
        }


        public Receita getReceita(int id) {
            Receita receita = _context.Receita.Find(id);

            var passos = _context.Passo.Where(p => p.receita_id == id);

            foreach (Passo p in passos) {
                receita.passos.Add(p);
            }

            return receita; 
        }

        // Para seguir o diagrama mas é overkill fazer isto não?
        public int getNumPassosReceita(int id) {

            return _context.Passo.Where(p => p.receita_id == id).Count();
        }

        

    }
}
