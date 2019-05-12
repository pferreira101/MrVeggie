using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Ingrediente {

        [Key]
        public int id_ingrediente { set; get; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }

        [Required]
        [Display(Name = "Calorias")]
        public float calorias { set; get; }



        public ICollection<IngredientesPasso> ingredientes_passo { get; set; }

        public List<UtilizadorIngredientesPref> utilizadores_pref { get; set; }
    }

    public class IngredienteContext : DbContext {

        public IngredienteContext(DbContextOptions<IngredienteContext> options) : base(options) {

        }

        public DbSet<Ingrediente> Ingrediente { get; set; }


    }
}
