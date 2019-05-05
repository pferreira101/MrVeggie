using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Utilizador {

        [Key]
        public int id_utilizador { set; get; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { set; get; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { set; get; }

        [Required]
        [Display(Name = "Idade")]
        public int idade { set; get; }

        [Required]
        [Display(Name = "Sexo")]
        public char sexo { get; set; }


    }



    public class UtilizadorContext : DbContext {

        public UtilizadorContext(DbContextOptions<UtilizadorContext> options) : base(options) {

        }

        public DbSet<Utilizador> utilizador { get; set; }
    }
}
