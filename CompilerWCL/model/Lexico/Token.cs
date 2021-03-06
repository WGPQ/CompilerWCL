﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class Token
    {
        public int numtoken { get; set; }
        public char sinonimo { get; set; }
        public string nombretoken { get; set; }
        public string lexema { get; set; }
        public int numFila { get; set; }

        public Token(int numtoken, char sinonimo, string nombretoken, string lexema)
        {
            this.numtoken = numtoken;
            this.sinonimo = sinonimo;
            this.nombretoken = nombretoken;
            this.lexema = lexema;
        }

        public Token(int numtoken, char sinonimo, string nombretoken, string lexema, int numFila)
        {
            this.numtoken = numtoken;
            this.sinonimo = sinonimo;
            this.nombretoken = nombretoken;
            this.lexema = lexema;
            this.numFila = numFila;
        }
    }
}
