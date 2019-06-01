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

        public Admin(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, OperacaoContext context_op, UtensilioContext context_uten, IngredientesPassoContext context_ip) {
            _context_i = context_i;
            _context_u = context_u;
            _context_r = context_r;
            _context_op = context_op;
            _context_uten = context_uten;
            _context_ip = context_ip;
        }


        public void registaIngrediente(string nome, string url) {
            Ingrediente ing = new Ingrediente {
                nome = nome,
                url_imagem = url
            };

            _context_i.Ingrediente.Add(ing);
            _context_i.SaveChanges();
        }

        public List<Utensilio> getUtensilios()
        {
            return _context_uten.Utensilio.ToList();
        }

        public List<Utensilio> getUtensilios(Microsoft.Extensions.Primitives.StringValues stringValues) {
            return _context_uten.Utensilio.Where(u => stringValues.Contains(u.id_utensilio.ToString()) ).ToList();
        }

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

        public List<Unidade> getUnidades()
        {
            return _context_ip.Unidade.ToList();  
        }

        public List<Ingrediente> getIngredientes() {
            return _context_i.Ingrediente.ToList();
        }

        internal List<Ingrediente> getIngredientes(StringValues stringValues)
        {
            return _context_i.Ingrediente.Where(i => stringValues.Contains(i.id_ingrediente.ToString())).ToList();
        }

        public void registaPasso(NewPasso model, StringValues unidades, StringValues quantidades)
        {
            _context_r.Passo.Add(model.passo);

            _context_r.SaveChanges();

            float quantidade;

            

            int x = 0;
            foreach (Ingrediente i in model.ingredientes)
            {
                
                quantidade = float.Parse(quantidades.ElementAt(x), CultureInfo.InvariantCulture);
               

                _context_ip.Add(new IngredientesPasso
                {
                    passo_id = model.passo.id_passo,
                    ingrediente_id = i.id_ingrediente,
                    unidade_id = int.Parse(unidades.ElementAt(x++)),
                    quantidade = quantidade

                });
            }

            _context_ip.SaveChanges();
        }

        public void finalizaReceita(int id_receita, int nPasso)
        {
            var passo = _context_r.Passo.First(p => p.receita_id == id_receita && p.nr == nPasso-1);
            passo.ultimo = true;
            _context_r.Passo.Update(passo);
            _context_r.SaveChanges();
        }

        public int getNewReceitaID(string nome) {
            return _context_r.Receita.Where(r => r.nome.Equals(nome)).First().id_receita;
        }

        public void registaReceita(Receita r, List<Utensilio> utensilios) {

            _context_r.Receita.Add(r);

            _context_r.SaveChanges();

            if (utensilios != null)
            {
                foreach (Utensilio u in utensilios)
                {
                    _context_r.UtensiliosReceita.Add(new UtensiliosReceita { receita_id = r.id_receita, utensilio_id = u.id_utensilio });
                }
            }
            _context_r.SaveChanges();
        }

        public List<Receita> getReceitas()
        {
            return _context_r.Receita.ToList();
        }

        public List<Receita> getReceitas(Microsoft.Extensions.Primitives.StringValues stringValues)
        {
            return _context_r.Receita.Where(r => stringValues.Contains(r.id_receita.ToString())).ToList();
        }

        public List<Operacao> getOperacoes()
        {
            return _context_op.Operacao.ToList();
        }

        public List<Operacao> getOperacoes(Microsoft.Extensions.Primitives.StringValues stringValues)
        {
            return _context_op.Operacao.Where(op => stringValues.Contains(op.id_op.ToString())).ToList();
        }
    }
}
