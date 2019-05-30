using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models
{
    public class NewPasso
    {
        public Passo passo { get; set; }
        public List<Operacao> operacoes { get; set; }
        public List<Receita> receitas { get; set; }
        public List<Ingrediente> ingredientes { get; set; }
        public int id_receita { get; set; }
        public int nPasso { get; set; }
    }
}
