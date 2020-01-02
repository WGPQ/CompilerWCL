using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerWCL.model.Sintactico
{
    class CargarSLR
    {
        public int[,] matriz_tabla_transicionAccion { get; set; }
        public int[,] matriz_tabla_transicionGoTo { get; set; }

        private XDocument documento;
        public List<int> listQ { get; set; } // terminales
        public List<char> listX { get; set; } // terminales
        public List<char> listN { get; set; } // no terminales
        public string axioma;
        public List<string> listP { get; set; } // no terminales
        public List<Transicion> listAccion { get; set; } // lista de transicion de un accion
        public List<Transicion> listGoTo { get; set; } // lista de transicion de un GoTo


        public CargarSLR(string ruta)
        {
            this.documento = XDocument.Load(@ruta);
            //this.matriz_tabla_transicion = crearMatrizTransicion(ruta);
            this.listQ = list_Q();
            this.listX = list_X();
            this.listN = list_N();
            this.axioma = axiomaP();
            this.listP = list_P();
            this.listAccion = list_Accion();
            this.listGoTo = list_GoTo();
            this.matriz_tabla_transicionGoTo = generarMatrizTransitivaGoTo(this.listQ, this.listN, this.listGoTo);
            this.matriz_tabla_transicionAccion = generarMatrizTransitivaAccion(this.listQ, this.listX, this.listAccion);
        }

        /**
         * Estraigo los estados de un archivo xml
         * 
         * return: retorno una lista de estados
         */
        private List<int> list_Q()
        {
            // Extraer X -> conjunto de terminales
            var Q = from cje in this.documento.Descendants("Q") select cje;
            List<int> lQ = new List<int>();

            foreach (XElement u in Q.Elements("estado"))
            {
                lQ.Add(int.Parse(u.Value));
            }
            return lQ;

        }

        /**
         * Estraigo los terminales de un archivo xml
         * 
         * return: retorno una lista de terminales
         */
        private List<char> list_X()
        {
            // Extraer X -> conjunto de terminales
            var X = from cje in this.documento.Descendants("X") select cje;
            List<char> lX = new List<char>();

            foreach (XElement u in X.Elements("terminal"))
            {
                lX.Add(u.Value.ElementAt(0));
            }
            return lX;

        }

        /**
         * Estraigo los no terminales de un archivo xml
         * 
         * return: retorno una lista de no terminales
         */
        private List<char> list_N()
        {
            // Extraer N -> no terminales
            var N = from al in this.documento.Descendants("N") select al;
            var lN = new List<char>();
            foreach (XElement u in N.Elements("no_terminal"))
            {
                lN.Add(u.Value.ElementAt(0));
            }
            return lN;
        }

        /**
         * Estraigo el estado inicial de un archivo xml
         * 
         * return: retorno un entro que es el estado inicial
         */
        private string axiomaP()
        {
            // Extraer S -> axioma
            var axi = from ei in this.documento.Descendants("S") select ei;
            string axioma = "";
            foreach (XElement u in axi.Elements("axioma"))
            {
                axioma = u.Value;
            }
            return axioma;
        }

        /**
         * Estraigo las Producciones de un archivo xml
         * 
         * return: retorno una lista de producciones
         */
        private List<string> list_P()
        {
            // Extraer P -> Produccion
            var P = from al in this.documento.Descendants("P") select al;
            var lP = new List<string>();
            foreach (XElement u in P.Elements("regla"))
            {
                lP.Add(u.Value);
            }
            return lP;
        }

        /**
         * Estraigo las transiciones de la gramatica de un archivo xml
         * 
         * return: retorno una lista de transiciones
         */
        private List<Transicion> list_Accion()
        {
            // transicion
            var transicion = from tran in this.documento.Descendants("accion") select tran;
            var listTransicionAccion = new List<Transicion>();
            int estado = -1;
            char simbolo = ' ';
            int movimiento = -1;
            foreach (XElement u in transicion.Elements("transicion"))
            {
                estado = int.Parse(u.Element("estado").Value);
                simbolo = char.Parse(u.Element("simbolo").Value);
                movimiento = int.Parse(u.Element("movimiento").Value);
                listTransicionAccion.Add(new Transicion(estado, simbolo, movimiento));
            }
            return listTransicionAccion;
        }

        /**
         * Estraigo el GoTo de un archivo xml
         * 
         * return: retorno una lista del GoTo
         */
        private List<Transicion> list_GoTo()
        {
            // transicion
            var transicion = from tran in this.documento.Descendants("GoTo") select tran;
            var listTransicionGoTo = new List<Transicion>();
            int estado_ini = -1;
            char no_terminal = ' ';
            int estado_fin = -1;
            foreach (XElement u in transicion.Elements("transicion"))
            {
                estado_ini = int.Parse(u.Element("estado_ini").Value);
                no_terminal = char.Parse(u.Element("no_terminal").Value);
                estado_fin = int.Parse(u.Element("estado_fin").Value);
                listTransicionGoTo.Add(new Transicion(estado_ini, no_terminal, estado_fin));
            }
            return listTransicionGoTo;
        }

        /**
         * Generar la matriz con los estados, simbolo y la moviimiento de las listas obtenidas
         * 
         * return : retorno la matriz
         */
        public int[,] generarMatrizTransitivaGoTo(List<int> listQ, List<char> listN, List<Transicion> listTransicion)
        {
            int[,] m = crearMatriz(listQ.Count, listN.Count);
            //Console.WriteLine("f: "+ listQ.Count+"    c: "+ listX.Count);
            int fila = 0;
            int columna = 0;
            foreach (Transicion dt in listTransicion)
            {
                fila = listQ.FindIndex(x => x == dt.estado);
                columna = listN.FindIndex(x => x.Equals(dt.simbolo));
                //Console.WriteLine("estado : " + dt.estado + "  lee: " + dt.leyendo);
                m[fila, columna] = dt.movimiento;
            }
            return m;
        }

        private int[,] crearMatriz(int fila, int columnas)
        {
            int[,] m = new int[fila, columnas];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[i, j] = -1000; // lleno toda la matriz con -1000 para saber que es un vacio
                }
            }
            return m;
        }

        /**
         * Generar la matriz con los estados, simbolo y la moviimiento de las listas obtenidas
         * 
         * return : retorno la matriz
         */
        public int[,] generarMatrizTransitivaAccion(List<int> listQ, List<char> listX, List<Transicion> listTransicion)
        {
            int[,] m = crearMatriz(listQ.Count, listX.Count);
            //Console.WriteLine("f: "+ listQ.Count+"    c: "+ listX.Count);         
            int fila = 0;
            int columna = 0;
            foreach (Transicion dt in listTransicion)
            {
                fila = listQ.FindIndex(x => x == dt.estado);
                columna = listX.FindIndex(x => x.ToString().Equals(dt.simbolo.ToString()));
                //Console.WriteLine("estado : " + dt.estado + "  lee: " + dt.leyendo);
                m[fila, columna] = dt.movimiento;
            }
            return m;
        }

        public void imprimir(string a)
        {
            Console.WriteLine(a);
        }
    }
}
