using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int avaliacao { set; get; }

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



        public ICollection<Passo> passos { get; set; }

    }

    public class ReceitaContext : DbContext {

        public ReceitaContext(DbContextOptions<ReceitaContext> options) : base(options) {

        }

        public DbSet<Receita> receita { get; set; }

    }
}

