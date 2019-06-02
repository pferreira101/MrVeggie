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
        private readonly ReceitaContext _context_r;


        public Execucao(PassoContext context, IngredientesPassoContext context_ip, IngredienteContext context_i, ReceitaContext context_r) {
            _context_passo = context;
            _context_ip = context_ip;
            _context_ing = context_i;
            _context_r = context_r;
        }



        public Dictionary<Ingrediente, Quantidade> getIngredientes(int id_passo) {
            Dictionary<Ingrediente, Quantidade> ingredientes = new Dictionary<Ingrediente, Quantidade>();

            List<IngredientesPasso> ips = _context_ip.IngredientesPasso.Where(ip => ip.passo_id == id_passo).ToList();

            foreach (var ip in ips) {
                Ingrediente i = _context_ing.Ingrediente.Find(ip.ingrediente_id);
                string unidade = _context_ip.Unidade.Find(ip.unidade_id).desc;
                Quantidade q = new Quantidade(ip.quantidade, unidade);

                ingredientes.Add(i, q);

                Console.WriteLine("****** LISTA PASSO {2} = INGREDIENTE {0} - QUANTIDADE {1} **************", i.id_ingrediente, ip.quantidade, ip.passo_id);
            }

            return ingredientes;
        }


        public Passo getDetalhesPasso(int id_receita, int nr_passo) {
            Passo passo = _context_passo.Passo.Where(p => (p.receita_id == id_receita) && (p.nr == nr_passo)).ToArray()[0];

            //_context_passo.IngredientesPassos.Where(ip => ip.passo_id == id_passo)
            passo.ingredientes = getIngredientes(passo.id_passo);
            passo.operacao = _context_passo.Operacao.Find(passo.operacao_id);
            if (passo.sub_receita_id != null) {
                passo.sub_receita = _context_r.Receita.Find(passo.sub_receita_id);
            }
            return passo;
        }
    }
}
