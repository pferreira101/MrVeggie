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

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { set; get; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { set; get; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Idade")]
        public int idade { set; get; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Sexo")]
        [StringLength(50)]
        public string sexo { get; set; }
        public static string[] Generos = new[] { "Masculino", "Feminino", "Outro" };
    

        [Display(Name = "Admin")]
        public bool admin { get; set; }

        [Display(Name = "Config")]
        public bool config_inicial { get; set; }

        
        [Display(Name = "Data Registo")]
        public DateTime data_reg { get; set; }






        [ForeignKey("utilizador_id")]
        public List<UtilizadorIngredientesPref> utilizador_ingredientes_pref { get; set; }


        [NotMapped]
        public List<Ingrediente> ingredientes_pref { get; set; }


        [ForeignKey("utilizador_id")]
        public List<UtilizadorReceitasPref> utilizador_receitas_pref { get; set; }


        [NotMapped]
        public List<Receita> receitas_pref { get; set; }


        [ForeignKey("utilizador_id")]
        public List<HistoricoUtilizador> historico { get; set; }

        [NotMapped]
        public List<Receita> historico_receitas { get; set; }



        [ForeignKey("utilizador_id")]
        public List<Agenda> agenda { get; set; }



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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtilizadorReceitasPref>().HasKey(urp => new { urp.utilizador_id, urp.receita_id });
            modelBuilder.Entity<UtilizadorIngredientesPref>().HasKey(uip => new { uip.utilizador_id, uip.ingrediente_id });

            modelBuilder.Entity<UtensiliosReceita>().HasKey(ut => new { ut.receita_id, ut.utensilio_id });

            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });

        }

        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<UtilizadorIngredientesPref> UtilizadorIngredientesPref { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<UtilizadorReceitasPref> UtilizadorReceitasPref { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<HistoricoUtilizador> HistoricoUtilizador { get; set; }


    }
}
