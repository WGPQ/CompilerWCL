using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.model.Semantico
{
    class pruebaSemantico
    {
        public Stack<Atributos> pilaSemantica = new Stack<Atributos>();
        public List<Cuadruplos> list_codigo_intermedio = new List<Cuadruplos>();
        //traemos de lexico y sintactico
        List<Regla_produccion> listaReglasReconocidas = Sintactico_srl.analizadorSRL.listReglasReconocidas;

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
                list_codigo_intermedio[i].resultado = "GOTO: " + direcion;
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
            //variable.Numero = listaTDS.Count + 1;
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
            if (t1 > 2 || t2 > 2)
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
            int posTS = 0;
            int tipoDato = -1;
            int tipoValor = -1;

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
                num_regla = this.listaReglasReconocidas[i].numero_regla;
                if (num_regla == 56)
                {

                }
                switch (num_regla)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:// D -> U : <declarar> -> <undeclare>
                           /* atrp = new Atributos();
                            atrp = pilaSemantica.Pop();
                            atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                            atrp.nombre = "Declare";
                            pilaSemantica.Push(atrp);*/
                        break;

                    case 4:
                        break;

                    case 5:
                        tipoDato = tipoDatoDeclarado(listaReglasReconocidas[i].dato.ToString());
                        break;

                    case 6:
                        tipoDato = tipoDatoDeclarado(listaReglasReconocidas[i].dato.ToString());
                        break;

                    case 7:
                        tipoDato = tipoDatoDeclarado(listaReglasReconocidas[i].dato.ToString());
                        break;

                    case 8:
                        tipoDato = tipoDatoDeclarado(listaReglasReconocidas[i].dato.ToString());
                        break;

                    case 9:
                        tipoDato = tipoDatoDeclarado(listaReglasReconocidas[i].dato.ToString());
                        break;

                    case 10:
                        break;

                    case 11:
                        break;

                    case 12:
                        tipoValor = tipoDatoIdentificado(listaReglasReconocidas[i].dato);     
                        if (resultadoAsigne(tipoDato, tipoValor, listaReglasReconocidas[i].dato))
                        {
                            listaTDS[posTS].value = listaReglasReconocidas[i].dato;
                        }
                        posTS++;
                        break;

                    case 13: // false
                        tipoValor = tipoDatoIdentificado(listaReglasReconocidas[i].dato);
                        if (resultadoAsigne(tipoDato, tipoValor, listaReglasReconocidas[i].dato))
                        {
                            listaTDS[posTS].value = listaReglasReconocidas[i].dato;
                        }
                        posTS++;
                        break;

                    case 14: // true
                        tipoValor = tipoDatoIdentificado(listaReglasReconocidas[i].dato);
                        if (resultadoAsigne(tipoDato, tipoValor, listaReglasReconocidas[i].dato))
                        {
                            listaTDS[posTS].value = listaReglasReconocidas[i].dato;
                        }
                        posTS++;
                        break;

                    case 15: // N -> _ <inicializa> -> _
                        posTS++; // incremento x q es un identifiacdor sin declare
                        break;

                    case 16:// I -> X : <instrucciones> -> <instruccion>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instrucciones";
                        pilaSemantica.Push(atrp);
                        break;

                    case 17:
                        break;

                    case 18:// X -> Y : <instruccion> -> <if>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 19:// X -> W : <instruccion> -> <while>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 20:// X->S : <instrucción> -> <for>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 21:// X->V : <instrucción> -> <escribir>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 22:// X->R : <instrucción> -> <leer>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 23: // X -> O : <instruccion> -> <do>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 24: // X->M : <instrucción> -> <incremento>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 25: // X -> A : <instruccion> -> <asigna>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Instruccion";
                        pilaSemantica.Push(atrp);
                        break;
                    case 26:
                        break;

                    case 27:
                        break;

                    case 28:
                        break;
                    case 29:
                        break;
                    case 30:
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
                    case 39:
                        
                        break;
                    case 40:
                        break;

                    case 41:
                        break;

                    case 42:
                        break;

                    case 43:
                        break;

                    case 44:
                        break;

                    case 45:
                        break;

                    case 46:
                        break;

                    case 47:
                        break;

                    case 48:
                        break;

                    case 49: //E->E + T : <expresion> -> <expresion> + <termino>
                        atrp = new Atributos();
                        atr1 = new Atributos();
                        atr2 = new Atributos();
                        atr1 = pilaSemantica.Pop(); //esto es igual a lo que tengo de factor 
                        atr2 = pilaSemantica.Pop();// esto es igual a termino      
                        compararTipos(atr1, atr2);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = demeTemporal(atr1.tipo);
                        Gen("+", atr1.valor.ToString(), atr2.valor.ToString(), atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 50: // E->E - T : <expresion> -> <expresion> - <termino>
                        atrp = new Atributos();
                        atr1 = new Atributos();
                        atr2 = new Atributos();
                        atr1 = pilaSemantica.Pop(); //esto es igual a lo que tengo de factor 
                        atr2 = pilaSemantica.Pop();// esto es igual a termino      
                        compararTipos(atr1, atr2);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = demeTemporal(atr1.tipo);
                        Gen("-", atr1.valor.ToString(), atr2.valor.ToString(), atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 51: // E-->T : <expresion> --> <termino>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Expresion";
                        pilaSemantica.Push(atrp);
                        break;

                    case 52://T-> T*F <termino>--> <termino> * <factor>
                        atrp = new Atributos();
                        atr1 = new Atributos();
                        atr2 = new Atributos();

                        atr1 = pilaSemantica.Pop();
                        atr2 = pilaSemantica.Pop();

                        // if (buscartipoTDS(atributosDePila1.valor.ToString()) != atributosDePila2.valor.ToString()))
                        //{
                        //mensaje de error no compatibñle
                        //}
                        compararTipos(atr1, atr2);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = demeTemporal(atr1.tipo);
                        Gen("*", atr1.valor.ToString(), atr2.valor.ToString(), atrp.valor.ToString());
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 53: //T-> T/F <termino>--> <termino> / <factor>
                        atrp = new Atributos();
                        atr1 = new Atributos();
                        atr2 = new Atributos();

                        atr1 = pilaSemantica.Pop();
                        atr2 = pilaSemantica.Pop();


                        // if (buscartipoTDS(atributosDePila1.valor.ToString()) != atributosDePila2.valor.ToString()))
                        //{
                        //mensaje de error no compatibñle
                        //}
                        compararTipos(atr1, atr2);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = demeTemporal(atr1.tipo);
                        Gen("/", atr1.valor.ToString(), atr2.valor.ToString(), demeTemporal(0));
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 54://T-> T^F <termino>--> <termino> ^ <factor>
                        atrp = new Atributos();
                        atr1 = new Atributos();
                        atr2 = new Atributos();

                        atr1 = pilaSemantica.Pop(); //esto es igual a lo que tengo de factor 
                        atr2 = pilaSemantica.Pop(); // esto es igual a termino 

                        // if (buscartipoTDS(atributosDePila1.valor.ToString()) != atributosDePila2.valor.ToString()))
                        //{
                        //mensaje de error no compatibñle
                        //}
                        compararTipos(atr1, atr2);
                        atrp.principio = Next();
                        atrp.siguiente = atrp.principio + 1;
                        atrp.valor = demeTemporal(atr1.tipo);
                        Gen("^", atr1.valor.ToString(), atr2.valor.ToString(), demeTemporal(0));
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 55:// T -> F : <termino> -> <factor>
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Termino";
                        pilaSemantica.Push(atrp);
                        break;

                    case 56: // F -> i : <factor> -> identificador
                        atrp = new Atributos();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.valor = listaReglasReconocidas[i].dato;
                        pilaSemantica.Push(atrp);
                        break;

                    case 57:// F -> (E) : < factor > -> < expresion >
                        atrp = new Atributos();
                        atrp = pilaSemantica.Pop();
                        atrp.no_terminal = listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        pilaSemantica.Push(atrp);
                        break;

                    case 58: // F -> a : <factor> -> literal
                        atrp = new Atributos();
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.valor = listaReglasReconocidas[i].dato;
                        atrp.tipo = tipoVariable(listaReglasReconocidas[i].tipo); // obtengo el tipo de la variable
                        pilaSemantica.Push(atrp);
                        break;

                    case 59: // F -> v : <factor> -> true
                        atrp = new Atributos();
                        atrp.no_terminal = this.listaReglasReconocidas[i].part_izquierda;
                        atrp.nombre = "Factor";
                        atrp.tipo = 5;
                        atrp.valor = true;
                        pilaSemantica.Push(atrp);
                        break;

                    case 60: // F -> f : <factor> -> falso
                        atrp = new Atributos();
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
            foreach (Cuadruplos c in list_codigo_intermedio)
            {
                imprimir(c.operador + "\t" + c.operando_1 + "\t" + c.operando_2 + "\t" + c.resultado);
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

        public bool resultadoAsigne(int tipoDato, int tipoValor, Object obj)
        {
            if (tipoDato == tipoValor)
            {
                return true;
            }
            else
            {
                MessageBox.Show("El valor -> " + obj + " : debe ser de tipo "+ tipoVariableNombre(tipoDato));
                return false;
            }
        }

        public string tipoVariableNombre(int id)
        {
            if (id == 1)
            {
                return "literal entero";
            }
            else if (id == 2)
            {
                return "literal real";
            }
            else if (id == 4)
            {
                return "literal cadena";
            }
            else if (id == 3)
            {
                return "literal char";
            }
            return "literal bool";
        }

        public int tipoDatoDeclarado(string tipo)
        {
            if (tipo.Equals("integer"))
            {
                return 1;
            }
            else if (tipo.Equals("real"))
            {
                return 2;
            }
            else if (tipo.Equals("string"))
            {
                return 4;
            }
            else if (tipo.Equals("char"))
            {
                return 3;
            }
            return 5;
        }
    }
}
