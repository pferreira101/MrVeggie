using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class UtilizadorIngredientesPref {

        [NotMapped]
        public Ingrediente Ingrediente { get; set; }

    
        [Column("ingrediente")]
        //[ForeignKey("Ingrediente")]
        public int ingrediente_id { get; set; }

        [NotMapped]
        public Utilizador Utilizador { get; set; }

        [Column("utilizador")]
        //[ForeignKey("Utilizador")]
        public int utilizador_id { get; set; }

        
    }


    public class UtilizadorIngredientesPrefContext : DbContext {

        public UtilizadorIngredientesPrefContext(DbContextOptions<UtilizadorIngredientesPrefContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtilizadorIngredientesPref>().HasKey(uip => new { uip.utilizador_id, uip.ingrediente_id });

            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });

            modelBuilder.Entity<UtilizadorIngredientesPref>()
                        .HasOne<Utilizador>(uip => uip.Utilizador)
                        .WithMany(u => u.utilizador_ingredientes_pref)
                        .HasForeignKey(uip => uip.utilizador_id)
                        .HasConstraintName("FKUtilizador595656");

            modelBuilder.Entity<UtilizadorIngredientesPref>()
                        .HasOne<Ingrediente>(uip => uip.Ingrediente)
                        .WithMany(i => i.utilizadores_pref)
                        .HasForeignKey(uip => uip.ingrediente_id)
                        .HasConstraintName("FKUtilizador467783");
        }

        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<UtilizadorIngredientesPref> UtilizadorIngredientesPref { get; set; }
    }

}
