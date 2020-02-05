using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Semantico
{
    class Cuadruplos
    {
        public int numero_tupla { get; set; }
        public string operador { get; set; }
        public string operando_1 { get; set; }
        public string operando_2 { get; set; }
        public string resultado { get; set; }

        public Cuadruplos(int numero_tupla, string operador, string operando_1, string operando_2, string resultado)
        {
            this.numero_tupla = numero_tupla;
            this.operador = operador;
            this.operando_1 = operando_1;
            this.operando_2 = operando_2;
            this.resultado = resultado;
        }

        public Cuadruplos()
        {
        }
    }
}
