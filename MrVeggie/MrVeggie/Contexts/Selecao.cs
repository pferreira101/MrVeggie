using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Selecao {

        private readonly ReceitaContext _context_r;
        private readonly IngredienteContext _context_ing;
        private readonly IngredientesPassoContext _context_ip;
        private readonly UtilizadorContext _context_u;
        private readonly AgendaContext _context_a;



        /// <summary>
        /// Construtor do objecto da classe Selecao
        /// </summary>
        /// <param name="context_r">Contexto das receitas</param>
        /// <param name="context_ing">Contexto dos ingredientes</param>
        /// <param name="context_ip">Contexto dos IngredientesPasso</param>
        /// <param name="context_u">Contexto do utilizador</param>
        /// <param name="context_a">Contexto da agenda</param>
        public Selecao(ReceitaContext context_r, IngredienteContext context_ing, IngredientesPassoContext context_ip, UtilizadorContext context_u, AgendaContext context_a) {
            _context_r = context_r;
            _context_ing = context_ing;
            _context_ip = context_ip;
            _context_u = context_u;
            _context_a = context_a;
        }



        /// <summary>
        /// Método que devolve a lista de todos as receitas presentes no sistema
        /// </summary>
        /// <returns>Array com todas as receitas</returns>
        public Receita[] getReceitas() {
            return _context_r.Receita.ToArray();
        }


        /// <summary>
        /// Método que faz o filtro das receitas que contenham os ingredientes (ids) passados como argumento
        /// </summary>
        /// <param name="s">ids dos ingredientes</param>
        /// <returns>Lista de receitas filtradas</returns>
        public Receita[] getReceitas(IEnumerable<int> s)
        {
            List<Receita> receitas = _context_r.Receita.ToList();
            List<Receita> result = new List<Receita>(receitas);



            foreach (Receita r in receitas)
            {

                List<Passo> passos = _context_r.Passo.Where(p => p.receita_id == r.id_receita).ToList();
                List<int> ings = new List<int>();
                foreach (Passo p in passos)
                {
                    ings.AddRange(_context_ip.IngredientesPasso.Where(ip => ip.passo_id == p.id_passo).Select( i => i.ingrediente_id).ToList());                    
                }
                
                if (ings.Intersect(s).Count() == 0)
                {
                    result.Remove(r);
                }
            }

            return result.ToArray();
        }

       
        /// <summary>
        /// Método que devolve a lista de todos os ingredientes presentes no sistema
        /// </summary>
        /// <returns>Array com todos os ingredientes</returns>
        public Ingrediente[] getIngredientes() {
            return _context_ing.Ingrediente.ToArray();
        }



        /// <summary>
        /// Método que devolve a lista das receitas favoritas de um utilizador dado o seu email
        /// </summary>
        /// <param name="email">Email do utilizador</param>
        /// <returns>Lista das receitas preferidas</returns>
        public List<Receita> getUtilizadorReceitasPref(string email) {
            List<Receita> receitas = new List<Receita>();
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            List<UtilizadorReceitasPref> receitas_ids = _context_u.UtilizadorReceitasPref.Where(urp => urp.utilizador_id == id_utilizador).ToList();

            foreach (var urp in receitas_ids) {
                receitas.Add(_context_r.Receita.Find(urp.receita_id));
            }

            return receitas;
        }



        /// <summary>
        /// Método que retorna a lista dos ids das receitas que contenham uma substring no seu nome
        /// </summary>
        /// <param name="nome">Nome a verificar</param>
        /// <returns>Lista dos ids das receitas filtradas</returns>
        public int[] getReceitas(string nome){

            return _context_r.Receita.Where(r => r.nome.Contains(nome)).Select(r => r.id_receita).ToArray();
        }



        /// <summary>
        /// Método que dada uma lista de ids de ingredientes e o email do utilizador, adiciona os ingredientes à lista de preferências do utilizador.
        /// </summary>
        /// <param name="ids">Lista dos ids dos ingredientes</param>
        /// <param name="email">Email do utilizador</param>
        public void setUtilizadorIngredientesPref(int[] ids, string email) {
            Utilizador utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First();

            for (int i = 0; i < ids.Length; i++) {
                UtilizadorIngredientesPref uip = new UtilizadorIngredientesPref();
                uip.ingrediente_id = ids[i];
                uip.utilizador_id = utilizador.id_utilizador;
                _context_u.UtilizadorIngredientesPref.Add(uip); // TRATAR DOS REPETIDOS?? --------------------------------------------------------------
            }

            utilizador.config_inicial = true;
            _context_u.Update(utilizador);

            _context_u.SaveChanges();
        }



        /// <summary>
        /// Método que dado o email do utilizador retorna a lista dos seu ingredientes preferidos.
        /// </summary>
        /// <param name="email">Email do utilizador</param>
        /// <returns>Lista dos ingredientes preferidos</returns>
        public List<Ingrediente> getUtilizadorIngredientesPref(string email) {
            List<Ingrediente> ingredientes = new List<Ingrediente>();
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            List<UtilizadorIngredientesPref> ingredientes_ids = _context_u.UtilizadorIngredientesPref.Where(uip => uip.utilizador_id == id_utilizador).ToList();

            foreach (var uip in ingredientes_ids) {
                ingredientes.Add(_context_ing.Ingrediente.Find(uip.ingrediente_id));
            }

            return ingredientes;
        }



        /// <summary>
        /// Função que verifica se é possível adicionar ou não uma receita na agenda de um utilizador numa determinada data.
        /// </summary>
        /// <param name="dia">Dia da semana</param>
        /// <param name="refeicao">'a'-almoço / 'j'-jantar</param>
        /// <param name="email">Email do utilizador</param>
        /// <returns>true - se puder puder inserir na data</returns>
        public bool verificaAgenda(int dia, char refeicao, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            if (_context_a.Agenda.Find(dia, refeicao, id_utilizador) == null) return true;
            else return false; 
        }



        /// <summary>
        /// Método que marca a refeição numa data na agenda de um determinado utilizador
        /// </summary>
        /// <param name="dia">Dia da semana</param>
        /// <param name="refeicao">'a'-almoço / 'j'-jantar</param>
        /// <param name="id_receita">Receita a inserir</param>
        /// <param name="email">Email do utilizador</param>
        public void marcaAgenda(int dia, char refeicao, int id_receita, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            Agenda agenda = new Agenda(dia, refeicao, id_utilizador, id_receita);

            Agenda test = _context_a.Agenda.Find(dia, refeicao, id_utilizador);
            if (test != null) _context_a.Agenda.Remove(test);

            _context_a.Agenda.Add(agenda);
            _context_a.SaveChanges();
        }



        /// <summary>
        /// Método que adiciona uma determinada receita aos favoritos de um utilizador
        /// </summary>
        /// <param name="id_receita">Receita a inserir</param>
        /// <param name="email">Email do utilizador</param>
        public void adicionaReceitaFavoritos(int id_receita, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            UtilizadorReceitasPref urp = new UtilizadorReceitasPref {
                receita_id = id_receita,
                utilizador_id = id_utilizador
            };

            if (_context_u.UtilizadorReceitasPref.Find(id_utilizador, id_receita) == null) {
                _context_u.UtilizadorReceitasPref.Add(urp);
                _context_u.SaveChanges();
            }
        }



        /// <summary>
        /// Método que adiciona um determinado ingrediente aos favoritos de um utilizador
        /// </summary>
        /// <param name="id_ingrediente">Ingrediente a inserir</param>
        /// <param name="email">Email do utilizador</param>
        public void adicionaIngredienteFavoritos(int id_ingrediente, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            UtilizadorIngredientesPref uip = new UtilizadorIngredientesPref {
                ingrediente_id = id_ingrediente,
                utilizador_id = id_utilizador
            };

            _context_u.UtilizadorIngredientesPref.Add(uip);
            _context_u.SaveChanges();
        }
    }
}
