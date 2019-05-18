using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {
    
    public class IngredientesPasso {

        [NotMapped]
        [JsonIgnore]
        public Passo passo { get; set; }

        [Key]
        [Column("passo")]
        public int passo_id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Ingrediente ingrediente { get; set; }

        [Column("ingrediente")]
        public int ingrediente_id { get; set; }

        [Required]
        public float quantidade { get; set; }


        [NotMapped]
        public Unidade unidade { get; set; }

        [Column("unidade")]
        public int unidade_id { get; set; }

     
    }

    public class IngredientesPassoContext : DbContext {

        public IngredientesPassoContext(DbContextOptions<IngredientesPassoContext> options) : base(options) {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id});

            modelBuilder.Entity<IngredientesPasso>()
                        .HasOne<Ingrediente>(ip => ip.ingrediente)
                        .WithMany(i => i.ingredientes_passo)
                        .HasForeignKey(ip => ip.ingrediente_id)
                        .HasConstraintName("FKIngredient52444");

            modelBuilder.Entity<IngredientesPasso>()
                        .HasOne<Passo>(ip => ip.passo)
                        .WithMany(i => i.ingredientes_passo)
                        .HasForeignKey(ip => ip.passo_id)
                        .HasConstraintName("FKIngredient895049");

            modelBuilder.Entity<IngredientesPasso>()
                        .HasOne<Unidade>(ip => ip.unidade)
                        .WithMany(u => u.ingredientes_passos)
                        .HasForeignKey(ip => ip.unidade_id)
                        .HasConstraintName("FKIngredient7679");
                        
        }

        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Passo> Passo { get; set; }
        public DbSet<IngredientesPasso> IngredientesPasso { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
        
    }
    
}
