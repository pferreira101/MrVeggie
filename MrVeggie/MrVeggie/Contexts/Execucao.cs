using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Execucao {

        private readonly PassoContext _context_p;
        private readonly IngredientesPassoContext _context_ip;
        private readonly IngredienteContext _context_ing;
        private readonly ReceitaContext _context_r;



        /// <summary>
        /// Construtor da classe Execucao
        /// </summary>
        /// <param name="context_p">Contexto dos passos</param>
        /// <param name="context_ip">Contexto dos ingredientes do passo</param>
        /// <param name="context_i">Contexto dos ingredientes</param>
        /// <param name="context_r">Contexto das receitas</param>
        public Execucao(PassoContext context_p, IngredientesPassoContext context_ip, IngredienteContext context_i, ReceitaContext context_r) {
            _context_p = context_p;
            _context_ip = context_ip;
            _context_ing = context_i;
            _context_r = context_r;
        }



        /// <summary>
        /// Método que retorna os ingredientes e as respetivas quantidades de um passo
        /// </summary>
        /// <param name="id_passo">ID do passo</param>
        /// <returns>Ingredintes e as suas quantidades</returns>
        public Dictionary<Ingrediente, Quantidade> getIngredientes(int id_passo) {
            Dictionary<Ingrediente, Quantidade> ingredientes = new Dictionary<Ingrediente, Quantidade>();

            List<IngredientesPasso> ips = _context_ip.IngredientesPasso.Where(ip => ip.passo_id == id_passo).ToList();

            foreach (var ip in ips) {
                Ingrediente i = _context_ing.Ingrediente.Find(ip.ingrediente_id);
                string unidade = _context_ip.Unidade.Find(ip.unidade_id).desc;
                Quantidade q = new Quantidade(ip.quantidade, unidade);

                ingredientes.Add(i, q);
            }

            return ingredientes;
        }



        /// <summary>
        /// Método que retorna toda a informação sobre um passo, sabendo a que receita pertence e qual o seu número
        /// </summary>
        /// <param name="id_receita">ID da receita</param>
        /// <param name="nr_passo">Número do passo</param>
        /// <returns>Pass completo</returns>
        public Passo getDetalhesPasso(int id_receita, int nr_passo) {
            Passo passo = _context_p.Passo.Where(p => (p.receita_id == id_receita) && (p.nr == nr_passo)).ToArray()[0];

            passo.ingredientes = getIngredientes(passo.id_passo);
            passo.operacao = _context_p.Operacao.Find(passo.operacao_id);

            if (passo.sub_receita_id != null) {
                passo.sub_receita = _context_r.Receita.Find(passo.sub_receita_id);
            }

            return passo;
        }
    }
}
