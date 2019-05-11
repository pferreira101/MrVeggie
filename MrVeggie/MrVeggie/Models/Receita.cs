using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Receita {

        [Key]
        public int id_receita { set; get; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(500)]
        public string desc { set; get; }

        [Required]
        [Display(Name = "Tempo de Confeção")]
        public float tempo_conf { set; get; }

        [Required]
        [Display(Name = "Avaliação")]
        public float avaliacao { set; get; }

        [Required]
        [Display(Name = "Número de avaliações")]
        public int n_avaliacoes { set; get; }

        [Required]
        [Display(Name = "Dificuldade")]
        public int dificuldade { set; get; }

        [Required]
        [Display(Name = "Calorias")]
        public int calorias { set; get; }

        [Required]
        [Display(Name = "Número de pessoas")]
        public int n_pessoas { set; get; }

        [Required]
        [Display(Name = "Imagem")]
        [StringLength(100)]
        public string url_imagem { set; get; }



        public IList<Passo> passos { get; set; }


        [NotMapped]
        public IDictionary<Ingrediente, int> ingredientes { get; set; }

        
    }

    public class ReceitaContext : DbContext {

        public ReceitaContext(DbContextOptions<ReceitaContext> options) : base(options) {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // configures one-to-many relationship
            modelBuilder.Entity<Passo>()
                        .HasOne<Receita>(p => p.receita)     
                        .WithMany(r => r.passos)
                        .HasForeignKey(p => p.receita_id)
                        .HasConstraintName("FKPasso200762");
        }



        public DbSet<Receita> Receita { get; set; }
        public DbSet<Passo> Passo { get; set; }

    }

}


