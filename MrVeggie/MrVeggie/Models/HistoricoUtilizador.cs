using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class HistoricoUtilizador {

        [NotMapped]
        public Utilizador utilizador { get; set; }

        [Key]
        [Column("utilizador")]
        public int utilizador_id { get; set; }

        [NotMapped]
        public Receita receita { get; set; }

        [Column("receita")]
        public int receita_id { get; set; }

        [Required]
        public DateTime data_conf { get; set; }

        public int avaliacao { get; set; }
    }

    public class HistoricoUtilizadorContext : DbContext {

        public HistoricoUtilizadorContext(DbContextOptions<HistoricoUtilizadorContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<HistoricoUtilizador>().HasKey(hu => new { hu.utilizador_id, hu.receita_id, hu.data_conf });

            modelBuilder.Entity<HistoricoUtilizador>()
                        .HasOne<Utilizador>(hu => hu.utilizador)
                        .WithMany(u => u.historico)
                        .HasForeignKey(hu => hu.utilizador_id)
                        .HasConstraintName("FKHistoricoU199099");

            modelBuilder.Entity<HistoricoUtilizador>()
                        .HasOne<Receita>(hu => hu.receita)
                        .WithMany(u => u.historico)
                        .HasForeignKey(hu => hu.receita_id)
                        .HasConstraintName("FKHistoricoU247281");

        }

        public DbSet<Receita> Receita { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<HistoricoUtilizador> HistoricoUtilizador { get; set; }

    }
}
