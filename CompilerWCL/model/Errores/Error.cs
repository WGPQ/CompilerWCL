﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Errores
{
    class Error
    {
        public int numError { get; set; }
        public string detalle { get; set; }

        public Error(int numError, string detalle)
        {
            this.numError = numError;
            this.detalle = detalle;
        }
    }
}
