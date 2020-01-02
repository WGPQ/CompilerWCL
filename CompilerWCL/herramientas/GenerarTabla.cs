using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.herramientas
{
    class GenerarTabla
    {

        // ------- LEXICO ------------------
        /**
        * Genro las columnas y filas que necesita la matriz de transicion
        * 
        * @param tabla: table entrante
        * @param culumn: los simbolos se encuentran en este vector
        * @param filas: las filas que conforma la tabla
        * 
        */
        /*public void generarTableMatrizTransicion(DataGridView tabla, string[] column, int filas)
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add(" ");
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]); // añado las columnas de la tabla (simbolos)
            }

            for (int i = 0; i < filas; i++)
            {
                dt.Rows.Add(""); // agrego las filas necesarias
            }
            tabla.DataSource = dt; // aplico los cambios a la tabla entrante
        }
        public void generarTableMatrizTransicion(DataGridView tabla, string[] column, int filas)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(" ");
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]); // añado las columnas de la tabla (simbolos)
            }

            for (int i = 0; i < filas; i++)
            {
                dt.Rows.Add(""); // agrego las filas necesarias
            }
            tabla.DataSource = dt; // aplico los cambios a la tabla entrante
        }*/
        public static void generarTableMatrizTransicion(DataGridView tabla, List<char> column, int filas)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(" ");
            for (int i = 0; i < column.Count; i++)
            {
                dt.Columns.Add(column[i] + ""); // añado las columnas de la tabla (simbolos)
            }

            for (int i = 0; i < filas; i++)
            {
                dt.Rows.Add(""); // agrego las filas necesarias
            }
            tabla.DataSource = dt; // aplico los cambios a la tabla entrante
        }

        /**
         * Imprimo la matriz de transicion de la tabl ya creada
         * 
         * @param tabla: tabla entrante
         * @param m: matriz de transición
         * 
         */
        /*public void imprimirTablaMatrizTransicion(DataGridView tabla, int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    tabla[j, i].Value = m[i, j];
                }
            }
        }*/
        public static void imprimirTablaMatrizTransicion(DataGridView tabla, int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                tabla[0, i].Value = i;
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    tabla[j + 1, i].Value = m[i, j];
                }
            }
        }

        /**
         * Genero la tabla compacta valor
         * 
         * @param tabla: tabla entrante
         * @param listTablaCompacta_valor: contiene la tabla compacta valor en un objeto vector
         */
        public static void generarTablaCompacta_valor(DataGridView tabla, List<Object[]> listTablaCompacta_valor)
        {
            DataTable dt = new DataTable();
            // creo mis columnas con su titulo
            dt.Columns.Add("id");
            dt.Columns.Add("valor");
            dt.Columns.Add("columna");

            for (int i = 0; i < listTablaCompacta_valor.Count; i++)
            {
                dt.Rows.Add(new Object[] { i, listTablaCompacta_valor[i][0], listTablaCompacta_valor[i][1] });
            }
            tabla.DataSource = dt;
        }

        /**
         * Genero la tabla compacta prifil
         * 
         * @param tabla: tabla entrante
         * @param listTablaCompacta_prifil: contiene la tabla compacta prifil en un objeto vector
         */
        public static void generarTablaCompacta_prifil(DataGridView tabla, List<Object[]> listTablaCompacta_prifil)
        {
            DataTable dt = new DataTable();
            // creo mis columnas con su titulo
            dt.Columns.Add("id");
            dt.Columns.Add("prifil");
            dt.Columns.Add("fila");

            for (int i = 0; i < listTablaCompacta_prifil.Count; i++)
            {
                dt.Rows.Add(new Object[] { i, listTablaCompacta_prifil[i][0], listTablaCompacta_prifil[i][1] });
            }
            tabla.DataSource = dt;
        }

        /**
         * Genero la tabla del alfabeto
         * 
         * @param tabla: tabla entrante
         * @param listAlfabeto: contiene una lista del alfabeto
         */
        public static void generarTablaAlfabeto(DataGridView tabla, List<Token> listAlfabeto)
        {
            DataTable dt = new DataTable();
            // creo mis columnas con su titulo
            dt.Columns.Add("numtoken");
            dt.Columns.Add("sinónimo");
            dt.Columns.Add("nombretoken");
            dt.Columns.Add("lexema");

            foreach (Token t in listAlfabeto)
            {
                dt.Rows.Add(new Object[] { t.numtoken, t.sinonimo, t.nombretoken, t.lexema });
            }
            tabla.DataSource = dt;
        }



        public static void generarTokensReconocidos(DataGridView tabla, List<Automata> listReconocidos)
        {
            DataTable dt = new DataTable();
            // creo mis columnas con su titulo
            dt.Columns.Add("estado");
            dt.Columns.Add("leyendo");
            dt.Columns.Add("newestado");

            foreach (Automata a in listReconocidos)
            {
                dt.Rows.Add(new Object[] { a.estado, a.leyendo, a.newestado });
            }
            tabla.DataSource = dt;
        }

        /**
         * Genero y imprimo la tabla TDS
         * 
         */
        public static void generarTableTDS(DataGridView tabla, List<TDS> listTDS)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Tipo de Dato");
            dt.Columns.Add("Tamaño");
            dt.Columns.Add("Value");

            foreach (TDS tds in listTDS)
            {
                if (tds.nametk.Length <= 8)
                {
                    dt.Rows.Add(new Object[] { tds.nametk, tds.type, tds.size, tds.value });
                }
                else
                {
                    dt.Rows.Add(new Object[] { tds.nametk.Substring(0, 8), tds.type, tds.size, tds.value });
                }
                
            }
            
            tabla.DataSource = dt; // aplico los cambios a la tabla entrante
        }


        // ------- SINTACTICO ------------------
        public static void generarTableMatrizTransicionSRL(DataGridView tabla, List<char> column, int filas)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(" ");
            for (int i = 0; i < column.Count; i++)
            {
                dt.Columns.Add(column[i] + ""); // añado las columnas de la tabla (simbolos)
            }

            for (int i = 0; i < filas; i++)
            {
                dt.Rows.Add(""); // agrego las filas necesarias
            }
            tabla.DataSource = dt; // aplico los cambios a la tabla entrante
        }

        public static void imprimirTablaMatrizSRL(DataGridView tabla, int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                tabla[0, i].Value = i;
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    tabla[j + 1, i].Value = m[i, j];
                }
            }
        }

        public static void generarTablaPilaSRL(DataGridView tabla, List<AnalisisDeUnaEntrada> analisisDeEntradas)
        {
            DataTable dt = new DataTable();
            // creo mis columnas con su titulo
            dt.Columns.Add("Pila");
            dt.Columns.Add("Entrada");
            dt.Columns.Add("Acción");

            foreach (AnalisisDeUnaEntrada ade in analisisDeEntradas)
            {
                dt.Rows.Add(new Object[] { ade.pila, ade.entarda, ade.accion});
            }
            tabla.DataSource = dt;
        }

    }
}
