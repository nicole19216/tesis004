using DATOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class NRepresentante
    {
        public static string Insertar(string ci, string nombre, string email, DateTime fecha, string nit,
           DataTable dtnum, DataTable dtdir, int id_pp, bool estado)
        {
            DPersona dPersona = new DPersona();
            dPersona.Ci = ci;
            dPersona.Nombre = nombre;
            dPersona.Email = email;
            dPersona.Nacimiento = fecha;
            dPersona.Nit = nit;
            dPersona.Imagen = null;

            DRepresentante dRepresentante = new DRepresentante();
            dRepresentante.Id_pp = id_pp;
            dRepresentante.Estado = estado;

            List<DNumero> dNums = new List<DNumero>();
            foreach (DataRow fila in dtnum.Rows)
            {
                DNumero dNum = new DNumero();
                dNum.Celular = Convert.ToInt64(fila["celular"].ToString());
                dNum.Telefono = Convert.ToInt64(fila["telefono"].ToString());
                dNums.Add(dNum);
            }
            List<DDireccion> dDireccions = new List<DDireccion>();
            foreach (DataRow fila in dtdir.Rows)
            {
                DDireccion dDireccion = new DDireccion();
                dDireccion.Pais = Convert.ToString(fila["pais"].ToString());
                dDireccion.Departamento = Convert.ToString(fila["departamento"].ToString());
                dDireccion.Provincia = Convert.ToString(fila["provincia"].ToString());
                dDireccion.Municipio = Convert.ToString(fila["municipio"].ToString());
                dDireccion.Direccion = Convert.ToString(fila["direccion"].ToString());
                dDireccions.Add(dDireccion);
            }

            return dRepresentante.Insertar(dDireccions, dNums, dPersona, dRepresentante);
        }

        public static DataTable Mostrar()
        {
            return new DRepresentante().Mostrar();
        }
    }
}
