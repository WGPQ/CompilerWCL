using CompilerWCL.model.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerWCL.model.Lexico
{
    class Lexico_tk
    {
        // code

        public static string[] codeEditor = null;
           // ADF
       
        public static int[,] matriz_tabla_transicion { get; set; }
        //public string[] columnSimbolos {get; set; }
        public static List<char> columnSimbolos { get; set; }
        public static List<Object[]> tablaCompacta_valor { get; set; }
        public static List<Object[]> tablaCompacta_prifil { get; set; }
        public static AFD afd;
        static TablaCompacta tc;

        public static void getRuta_afd(string ruta)
        {
            afd = new AFD(ruta);
            columnSimbolos = afd.listX;
            matriz_tabla_transicion = afd.matriz_tabla_transicion;
            tc = new TablaCompacta(matriz_tabla_transicion, columnSimbolos);
            tablaCompacta_valor = tc.llenar_table_valor(matriz_tabla_transicion);
            tablaCompacta_prifil = tc.llenar_table_prifil(matriz_tabla_transicion, tablaCompacta_valor.Count);
        }

        // Errores
        public static ErroresReconocidos claseErroresReconocidos;
        //public static string erroresEjecucion;
        public static void inicializarError(string ruta)
        {
            claseErroresReconocidos = new ErroresReconocidos(ruta);
        }


        // Token
        static XDocument documento;
        public static List<Token> listAlfabeto { get; set; }

        public static void getRuta_alfabeto(string ruta)
        {
            documento = XDocument.Load(ruta);
            listAlfabeto = generarListaAlfabeto(documento);
        }

        public static List<Token> generarListaAlfabeto(XDocument d)
        {
            // Extraer alfabeto -> conjunto de estados
            var tokens = from cje in d.Descendants("alfabeto") select cje;
            List<Token> l_tokens = new List<Token>();

            // inicializo variables
            int numtoken = 0;
            char simbolo = ' ';
            string nombretoken = "";
            string lexema = "";
            foreach (XElement u in tokens.Elements("token")) // leo la estructura
            {
                numtoken = int.Parse(u.Element("numtoken").Value);
                simbolo = char.Parse(u.Element("sinonimo").Value);
                nombretoken = u.Element("nombretoken").Value;
                lexema = u.Element("lexema").Value;
                //Console.WriteLine("<"+numtoken+","+simbolo+","+nombretoken+","+lexema+">");
                l_tokens.Add(new Token(numtoken, simbolo, nombretoken, lexema));
            }
            return l_tokens;
        }

        // TDS
        public static TDSManager claseMgTDS { get; set; }
        public static List<TDS> listDTSReconocidos { get; set; }
        public static void constructorTDS()
        {
            claseMgTDS = new TDSManager();
            listDTSReconocidos = claseMgTDS.listaTDS;
        }


        // Reconocer tokens
        public static TokensReconocidos claseTokenReconocidos;
        public static List<Automata> listSimbolosReconocidos;
        public static List<Token> listTokensReconocidos;
        public static void getRutaTokensReconocidos(string ruta)
        {
            constructorTDS();
            claseTokenReconocidos = new TokensReconocidos(ruta, listAlfabeto, claseMgTDS, claseErroresReconocidos);
            listSimbolosReconocidos = claseTokenReconocidos.listSimbolosReconocidos(listAlfabeto, tc);
            listTokensReconocidos = claseTokenReconocidos.listTokensReconocidos;
        }

        public static void getIniciarTokensReconocidos(string[] txt_Editor)
        {
            if (txt_Editor != null)
            {
                codeEditor = txt_Editor;
            }
            
            constructorTDS();
            claseErroresReconocidos.resetearLista(); // reseteo la lista de errores reconocidos
            claseTokenReconocidos = new TokensReconocidos(txt_Editor, listAlfabeto, claseMgTDS, claseErroresReconocidos);
            listSimbolosReconocidos = claseTokenReconocidos.listSimbolosReconocidos(listAlfabeto, tc);
            listTokensReconocidos = claseTokenReconocidos.listTokensReconocidos;
            //erroresEjecucion = claseErroresReconocidos.imprimir_erroresConsola();
        }

        /**
         * Renvia el texto que escribio al editor cuando se cambia de pestaña
         * 
         *  retorn : retorna las lineas de codigo ingresado
         */
        public static string enviarCodeEditor()
        {
            string res = "";
            if (codeEditor != null)
            {
                foreach (string c in codeEditor)
                {
                    res += c + "\n";
                }
            }
            
            return res;
        }

    }
}
