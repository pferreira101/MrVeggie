using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Preparacao {

        private readonly ReceitaContext _context_r;
        private readonly PassoContext _context_passo;
        private readonly IngredientesPassoContext _context_ip;
        private readonly IngredienteContext _context_ing;

        public Preparacao(ReceitaContext context_r, PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            _context_passo = context_p;
            _context_ip = context_ip;
            _context_ing = context_i;
            _context_r = context_r;
        }



        // o nome é para estar em conformidade com o diagrama 
        public Receita getIngredientes(int id_receita) {
            Receita receita = _context_r.Receita.Find(id_receita);

            var passos = _context_r.Passo.Where(p => p.receita_id == id_receita);

            foreach (Passo p in passos) {
                receita.passos.Add(p);
            }

            Dictionary<Ingrediente, int> ingredientes = new Dictionary<Ingrediente, int>();

            foreach (var p in receita.passos) {
                Dictionary<Ingrediente, int> ings_passo = getIngredientesPasso(p.id_passo);

                foreach (var i in ings_passo) {
                    if (!ingredientes.ContainsKey(i.Key)) ingredientes.Add(i.Key, 0);
                    ingredientes[i.Key] += i.Value;

                    Console.WriteLine("****** LISTA TOTAL : INGREDIENTE {0} - QUANTIDADE {1} **************", i.Key.id_ingrediente, i.Value);
                }
            }
            receita.ingredientes = ingredientes;

            return receita;
        }

        private Dictionary<Ingrediente, int> getIngredientesPasso(int id_passo) {
            Dictionary<Ingrediente, int> ingredientes = new Dictionary<Ingrediente, int>();

            List<IngredientesPasso> ips = _context_ip.IngredientesPasso.Where(ip => ip.passo_id == id_passo).ToList();

            foreach (var ip in ips) {
                Ingrediente i = _context_ing.Ingrediente.Find(ip.ingrediente_id);
                ingredientes.Add(i, ip.quantidade);

                Console.WriteLine("****** LISTA PASSO {2} = INGREDIENTE {0} - QUANTIDADE {1} **************", i.id_ingrediente, ip.quantidade, ip.passo_id);
            }

            return ingredientes;
        }
    }
}
