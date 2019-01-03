using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NPrivilegio
    {
        public static DataTable Mostrar()
        {
            return new DPrivilegio().Mostrar();
        }
    }
}
