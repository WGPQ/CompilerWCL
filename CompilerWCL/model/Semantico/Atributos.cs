using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Semantico
{
    class Atributos
    {
        public char no_terminal { get; set; }
        public string nombre { get; set; }
        public int principio { get; set; }
        public int siguiente { get; set; }
        public List<int> list_verdaderos { get; set; }
        public List<int> list_falsos { get; set; }
        public object valor { get; set; }
        public int tipo { get; set; }
        public string lex { get; set; }

        public Atributos(char no_terminal, string name, int principio, int siguinete, List<int> list_verdaderos, List<int> list_falsos, object valor, int tipo)
        {
            this.no_terminal = no_terminal;
            this.nombre = name;
            this.principio = principio;
            this.siguiente = siguinete;
            this.list_verdaderos = list_verdaderos;
            this.list_falsos = list_falsos;
            this.valor = valor;
            this.tipo = tipo;
        }


        /*public Atributos_pila(char no_terminal, string name, int principio, int siguinete, List<int> list_verdaderos, List<int> list_falsos, object valor)
        {
            this.no_terminal = no_terminal;
            this.name = name;
            this.principio = principio;
            this.siguinete = siguinete;
            this.list_verdaderos = list_verdaderos;
            this.list_falsos = list_falsos;
            this.valor = valor;
        }*/

        public Atributos()
        {
        }
    }
}
