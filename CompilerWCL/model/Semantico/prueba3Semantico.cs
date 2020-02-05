using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
using CompilerWCL.view.Lexico;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CompilerWCL.model.Semantico
{
    class prueba3Semantico
    {
        public Stack<Atributos> pilaSemantica;
        public List<Cuadruplos> list_codigo_intermedio;
        //traemos de lexico y sintactico
        public List<Regla_produccion> listaReglasReconocidas;

        // al hacer esta esta igualacion no se copia esta lista como un axiliar, sino se transforma en la lista principal 
        List<TDS> listaTDS; // si se agrega un nuevo TDS, se agregara en la lista principal

        //variables de semanticas
        private int gbl_numero_tupla;
        private int cont_temporal;

        FrmEditor frmEd;

        public prueba3Semantico(FrmEditor frmEd)
        {
            this.pilaSemantica = new Stack<Atributos>();
            this.list_codigo_intermedio = new List<Cuadruplos>();
            this.listaReglasReconocidas = Sintactico_srl.analizadorSRL.listReglasReconocidas;
            this.listaTDS = Lexico_tk.claseMgTDS.listaTDS; // si se agrega un nuevo TDS, se agregara en la lista principal

            //variables de semanticas
            this.gbl_numero_tupla = 0;
            this.cont_temporal = 0;

            this.frmEd = frmEd;
        } 

        /**
         * Metodo: genera un cuadrupo y me devuelve el numero de cuadruplo.
         * return: retorna el numero global de tupla.
         */
        public int Next()
        {
            gbl_numero_tupla++;
            Cuadruplos cdplo = new Cuadruplos();
            cdplo.numero_tupla = gbl_numero_tupla;
            list_codigo_intermedio.Add(cdplo);
            return gbl_numero_tupla;
        }

        /**
         * En la tupla activa rellena los campo correspondoientes
         * 
         */
        public void Gen(string operador, string operando_1, string operando_2, string result)
        {
            list_codigo_intermedio[gbl_numero_tupla - 1].operador = operador;
            list_codigo_intermedio[gbl_numero_tupla - 1].operando_1 = operando_1;
            list_codigo_intermedio[gbl_numero_tupla - 1].operando_2 = operando_2;
            list_codigo_intermedio[gbl_numero_tupla - 1].resultado = result;
        }

        /**
         * Genero un lista de enteros deacuardo al atributo que yo necesite
         * return: list de enteros
         */
        public List<int> make_list(int dir)
        {
            List<int> aux_list = new List<int>();
            aux_list.Add(dir);
            return aux_list;
        }

        /**
         * Concatena dos listas de enteros
         * return: Retorna un aliosta concatenada.
         */
        public List<int> merge(List<int> list_1, List<int> list_2)
        {
            List<int> merge_list = new List<int>();
            if (list_1 != null)
            {
                foreach (int elemento in list_1)
                {
                    merge_list.Add(elemento);
                }
            }

            if (list_2 != null)
            {
                foreach (int elemento in list_2)
                {
                    merge_list.Add(elemento);
                }
            }

            return merge_list;
        }

        /**
         * Rellena en las direcciones de la lista de cuadruplos
         * con esa direccion colocando le en la ultima tupla que es de resultado
         * 
         */
        public void backPatch(int direcion, List<int> lista)
        {
            foreach (int i in lista)
            {
                list_codigo_intermedio[i - 1].resultado = "GOTO: " + direcion;
            }
        }



        /**
         * Me crea un variable temporal para guradar el resultado de las 
         * operaciones como suma, resta, multiplicacion, division
         * retun: nombre variable temporal
         */
        public string demeTemporal(int tipo)
        {
            cont_temporal++;
            string temporal = "T" + cont_temporal;
            //guardar en la tabla de simbolos el temporal con el simbolo
            TDS variable = new TDS();
            variable.nametk = temporal;
            //variable.Numero = listaTDS.Count + 1;  // el indice es nuestra numero
            variable.type = tipo;
            switch (tipo)
            {
                case 1: variable.size = 4; break;
                case 2: variable.size = 8; break;
                case 3: variable.size = 1; break;
                case 4: variable.size = 80; break;
                case 5: variable.size = 1; break;
            }
            variable.value = null;
            listaTDS.Add(variable);

            return temporal;
        }

        public void imprimir(string s)
        {
            Console.WriteLine(s);
        }

        //Metodo que busca en la tabla de simbolos 
        public int buscarTipoTDS(string variable)
        {
            for (int i = 0; i < listaTDS.Count; i++)
            {
                if (listaTDS[i].nametk == variable)
                {
                    return listaTDS[i].type;
                }
            }
            return 0;
        }
        public void compararTipos(Atributos atr1, Atributos atr2)
        {
            int t1, t2;
            if (atr1.tipo == 0) // los dos son identificadores
            {
                t1 = buscarTipoTDS(atr1.lex);
                atr1.tipo = t1;
            }
            else
            {
                t1 = atr1.tipo;

            }
            if (atr2.tipo == 0)
            {
                t2 = buscarTipoTDS(atr2.lex);
                atr2.tipo = t2;
            }
            else
            {
                t2 = atr2.tipo;
            }
            //if (t1 > 2 || t2 > 2)
            if (t1 > 2 && t2 < 3)
            {
                frmEd.PrintSemantico(2, "ERROR: no se puede realizar operaciones con char, string o bool");
                //MessageBox.Show("ERROR: no se puede realizar operaciones con char, string o bool");
            }
            else
            {
                if (t1 != t2)
                {
                    frmEd.PrintSemantico(2, "ERROR TIPOS NO COMPATIBLES!!");
                    //mensaje de error tipos no compatibles                       
                }
            }
        }

        public object buscarVariableTDS(string variable)
        {
            foreach (TDS tds in listaTDS)
            {
                if (tds.nametk.Equals(variable))
                {
                    return tds.value;
                }
            }
            return null;
        }

        public void buscarVariableValorTDS(Atributos atr1, Atributos atr2)
        {
            atr1.valor = atr1.lex;
            foreach (TDS tds in listaTDS)
            {
                if (tds.nametk.Equals(atr1.lex))
                {
                    atr1.valor = tds.value;
                    break;
                }
            }

            atr2.valor = atr2.lex;
            foreach (TDS tds in listaTDS)
            {
                if (tds.nametk.Equals(atr2.lex))
                {
                    atr2.valor = tds.value;
                    break;
                }
            }
        }

        public void ponerValorEnTDS(string variable, object valor)
        {
            for (int cont = 0; cont < listaTDS.Count; cont++)
            {
                if (listaTDS[cont].nametk.Equals(variable))
                {
                    listaTDS[cont].value = valor;
                    return;
                }
            }
        }

        /**
         * Funcion principal del analizador semantico para la generación de codigo
         * 
         */
        public void generar_codigoCuadruplo()
        {
            int num_regla = 0;
            int tipo_variable = -1;
            Atributos atrp = null;
            Atributos atr1 = null;
            Atributos atr2 = null;
            Atributos atr3 = null;
            Atributos atr4 = null;
            Next();
            Gen("INI", null, null, null);
            imprimir("regla\tdato\ttipo\tpIzq\tpDer");
            foreach (Regla_produccion r in listaReglasReconocidas)
            {
                imprimir(r.numero_regla +
                                "\t" + r.dato + "\t" + r.tipo + "\t" + r.part_izquierda + "\t" + r.part_derecha);
            }

            foreach (TDS t in listaTDS)
            {
                imprimir("name: " + t.nametk + " size: " + t.size + " type: " + t.type + " value" + t.value);
            }

            for (int i = 0; i < this.listaReglasReconocidas.Count; i++)
            {
                atrp = new Atributos();
                atr1 = new Atributos();
                atr2 = new Atributos();
                atr3 = new Atributos();
                atr4 = new Atributos();
                num_regla = this.listaReglasReconocidas[i].numero_regla;
                imprimir("numRegla : " + num_regla + " : "+ this.listaReglasReconocidas[i].part_izquierda+" -> " +
                    this.listaReglasReconocidas[i].part_derecha);
                if (num_regla == 49)
                {

                }
                switch (num_regla)
                {
                    case 1:
                        atrp = pilaSemantica.Pop();
                        atr1 = pilaSemantica.Pop();
                        if (atrp.list_verdaderos != null)
                        {
                            backPatch(atrp.siguiente, atrp.list_verdaderos);
                        }
                        Next();
                        Gen("END", null, null, null);
                        break;

                    case 2://D --> U : <declarar> -> <undeclare>
                        atr1 = pilaSemantica.Pop(); // saco U
                        atr2 = pilaSemantica.Pop(); // saco D
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Undeclare";
                        pilaSemantica.Push(atrp);
                        /*atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Declare";
                        pilaSemantica.Push(atrp);*/
                        break;

                    case 3: // D  --> U D   :  <declarar> -> <undeclare> <declarar>
                        atr1 = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Declarar";
                        pilaSemantica.Push(atrp);
                        //atrp = new Atributos();
                        /*atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Declare";
                        pilaSemantica.Push(atrp);*/

                        /*atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "declarar";
                        pilaSemantica.Push(atrp);*/
                        break;

                    case 4: // U --> C L ;     : <undeclare> -> <tipo> <listavar>;
                        atr1 = pilaSemantica.Pop(); // saco C
                        atr2 = pilaSemantica.Pop(); // saco L
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Undeclare";
                        pilaSemantica.Push(atrp);
                        break;

                    case 5: // C -> e : <tipo> --> integer
                        Next(); //creo un espacio vacio
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        Gen("Declare", atrp.lex, null, null);
                        atrp.tipo = 1;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Tipo";
                        pilaSemantica.Push(atrp);
                        break;

                    case 6: // C -> r : <tipo> --> real 
                        Next(); //creo un espacio vacio
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        Gen("Declare", atrp.valor.ToString(), null, null);
                        atrp.tipo = 2;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Tipo";
                        pilaSemantica.Push(atrp);
                        break;

                    case 7: // C -> l : <tipo> --> bool
                        Next(); //creo un espacio vacio
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        Gen("Declare", atrp.lex, null, null);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.tipo = 5;
                        atrp.nombre = "Tipo";
                        pilaSemantica.Push(atrp);
                        break;

                    case 8: // C -> c : <tipo> --> char
                        Next(); //creo un espacio vacio
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        Gen("Declare", atrp.lex, null, null);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.tipo = 3;
                        atrp.nombre = "Tipo";
                        pilaSemantica.Push(atrp);
                        break;

                    case 9: // C -> d : <tipo> --> string
                        Next(); //creo un espacio vacio
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        Gen("Declare", atrp.lex, null, null);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.tipo = 4;
                        atrp.nombre = "Tipo";
                        pilaSemantica.Push(atrp);
                        break;

                    case 10: // L --> iN , L : <listavar> -> identificador <inicializa>,<listavar> 
                        atr1 = pilaSemantica.Pop(); // saco L
                        atr2 = pilaSemantica.Pop(); // saco N
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Listavar";
                        pilaSemantica.Push(atrp);
                        break;

                    case 11: // L -> iN : <listavar> -> identificador <inicializa>  "Metodo propio"
                        //atr1 = pilaSemantica.Pop();
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        tipo_variable = buscarTipoTDS(atrp.lex);
                        if (tipo_variable > 0)
                        {
                            atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                            atrp.nombre = "listavar";
                            atrp.tipo = tipo_variable;
                            atrp.valor = listaReglasReconocidas[i].dato;
                            pilaSemantica.Push(atrp);
                        }
                        break;

                    case 12:// N -> a : <inicializa> -> := literal   "Metodo propio"
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del L de la regla 11

                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        //atrp.valor = listaReglasReconocidas[i].dato;
                        //atrp.tipo = tipoDatoIdentificado(atrp.valor); // busco de que tipo es el valor para ingrsar al identificador
                        //atrp.tipo = tipoVariable(listaReglasReconocidas[i].tipo);
                        tipoVariableOtorgar(atr1, atrp, listaReglasReconocidas[i].tipo, atrp.lex);
                        compararTipos(atrp, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.nombre = "Inicializa";

                        Gen(":=", atrp.lex, null, atr1.lex);
                        pilaSemantica.Push(atrp);
                        break;

                    case 13: // N -> :f   :: <inicializa> -> := false   "Metodo propio"
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del L de la regla 11

                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.valor = listaReglasReconocidas[i].dato;
                        //atrp.tipo = tipoDatoIdentificado(atrp.valor); // busco de que tipo es el valor para ingrsar al identificador
                        atrp.tipo = 5;
                        compararTipos(atrp, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.nombre = "Inicializa";
                        Gen(":=", atrp.valor.ToString(), null, atr1.valor.ToString());
                        // poner_Valor_TDS(atr1.valor.ToString(), false);

                        pilaSemantica.Push(atrp);
                        break;

                    case 14: // N -> : a  : <inicializa> -> := true "Metodo propio"
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del L de la regla 11

                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.valor = listaReglasReconocidas[i].dato;
                        //atrp.tipo = tipoDatoIdentificado(atrp.valor); // busco de que tipo es el valor para ingrsar al identificador
                        atrp.tipo = 5;
                        compararTipos(atrp, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.nombre = "Inicializa";
                        Gen(":=", atrp.valor.ToString(), null, atr1.valor.ToString());
                        // poner_Valor_TDS(atr1.valor.ToString(), false);

                        pilaSemantica.Push(atrp);
                        break;

                    case 15: // N -> _ : <inicializa> -> _
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        atrp.tipo = 5;
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        Gen(null, null, null, atrp.lex);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Inicializa";
                        pilaSemantica.Push(atrp);
                        break;

                    case 16:// I -> X : <instrucciones> -> <instruccion>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instrucciones";
                        pilaSemantica.Push(atrp);
                        break;

                    case 17: // I->XI : <instrucciones> -> <instrucción><instrucciones>
                        atr2 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de I
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de X

                        atrp.principio = atr1.principio;
                        atrp.list_verdaderos = merge(atr2.list_verdaderos, atr1.list_verdaderos);
                        atrp.siguiente = atr2.siguiente;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instrucciones";
                        pilaSemantica.Push(atrp);
                        break;

                    case 18:// X -> Y : <instruccion> -> <if>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 19:// X -> W : <instruccion> -> <while>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 20:// X->S : <instrucción> -> <for>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 21:// X->V : <instrucción> -> <escribir>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 22:// X->R : <instrucción> -> <leer>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 23: // X -> O : <instruccion> -> <do>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 24: // X->M : <instrucción> -> <incremento>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 25: // X -> A : <instruccion> -> <asigna>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 26: // X --> ] : <instruccion> --> break
                        atrp.principio = Next();
                        Gen("Break", null, null, "GOTO :");
                        atrp.list_verdaderos = make_list(atrp.principio);
                        atrp.siguiente = atrp.principio + 1;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instrucción";
                        pilaSemantica.Push(atrp);
                        break;

                    case 27: // Y --> q (Z){I}Q  :  <if> -> if (<condicion>) {<instruciones>} <else>
                        atr3 = pilaSemantica.Pop(); // caso Q
                        atr2 = pilaSemantica.Pop(); // caso I
                        atr1 = pilaSemantica.Pop(); // caso Z

                        atrp.principio = atr1.principio;
                        backPatch(atr2.principio, atr1.list_verdaderos);
                        if (atr3.principio == 0)
                        {
                            atrp.siguiente = atr2.siguiente;
                            backPatch(atr2.siguiente, atr1.list_falsos);
                        }
                        else
                        {
                            backPatch(atr3.principio, atr1.list_falsos);
                            atrp.siguiente = atr3.siguiente;
                        }
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "If";
                        pilaSemantica.Push(atrp);
                        break;

                    case 28: // Q --> m {I}   :  <else> -> continue {<instrucciones>}
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Else";
                        pilaSemantica.Push(atrp);
                        break;
                    case 29: // Q--> vacio : <else> -> vacio
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Else";
                        atrp.principio = 0;
                        atrp.siguiente = 0;
                        pilaSemantica.Push(atrp);

                        break;
                    case 30: // W->x(Z){I} : <while> -> while (<condición>){<instrucciones>}
                        atr2 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de I
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de Z

                        //backPatch(atr2.principio, atr1.list_verdaderos);
                        backPatch(atr1.siguiente, atr1.list_verdaderos);
                        atrp.principio = atr2.principio;
                        atrp.siguiente = Next() + 1;
                        Gen(null, null, null, "GOTO: " + atr1.principio);
                        backPatch(atrp.siguiente, atr1.list_falsos);

                        // realizamos un backPatch cuando se aplica un bleak en I -> instricciones
                        if (atr2.list_verdaderos != null)
                        {
                            backPatch(atrp.siguiente, atr2.list_verdaderos);
                        }                      
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        //backPatch(atrp.siguiente, atr2.list_verdaderos);
                        atrp.nombre = "While";
                        pilaSemantica.Push(atrp);
                        break;
                    case 31: // S -> g ( iN ) { I } ( Z ) M ; -> for ( identificador<inicializa> ) { instrucciones> }   ******
                             // ( <condicion> ) <incremento> ; 
                        atr1 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de M -> Incremento
                        atr2 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de Z -> Condicion
                        atr3 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de I -> Instrucciones
                        atr4 = pilaSemantica.Pop(); //aqui esta el valor del no terminal de N -> Inicializa

                        backPatch(atr1.principio, atr2.list_verdaderos); // si esta correcto vaya al incremento
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;

                        Gen(null, null, null, "GOTO: " + atr4.siguiente); // cuando inicializa tengo el siguiente que es la instruccion
                        backPatch(atrp.siguiente, atr2.list_falsos);
                        // realizamos un backPatch cuando se aplica un bleak en I -> instricciones
                        if (atr3.list_verdaderos != null)
                        {
                            backPatch(atrp.siguiente, atr3.list_verdaderos);
                        }
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        //backPatch(atrp.siguiente, atr3.list_verdaderos);
                        atrp.nombre = "For";
                        pilaSemantica.Push(atrp);

                        break;
                    case 32: // M -> hi+ : <incremento> -> increment identificador +
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.tipo = buscarTipoTDS(atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.nombre = "Increment";

                        atr1.valor = demeTemporal(atrp.tipo);
                        Gen("+", "1", atrp.valor.ToString(), atr1.valor.ToString());
                        atr1.principio = Next();
                        atr1.principio = atr1.principio + 1;

                        Gen(":=", atr1.valor.ToString(), null, atrp.valor.ToString());
                        pilaSemantica.Push(atrp);
                        break;

                    case 33: // M -> hi- : <incremento> -> increment identificador -
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.tipo = buscarTipoTDS(atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.nombre = "Increment";

                        atr1.valor = demeTemporal(atrp.tipo);
                        Gen("-", "1", atrp.valor.ToString(), atr1.valor.ToString());
                        atr1.principio = Next();
                        atr1.principio = atr1.principio + 1;

                        Gen(":=", atr1.valor.ToString(), null, atrp.valor.ToString());
                        pilaSemantica.Push(atrp);
                        break;
                    case 34: // O -> y { I } x ( Z ) ; => <do> -> do { <instrucciones> } while ( <condicion> ) ;
                        atr1 = pilaSemantica.Pop(); // Z -> Condicion
                        atr2 = pilaSemantica.Pop(); // I -> Instrucciones

                        backPatch(atr2.principio, atr1.list_verdaderos);
                        atrp.principio = atr2.principio;
                        atrp.siguiente = atr1.siguiente;

                        /*atrp.principio = atr2.principio;
                        atrp.siguiente = Next() + 1;
                        Gen(null, null, null, "GOTO: " + atr1.principio);
                        backPatch(atrp.siguiente, atr1.list_falsos);*/

                        backPatch(atrp.siguiente, atr1.list_falsos);

                        // realizamos un backPatch cuando se aplica un bleak en I -> instricciones
                        if (atr2.list_verdaderos != null)
                        {
                            backPatch(atrp.siguiente, atr2.list_verdaderos);
                        }
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        //backPatch(atrp.siguiente, atr2.list_verdaderos);
                        atrp.nombre = "Do while";
                        pilaSemantica.Push(atrp);
                        break;
                    case 35: // V -->k(G);  :<escribir> -> print(<argumentos>);
                        atr1 = pilaSemantica.Pop(); //saco G
                        string[] argumentos = atr1.lex.Split(',');
                        for (int k = 0; k < argumentos.Length; k++)
                        {
                            if (k == 0)
                            {
                                atrp.principio = Next();
                                atrp.siguiente = atrp.principio + 1;
                            }
                            else
                            {
                                atrp.siguiente = Next() + 1;
                            }
                            Gen("Escribir", null, null, argumentos[k]);
                            atrp.valor = buscarVariableTDS(argumentos[k]);
                           //MessageBox.Show("Variable " + argumentos[k] + ": " + atrp.valor.ToString());
                            frmEd.PrintSemantico(1, argumentos[k] + ": " + atrp.valor.ToString());



                        }
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Escribir";
                        pilaSemantica.Push(atrp);
                        break;
                    case 36: // R --> j(G);  :  <leer>  ->  get(<argumentos>);
                        atr1 = pilaSemantica.Pop(); //saco G
                        string[] argumentos1 = atr1.lex.ToString().Split(',');
                        for (int k = 0; k < argumentos1.Length; k++)
                        {
                            if (k == 0)
                            {
                                atrp.principio = Next();
                                atrp.siguiente = atrp.principio + 1;
                            }
                            else
                            {
                                atrp.siguiente = Next() + 1;
                            }
                            Gen("Leer", null, null, argumentos1[k]);
                            atrp.valor = buscarVariableTDS(argumentos1[k]);
                            MessageBox.Show("Variable " + argumentos1[k] + ": ");
                            atrp.valor = 0;
                        }
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Leer";
                        pilaSemantica.Push(atrp);
                        break;
                    case 37: // G --> i , G1   :  <argumentos> -> identificador, <argumentos>
                        atr1 = pilaSemantica.Pop(); //saco G1
                        atrp.lex = listaReglasReconocidas[i].dato.ToString() + "," + atr1.lex;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Argumentos";
                        pilaSemantica.Push(atrp);
                        break;
                    case 38: // G --> i      :  <argumentos>  -> identificador
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        // hacer una funcion que busque en la tabla de simbolos y me devuelva el valor
                        //atr.Valor = buscarVariableTDS(atr.Lex);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Argumentos";
                        pilaSemantica.Push(atrp);
                        break;
                    case 39: // A->i:E;     : <asigna> -> identificador := <expresión>
                        /*atr1 = pilaSemantica.Pop();
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = listaReglasReconocidas[i].dato;
                        compararTipos(atrp, atr1);
                        Gen(":=", atr1.valor.ToString(), null, atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Asigna";
                        //atrp.principio = atr1.principio;
                        atrp.tipo = atr1.tipo;
                        pilaSemantica.Push(atrp);*/

                        atr1 = pilaSemantica.Pop(); //esto es igual a lo que tengo de expresion
                        atrp.siguiente = Next();
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        compararTipos(atrp, atr1);
                        Gen(":=", atr1.lex, null, atrp.lex);
                        atrp.valor = atr1.valor;
                        // guardar ese valor en la tabla de simbolos
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.tipo = atr1.tipo;
                        atrp.principio = atr1.principio;
                        //atrp.principio = atrp.siguiente;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Asigna";
                        pilaSemantica.Push(atrp);

                        /*atr1 = pilaSemantica.Pop();
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio;
                        atrp.lex = listaReglasReconocidas[i].dato.ToString();
                        compararTipos(atrp, atr1);
                        Gen(":=", atr1.lex, null, atrp.lex);
                        atrp.valor = atr1.valor;
                        // guardar ese valor en la tabla de simbolos
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.tipo = atr1.tipo;
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Asigna";
                        pilaSemantica.Push(atrp);*/
                        break;
                    case 40: //Z -> E B E   :  <condicion> -> <expresion><operel><expresion>
                        atr2 = pilaSemantica.Pop(); // E1
                        atr3 = pilaSemantica.Pop(); // B
                        atr1 = pilaSemantica.Pop(); // E2

                        //ANALIZO EL TIPO DE OPERADOR QUE RECIBO
                        if (atr3.lex == "||" || atr3.lex == "&&")
                        {
                            if (atr1.principio == 0)
                            {
                                atr1.principio = Next();
                                atr1.list_verdaderos = make_list(atr1.principio);
                                Gen("if", atr1.lex, null, "Goto: ");
                                atr1.siguiente = Next();
                                atr1.list_falsos = make_list(atr1.siguiente);
                                Gen(null, null, null, "Goto: ");
                                atr1.siguiente++;
                            }
                            if (atr2.principio == 0)
                            {
                                atr2.principio = Next();
                                atr2.list_verdaderos = make_list(atr2.principio);
                                Gen("if", atr2.lex, null, "Goto: ");
                                atr2.siguiente = Next();
                                atr2.list_falsos = make_list(atr2.siguiente);
                                Gen(null, null, null, "Goto: ");
                                atr2.siguiente++;
                            }
                            if (atr3.lex == "||")
                            {
                                //voy a hacer codigo para el OR  
                                atrp.principio = atr1.principio;
                                atrp.siguiente = atr2.siguiente;
                                atrp.list_verdaderos = merge(atr1.list_verdaderos, atr2.list_verdaderos);
                                atrp.list_falsos = atr2.list_falsos;
                                backPatch(atr1.siguiente, atr1.list_falsos); // ****
                                atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                                atrp.nombre = "Condicion";
                                pilaSemantica.Push(atrp);
                            }

                            if (atr3.lex == "&&")
                            {
                                //voy a generar codigo para el AND
                                atrp.principio = atr1.principio;
                                atrp.siguiente = atr2.siguiente;
                                atrp.list_verdaderos = atr2.list_verdaderos;
                                atrp.list_falsos = merge(atr1.list_falsos, atr2.list_falsos);
                                backPatch(atr2.principio, atr1.list_verdaderos);
                                atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                                atrp.nombre = "Condicion";
                                pilaSemantica.Push(atrp);
                            }
                            //Agregado aqui!!!!!!!!!!!
                            //atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                            //atrp.nombre = "Condicion";


                        }
                        else
                        {
                            //TODOS LOS OTROS OPERADORES                          
                            atrp.principio = Next();
                            atrp.list_verdaderos = make_list(atrp.principio);
                            Gen(atr3.lex, atr1.lex, atr2.lex, "Goto: ");
                            atrp.siguiente = Next();
                            atrp.list_falsos = make_list(atrp.siguiente);
                            Gen(null, null, null, "Goto: ");
                            atrp.siguiente++;

                            //Agregado aqui!!!!!!!!!!!
                            atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                            atrp.nombre = "Condicion";

                            pilaSemantica.Push(atrp);
                        }
                        break;

                    case 41: // B -> | : <operel> -> ||
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "||";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 42: // B -> &  : <operel> -> &&
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "&&";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 43: // B -> #  : <operel> -> <>
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "<>";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 44: // B -> >  : <operel> -> >
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = ">";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 45: // B -> <  : <operel> -> <
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "<";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 46: // B -> $  : <operel> -> >=
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = ">=";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 47: // B -> %  : <operel> -> <=
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "<=";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 48: // B -> =  : <operel> -> =
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.lex = "=";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 49: //E->E + T : <expresion> -> <expresion> + <termino>  
                        atr2 = pilaSemantica.Pop(); //esto es igual a lo que tengo termino T
                        atr1 = pilaSemantica.Pop();// esto es igual a expresion  E   
                        compararTipos(atr2, atr1);

                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        if (atr2.principio != 0)
                        {
                            atrp.principio = atr2.principio;
                        }

                        atrp.lex = demeTemporal(atr2.tipo);
                        Gen("+", atr2.lex, atr1.lex, atrp.lex);
                        buscarVariableValorTDS(atr1, atr2); // busc el valor en TDS de un identificador
                        //atrp.valor = Convert.ToDouble(atr2.valor) + Convert.ToDouble(buscarVariableTDS(atr1.lex));
                        atrp.valor = Convert.ToDouble(atr1.valor) + Convert.ToDouble(atr2.valor);
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        //atrp.principio = atr2.principio;
                        pilaSemantica.Push(atrp);
                        break;

                    case 50: // E->E - T : <expresion> -> <expresion> - <termino>
                        atr2 = pilaSemantica.Pop(); //esto es igual a lo que tengo termino T
                        atr1 = pilaSemantica.Pop();// esto es igual a expresion  E       
                        compararTipos(atr2, atr1);
                        //atrp.principio = Next();

                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        if (atr2.principio != 0)
                        {
                            atrp.principio = atr2.principio;
                        }

                        atrp.lex = demeTemporal(atr2.tipo);
                        Gen("-", atr2.lex, atr1.lex, atrp.lex);
                        buscarVariableValorTDS(atr1, atr2); // busc el valor en TDS de un identificador
                        //atrp.valor = Convert.ToDouble(atr2.valor) - Convert.ToDouble(buscarVariableTDS(atr1.lex));
                        atrp.valor = Convert.ToDouble(atr1.valor) - Convert.ToDouble(atr2.valor);
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        //atrp.principio = atr2.principio;
                        pilaSemantica.Push(atrp);
                        break;

                    case 51: // E-->T : <expresion> --> <termino>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 52://T-> T*F <termino>--> <termino> * <factor>
                        atr2 = pilaSemantica.Pop(); //esto es igual a lo que tengo termino F
                        atr1 = pilaSemantica.Pop();// esto es igual a expresion  T     

                        compararTipos(atr2, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        if (atr2.principio != 0)
                        {
                            atrp.principio = atr2.principio;
                        }

                        atrp.lex = demeTemporal(atr2.tipo);
                        Gen("*", atr2.lex, atr1.lex, atrp.lex);
                        buscarVariableValorTDS(atr1, atr2); // busc el valor en TDS de un identificador
                        //atrp.valor = Convert.ToDouble(atr2.valor) * Convert.ToDouble(buscarVariableTDS(atr1.lex));
                        atrp.valor = Convert.ToDouble(atr1.valor) * Convert.ToDouble(atr2.valor);
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 53: //T-> T/F <termino>--> <termino> / <factor>
                        atr2 = pilaSemantica.Pop(); //esto es igual a lo que tengo termino F
                        atr1 = pilaSemantica.Pop();// esto es igual a expresion  T

                        compararTipos(atr2, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        if (atr2.principio != 0)
                        {
                            atrp.principio = atr2.principio;
                        }

                        atrp.lex = demeTemporal(atr2.tipo);
                        Gen("/", atr2.lex, atr1.lex, atrp.lex);
                        buscarVariableValorTDS(atr1, atr2); // busc el valor en TDS de un identificador
                        //atrp.valor = Convert.ToDouble(atr2.valor) / Convert.ToDouble(buscarVariableTDS(atr1.lex));
                        atrp.valor = Convert.ToDouble(atr1.valor) / Convert.ToDouble(atr2.valor);
                        ponerValorEnTDS(atrp.lex, atrp.valor);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 54://T-> T^F <termino>--> <termino> ^ <factor>
                        atr2 = pilaSemantica.Pop(); //esto es igual a lo que tengo termino F
                        atr1 = pilaSemantica.Pop();// esto es igual a expresion  T

                        compararTipos(atr2, atr1);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        if (atr2.principio != 0)
                        {
                            atrp.principio = atr2.principio;
                        }

                        atrp.lex = demeTemporal(atr2.tipo);
                        Gen("^", atr2.lex, atr1.lex, atrp.lex);
                        buscarVariableValorTDS(atr1, atr2); // busc el valor en TDS de un identificador
                        //atrp.valor = Math.Pow(Convert.ToDouble(atr2.valor), Convert.ToDouble(atr1.valor));
                        atrp.valor = Math.Pow(Convert.ToDouble(atr1.valor), Convert.ToDouble(atr2.valor));
                        ponerValorEnTDS(atrp.lex, atrp.valor);

                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 55:// T -> F : <termino> -> <factor>
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 56: // F -> i : <factor> -> identificador
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.lex = atrp.valor.ToString();

                        //atrp.principio = gbl_numero_tupla + 1; // obtengo el principio de Intrucciones que me sirve
                                                               // para el do while cuando en instrucciones solo ingreso #a := #a ;
                                                               // si no pogo en el GOTO : 0 de la lista de verdadero

                        pilaSemantica.Push(atrp);
                        break;

                    case 57:// F -> (E) : < factor > -> < expresion >
                        atrp = pilaSemantica.Pop();
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.lex = atrp.valor.ToString();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        pilaSemantica.Push(atrp);
                        break;

                    case 58: // F -> a : <factor> -> literal
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.lex = atrp.valor.ToString();
                        atrp.nombre = "Factor";
                        atrp.tipo = tipoVariable(listaReglasReconocidas[i].tipo); // obtengo el tipo de la variable

                        //atrp.principio = gbl_numero_tupla + 1; // obtengo el principio de Intrucciones que me sirve
                                                               // para el do while cuando en instrucciones solo ingreso #a := 1 ;
                                                               // si no pogo en el GOTO : 0 de la lista de verdadero

                        pilaSemantica.Push(atrp);
                        break;

                    case 59: // F -> v : <factor> -> true
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.tipo = 5;
                        atrp.valor = true;
                        atrp.lex = "true";
                        pilaSemantica.Push(atrp);
                        break;

                    case 60: // F -> f : <factor> -> falso
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.tipo = 5;
                        atrp.valor = false;
                        atrp.lex = "false";
                        pilaSemantica.Push(atrp);
                        break;

                    default:
                        break;
                }
            }
            imprimir("op" + "\top1" + "\top2" + "\tres");
            int cont = 1;
            foreach (Cuadruplos c in list_codigo_intermedio)
            {
                imprimir(cont + "\t" + c.operador + "\t" + c.operando_1 + "\t" + c.operando_2 + "\t" + c.resultado);
                cont++;
            }
        }

        /**
         * Obtengo el tipo de la variable
         * return : retorno el id del tipo de la variable
         */
        public int tipoVariable(string nombretoken)
        {
            if (nombretoken == "literalentero")
            {
                return 1;
            }
            else if (nombretoken == "literalreal")
            {
                return 2;
            }
            else if (nombretoken == "literalcadena")
            {
                return 4;
            }
            else if (nombretoken == "literalchar")
            {
                return 3;
            }
            return -1;
        }

        public void tipoVariableOtorgar(Atributos atr1, Atributos atr, string nombretoken, string objValor)
        {
            if (nombretoken == "literalentero")
            {
                atr.tipo = 1;
                atr.valor = Convert.ToInt32(objValor);
                ponerValorEnTDS(atr1.lex, atr.valor);
            }
            else if (nombretoken == "literalreal")
            {
                atr.tipo = 2;
                atr.valor = Convert.ToDouble(objValor);
                ponerValorEnTDS(atr1.lex, atr.valor);
            }
            else if (nombretoken == "literalcadena")
            {
                atr.tipo = 4;
                atr.valor = objValor;
                ponerValorEnTDS(atr1.lex, atr.valor);
            }
            else if (nombretoken == "literalchar")
            {
                atr.tipo = 3;
                atr.valor = Convert.ToChar(objValor);
                ponerValorEnTDS(atr1.lex, atr.valor);
            }
        }

        public int tipoDatoIdentificado(Object obj)
        {
            if (int.TryParse(obj.ToString(), out int resul) == true)
            {
                return 1;
            }
            else if (double.TryParse(obj.ToString(), out double resul2) == true)
            {
                return 2;
            }
            else if (char.TryParse(obj.ToString(), out char resul3) == true)
            {
                return 3;
            }
            else if (bool.TryParse(obj.ToString(), out bool resul4) == true)
            {
                return 5;
            }
            else
            {
                return 4;
            }
        }
    }
}

