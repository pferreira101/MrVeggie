using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Unidade {

        [Key]
        public int id_unidade { set; get; }

        [Required]
        [Display(Name = "Desc")]
        [StringLength(50)]
        public string desc { set; get; }

        public ICollection<IngredientesPasso> ingredientes_passos { get; set; }


    }

    public class UnidadeContext : DbContext {

        public UnidadeContext(DbContextOptions<UnidadeContext> options) : base(options) {

        }

        public DbSet<Unidade> Unidade { get; set; }


    }
}
