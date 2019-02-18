using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NMarca
    {
        public static string Insertar(string nombre)
        {
            DMarca dMarca = new DMarca();
            dMarca.Nombre = nombre;
            return dMarca.Insertar(dMarca);
        }

        public static DataTable Mostrar()
        {
            return new DMarca().Mostrar();
        }
    }
}
