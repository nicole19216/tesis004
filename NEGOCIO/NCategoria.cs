using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NCategoria
    {
        public static string Insertar(string nombre)
        {
            DCategoria dCategoria = new DCategoria();
            dCategoria.Nombre = nombre;
            return dCategoria.Insertar(dCategoria);
        }

        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }
    }
}
