using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts
{

    public class Sugestao
    {
        private readonly ReceitaContext _context_r;
        private readonly UtilizadorContext _context_u;

        public Sugestao(ReceitaContext context_r, UtilizadorContext context_u)
        {
            _context_r = context_r;
            _context_u = context_u;
        }

        public Receita[] getReceitasSugeridas(int idUtilizador)
        {
            List<Receita> r = new List<Receita>(4);

            //r.Add(getReceitaSugeridaPorHistorico(idUtilizador));
            r.Add(getReceitaSugeridaPorReceitasFav(idUtilizador));
            //r.Add(getReceitaSugeridaPorIngredientesFav(idUtilizador));
            //r.Add(getReceitaSugeridaPorHistorico(idUtilizador));



            return r.ToArray();
        }

        private Receita getReceitaSugeridaPorReceitasFav(int idUtilizador)
        {
            List<int> rIds = _context_u.UtilizadorReceitasPref.Where(u => u.utilizador_id == idUtilizador).Select(u => u.receita_id).ToList();

            List<Receita> receitas = _context_r.Receita.Where(r => rIds.Contains(r.id_receita)).ToList();

            Receita x = receitas.First();

            return x;
        }

        private Receita getReceitaSugeridaPorIngredientesFav(int idUtilizador)
        {
            throw new NotImplementedException();
        }

        private Receita getReceitaSugeridaPorHistorico(int idUtilizador)
        {
            
            throw new NotImplementedException();
        }
    }
}
