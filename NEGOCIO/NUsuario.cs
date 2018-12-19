using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DATOS;

namespace NEGOCIO
{
    public class NUsuario
    {
        public static string Insertar(string ci, string nombre, string email, DateTime fecha, long nit,
            DataTable dtnum, DataTable dtdir, string usuario, string contraseña, bool estado, DataTable dtrolusu, byte[] imagen)
        {
            DPersona dPersona = new DPersona();
            dPersona.Ci = ci;
            dPersona.Nombre = nombre;
            dPersona.Email = email;
            dPersona.Nacimiento = fecha;
            dPersona.Nit = nit;
            dPersona.Imagen = imagen;

            DUsuario dUsuario = new DUsuario();
            dUsuario.Usuario = usuario;
            dUsuario.Contraseña = contraseña;
            dUsuario.Estado = estado;

            List<DUsuariorol> dUsuariorols = new List<DUsuariorol>();
            foreach (DataRow fila in dtrolusu.Rows)
            {
                DUsuariorol dUsuariorol = new DUsuariorol();
                dUsuariorol.Idrol = Convert.ToInt32(fila["id"]);
                dUsuariorol.Estado = Convert.ToBoolean(fila["estado"]);
                dUsuariorols.Add(dUsuariorol);
            }
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

            return dUsuario.Insertar(dDireccions, dNums, dPersona, dUsuario, dUsuariorols);
        }

        public static DataTable Mostrar()
        {
            return new DUsuario().Mostrar();
        }

        public static DataView MostrarView()
        {
            return new DUsuario().Mostrar().DefaultView;
        }
    }
}
