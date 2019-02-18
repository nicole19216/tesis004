using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NUbicacion
    {
        public static string Insertar(string nombre)
        {
            DUbicacion dUbicacion = new DUbicacion();
            dUbicacion.Nombre = nombre;
            return dUbicacion.Insertar(dUbicacion);
        }

        public static DataTable Mostrar()
        {
            return new DUbicacion().Mostrar();
        }
    }
}
