using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class Estatistica {

        public int nr_utilizadores { get; set; }
        public int nr_masculino { get; set; }
        public int nr_feminino { get; set; }
        public int nr_receitas { get; set; }
        public int nr_ingredientes { get; set; }
        public int registos_ultimo_mes { get; set; }

        public Estatistica(int nr_utilizadores, int nr_masculino, int nr_feminino, int nr_receitas, int nr_ingredientes, int registos_ultimo_mes) {
            this.nr_utilizadores = nr_utilizadores;
            this.nr_masculino = nr_masculino;
            this.nr_feminino = nr_feminino;
            this.nr_receitas = nr_receitas;
            this.nr_ingredientes = nr_ingredientes;
            this.registos_ultimo_mes = registos_ultimo_mes;
        }
    }
}
