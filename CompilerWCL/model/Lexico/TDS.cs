using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TDS
    {
        public String nametk { get; set; }
        public int type { get; set; }
        public int size { get; set; }
        public object value { get; set; }
        public bool flagExistencia { get; set; }

        public TDS(string nametk, int type, int size, object value, bool flagExistencia)
        {
            this.nametk = nametk;
            this.type = type;
            this.size = size;
            this.value = value;
            this.flagExistencia = flagExistencia;
        }

    }
}
