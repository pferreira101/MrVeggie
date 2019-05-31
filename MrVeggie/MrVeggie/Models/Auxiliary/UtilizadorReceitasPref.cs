using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class UtilizadorReceitasPref {

        [NotMapped]
        public Receita receita { get; set; }

        [Column("receita")]
        public int receita_id { get; set; }

        [NotMapped]
        public Utilizador utilizador { get; set; }


        [Column("utilizador")]
        public int utilizador_id { get; set; }


    }


    public class UtilizadorReceitasPrefContext : DbContext {

        public UtilizadorReceitasPrefContext(DbContextOptions<UtilizadorReceitasPrefContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtilizadorReceitasPref>().HasKey(urp => new { urp.utilizador_id, urp.receita_id });

            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });

            modelBuilder.Entity<UtilizadorReceitasPref>()
                        .HasOne<Utilizador>(urp => urp.utilizador)
                        .WithMany(u => u.utilizador_receitas_pref)
                        .HasForeignKey(uip => uip.utilizador_id)
                        .HasConstraintName("FKUtilizador922587");

            modelBuilder.Entity<UtilizadorReceitasPref>()
                        .HasOne<Receita>(urp => urp.receita)
                        .WithMany(i => i.utilizadores_pref)
                        .HasForeignKey(uip => uip.receita_id)
                        .HasConstraintName("FKUtilizador874405");
        }

        public DbSet<Receita> Receita { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<UtilizadorReceitasPref> UtilizadorReceitasPref { get; set; }
    }

}
