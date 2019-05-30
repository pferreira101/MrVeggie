using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models
{
    public class NewReceita
    {
        public Receita receita { get; set; }
        public List<Utensilio> utensilios { get; set; }
    }
}
