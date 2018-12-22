using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DATOS;

namespace NEGOCIO
{
    public class NRol
    {
        public static string Insertar(string nombre, bool estado)
        {
            DRol dRol = new DRol();
            dRol.Nombre = nombre;
            dRol.Estado = estado;

            return dRol.Insertar(dRol);
        }

        public static DataTable Mostrar()
        {
            return new DRol().Mostrar();
        }
    }
}
