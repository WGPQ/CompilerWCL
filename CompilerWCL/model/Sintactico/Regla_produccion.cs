using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Sintactico
{
    class Regla_produccion
    {
        public int numero_regla { get; set; }
        public string part_derecha { get; set; }
        public char part_izquierda { get; set; }
        public int long_Part_derecha { get; set; }
        public Object dato { get; set; }
        public string tipo { get; set; }

        public Regla_produccion(int numero_regla, string part_derecha, char part_izquierda, int long_Part_derecha, object dato, string tipo)
        {
            this.numero_regla = numero_regla;
            this.part_derecha = part_derecha;
            this.part_izquierda = part_izquierda;
            this.long_Part_derecha = long_Part_derecha;
            this.dato = dato;
            this.tipo = tipo;
        }

        /*public Regla_produccion(int numero_regla, string part_derecha, char part_izquierda, int long_Part_derecha, object dato)
        {
            this.numero_regla = numero_regla;
            this.part_derecha = part_derecha;
            this.part_izquierda = part_izquierda;
            this.long_Part_derecha = long_Part_derecha;
            this.dato = dato;
        }*/



        /*public Regla_produccion(int numero_regla, string part_derecha, char part_izquierda, int long_Part_derecha)
        {
            this.numero_regla = numero_regla;
            this.part_derecha = part_derecha;
            this.part_izquierda = part_izquierda;
            this.long_Part_derecha = long_Part_derecha;
        }

        public Regla_produccion(int numero_regla, string part_derecha, char part_izquierda, int long_Part_derecha, object dato) : this(numero_regla, part_derecha, part_izquierda, long_Part_derecha)
        {
            this.dato = dato;
        }*/


    }
}
