using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Sintactico
{
    class TablaCompacta
    {
        // Atributos declarados
        int error_vacio = -1000; // -1000 es llenado debes cuando existe un vacio 
        public List<Object[]> list_tablaAccionValor { get; set; } // Esta lista contiene la tabla de valores del Accion
        public List<Object[]> list_tablaAccionPrifil { get; set; } // Esta lista cintienen la tabla prifil del Accion

        public List<Object[]> list_tablaGoToValor { get; set; } // Esta lista contiene la tabla de valores del GoTo
        public List<Object[]> list_tablaGoToPrifil { get; set; } // Esta lista cintienen la tabla prifil del GoTo
        public TablaCompacta(List<char> listColumnasAccion, int[,] matrizAccion, List<char> ListColumnasGoTo, int[,] matrizGoTo)
        {
            this.list_tablaAccionValor = llenar_table_valor(matrizAccion, listColumnasAccion);
            this.list_tablaAccionPrifil = llenar_table_prifil(matrizAccion, this.list_tablaAccionValor.Count);

            this.list_tablaGoToValor = llenar_table_valor(matrizGoTo, ListColumnasGoTo);
            this.list_tablaGoToPrifil = llenar_table_prifil(matrizGoTo, this.list_tablaGoToValor.Count);
        }

        /**
         *  Con el algoritmo generamos la labla valor
         *
         * @param matriz: debe ingresar la matriz de transicion
         * @return : retorno un objeto vector donde { valor, simbolo}
         * **/
        public List<Object[]> llenar_table_valor(int[,] matriz, List<char> listColumnas)
        {
            List<Object[]> lista_valor = new List<Object[]>();
            // recorro toda la matriz
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] > error_vacio) // > -1000 : porque los espacion en blancon fueron llenados con -1000
                    {
                        lista_valor.Add(new object[] { matriz[i, j], listColumnas[j].ToString() }); // ingreso los datos a la lista
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
                    if (m[i, j] > error_vacio) // > -1000: porque es un vacio
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
         * TABLA ACCION
         * Encargado de seguir los movimientos del archivo ejecutado
         * 
         * @param fila: esta valor iniciara en 0 porque es el nodo inicial y dea ira cambiando dependiendo lo que se vaya leyendo
         * @param simbolo: cada simbolo o letra que vaya leyendo
         * return : reterno el proximo nodo a leer
         */
        public int movimientoAccion(int fila, char simbolo)
        {
            if (fila > -1)
            {
                int fila_tvalor = (int)this.list_tablaAccionPrifil[fila][0]; // obtego el prifil
                int cantidad = (int)this.list_tablaAccionPrifil[fila][1]; // obtengo la cantidad de elementos encontrados
                for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
                {
                    if ((simbolo.ToString()).Equals(this.list_tablaAccionValor[i][1])) // si el simbolo a leer es controntrado
                    {
                        return (int)this.list_tablaAccionValor[i][0]; // retorno el nodo que sigue
                    }
                }
            }
            return error_vacio; // el caso de que no se encuentre
        }


        /**
         * TABLA GOTO
         * Encargado de seguir los movimientos del archivo ejecutado
         * 
         * @param fila: esta valor iniciara en 0 porque es el nodo inicial y dea ira cambiando dependiendo lo que se vaya leyendo
         * @param simbolo: cada simbolo o letra que vaya leyendo
         * return : reterno el proximo nodo a leer
         */
        public int movimientoGoTo(int fila, char simbolo)
        {
            if (fila > -1)
            {
                int fila_tvalor = (int)this.list_tablaGoToPrifil[fila][0]; // obtego el prifil
                int cantidad = (int)this.list_tablaGoToPrifil[fila][1]; // obtengo la cantidad de elementos encontrados
                for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
                {
                    if ((simbolo.ToString()).Equals(this.list_tablaGoToValor[i][1])) // si el simbolo a leer es controntrado
                    {
                        return (int)this.list_tablaGoToValor[i][0]; // retorno el nodo que sigue
                    }
                }
            }
            return error_vacio; // el caso de que no se encuentre
        }

        /**
         * TABLA ACCION
         * Esta lista contiene los simbolos siguientes de un lexema
         * mal escrito
         * 
         * @param estado: el estado que se quede en la transicion
         * return : retorno la lista de los simbolos siguientes a reconocer
         */
        public List<char> listSimbolosSiguientesAccion(int estado)
        {
            List<char> l = new List<char>();
            int fila_tvalor = (int)this.list_tablaAccionPrifil[estado][0]; // obtego el prifil
            int cantidad = (int)this.list_tablaAccionPrifil[estado][1]; // obtengo la cantidad de elementos encontrados
            for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
            {
                l.Add(char.Parse(this.list_tablaAccionValor[i][1].ToString()));
            }
            return l;
        }


        /**
         * TABLA GOTO
         * Esta lista contiene los simbolos siguientes de un lexema
         * mal escrito
         * 
         * @param estado: el estado que se quede en la transicion
         * return : retorno la lista de los simbolos siguientes a reconocer
         */
        public List<char> listSimbolosSiguientesGoTo(int estado)
        {
            List<char> l = new List<char>();
            int fila_tvalor = (int)this.list_tablaGoToPrifil[estado][0]; // obtego el prifil
            int cantidad = (int)this.list_tablaGoToPrifil[estado][1]; // obtengo la cantidad de elementos encontrados
            for (int i = fila_tvalor; i < fila_tvalor + cantidad; i++) // fila_tvalor + cantidad: con esto se cuantos nodos debo recorrer
            {
                l.Add(char.Parse(this.list_tablaGoToValor[i][1].ToString()));
            }
            return l;
        }
    }
}
