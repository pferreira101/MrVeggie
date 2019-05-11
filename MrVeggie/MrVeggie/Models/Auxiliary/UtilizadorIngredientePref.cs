using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class UtilizadorIngredientePref {

        [NotMapped]
        public Ingrediente ingrediente { get; set; }

        [Key]
        [Column("ingrediente")]
        public int ingrediente_id { get; set; }

        [NotMapped]
        public Utilizador utilizador { get; set; }

        
        [Column("utilizador")]
        public int utilizador_id { get; set; }

        
    }


    public class UtilizadorIngredientePrefContext : DbContext {

        public UtilizadorIngredientePrefContext(DbContextOptions<UtilizadorIngredientePrefContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtilizadorIngredientePref>().HasKey(uip => new { uip.utilizador_id, uip.ingrediente_id });

            modelBuilder.Entity<UtilizadorIngredientePref>()
                        .HasOne<Utilizador>(uip => uip.utilizador)
                        .WithMany(u => u.utilizador_ingrediente_pref)
                        .HasForeignKey(uip => uip.utilizador_id)
                        .HasConstraintName("FKUtilizador595656");

            modelBuilder.Entity<UtilizadorIngredientePref>()
                        .HasOne<Ingrediente>(uip => uip.ingrediente)
                        .WithMany(i => i.utilizadores_pref)
                        .HasForeignKey(uip => uip.ingrediente_id)
                        .HasConstraintName("FKUtilizador467783");
        }

        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<UtilizadorIngredientePref> UtilizadorIngredientePref { get; set; }
    }

}
