using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Imagem")]
        [StringLength(300)]
        public string url_imagem { set; get; }



        public ICollection<IngredientesPasso> ingredientes_passo { get; set; }

        [ForeignKey("ingrediente_id")]
        public List<UtilizadorIngredientesPref> utilizadores_pref { get; set; }
    }

    public class IngredienteContext : DbContext {

        public IngredienteContext(DbContextOptions<IngredienteContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UtilizadorIngredientesPref>().HasKey(uip => new { uip.utilizador_id, uip.ingrediente_id });
            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });

        }
        public DbSet<Ingrediente> Ingrediente { get; set; }


    }
}
