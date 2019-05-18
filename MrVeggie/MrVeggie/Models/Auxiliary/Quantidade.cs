using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary {

    public class Quantidade {
        public Quantidade(float quantidade, string unidade) {
            this.quantidade = quantidade;
            this.unidade = unidade;
        }

        public float quantidade { get; set; }

        public string unidade { get; set; }

        public void add(float value) {
            this.quantidade += value;
        }
    }
}
