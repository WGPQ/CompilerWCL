using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TablaCompacta
    {
        // Atributos declarados
        public List<char> l_columnas { get; set; } // guarda los simbolos de la matriz de transicion
        public List<Object[]> list_tablaValor { get; set; } // Esta lista contiene la tabla de valores
        public List<Object[]> list_tablaPrifil { get; set; } // Esta lista cintienen la tabla prifil
        public TablaCompacta(int[,] matriz, List<char> columnas)
        {
            this.l_columnas = columnas;
            this.list_tablaValor = llenar_table_valor(matriz);
            this.list_tablaPrifil = llenar_table_prifil(matriz, this.list_tablaValor.Count);
        }

        /**
         *  Con el algoritmo generamos la labla valor
         *
         * @param matriz: debe ingresar la matriz de transicion
         * @return : retorno un objeto vector donde { valor, simbolo}
         * **/
        public List<Object[]> llenar_table_valor(int[,] matriz)
        {
            List<Object[]> lista_valor = new List<Object[]>();
            // recorro toda la matriz
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] > -1) // > -1 : porque los espacion en blancon fueron llenados con -1
                    {
                        lista_valor.Add(new object[] { matriz[i, j], this.l_columnas[j].ToString() }); // ingreso los datos a la lista
                    }
                }
            }
            return lista_valor;
        }

        /**
         *  Con el algoritmo generamos la labla prifil
         *
         * @param matriz: debe ingresar la matriz de transicion
         * @param filas: la filas que creo al momento de hacer la tabla valor
         * @return : retorno un objeto vector donde { prifil, fil}
         * **/
        public List<Object[]> llenar_table_prifil(int[,] m, int filas)
        {
            List<Object[]> lista = new List<Object[]>();
            int cont_suma = 0; // cuenta el salto
            int cant_valores = 0; // cuenta los elemetos encontrados de cada fila
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (m[i, j] > -1) // > -1: porque los estados son positivos y el -1 es un vacio
                    {
                        cant_valores++; // cuento los elementos que se encuentra en esa fila
                    }
                }

                if (cont_suma <= filas) // debe ser meno igual con la filas obtenidas de la lista tabla_valor
                {
                    lista.Add(new object[] { cont_suma, cant_valores });
                    // sumo el prifil y el nro de elementos encontrado en la fila para llevar el nuevo prifil
                    cont_suma += cant_valores;
                    cant_valores = 0; // reinicio el valor en 0
                }
                else
                {
                    break; // cuando cont_suma sobrepasa de las filas obtenidas con la lista tabla_valor
                }

            }
            return lista;
        }

        /**
         * Encargado de seguir los movimientos del archivo ejecutado
         * 
         * @param fila: esta valor iniciara en 0 porque es el nodo inicial y dea ira cambiando dependiendo lo que se vaya leyendo
         * @param simbolo: cada simbolo o letra que vaya leyendo
         * return : reterno el proximo nodo a leer
         */
        public int movimiento(int fila, char simbolo)
        {
            if (fila > -1)
            {
                int fila_tvalor = (int)this.list_tablaPrifil[fila][0]; // obtego el prifil
                int cantidad = (int)this.list_tablaPrifil[fila][1]; // obtengo la cantidad de elementos encontrados
                for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
                {
                    if ((simbolo.ToString()).Equals(this.list_tablaValor[i][1])) // si el simbolo a leer es controntrado
                    {
                        return (int)this.list_tablaValor[i][0]; // retorno el nodo que sigue
                    }
                }
            }
            return -1; // el caso de que no se encuentre
        }

        /**
         * Esta lista contiene los simbolos siguientes de un lexema
         * mal escrito
         * 
         * @param estado: el estado que se quede en la transicion
         * return : retorno la lista de los simbolos siguientes a reconocer
         */
        public List<string> listSimbolosSiguientes(int estado)
        {
            List<string> l = new List<string>();
            int fila_tvalor = (int)this.list_tablaPrifil[estado][0]; // obtego el prifil
            int cantidad = (int)this.list_tablaPrifil[estado][1]; // obtengo la cantidad de elementos encontrados
            for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
            {
                l.Add(this.list_tablaValor[i][1].ToString());
            }
            return l;
        }
    }
}
