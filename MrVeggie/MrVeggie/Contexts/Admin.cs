using Microsoft.Extensions.Primitives;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Models.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Admin {

        private IngredienteContext _context_i;
        private UtilizadorContext _context_u;
        private ReceitaContext _context_r;
        private OperacaoContext _context_op;
        private UtensilioContext _context_uten;
        private IngredientesPassoContext _context_ip;



        /// <summary>
        /// Construtor da classe Admin
        /// </summary>
        /// <param name="context_i">Contexto dos ingredientes</param>
        /// <param name="context_u">Contexto dos utilizadores</param>
        /// <param name="context_r">Contexto das receitas</param>
        /// <param name="context_op">Contexto das operações</param>
        /// <param name="context_uten">Contexto dos utensílios</param>
        /// <param name="context_ip">Contexto dos ingredientes passo</param>
        public Admin(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, OperacaoContext context_op, UtensilioContext context_uten, IngredientesPassoContext context_ip) {
            _context_i = context_i;
            _context_u = context_u;
            _context_r = context_r;
            _context_op = context_op;
            _context_uten = context_uten;
            _context_ip = context_ip;
        }


        /// <summary>
        /// Método que regista um ingrediente dado o seu nome e o URL da imagem.
        /// </summary>
        /// <param name="nome">Nome da receita</param>
        /// <param name="url">URL da imagem</param>
        public void registaIngrediente(string nome, string url) {
            Ingrediente ing = new Ingrediente {
                nome = nome,
                url_imagem = url
            };

            _context_i.Ingrediente.Add(ing);
            _context_i.SaveChanges();
        }



        /// <summary>
        /// Método que retorna a lista dos utensílios disponíveis.
        /// </summary>
        /// <returns>Lista dos utensílios</returns>
        public List<Utensilio> getUtensilios() {
            return _context_uten.Utensilio.ToList();
        }



        /// <summary>
        /// Método que retorna a lista dos utensílios cujo o ID esteja contido na lista argumento.
        /// </summary>
        /// <param name="ids">Lista dos IDs dos utensílios</param>
        /// <returns>Lista dos respetivos utensílios</returns>
        public List<Utensilio> getUtensilios(Microsoft.Extensions.Primitives.StringValues ids) {
            return _context_uten.Utensilio.Where(u => ids.Contains(u.id_utensilio.ToString())).ToList();
        }



        /// <summary>
        /// Método que retornas as estatísticas do crescimento do programa.
        /// </summary>
        /// <returns>Estatísticas</returns>
        public Estatistica getEstatistica() {

            int nr_utilizadores = _context_u.Utilizador.Count();
            int nr_masculino = _context_u.Utilizador.Where(u => u.sexo.Equals("Masculino")).Count();
            int nr_feminino = nr_utilizadores - nr_masculino;
            int nr_receitas = _context_r.Receita.Count();
            int nr_ingredientes = _context_i.Ingrediente.Count();

            DateTime today = DateTime.Now;
            DateTime month_ago = today.AddMonths(-1);
            int registos_ultimo_mes = _context_u.Utilizador.Where(u => u.data_reg.Date >= month_ago.Date).Count();

            return new Estatistica(nr_utilizadores, nr_masculino, nr_feminino, nr_receitas, nr_ingredientes, registos_ultimo_mes);
        }



        /// <summary>
        /// Método que retorna as unidades presentes no sistema.
        /// </summary>
        /// <returns>Lista das unidades</returns>
        public List<Unidade> getUnidades() {
            return _context_ip.Unidade.ToList();
        }



        /// <summary>
        /// Método que retorna os ingredientes presentes no sistema.
        /// </summary>
        /// <returns>Lista dos ingredientes</returns>
        public List<Ingrediente> getIngredientes() {
            return _context_i.Ingrediente.ToList();
        }



        /// <summary>
        /// Método que retorna a lista dos ingredientes cujo ID esteja presente na lista argumento.
        /// </summary>
        /// <param name="ids">IDs a verificar</param>
        /// <returns>Lista de ingredientes filtrada</returns>
        public List<Ingrediente> getIngredientes(StringValues ids) {
            return _context_i.Ingrediente.Where(i => ids.Contains(i.id_ingrediente.ToString())).ToList();
        }



        /// <summary>
        /// Método que regista um novo passo para uma receita.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unidades"></param>
        /// <param name="quantidades"></param>
        public void registaPasso(NewPasso model, StringValues unidades, StringValues quantidades) {
            _context_r.Passo.Add(model.passo);

            _context_r.SaveChanges();

            float quantidade;
            int x = 0;
            foreach (Ingrediente i in model.ingredientes) {
                quantidade = float.Parse(quantidades.ElementAt(x), CultureInfo.InvariantCulture);

                _context_ip.Add(new IngredientesPasso {
                    passo_id = model.passo.id_passo,
                    ingrediente_id = i.id_ingrediente,
                    unidade_id = int.Parse(unidades.ElementAt(x++)),
                    quantidade = quantidade

                });
            }

            _context_ip.SaveChanges();
        }



        /// <summary>
        /// Método que regista uma nova operação no sistema.
        /// </summary>
        /// <param name="op">Operação a adicionar</param>
        public void registaOperacao(Operacao op) {
            _context_op.Add(op);
            _context_op.SaveChanges();
        }



        /// <summary>
        /// Método que regista como último um passo de uma receita
        /// </summary>
        /// <param name="id_receita">ID da receita</param>
        /// <param name="nPasso">Nr do último passo</param>
        public void finalizaReceita(int id_receita, int nPasso) {
            var passo = _context_r.Passo.First(p => p.receita_id == id_receita && p.nr == nPasso - 1);
            passo.ultimo = true;
            _context_r.Passo.Update(passo);
            _context_r.SaveChanges();
        }



        /// <summary>
        /// Método que regista uma receita juntamente com os seus utensílios
        /// </summary>
        /// <param name="r">Receita a registar</param>
        /// <param name="utensilios">Utensílios da receita</param>
        public void registaReceita(Receita r, List<Utensilio> utensilios) {

            _context_r.Receita.Add(r);

            _context_r.SaveChanges();

            if (utensilios != null) {
                foreach (Utensilio u in utensilios) {
                    _context_r.UtensiliosReceita.Add(new UtensiliosReceita { receita_id = r.id_receita, utensilio_id = u.id_utensilio });
                }
            }
            _context_r.SaveChanges();
        }



        /// <summary>
        /// Método que retorna a lista de todas as receitas do sistema.
        /// </summary>
        /// <returns>Lista de receitas</returns>
        public List<Receita> getReceitas() {
            return _context_r.Receita.ToList();
        }



        /// <summary>
        /// Método que retorna a lista de todas as receitas filtradas pelos IDs da string argumento.
        /// </summary>
        /// <param name="ids">Lista de IDs das receitas</param>
        /// <returns>Lista de receitas filtradas</returns>
        public List<Receita> getReceitas(Microsoft.Extensions.Primitives.StringValues ids) {
            return _context_r.Receita.Where(r => ids.Contains(r.id_receita.ToString())).ToList();
        }



        /// <summary>
        /// Método que retorna a lista de Operações do sistema
        /// </summary>
        /// <returns>Lista de operações</returns>
        public List<Operacao> getOperacoes() {
            return _context_op.Operacao.ToList();
        }



        /// <summary>
        /// Método que retorna a lista de operações filtradas pelos IDs da string argumento.
        /// </summary>
        /// <param name="ids">Lista de IDs das operações</param>
        /// <returns>Lista de operações filtradas</returns>
        public List<Operacao> getOperacoes(Microsoft.Extensions.Primitives.StringValues ids) {
            return _context_op.Operacao.Where(op => ids.Contains(op.id_op.ToString())).ToList();
        }
    }
}
