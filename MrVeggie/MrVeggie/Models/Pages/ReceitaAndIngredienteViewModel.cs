using MrVeggie.Models;
using System.Collections.Generic;


namespace MrVeggie.Models.Pages {

    public class ReceitaAndIngredienteViewModel {

        public IEnumerable<Ingrediente> Ingredientes { get; set; }
        public IEnumerable<Receita> receitas { get; set; }
    }
}