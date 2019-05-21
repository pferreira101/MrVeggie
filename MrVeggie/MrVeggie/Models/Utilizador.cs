using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        [Display(Name = "Sexo")]
        public bool admin { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public bool config_inicial { get; set; }



        [ForeignKey("utilizador_id")]
        public List<UtilizadorIngredientesPref> utilizador_ingredientes_pref { get; set; }


        [NotMapped]
        public List<Ingrediente> ingredientes_pref { get; set; }


        [ForeignKey("utilizador_id")]
        public List<UtilizadorReceitasPref> utilizador_receitas_pref { get; set; }



        [NotMapped]
        public List<Receita> receitas_pref { get; set; }











        public void print() {
            Console.WriteLine("\nUTILIZADOR ------------------------------------------");
            Console.WriteLine("ID = {0}", id_utilizador);
            Console.WriteLine("Email = {0}", email);
            Console.WriteLine("Admin = {0}", admin);
            Console.WriteLine("Config = {0}", config_inicial);

            Console.WriteLine("-------------------------------------------------------");
        }
    }



    public class UtilizadorContext : DbContext {

        public UtilizadorContext(DbContextOptions<UtilizadorContext> options) : base(options) {



        }



        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<UtilizadorIngredientesPref> UtilizadorIngredientesPref { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<UtilizadorReceitasPref> UtilizadorReceitasPref { get; set; }
        public DbSet<Receita> Receita { get; set; }


    }
}
