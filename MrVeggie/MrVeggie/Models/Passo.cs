using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models{

    public class Passo {


        [Key]
        public int id_passo{ set; get; }

        [Required]
        [Display(Name = "Número")]
        public int nr { set; get; }


        [Required]
        [Display(Name = "Tempo")]
        public float tempo { set; get; }
/*
        [Required]
        [Column("operacao")]
        public int operacao_id { set; get; }

        [NotMapped]
        [JsonIgnore]
        public Operacao operacao { get; set; }
*/
        [Required]
        [Column("receita")]
        public int receita_id { set; get; }

        [NotMapped]
        [JsonIgnore]
        public Receita receita { get; set; }

/*
        [Column("sub_receita")]
        public int sub_receita_id { set; get; }

        [NotMapped]
        [JsonIgnore]
        public Receita sub_receita { get; set; }
*/

        public ICollection<IngredientesPasso> ingredientes_passo { get; set; }

        [NotMapped]
        public Dictionary<Ingrediente, int> ingredientes { get; set; }

        [NotMapped]
        public int nr_total_passos { get; set; }
    }
    
    public class PassoContext : DbContext {

        public PassoContext(DbContextOptions<PassoContext> options) : base(options) {

        }
        
        public DbSet<Passo> Passo { get; set; }
        public DbSet<IngredientesPasso> IngredientesPassos { get; set; }

    }
}
