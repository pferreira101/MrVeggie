using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class UtensiliosReceita {

        [NotMapped]
        [JsonIgnore]
        public Receita receita { get; set; }

        
        [Column("receita")]
        public int receita_id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Utensilio utensilio { get; set; }

       
        [Column("utensilio")]
        public int utensilio_id { get; set; }



    }

    public class UtensiliosReceitaContext : DbContext {

        public UtensiliosReceitaContext(DbContextOptions<UtensiliosReceitaContext> options) : base(options) {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtensiliosReceita>().HasKey(ut => new {
                ut.receita_id,
                ut.utensilio_id
            });

            modelBuilder.Entity<UtensiliosReceita>()
                        .HasOne<Utensilio>(ur => ur.utensilio)
                        .WithMany(u => u.utensilios_receita)
                        .HasForeignKey(ur => ur.utensilio_id)
                        .HasConstraintName("FKUtensilios678874");

            modelBuilder.Entity<UtensiliosReceita>()
                        .HasOne<Receita>(ur => ur.receita)
                        .WithMany(u => u.utensilios_receita)
                        .HasForeignKey(ur => ur.receita_id)
                        .HasConstraintName("FKUtensilios299728");
        }

        public DbSet<Utensilio> Utensilio { get; set; }
        public DbSet<Passo> Passo { get; set; }
        public DbSet<UtensiliosReceita> UtensiliosReceita { get; set; }

    }
}
