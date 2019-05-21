using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary
{
    public class HistoricoUtilizador
    {
        [Key]
        [Column("data_conf")]
        public DateTime data { get; set; }

        [Key]
        [Column("receita")]
        public int receita_id { get; set; }

        [Key]
        [Column("utilizador")]
        public int utilizador_id { get; set; }


        [NotMapped]
        public Receita receita { get; set; }

        [NotMapped]
        public Utilizador utilizador { get; set; }



    }

     public class HistoricoUtilizadorContext : DbContext
        {

            public HistoricoUtilizadorContext(DbContextOptions<UtilizadorReceitasPrefContext> options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<HistoricoUtilizador>().HasKey(h => new {h.data, h.utilizador_id, h.receita_id });

                modelBuilder.Entity<HistoricoUtilizador>()
                            .HasOne<Utilizador>(h => h.utilizador)
                            .WithMany(u => u.historico)
                            .HasForeignKey(h => h.utilizador_id)
                            .HasConstraintName("FKHistoricoU199099");

                modelBuilder.Entity<HistoricoUtilizador>()
                            .HasOne<Receita>(h => h.receita)
                            .WithMany(i => i.historico)
                            .HasForeignKey(uip => uip.receita_id)
                            .HasConstraintName("FKHistoricoU247281");
            }

            public DbSet<Receita> Receita { get; set; }
            public DbSet<Utilizador> Utilizador { get; set; }
            public DbSet<HistoricoUtilizador> Historico { get; set; }
        }
}
