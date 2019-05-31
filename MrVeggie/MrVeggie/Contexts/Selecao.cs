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

        public Selecao(ReceitaContext context_r, IngredienteContext context_ing, IngredientesPassoContext context_ip, UtilizadorContext context_u, AgendaContext context_a) {
            _context_r = context_r;
            _context_ing = context_ing;
            _context_ip = context_ip;
            _context_u = context_u;
            _context_a = context_a;
        }

        public Receita[] getReceitas() {
            return _context_r.Receita.ToArray();
        }

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

        

        public Ingrediente[] getIngredientes() {
            return _context_ing.Ingrediente.ToArray();
        }





        // fazer este caso para quando não está ninguem logado
        public Receita[] getSugestoes() {
            List<Receita> receitas = new List<Receita>();

            Random random = new Random();
            
   

            for (int i = 0; i < 5; i++) {
                
            }

            _context_r.Receita.Find(1);

            return receitas.ToArray();
        }


        public List<Receita> getUtilizadorReceitasPref(int id) {
            List<Receita> receitas = new List<Receita>();

            List<UtilizadorReceitasPref> receitas_ids = _context_u.UtilizadorReceitasPref.Where(uip => uip.utilizador_id == id).ToList();

            foreach (var uip in receitas_ids) {
                receitas.Add(_context_r.Receita.Find(uip.receita_id));
            }

            return receitas;
        }

        public int[] getReceitas(string nome){

            return _context_r.Receita.Where(r => r.nome.Contains(nome)).Select(r => r.id_receita).ToArray();
        }




        public void setUserIngrPrefs(int[] ids, string email) {
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






        // Retorna true se poder inserir na data
        public bool verificaAgenda(int dia, char refeicao, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            if (_context_a.Agenda.Find(dia, refeicao, id_utilizador) == null) return true;
            else return false; 
        }



        public void marcaAgenda(int dia, char refeicao, int id_receita, string email) {
            int id_utilizador = _context_u.Utilizador.Where(u => u.email.Equals(email)).First().id_utilizador;

            Agenda agenda = new Agenda(dia, refeicao, id_utilizador, id_receita);

            Agenda test = _context_a.Agenda.Find(dia, refeicao, id_utilizador);
            if (test != null) _context_a.Agenda.Remove(test);

            _context_a.Agenda.Add(agenda);
            _context_a.SaveChanges();
        }



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

    }
}
