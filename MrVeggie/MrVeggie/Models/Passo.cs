using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CognitiveServices.Speech;



namespace MrVeggie.Models
{

    public class Passo
    {


        [Key]
        public int id_passo { set; get; }

        [Required]
        [Display(Name = "Número")]
        public int nr { set; get; }


        [Required]
        [Display(Name = "Tempo")]
        public float tempo { set; get; }

        [Required]
        [Display(Name = "Descrição")]
        public string desc { set; get; }

        [Required]
        [Column("operacao")]
        public int operacao_id { set; get; }

        [NotMapped]
        public Operacao operacao { get; set; }


        [Required]
        [Column("receita")]
        public int receita_id { set; get; }

        [NotMapped]
        [JsonIgnore]
        public Receita receita { get; set; }

        
        [Column("sub_receita")]
        public int? sub_receita_id { set; get; }

        [NotMapped]
        [JsonIgnore]
        public Receita sub_receita { get; set; }
      

        public bool ultimo { get; set; }

        public ICollection<IngredientesPasso> ingredientes_passo { get; set; }


        [NotMapped]
        public Dictionary<Ingrediente, Quantidade> ingredientes { get; set; }


    }

        public class PassoContext : DbContext
        {

            public PassoContext(DbContextOptions<PassoContext> options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                // configures one-to-many relationship
                modelBuilder.Entity<Passo>()
                            .HasOne<Operacao>(p => p.operacao)
                            .WithMany(op => op.passos)
                            .HasForeignKey(p => p.operacao_id)
                            .HasConstraintName("FKPasso568056");
            }

            public DbSet<Passo> Passo { get; set; }
            public DbSet<IngredientesPasso> IngredientesPassos { get; set; }
            public DbSet<Operacao> Operacao { get; set; }

        }
}
