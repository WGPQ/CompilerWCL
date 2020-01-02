using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Sintactico
{
    class Transicion
    {
        public int estado { get; set; }
        public char simbolo { get; set; }
        public int movimiento { get; set; }

        public Transicion(int estado, char simbolo, int movimiento)
        {
            this.estado = estado;
            this.simbolo = simbolo;
            this.movimiento = movimiento;
        }
    }
}
