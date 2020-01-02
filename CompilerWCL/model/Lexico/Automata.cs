using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class Automata
    {
        public int estado { get; set; }
        public char leyendo { get; set; }
        public int newestado { get; set; }

        public Automata(int estado, char leyendo, int newestado)
        {
            this.estado = estado;
            this.leyendo = leyendo;
            this.newestado = newestado;
        }
    }
}
