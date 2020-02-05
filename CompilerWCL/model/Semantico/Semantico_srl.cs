using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CompilerWCL.model.Semantico
{
    class Semantico_srl
    {
     
        public Semantico_srl()
        {

        }
        public Stack<Atributos> pilaSemantica = new Stack<Atributos>();
        public List<Cuadruplos> list_codigo_intermedio = new List<Cuadruplos>();
        //traemos de lexico y sintactico
        public List<Regla_produccion> listaReglasReconocidas = Sintactico_srl.analizadorSRL.listReglasReconocidas;

        // al hacer esta esta igualacion no se copia esta lista como un axiliar, sino se transforma en la lista principal 
        List<TDS> listaTDS = Lexico_tk.claseMgTDS.listaTDS; // si se agrega un nuevo TDS, se agregara en la lista principal

        

        //variables de semanticas
        private int gbl_numero_tupla = 0;
        private int cont_temporal = 0;

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
        public List<int> make_list(int valor)
        {
            List<int> aux_list = new List<int>();
            aux_list.Add(valor);
            return aux_list;
        }

        /**
         * Concatena dos listas de enteros
         * return: Retorna un aliosta concatenada.
         */
        public List<int> merge(List<int> list_1, List<int> list_2)
        {
            List<int> merge_list = new List<int>();

            for (int i = 0; i < list_1.Count; i++)
            {
                merge_list.Add(list_1[i]);
            }

            for (int i = 0; i < list_2.Count; i++)
            {
                merge_list.Add(list_2[i]);
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
                list_codigo_intermedio[i-1].resultado = "GOTO: " + direcion; // ******* CAMBIO ********
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
        public int buscarTipoTDS(string valor)
        {
            for (int i = 0; i < listaTDS.Count; i++)
            {
                if (listaTDS[i].nametk == valor)
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
                t1 = buscarTipoTDS(atr1.valor.ToString());
                atr1.tipo = t1;
            }
            else
            {
                t1 = atr1.tipo;

            }
            if (atr2.tipo == 0)
            {
                t2 = buscarTipoTDS(atr2.valor.ToString());
                atr2.tipo = t2;
            }
            else
            {
                t2 = atr2.tipo;
            }
            //if (t1 > 2 || t2 > 2)
            if (t1 > 2 && t2 < 3)
            {
                MessageBox.Show("ERROR: no se puede realizar operaciones con char, string o bool");
            }
            else
            {
                if (t1 != t2)
                {
                    MessageBox.Show("ERROR TIPOS NO COMPATIBLES!!");
                    //mensaje de error tipos no compatibles                       
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
            Atributos atrp = null;
            Atributos atr1 = null;
            Atributos atr2 = null;
            Atributos atr3 = null;

            imprimir("regla\tdato\ttipo\tpIzq\tpDer");
            foreach (Regla_produccion r in listaReglasReconocidas)
            {
                imprimir(r.numero_regla +
                                "\t"  + r.dato + "\t" + r.tipo  + "\t" + r.part_izquierda + "\t" + r.part_derecha);
            }

            foreach(TDS t in listaTDS)
            {
                imprimir("name: "+t.nametk+" size: "+t.size+" type: "+t.type+ " value"+t.value);
            }

            for (int i = 0; i < this.listaReglasReconocidas.Count; i++)
            {
                atrp = new Atributos();
                atr1 = new Atributos();
                atr2 = new Atributos();
                atr3 = new Atributos();
                num_regla = this.listaReglasReconocidas[i].numero_regla;
                if (num_regla == 49)
                {

                }
                switch (num_regla)
                {
                    case 1:
                        break;

                    case 2: //solo DE PRUEBA
                        /*atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Declare";
                        pilaSemantica.Push(atrp);
                        break;*/

                    case 3:// D -> U : <declarar> -> <undeclare>
                        //atrp = new Atributos();
                        /*atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Declare";
                        pilaSemantica.Push(atrp);*/

                        /*atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "declarar";
                        pilaSemantica.Push(atrp);
                        break;*/

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 8:
                        break;

                    case 9:
                        break;

                    case 10:
                        break;

                    case 11:
                        break;

                    case 12:
                        break;

                    case 13:
                        break;

                    case 14:
                        break;

                    case 15:
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
                    case 26:
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

                        backPatch(atr1.siguiente, atr1.list_verdaderos);
                        atrp.principio = atr2.principio;
                        atrp.siguiente = Next() + 1;
                        Gen(null, null, null, "Goto: "+atr1.principio);
                        backPatch(atrp.siguiente, atr1.list_falsos);
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "While";
                        pilaSemantica.Push(atrp);
                        break;
                    case 31:


                        break;
                    case 32:

                        break;

                    case 33:
                        break;
                    case 34:
                        break;
                    case 35:
                        break;
                    case 36:
                        break;
                    case 37:
                        break;
                    case 38:
                        break;
                    case 39: // A->i:E;     : <asigna> -> identificador := <expresión>
                        atr1 = pilaSemantica.Pop();
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = listaReglasReconocidas[i].dato;
                        compararTipos(atrp, atr1);
                        Gen(":=", atr1.valor.ToString(), null, atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Asigna";
                        //atrp.principio = atr1.principio;
                        atrp.tipo = atr1.tipo;
                        pilaSemantica.Push(atrp);
                        break;
                    case 40: //Z -> E B E   :  <condicion> -> <expresion><operel><expresion>
                        atr2 = pilaSemantica.Pop(); // E1
                        atr3 = pilaSemantica.Pop(); // B
                        atr1 = pilaSemantica.Pop(); // E2

                        //ANALIZO EL TIPO DE OPERADOR QUE RECIBO
                        if (atr3.valor.ToString() == "||" || atr3.valor.ToString() == "&&")
                        {
                            if (atr1.principio == 0)
                            {
                                atr1.principio = Next();
                                atr1.list_verdaderos = make_list(atr1.principio);
                                Gen("if", atr1.valor.ToString(), null, "Goto: ");
                                atr1.siguiente = Next();
                                atr1.list_falsos = make_list(atr1.siguiente);
                                Gen(null, null, null, "Goto: ");
                                atr1.siguiente++;
                            }
                            if (atr2.principio == 0)
                            {
                                atr2.principio = Next();
                                atr2.list_verdaderos = make_list(atr2.principio);
                                Gen("if", atr2.valor.ToString(), null, "Goto: ");
                                atr2.siguiente = Next();
                                atr2.list_falsos = make_list(atr2.siguiente);
                                Gen(null, null, null, "Goto: ");
                                atr2.siguiente++;
                            }
                            if (atr3.valor.ToString() == "||")
                            {
                                //voy a hacer codigo para el OR  
                                atrp.principio = atr1.principio;
                                atrp.siguiente = atr2.siguiente;
                                atrp.list_verdaderos = merge(atr1.list_verdaderos, atr2.list_verdaderos);
                                atrp.list_falsos = atr2.list_falsos;
                                backPatch(atr1.siguiente, atr1.list_falsos); // ****
                                pilaSemantica.Push(atrp);
                            }

                            if (atr3.valor.ToString() == "&&")
                            {
                                //voy a generar codigo para el AND
                                atrp.principio = atr1.principio;
                                atrp.siguiente = atr2.siguiente;
                                atrp.list_verdaderos = atr2.list_verdaderos;
                                atrp.list_falsos = merge(atr1.list_falsos, atr2.list_falsos);
                                backPatch(atr2.principio, atr1.list_verdaderos);
                                pilaSemantica.Push(atrp);
                            }

                        }                      
                        else
                        {
                            //TODOS LOS OTROS OPERADORES                          
                            atrp.principio = Next();
                            atrp.list_verdaderos = make_list(atrp.principio);
                            Gen(atr3.valor.ToString(), atr1.valor.ToString(), atr2.valor.ToString(), "Goto: ");
                            atrp.siguiente = Next();
                            atrp.list_falsos = make_list(atrp.siguiente);
                            Gen(null, null, null, "Goto: ");
                            atrp.siguiente++;
                            pilaSemantica.Push(atrp);
                        }
                        break;

                    case 41: // B -> | : <operel> -> ||
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "||";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 42: // B -> &  : <operel> -> &&
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "&&";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 43: // B -> #  : <operel> -> <>
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "<>";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 44: // B -> >  : <operel> -> >
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = ">";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 45: // B -> <  : <operel> -> <
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "<";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 46: // B -> $  : <operel> -> >=
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = ">=";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 47: // B -> %  : <operel> -> <=
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "<=";
                        atrp.tipo = 5;
                        pilaSemantica.Push(atrp);
                        break;

                    case 48: // B -> =  : <operel> -> =
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Operel";
                        atrp.valor = "=";
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

                        atrp.valor = demeTemporal(atr2.tipo);
                        Gen("+", atr2.valor.ToString(), atr1.valor.ToString(), atrp.valor.ToString());
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

                        atrp.valor = demeTemporal(atr2.tipo);
                        Gen("-", atr2.valor.ToString(), atr1.valor.ToString(), atrp.valor.ToString());
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
                       
                        atrp.valor = demeTemporal(atr2.tipo);
                        Gen("*", atr2.valor.ToString(), atr1.valor.ToString(), atrp.valor.ToString());
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
                        
                        atrp.valor = demeTemporal(atr2.tipo);
                        Gen("/", atr2.valor.ToString(), atr1.valor.ToString(), atrp.valor.ToString());
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
                        
                        atrp.valor = demeTemporal(atr2.tipo);
                        Gen("^", atr2.valor.ToString(), atr1.valor.ToString(), atrp.valor.ToString());
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
                        pilaSemantica.Push(atrp);
                        break;

                    case 57:// F -> (E) : < factor > -> < expresion >
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        pilaSemantica.Push(atrp);
                        break;

                    case 58: // F -> a : <factor> -> literal
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.tipo = tipoVariable(listaReglasReconocidas[i].tipo); // obtengo el tipo de la variable
                        pilaSemantica.Push(atrp);
                        break;

                    case 59: // F -> v : <factor> -> true
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.tipo = 5;
                        atrp.valor = true;
                        pilaSemantica.Push(atrp);
                        break;

                    case 60: // F -> f : <factor> -> falso
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.tipo = 5;
                        atrp.valor = false;
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
    }
}
