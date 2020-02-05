using CompilerWCL.model.Errores;
using CompilerWCL.model.Lexico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompilerWCL.model.Sintactico
{
    class AnalizadorSLR
    {
        private int error_vacio = -1000; // -1000 esta representado por vacio de la tablas generadas
        public List<Object> lista_pila { get; set; }
        private List<Token> listaTokens;
        private int nroNewRegla = 0;
        private int[,] matrizAccion;
        private int[,] matrizGoTo;
        private List<char> listaX;
        private List<string> listP;
        private List<char> listaN;
        private List<char> listaDeEntrada;
        public List<AnalisisDeUnaEntrada> analisisDeEntradas;
        private TablaCompacta tablaCompacta;
        private List<Token> listAlfabeto;
        private ErroresReconocidos erroresReconocidos;
        public List<Regla_produccion> listReglasReconocidas { get; set; }

    public AnalizadorSLR(List<Token> listaTokensReconocidos, List<Token> listAlfabeto,
            CargarSLR cargar_slr, TablaCompacta tablaCompacta, ErroresReconocidos erroresReconocidos)
        {
            this.erroresReconocidos = erroresReconocidos;
            this.lista_pila = new List<Object>();
            this.listaTokens = listaTokensReconocidos;
            this.matrizAccion = cargar_slr.matriz_tabla_transicionAccion;
            this.matrizGoTo = cargar_slr.matriz_tabla_transicionGoTo;
            this.listaX = cargar_slr.listX;
            this.listP = cargar_slr.listP;
            this.listaN = cargar_slr.listN;
            this.listAlfabeto = listAlfabeto;
            this.nroNewRegla = listAlfabeto.Count + 1; // +1 porque se agraga nueva regla

            this.tablaCompacta = tablaCompacta;
            listReglasReconocidas = new List<Regla_produccion>();

        }

        public void analizadorSLR()
        {
            int nerror = 0;
            int idtk = 0; // contador de los token reconocidos
            int estado = -1;
            char e = ' ';
            int newEstado = -1;
            int regla = -1;
            int numElementosParteDerecha = -1;
            char no_terminal = ' ';
            bool rrr = true;

            this.analisisDeEntradas = new List<AnalisisDeUnaEntrada>();
            push(0); //guardar en la pila              
            PonerTKreconocidos('∆'); //el lexico al terminar podria guardar este simbolo de finalizacion al final de los tokens reconocidos

            this.listaDeEntrada = generarListaDeEntrada(this.listaTokens); // inicializo la lista de los simbolos de entrada
            this.analisisDeEntradas.Add(new AnalisisDeUnaEntrada(resultadoPila(this.lista_pila), resultadoPilaEntrada(this.listaDeEntrada),
                "")); // "" vacio porque en esta posicion aun no hace el desplazo
            do
            {
                estado = cimadepila();
                e = tkreconocidos(idtk); // es el sinónimo
                //newEstado = APSLR_accion(estado, e);
                newEstado = this.tablaCompacta.movimientoAccion(estado, e);
                //imprimir("estado: " + estado + "\te: " + e + "\tnewEstado: " + newEstado);

                // intento recuperarme del error
                if ( newEstado == error_vacio) // error_vacio = -1000
                {
                    newEstado = reglaProbableAccion(estado, this.lista_pila, idtk);
                    nerror++;
                    //imprimir("newEstado: " + newEstado);
                    //imprimir("Error: Falta " + this.listaTokens[newEstado].sinonimo);
                }

                if (newEstado >= 0 && newEstado < 200)
                {//aqui es el desplazarse del algoritmo
                    push(e);
                    // elimino sinonimo reconocido
                    removerSimboloDeListaDeEntrada(e);
                    push(newEstado);
                    this.analisisDeEntradas[this.analisisDeEntradas.Count - 1].accion = "Desplaza"; // recian hace el desplazo en las primeras posiciones
                    this.analisisDeEntradas.Add(new AnalisisDeUnaEntrada(resultadoPila(this.lista_pila), resultadoPilaEntrada(this.listaDeEntrada), ""));
                    idtk++;
                }
                else if (newEstado < 0 && newEstado > -999)
                {//aqui es reconocimiento de regla
                    
                    regla = -newEstado; //cambiamos de signo para que busque la regla 
                    numElementosParteDerecha = buscarEnReglasLaLongitudDeParteDerecha(regla);
                    no_terminal = encontrarLaParteIzquierdaDeLaRegla_NoTerminal(regla);
                    //imprimir("-------> idtk: " + idtk + "\tlexema: " + this.listaTokens[idtk - 1].lexema + "\tregla: " + (regla));
                    for (int i = 1; i <= 2 * numElementosParteDerecha; i++)
                    {
                        pop();
                    }

                    // cambio
                    if ((regla > 11 && regla < 16) && numElementosParteDerecha > 0) // reglas del inicializa -> retrocedo para encontar el identificador
                    {
                        /*this.listReglasReconocidas.Add(new Regla_produccion(regla - 1, reglas_parteDerechar(regla - 1),
                        encontrarLaParteIzquierdaDeLaRegla_NoTerminal(regla-1), buscarEnReglasLaLongitudDeParteDerecha(regla-1), 
                        this.listaTokens[(idtk - 1) - numElementosParteDerecha].lexema, 
                        this.listaTokens[(idtk - 1) - numElementosParteDerecha].nombretoken));*/
                        // le mando a la regla 11 para agregar el identificador
                        this.listReglasReconocidas.Add(new Regla_produccion(11, reglas_parteDerechar(11),
                       encontrarLaParteIzquierdaDeLaRegla_NoTerminal(11), buscarEnReglasLaLongitudDeParteDerecha(11), 
                       this.listaTokens[(idtk - 1) - numElementosParteDerecha].lexema, 
                       this.listaTokens[(idtk - 1) - numElementosParteDerecha].nombretoken));
                    }
                    // -------------

                    estado = this.tablaCompacta.movimientoGoTo(cimadepila(), no_terminal);

                    if (estado >= 0 && estado < 200)
                    {
                        push(no_terminal);
                        push(estado);
                        this.analisisDeEntradas[this.analisisDeEntradas.Count - 1].accion = obtenerReglaReduce(regla - 1);  // -1 porque la regla tiene su numerado desde el 1
                        this.analisisDeEntradas.Add(new AnalisisDeUnaEntrada(resultadoPila(this.lista_pila), resultadoPilaEntrada(this.listaDeEntrada),
                            ""));

                        if (regla == 56 || regla == 58)
                        {
                            this.listReglasReconocidas.Add(new Regla_produccion(regla, reglas_parteDerechar(regla),
                            no_terminal, numElementosParteDerecha, this.listaTokens[idtk-1].lexema, this.listaTokens[idtk - 1].nombretoken));
                        }
                        else if (regla == 39) // Encuento el identifica al cual vamos asignar una operacion ejm: #a := #b * 4 ; -> el que busco es #a
                        {
                            for (int j = idtk; j >= 0; j--)
                            {
                                if (":=".Equals(this.listaTokens[j].lexema))
                                {
                                    this.listReglasReconocidas.Add(new Regla_produccion(regla, reglas_parteDerechar(regla),
                                    no_terminal, numElementosParteDerecha, this.listaTokens[j - 1].lexema, 
                                    this.listaTokens[j - 1].nombretoken));
                                    break;
                                }
                            }
                        }
                        else if (regla == 32 || regla == 33) // regla del incremento 32: +  33: - -> retrocedo para encontrar el identificador
                        {
                            this.listReglasReconocidas.Add(new Regla_produccion(regla, reglas_parteDerechar(regla),
                            no_terminal, numElementosParteDerecha, this.listaTokens[idtk - 2].lexema, this.listaTokens[idtk - 2].nombretoken));
                        }
                        else if (regla == 37)
                        {
                            for (int i = idtk - 1; i > 0; i--)
                            {
                                if (listaTokens[i].lexema.Equals(","))
                                {
                                    this.listReglasReconocidas.Add(new Regla_produccion(regla, reglas_parteDerechar(regla),
                                    no_terminal, numElementosParteDerecha, this.listaTokens[i - 1].lexema,
                                    this.listaTokens[i - 1].nombretoken));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this.listReglasReconocidas.Add(new Regla_produccion(regla, reglas_parteDerechar(regla),
                            no_terminal, numElementosParteDerecha, this.listaTokens[idtk - 1].lexema, this.listaTokens[idtk - 1].nombretoken));
                        }

                        
                    }
                    else // Error
                    {
                        break;
                    }

                    //AQUI LLAMAREMOS AL SEMÁNTICO ENVIANDOLE EL NÚMERO DE REGLA
                    //semantico(regla);
                    //ó se guardará en listaReglasReconocidos(regla);
                }
                else if (newEstado == 999)
                {//aqui aceptar
                    this.analisisDeEntradas[this.analisisDeEntradas.Count - 1].accion = "Aceptar";
                    break;
                }
                else
                {//aqui error
                    //presentarMensajeError(n);
                    nerror++;
                    //imprimir("Errro: La estructura esta incorrecto");
                    // -201
                    this.erroresReconocidos.insertarErroresReconocidos(-201, "", this.listaTokens[idtk].numFila);
                    break;
                }
            } while (nerror <= 5);

            if (newEstado == 999 && nerror == 0)
            {
                //presentarMensaje("programa fuente reconocido sin errores sintácticos");
            }
            else
            {
                //presentarMensaje("programa fuente con errores sintácticos");
            }
        }  

        /**
         * TABLA ACCION
         * Me permite saber cual regla es la correcta al momente de equivocarse
         * 
         * return : me retorna la cadena correcta o incorrecta
         */
        public int reglaProbableAccion(int estadoPorReconocer, List<Object> lPila, int idtk)
        {
            imprimir("RECUPERAR");
            imprimir("estadoPorReconocer: "+ estadoPorReconocer+ "  idtk: " +idtk);
            List<Object> listCopyPila = null;
            int copy_idtk = idtk;
            int newEstado = -1;
            char simboloSiguiente = tkreconocidos(idtk + 1); // +1 para obteger el simbolo que sigue
            if (simboloSiguiente == ' ')
            {
                return error_vacio;
            }
            int pos = -1;
            int newEstado2 = -1;
            int cima_pila = -1;
            int regla = -1;
            int numElementosParteDerecha = -1;
            char no_terminal = ' ';
            int estadoReconocido = -1;
            int estadoProximo = -1;
            char e = ' '; 
            int estado = estadoPorReconocer;
            string lexemaSugerencia = "";



            foreach (char simbolo in tablaCompacta.listSimbolosSiguientesAccion(estadoPorReconocer))
            {
                estadoReconocido = this.tablaCompacta.movimientoAccion(estado, simbolo);
                imprimir("");
                
                estadoProximo = this.tablaCompacta.movimientoAccion(estadoReconocido, simboloSiguiente);
                imprimir("estadoReconocido: " + estadoReconocido + "\testadoProximo: "+estadoProximo);
                if (estadoProximo > error_vacio && estadoProximo < 200)
                {
                    imprimir("1: --> se roconocio el estado : " + estadoReconocido);
                    pos = this.listAlfabeto.FindIndex(x => x.sinonimo == simbolo);
                    imprimir("--->> " + this.listAlfabeto[pos].lexema);
                    lexemaSugerencia = this.listAlfabeto[pos].lexema;
                    if (lexemaSugerencia.Equals(";") || lexemaSugerencia.Equals("{"))
                    {
                        // -203 Falta Error falta
                        this.erroresReconocidos.insertarMesajeSurencia(-203, this.listaTokens[idtk].lexema, lexemaSugerencia,
                            this.listaTokens[copy_idtk].numFila);
                    }
                    else
                    {
                        // -202 No se espero la siguiente instrucción, tal vez quiso decir
                        this.erroresReconocidos.insertarMesajeSurencia(-202, this.listaTokens[idtk].lexema, lexemaSugerencia, 
                            this.listaTokens[copy_idtk].numFila);
                    }
                    
                    imprimir("Error : tal vez quiso decir -> "+ this.listAlfabeto[pos].lexema + "(Line: "+ this.listaTokens[copy_idtk].numFila + ")");
                    return estadoReconocido;
                }
                else
                {
                    listCopyPila = lPila;
                    do
                    {
                        estado = copyPilaCimapila(listCopyPila); ;
                        e = simbolo; // es el sinónimo
                                                      //newEstado = APSLR_accion(estado, e);
                        newEstado = this.tablaCompacta.movimientoAccion(estado, e);
                        imprimir("1: Recuperar --> estado: " + estado + "   e: " + e + "\tnewEstado: " + newEstado);
                        pos = this.listAlfabeto.FindIndex(x => x.sinonimo == simbolo);
                        imprimir("--->> " + this.listAlfabeto[pos].lexema);

                        

                        if (newEstado >= 0 && newEstado < 200)
                        {//aqui es el desplazarse del algoritmo
                            copyPilaPush(simbolo, listCopyPila);
                            copyPilaPush(newEstado, listCopyPila);
                            copy_idtk++;
                            if (copy_idtk < this.listaTokens.Count && tkreconocidos(copy_idtk) == simboloSiguiente)
                            {
                                imprimir("2: --> se roconocio el estado : " + estadoReconocido);
                                imprimir("Error : Fatal Error falta -> " + this.listAlfabeto[pos].lexema + "(Line: " + this.listaTokens[copy_idtk].numFila + ")");

                                lexemaSugerencia = this.listAlfabeto[pos].lexema;
                                if (lexemaSugerencia.Equals(";") || lexemaSugerencia.Equals("{"))
                                {
                                    // -203 Falta Error falta
                                    this.erroresReconocidos.insertarMesajeSurencia(-203, this.listaTokens[idtk].lexema, lexemaSugerencia,
                                        this.listaTokens[copy_idtk].numFila);
                                }
                                else
                                {
                                    // -202 No se espero la siguiente instrucción, tal vez quiso decir
                                    this.erroresReconocidos.insertarMesajeSurencia(-202, this.listaTokens[idtk].lexema, lexemaSugerencia,
                                        this.listaTokens[copy_idtk].numFila);
                                }

                                 return estadoReconocido;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (newEstado < 0 && newEstado > error_vacio)
                        {

                            regla = -newEstado; //cambiamos de signo para que busque la regla 
                            numElementosParteDerecha = buscarEnReglasLaLongitudDeParteDerecha(regla);
                            no_terminal = encontrarLaParteIzquierdaDeLaRegla_NoTerminal(regla);

                            for (int i = 1; i <= 2 * numElementosParteDerecha; i++)
                            {
                                copyPilaPop(listCopyPila);
                            }
                            cima_pila = copyPilaCimapila(listCopyPila);
                            //imprimir("");
                            //imprimir("regla: " + regla + "numElementosParteDerecha: "+ numElementosParteDerecha+ "  no_terminal: "+ no_terminal);
                            estado = this.tablaCompacta.movimientoGoTo(cima_pila, no_terminal);
                            //imprimir("2: Recuperar --> cima_pila: " + cima_pila + "   no_terminal: " + no_terminal + "\testado: " + estado);
                            copyPilaPush(no_terminal, listCopyPila);
                            copyPilaPush(estado, listCopyPila);
                        }
                        else // Error
                        {
                            break;
                        }

                    } while (true);
                }
            }
            return error_vacio; // -1000 significa vacio
        }

        public void copyPilaPush(Object obj, List<Object> pila)
        {
            pila.Add(obj);
        }

        public void copyPilaPop(List<Object> pila)
        {
            pila.RemoveAt(pila.Count-1);
        }

        public int copyPilaCimapila(List<Object> pila)
        {
            return (int)pila[pila.Count - 1];
        }

        /**
         * TABLA GOTO
         * Me permite saber cual regla es la correcta al momente de equivocarse
         * 
         * return : me retorna la cadena correcta o incorrecta
         */
        public int reglaProbableGoTo(int estado)
        {
            //char no_terminal = ' ';
            int newEstado = -1;
            foreach (char no_terrminal in tablaCompacta.listSimbolosSiguientesGoTo(estado))
            {

                newEstado = this.tablaCompacta.movimientoGoTo(estado, no_terrminal);
                imprimir("Recuperar --> estado: " + estado + "\te: " + no_terrminal + "\tnewEstado: " + newEstado);
                if (newEstado >= 0)
                {
                    return no_terrminal;
                }
            }
            return error_vacio; // -1000 significa vacio
        }

        // ---- PILA ------

        public void push(Object obj)
        {
            this.lista_pila.Add(obj);
        }

        public Object pop()
        {
            Object res = null;
            if (this.lista_pila.Count > 0)
            {
                res = this.lista_pila[this.lista_pila.Count - 1];
                this.lista_pila.RemoveAt(this.lista_pila.Count - 1);
            }
            return res;
        }

        /**
         * retorno el ultimo elemento ingresado en la pila
         */
        public int cimadepila()
        {
            return (int)this.lista_pila[this.lista_pila.Count - 1];
        }

        public string resultadoPila(List<Object> v)
        {
            string r = "[ ";
            foreach (Object res in v)
            {
                r += res + ", ";
            }
            if (r.Length == 2) // cuando la pila esta vacia
            {
                return "[ ]";
            }
            r = r.Substring(0, r.Length - 2) + " ] ";
            return r;
        }

        public string resultadoPilaEntrada(List<char> v)
        {
            string r = "";
            foreach (char res in v)
            {
                r += res + " ";
            }
            if (r.Length == 1) // cuando la pila esta vacia
            {
                return "";
            }
            r = r.Substring(0, r.Length - 1);
            return r;
        }
        // ---- FIN PILA -------

        /** 
         * Pongo un terminal para que el programa sepa donde debe
         * terminar de ejecutar
         */
        public void PonerTKreconocidos(char agregar)
        {
            this.listaTokens.Add(new Token(this.nroNewRegla, agregar, "Fin", "Aceptar"));
        }

        /**
         * estraigo los tokes que estoy recorriendo de forma ordenada
         * return: retorno 
        */
        public char tkreconocidos(int idtk)
        {
            if (idtk < this.listaTokens.Count)
            {
                return this.listaTokens[idtk].sinonimo;
            }
            return ' ';  
        }


        // ------ USAR REGLAS DE PRODUCCION --------
        public int buscarEnReglasLaLongitudDeParteDerecha(int regla)
        {
            return regla_parteDerechar(this.listP[regla - 1]); // -1 porque las reglas de produccion comienza desde 1
        }

        /**
         * Me cuenta los elemetos de la parte derecha
         * de las reglas de producción
         * 
         * return : retorno el número de elementos
         */
        public int regla_parteDerechar(string c)
        {
            if (!"".Equals(c))
            {
                int pos = c.IndexOf(">") + 2; // busco en que posicion se encuentra ">" y 
                                              //le aumenento +2 para comenzar desde ahi la nueva cadena
                string cadena = c.Substring(pos, c.Length - pos);
                if (cadena.Equals("λ"))
                {
                    return 0;
                }
                return cadena.Split(' ').Length;
            }
            else
            {
                return -1;
            }
        }

        public string reglas_parteDerechar(int regla)
        {
            string c = this.listP[regla - 1];
            if (!"".Equals(c))
            {
                int pos = c.IndexOf(">") + 2; // busco en que posicion se encuentra ">" y 
                                              //le aumenento +2 para comenzar desde ahi la nueva cadena
                return c.Substring(pos, c.Length - pos);
            }
            else
            {
                return "";
            }
        }

        /**
         * Encontrar la regla de la tarte izquierda
         * 
         * return : retorna la regla que corresponde
         */
        public char encontrarLaParteIzquierdaDeLaRegla_NoTerminal(int regla)
        {
            return regla_parteIzquierda(this.listP[regla - 1]); // +1 porque las reglas de produccion comienza
        }

        /**
         * Separa la regla que corresponde de la pare izquierda
         * 
         * return : retorno el no terminal de la regla
         */
        public char regla_parteIzquierda(string c)
        {
            if (!"".Equals(c))
            {
                int pos = c.IndexOf(":") + 2; // busco en que posicion se encuentra ">" y 
                                              //le aumenento +2 para comenzar desde ahi la nueva cadena
                imprimir("caracter: " + c.Substring(pos, 1));
                return char.Parse(c.Substring(pos, 1)); // +1 para solo obtener el no terminal
            }
            else
            {
                return ' ';
            }
        }

        // ------ FIN USAR REGLAS DE PRODUCCION --------

        /**
         * obtengo el estado siguiente de la matriz de accion
         * 
         * return : retorno el estadp siguiemte
         */
        public int APSLR_accion(int estado, char sinonimo)
        {
            return this.matrizAccion[estado, this.listaX.FindIndex(x => x.Equals(sinonimo))];
        }

        /**
         * obtengo el estado siguiente de la matriz de GoTo
         * 
         * return : retorno el estado siguiemte
         */
        public int APSLR_GoTo(int estado, char sinonimo)
        {
            return this.matrizGoTo[estado, this.listaN.FindIndex(x => x.Equals(sinonimo))];
        }

        public void imprimir(string r)
        {
            Console.WriteLine(r);
        }

        /**
         * Genero la lista de entrada que incluye solo los simbolos
         * de forma ordenada al ejecutar el codigo ingresado
         * 
         * return : retorna una lista si simbolos de la lista tokens
         */
        public List<char> generarListaDeEntrada(List<Token> listTokens)
        {
            List<char> listaEntrada = new List<char>();
            foreach (Token t in listTokens)
            {
                listaEntrada.Add(t.sinonimo);
            }
            return listaEntrada;
        }

        /**
         * Remueve el sibolo que es reconocido por la pila
         * 
         */
        public void removerSimboloDeListaDeEntrada(char sinonimo)
        {
            if (this.listaDeEntrada.Count > 0)
            {
                if (this.listaDeEntrada[0] == sinonimo)
                {
                    this.listaDeEntrada.RemoveAt(0);
                }
            }
        }

        /**
         * Obtengo toda la regla de produccion con una posicion
         * 
         * return : retorno la regla de la posicion
         */
        private string obtenerReglaReduce(int numRegla)
        {
            if (numRegla > -1)
            {
                return "Reduce: " + this.listP[numRegla];
            }
            return "Error";
        }
    }
}
