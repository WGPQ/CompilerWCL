using CompilerWCL.model.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TokensReconocidos
    {
        public List<Token> listTokensReconocidos;
        private List<Token> listAlfabeto;
        public List<Object[]> listErrores;
        private bool banderaDeclare = false;
        private bool banderaMain = false;
        public TDSManager claseMgTDS;

        private string[] lineas_codigo;
        private ErroresReconocidos erroresR;

        //private bool variableRepetida = false;

        private int variableNoDeclarada = -103;
        public TokensReconocidos(string ruta, List<Token> listAlfabeto, TDSManager claseMgTDS, ErroresReconocidos erroresR)
        {
            this.listAlfabeto = listAlfabeto;
            this.listTokensReconocidos = new List<Token>();
            this.listErrores = new List<Object[]>();
            this.claseMgTDS = claseMgTDS;

            this.erroresR = erroresR;
            this.lineas_codigo = abriArchivo(ruta);
        }

        public TokensReconocidos(string[] txt_lineas, List<Token> listAlfabeto, TDSManager claseMgTDS, ErroresReconocidos erroresR)
        {
            this.listAlfabeto = listAlfabeto;
            this.listTokensReconocidos = new List<Token>();
            this.listErrores = new List<Object[]>();
            this.claseMgTDS = claseMgTDS;

            this.erroresR = erroresR;
            this.lineas_codigo = txt_lineas;
        }

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
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error: al cargar el archivo", "original");
            }
            return lines;
        }

        
        /**
         * Realizo la transicion de cada caracter de la cadena ingresada
         * y lo agrego a una lista
         * 
         * @param listAlfabeto : ingresa la lista de alfabeto cargada
         * @param tc: ingresa las listas de la tabla compacta
         */
        public List<Automata> listSimbolosReconocidos(List<Token> listAlfabeto, TablaCompacta tc)
        {
            //string[] txt_lines = abriArchivo(this.ruta);
            string[] txt_lines = this.lineas_codigo;
            string lexemaProbable = "";
            string lexemaIncorrecto = "";
            List<Automata> listaReconocida = new List<Automata>();

            int estado = 0;
            char leyendo = ' ';
            int newestado = 0;
            string cadenaReconocida = "";
            Object[] obj_lexemaIncorrecto = null;

            //foreach (string line in txt_lines) // recorro por cada linea del archivo
            for (int lin = 0; lin < txt_lines.Length; lin++) // recorro por cada linea del archivo
            {
                for (int i = 0; i < txt_lines[lin].Length; i++)
                {
                    leyendo = txt_lines[lin].ElementAt(i);
                    if ((leyendo.ToString()).Equals(" ") || (leyendo.ToString()).Equals("\t")) // si encuentra un espacio lo remplazo por un $
                    {
                        leyendo = '$';
                    }
                    newestado = tc.movimiento(estado, leyendo); // obtengo la nueva posicion para leer
                    //imprin("Leyendo: " + leyendo + " i = " + i + "  len: " + txt_lines[lin].Length+"  estado   "+estado+ "   newestado "+newestado);
                    if (newestado >= 0) // los simbolos son reconocidos desde el 0 +
                    {
                        listaReconocida.Add(new Automata(estado, leyendo, newestado));
                        estado = newestado;
                        if ((leyendo.ToString()).Equals("$") && newestado != 80) // 80 porque esta en el nodo de comentario del AFD
                        {
                            agregarTokenReconocido(cadenaReconocida, newestado, lin);
                            cadenaReconocida = "";
                            estado = 0; // por este momento cuando un lexema sea reconocido le envio al estado inicial 0
                        }
                        else
                        {                         
                            cadenaReconocida += leyendo.ToString();
                        }
                    }
                    else // no se reconoce el simbolo
                    {
                        // este objeto[] contirne { numero de columans recoridas de cada fiala, palabra que solo se necesita leer}
                        obj_lexemaIncorrecto = palabraSoloReconocer(txt_lines[lin], cadenaReconocida, i);

                        lexemaIncorrecto = obj_lexemaIncorrecto[1].ToString(); //[1] se encuentra la palabra que solo se necesita leer sin ningun espacio
                        lexemaProbable = this.lexemaProbable(i, txt_lines[lin], estado, cadenaReconocida, tc); // me retorna el lexema corregido

                        // lexema que porbablemente quiso decir
                        if (lexemaIncorrecto.Length == lexemaProbable.Length)
                        {
                            this.erroresR.insertarMesajeSurencia(-104, lexemaIncorrecto, lexemaProbable, lin+1); // error 104 -> lexema probable
                            this.banderaDeclare = false; // si se produce un error no permito que sigua ingresando datos al TDS
                        }
                        else // se produjo un error -999
                        {
                            this.erroresR.insertarErroresReconocidos(-999, lexemaIncorrecto, lin+1);
                            this.banderaDeclare = false; // si se produce un error no permito que sigua ingresando datos al TDS
                        }
                        i += int.Parse(obj_lexemaIncorrecto[0].ToString())-1; // le resto -1 porque la letra error ya fue leida
                        cadenaReconocida = ""; // reinicio la cadena porque ya terminados el lexema porbablemente que quiso decir
                        estado = 0; // por este momento cuando un lexema sea reconocido le envio al estado inicial 0
                    }
                }

                leyendo = '$'; // cuando la linea fue reconocia y el lexema continua con $
                newestado = tc.movimiento(estado, leyendo); // genero el movimiento que sigue con $ para saber en que estado termina
                estado = 0; // cuando hay un salto de linea entonces le volvemos al nodo inicial 0 

                // cuando el newestado me retorna un num negativo
                if (newestado < 0)
                {
                    return listaReconocida;
                }

                listaReconocida.Add(new Automata(estado, leyendo, newestado));

                if ((leyendo.ToString()).Equals("$")) // reconozco el token de la liena anterior cuando hay un salto de lenea
                {
                    agregarTokenReconocido(cadenaReconocida, newestado, lin);
                    cadenaReconocida = "";
                }
            }

            return listaReconocida;
        }

        public void imprin(string c)
        {
            Console.WriteLine(c);
        }

        /**
         * Recorto solo la palabra que quiero reconocer
         * 
         * @param txt_linea : es la cadena de la linea que una palabra no fue reconocida
         * return: retorno la palabra solo a reconocer
         */
        public Object[] palabraSoloReconocer(string txt_linea, string palabraReconocer, int linea)
        {
            string palabraLeer = palabraReconocer;
            int cont = 0;
            // separo solo la palabra que quiero buscar
            for (int i = linea; i < txt_linea.Length; i++)
            {
                if (!" ".Equals(txt_linea.ElementAt(i).ToString()))
                {
                    palabraLeer += txt_linea.ElementAt(i).ToString();
                    cont++;                }
                else
                {
                    break;
                }
            }
            return new object[] { cont, palabraLeer};
        }

        /**
         * Me permite saber cual es la palabra correcta por equivocarse una letra
         * 
         * return : me retorna la cadena correcta o incorrecta
         */
        public string lexemaProbable(int posicion_columna, string palabraLeer, int estado, string cadenaPalabra, TablaCompacta tc)
        {
            int newestado = -1;
            string simbolo = "";
            int aux = estado;
            string newCadenaPalabra = cadenaPalabra;
            char lee = ' ';
            foreach (string s in tc.listSimbolosSiguientes(estado)) // recorro la lista de los simbolos que pueden seguir
            {
                newestado = tc.movimiento(estado, char.Parse(s));
                aux = newestado; // aux sera nuestro nuevo estado
                newCadenaPalabra += s; // agrego la letra para completar la palabra correcta
                for (int i = posicion_columna + 1; i < palabraLeer.Length; i++) // recorro el texto de la fila, le aumento +1 para no ler la letra equivocada
                {
                    lee = palabraLeer.ElementAt(i);
                    if (" ".Equals(lee.ToString())) // si encuentro un espacio salgo de la funcion x q puede ser que ya reconoci la palabr o no
                    {
                        return newCadenaPalabra;
                    }
                    newestado = tc.movimiento(aux, lee); // busco el nuevo movimiento y si hay coincidencias, ya estoy encontrando la´palabra
                    if (newestado > -1) // entro si nos da un nuemero positivo 
                    {
                        aux = newestado; // igualo en estado
                        newCadenaPalabra += lee.ToString(); // completo la palabra
                    }
                    else // si nos da un -1 no hay coincidencias
                    {
                        newCadenaPalabra = lexemaProbable(i, palabraLeer, aux, newCadenaPalabra, tc);
                        //imprin("lenea: " + i);

                        // si el lexema encontrado tiene la misma longitud de la palabra que estoy buscando
                        // y si esa palabra encontrada se encuentra en el alfabeto entro
                        if (newCadenaPalabra.Length == palabraLeer.Length && this.listAlfabeto.FindIndex(x => (x.lexema).Equals(newCadenaPalabra)) > -1)
                        {
                            return newCadenaPalabra; // retorno la palabra probable
                        }
                        else // cuando no es reconocida la palabra probable
                        {
                            newCadenaPalabra = cadenaPalabra; // reseteo a la palabra que estaba bien hasta una sierta parte
                            break; // salgo del for
                        }
                    }
                }
            }
            //return ""; // si no reconosco ninguna palabra envio vacio   
            return newCadenaPalabra;            
        }

        /**
         * Agrego todos los tokens reconocidos a la lista
         * 
         * @param lexema: ingresa el lexema a reconocer
         * @param estadoAnticipado: el estado adelantado reconocido
         * @param lineaError: mando la linea cuando se produce un error
         */
        public void agregarTokenReconocido(string lexema, int estadoAnticipado, int lineError)
        {
            lineError++; // Linea de error comenzando desde 1
            int token = this.listAlfabeto.FindIndex(x => (x.lexema).Equals(lexema));
            //Console.WriteLine("lexema: " + lexema + "  newestado: " + estadoAnticipado + " numToken: " + token + "\n");

            if (token > -1)
            {
                this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                    this.listAlfabeto[token].nombretoken, this.listAlfabeto[token].lexema, lineError));
                if (this.listAlfabeto[token].numtoken == 40) // 40 es el numtoken : declare
                {
                    this.banderaDeclare = true;
                }
                /**
                 * Ingresar en la tabla de simbolos
                 * 17: integer
                 * 18: real
                 * 19: bool
                 * 20: char
                 * 21: string
                 */
                if (this.banderaDeclare && (this.listAlfabeto[token].numtoken == 17 ||
                    this.listAlfabeto[token].numtoken == 18 || this.listAlfabeto[token].numtoken == 19 ||
                    this.listAlfabeto[token].numtoken == 20 || this.listAlfabeto[token].numtoken == 21))
                {
                    claseMgTDS.tipoDato(lexema);
                }

                if (this.listAlfabeto[token].numtoken == 22) // 22 es el numtoken : main
                {
                    this.banderaDeclare = false;
                    this.banderaMain = true;
                }
            }
            else
            {
                if (!"".Equals(lexema))
                {
                    
                    // identificador
                    if (estadoAnticipado == 109) // 109 nodo terminal del identificador
                    {
                        token = 0; // numtoken 0 es identificador
                        this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                            this.listAlfabeto[token].nombretoken, lexema.Substring(1), lineError)); //lexema.Substring(1) para quitar el #
                        if (banderaDeclare)
                        {
                            if (lexema.Length <= 30)
                            {
                                if (!claseMgTDS.nombreVariable(lexema, this.erroresR, lineError))
                                {
                                    this.erroresR.insertarErroresReconocidos(-102, lexema, lineError); // Error -102 : Variable ya declarada                           
                                    this.banderaDeclare = false; // si se produce un error no permito que sigua ingresando datos al TDS
                                }
                            }
                            else
                            {
                                this.erroresR.insertarErroresReconocidos(-106, lexema, lineError); // Error -106 : error cuando se exede el nro de caracteres de un identificador   
                                claseMgTDS.eliminarIdentificador(); // elimino la ultima posicion ingresada al declarar una variable y genera un error
                                this.banderaDeclare = false;
                            }
                        }
                        if (banderaMain)
                        {
                            imprin("lexema : "+lexema);
                            if (!claseMgTDS.searchOnTDSMain(lexema.Substring(1, lexema.Length - 1)))
                            {
                                this.erroresR.insertarErroresReconocidos(variableNoDeclarada, lexema, lineError); // Error -103 : Variable no declarada   
                            }
                        }
                    }
                    // literalreal
                    else if (estadoAnticipado == 134) // 134 nodo terminal de numeros dobles con signos + o -
                    {
                        token = 41; // numtoken 42 es literalreal se encuentra en el alfabeto
                        this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                            this.listAlfabeto[token].nombretoken, lexema, lineError));

                        if (banderaDeclare)
                        {
                            claseMgTDS.valorVariable(double.Parse(lexema));
                        }
                    }
                    // literalentero
                    else if (estadoAnticipado == 55) // 55 nodo terminal de literalentero
                    {
                        token = 42; // numtoken 43 es literalentero se encuentra en el alfabeto
                        this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                            this.listAlfabeto[token].nombretoken, lexema, lineError));

                        if (banderaDeclare)
                        {
                            claseMgTDS.valorVariable(int.Parse(lexema));
                        }
                    }
                    // literalcadena
                    else if (estadoAnticipado == 135) // 135 nodo terminal de literalcadena
                    {
                        token = 43; // numtoken 44 es literalcadena se encuentra en el alfabeto
                        this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                            this.listAlfabeto[token].nombretoken, lexema, lineError));

                        if (banderaDeclare)
                        {
                            if (lexema.Length <= 80)
                            {
                                claseMgTDS.valorVariable(lexema);
                            }
                            else
                            {
                                this.erroresR.insertarErroresReconocidos(-107, lexema, lineError); // Error -107 : Error cuando se exede el nro de caracteres de un literalcadena
                                this.banderaDeclare = false;
                            }
                        }
                    }
                    // literalchar
                    else if (estadoAnticipado == 136) // 136 nodo terminal de literalchar
                    {
                        token = 44; // numtoken 45 es literalchar se encuentra en el alfabeto
                        this.listTokensReconocidos.Add(new Token(this.listAlfabeto[token].numtoken, this.listAlfabeto[token].sinonimo,
                            this.listAlfabeto[token].nombretoken, lexema, lineError));

                        if (banderaDeclare)
                        {
                            claseMgTDS.valorVariable(lexema);
                            //claseMgTDS.valorVariable(char.Parse(lexema));
                        }
                    }
                    // cuando las palabras no se reconocen
                    else
                    {
                        //this.listErrores.Add(new Object[] { token, lexema });
                    }
                }
            }
        }       
    }
}
