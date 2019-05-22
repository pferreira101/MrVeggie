using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Sugestao {

        private readonly ReceitaContext _context_r;
        private readonly UtilizadorContext _context_u;
        private readonly IngredientesPassoContext _context_ip;


        public Sugestao(ReceitaContext context_r, UtilizadorContext context_u, IngredientesPassoContext context_ip)
        {
            _context_r = context_r;
            _context_u = context_u;
            _context_ip = context_ip;
        }

        public List<Receita> getSugestoes() {
            List<Receita> r = new List<Receita>();

            r.Add(_context_r.Receita.Find(1));
            Console.WriteLine("********************************************************* {0}", r.Count());
            return r;
        }


        public List<Receita> getSugestoes(string email) {
            Utilizador utilizador = _context_u.Utilizador.Where(u => u.email == email).First();
            List<Receita> r = new List<Receita>(4);

            if (utilizador != null)
            {
                int id_utilizador = utilizador.id_utilizador;
                //r.Add(getReceitaSugeridaPorHistorico(id_utilizador));
                r.Add(getReceitaSugeridaPorReceitasFav(id_utilizador));
                r.Add(getReceitaSugeridaPorIngredientesFav(id_utilizador));
                //r.Add(getReceitaSugeridaPorHistorico(id_utilizador));
            }


            return r;
        }
        private Receita getReceitaSugeridaPorReceitasFav(int idUtilizador)
        {
            Receita x;

            List<int> rIds = _context_u.UtilizadorReceitasPref.Where(u => u.utilizador_id == idUtilizador).Select(u => u.receita_id).ToList();

            if (rIds.Count() == 0)
            {
                Random random = new Random();
                int rInt = random.Next(1, _context_r.Receita.ToArray().Count());

                x = _context_r.Receita.Where(r => r.id_receita == rInt).First();
            }
            else
            {
                List<Receita> receitas = _context_r.Receita.Where(r => rIds.Contains(r.id_receita)).ToList();

                Random random = new Random();
                int rInt = random.Next(0, receitas.Count() - 1);

                x = receitas.ElementAt(rInt);
            }

            

            return x;
        }

        private Receita getReceitaSugeridaPorIngredientesFav(int idUtilizador)
        {
            List<int> ingFav =  _context_u.UtilizadorIngredientesPref
                                                            .Where(uip => uip.utilizador_id == idUtilizador)
                                                            .Select(uip => uip.ingrediente_id).ToList()
                                                            .ToList();

            List<Receita> receitas = _context_r.Receita.ToList();
            List<(int, Receita)> sugestoes = new List<(int, Receita)>();


            foreach (Receita r in receitas)
            {

                List<Passo> passos = _context_r.Passo.Where(p => p.receita_id == r.id_receita).ToList();
                List<int> ings = new List<int>();
                foreach (Passo p in passos)
                {
                    ings.AddRange(_context_ip.IngredientesPasso.Where(ip => ip.passo_id == p.id_passo).Select(i => i.ingrediente_id).ToList());
                }
                sugestoes.Add((ings.Intersect(ingFav).Count(), r));

            }

            sugestoes.Sort((x1, x2) => x1.Item1.CompareTo(x2.Item1));

            Random random = new Random();
            int rInt = random.Next(1, Math.Min(4,sugestoes.Count()));

            return sugestoes.ElementAt(rInt).Item2;



        }

        private Receita getReceitaSugeridaPorHistorico(int id_utilizador) {
            
            throw new NotImplementedException();
        }
    }
}
