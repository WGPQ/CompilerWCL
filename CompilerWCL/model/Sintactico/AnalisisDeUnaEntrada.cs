using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Sintactico
{
    class AnalisisDeUnaEntrada
    {
        public string pila { get; set; }
        public string entarda { get; set; }
        public string accion { get; set; }

        public AnalisisDeUnaEntrada(string pila, string entarda, string accion)
        {
            this.pila = pila;
            this.entarda = entarda;
            this.accion = accion;
        }
    }
}
