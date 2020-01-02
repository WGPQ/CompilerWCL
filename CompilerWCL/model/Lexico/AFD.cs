using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerWCL.model.Lexico
{
    class AFD
    {
        public string[] columnas_simbolos { get; set; }
        public int[,] matriz_tabla_transicion { get; set; }
        private XDocument documento;
        public List<int> listQ { get; set; } // estados
        public List<char> listX { get; set; } // alfabeto
        public int qo { get; set; } // estado inicial
        public List<int> listF { get; set; } // estados finales
        public List<Automata> listTransicion { get; set; } // transicion

        public AFD(string ruta)
        {
            this.documento = XDocument.Load(@ruta);
            //this.matriz_tabla_transicion = crearMatrizTransicion(ruta);

            this.listQ = list_Q();
            this.listX = list_X();
            this.qo = q0();
            this.listF = list_F();
            this.listTransicion = list_Transicion();
            this.matriz_tabla_transicion = generarMatrizTransitiva(this.listQ, this.listX, this.listTransicion);
        }

        /**
         * Estraigo los estados de un archivo xml
         * 
         * return: retorno una lista de estados
         */
        private List<int> list_Q()
        {
            // Extraer Q -> conjunto de estados
            var Q = from cje in this.documento.Descendants("Q") select cje;
            List<int> lQ = new List<int>();

            foreach (XElement u in Q.Elements("estado"))
            {
                lQ.Add(int.Parse(u.Value));
            }
            return lQ;
        }

        /**
         * Estraigo el alfabeto de un archivo xml
         * 
         * return: retorno una lista de alfabeto
         */
        private List<char> list_X()
        {
            // Extraer X -> alfabeto
            var X = from al in this.documento.Descendants("X") select al;
            var listX = new List<char>();
            foreach (XElement u in X.Elements("simbolo"))
            {
                listX.Add(u.Value.ElementAt(0));
            }
            return listX;
        }

        /**
         * Estraigo el estado inicial de un archivo xml
         * 
         * return: retorno un entro que es el estado inicial
         */
        private int q0()
        {
            // Extraer qo -> estado inicial
            var qo = from ei in this.documento.Descendants("qo") select ei;
            int qo_ei = 0;
            foreach (XElement u in qo.Elements("estadoInicial"))
            {
                qo_ei = int.Parse(u.Value);
            }
            return qo_ei;
        }

        /**
         * Estraigo el conjunto de estados finales de un archivo xml
         * 
         * return: retorno una lista de estados finales
         */
        public List<int> list_F()
        {
            // Extraer F -> Conjunto de estados finales
            var F = from al in this.documento.Descendants("F") select al;
            List<int> listF = new List<int>();
            foreach (XElement u in F.Elements("estadoFinal"))
            {
                listF.Add(int.Parse(u.Value));
            }
            return listF;
        }

        /**
         * Estraigo las transiciones del automata de un archivo xml
         * 
         * return: retorno una lista de transiciones
         */
        private List<Automata> list_Transicion()
        {
            // transicion
            var transicion = from tran in this.documento.Descendants("T") select tran;
            var listTransicion = new List<Automata>();
            int estado_ini = -1;
            char lee = ' ';
            int estado_fin = -1;
            foreach (XElement u in transicion.Elements("transicion"))
            {
                estado_ini = int.Parse(u.Element("estado_ini").Value);
                lee = char.Parse(u.Element("lee").Value);
                estado_fin = int.Parse(u.Element("estado_fin").Value);
                listTransicion.Add(new Automata(estado_ini, lee, estado_fin));
            }
            return listTransicion;
        }

        /**
         * Generar la matriz con los estados, el alfabeto y la transicion del archivo xml
         * 
         * return : retorno la matriz
         */
        public int[,] generarMatrizTransitiva(List<int> listQ, List<char> listX, List<Automata> listTransicion)
        {
            int[,] m = crearMatriz(listQ.Count, listX.Count);
            //Console.WriteLine("f: "+ listQ.Count+"    c: "+ listX.Count);
            int fila = 0;
            int columna = 0;
            foreach (Automata dt in listTransicion)
            {
                fila = listQ.FindIndex(x => x == dt.estado);
                columna = listX.FindIndex(x => x.Equals(dt.leyendo));
                //Console.WriteLine("estado : " + dt.estado + "  lee: " + dt.leyendo);
                m[fila, columna] = dt.newestado;
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
                    m[i, j] = -999;
                }
            }
            return m;
        }

        // ---------------------- METODO ANTIGUO --------------------------------------

        /**
         * Abro el achivo y lo convierto a texto
         * 
         * @param ruta: la ruta del archivo a leer
         * return : el texto del archivo
         */
        private string[] abriArchivo(string ruta)
        {
            string[] lines = null;
            try
            {
                lines = System.IO.File.ReadAllLines(@ruta); // me lee todas las lineas que existe
                this.columnas_simbolos = this.v_column(lines[0]); // extraemos los simbolos que se encuentra en la fila 0
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error: al cargar el archivo", "original");
            }
            return lines;
        }

        /**
         * Del archivo leido genero la matriz de transicion
         * 
         * @param ruta: la ruta del archivo a ser leido
         * return : retorno la matriz de transicion
         */
        public int[,] crearMatrizTransicion(string ruta)
        {
            string[] partes_txt = abriArchivo(ruta); // texto de cada linea
            int[,] m = new int[partes_txt.Length - 1, this.columnas_simbolos.Length]; // -1 porque no queremos la fila de los simbolos
            string[] part = null;

            for (int i = 0; i < m.GetLength(0); i++) // recorro todas las filas del archivo
            {
                part = partes_txt[i + 1].Split('~'); // +1 : porque en la posicion 0 se encuentra los simbolos ya sacados
                for (int j = 0; j < m.GetLength(1); j++) // recorro todas las partes de cada fila
                {
                    if (!"".Equals(part[j]))// si no existe un vacio
                    {
                        m[i, j] = int.Parse(part[j]); // guardo el nodo obtenido
                    }
                    else
                    {
                        m[i, j] = -1; // si esta vacio le lleno con -1
                    }
                }
            }
            return m;
        }

        /**
         * Separo todos los simbolos qu existen del archivo
         * 
         * @param txt: son las culumnas de  los simbolos
         * return: retorno un vector con los simbolos
         */
        private string[] v_column(string txt)
        {
            return txt.Split('~');
        }

    }
}
