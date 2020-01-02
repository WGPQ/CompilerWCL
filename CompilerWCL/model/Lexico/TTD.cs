using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TTD
    {
        public int idType { get; set; }
        public string nameType { get; set; }
        public int size { get; set; }

        public TTD(int idType, string nameType, int size)
        {
            this.idType = idType;
            this.nameType = nameType;
            this.size = size;
        }
    }
}
