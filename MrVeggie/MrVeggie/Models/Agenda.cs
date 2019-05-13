using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {


    public class Agenda {

        [Key]
        public int dia { get; set; }

        public char refeicao { get; set; }


        [Column("utilizador")]
        public int utilizador_id { get; set; }

        public Utilizador utilizador { get; set; }


        [Column("receita")]
        public int receita_id { get; set; }

        public Receita receita { get; set; }
    }


    public class AgendaContext : DbContext {



    }
}
