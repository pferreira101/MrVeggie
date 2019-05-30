using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Utensilio {

        [Key]
        public int id_utensilio { set; get; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }


        [Required]
        [Display(Name = "Imagem")]
        [StringLength(300)]
        public string url_imagem { set; get; }

        [ForeignKey("utensilio_id")]
        public ICollection<UtensiliosReceita> utensilios_receita { get; set; }

        
    }

    public class UtensilioContext : DbContext {

        public UtensilioContext(DbContextOptions<UtensilioContext> options) : base(options) {

        }

        public DbSet<Utensilio> Utensilio { get; set; }

    }

}
