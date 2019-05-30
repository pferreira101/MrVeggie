using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {


    public class Admin {

        private IngredienteContext _context_i;
        private UtilizadorContext _context_u;
        private ReceitaContext _context_r;
        private OperacaoContext _context_op;

        public Admin(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, OperacaoContext context_op) {
            _context_i = context_i;
            _context_u = context_u;
            _context_r = context_r;
            _context_op = context_op;
        }


        public void registaIngrediente(string nome, string url) {
            Ingrediente ing = new Ingrediente(); // passar logo no construtor ?
            ing.nome = nome; 
            ing.url_imagem = url;

            _context_i.Ingrediente.Add(ing);
            _context_i.SaveChanges();
        }

        public Estatistica getEstatistica() {

            int nr_utilizadores = _context_u.Utilizador.Count();
            int nr_masculino = _context_u.Utilizador.Where(u => u.sexo.Equals('m')).Count();
            int nr_feminino = nr_utilizadores - nr_masculino;
            int nr_receitas = _context_r.Receita.Count();
            int nr_ingredientes = _context_i.Ingrediente.Count();
            int registos_ultimo_mes = 0;


            return new Estatistica(nr_utilizadores, nr_masculino, nr_feminino, nr_receitas, nr_ingredientes, registos_ultimo_mes);
        }


        public void registaReceita(string nome, string desc, int dificuldade, float tempo_conf, int calorias, int n_pessoas, string url_imagem) {
            throw new NotImplementedException();
        }

        public List<Receita> getReceitas(){
            return _context_r.Receita.ToList();
        }

        public List<Operacao> getOperacoes(){
            return _context_op.Operacao.ToList();
        }
    }
}
