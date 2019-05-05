using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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



        public Receita receita { get; set; }

    }
}
