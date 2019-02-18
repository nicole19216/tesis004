using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NImpuesto
    {
        public static string Insertar(string nombre, decimal valor)
        {
            DImpuesto dImpuesto = new DImpuesto();
            dImpuesto.Nombre = nombre;
            dImpuesto.Valor = valor;
            return dImpuesto.Insertar(dImpuesto);
        }

        public static DataTable Mostrar()
        {
            return new DImpuesto().Mostrar();
        }
    }
}
