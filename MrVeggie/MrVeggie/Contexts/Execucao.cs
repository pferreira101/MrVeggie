using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Execucao {

        private readonly PassoContext _context_passo;
        private readonly IngredientesPassoContext _context_ip;
        private readonly IngredienteContext _context_ing;


        public Execucao(PassoContext context, IngredientesPassoContext context_ip, IngredienteContext context_i) {
            _context_passo = context;
            _context_ip = context_ip;
            _context_ing = context_i;
        }



        public Dictionary<Ingrediente, int> getIngredientes(int id_passo) {
            Dictionary<Ingrediente, int> ingredientes = new Dictionary<Ingrediente, int>();

            List<IngredientesPasso> ips = _context_ip.IngredientesPasso.Where(ip => ip.passo_id == id_passo).ToList();

            foreach (var ip in ips) {
                Ingrediente i = _context_ing.Ingrediente.Find(ip.ingrediente_id);
                ingredientes.Add(i, ip.quantidade);

                Console.WriteLine("****** LISTA PASSO {2} = INGREDIENTE {0} - QUANTIDADE {1} **************", i.id_ingrediente, ip.quantidade, ip.passo_id);
            }

            return ingredientes;
        }


        public Passo getDetalhesPasso(int id_receita, int nr_passo) {
            Passo passo = _context_passo.Passo.Where(p => (p.receita_id == id_receita) && (p.nr == nr_passo)).ToArray()[0];

            //_context_passo.IngredientesPassos.Where(ip => ip.passo_id == id_passo)
            passo.ingredientes = getIngredientes(passo.id_passo);
            passo.nr_total_passos = _context_ip.IngredientesPasso.Where(ip => ip.passo_id == passo.id_passo).ToList().Count();
            passo.operacao = _context_passo.Operacao.Find(passo.operacao_id);

            return passo;
        }
    }
}
