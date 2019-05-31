using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {


    public class Operacao {

        [Key]
        public int id_op { set; get; }

        [Required]
        [Display(Name = "Desc")]
        public String desc { set; get; }



        public IList<Passo> passos { get; set; }
    }


    
    public class OperacaoContext : DbContext {
        public OperacaoContext() {
        }

        public OperacaoContext(DbContextOptions<OperacaoContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });


        }
        public DbSet<Operacao> Operacao { get; set; }
    }
    
}
