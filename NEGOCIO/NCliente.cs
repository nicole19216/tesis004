﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DATOS;

namespace NEGOCIO
{
    public class NCliente
    {
        public static string Insertar(string ci, string nombre, string email, DateTime fecha, string nit,
           DataTable dtnum, DataTable dtdir, string tipo, bool estado)
        {
            DPersona dPersona = new DPersona();
            dPersona.Ci = ci;
            dPersona.Nombre = nombre;
            dPersona.Email = email;
            dPersona.Nacimiento = fecha;
            dPersona.Nit = nit;
            dPersona.Imagen = null;

            DCliente dCliente = new DCliente();
            dCliente.Tipo = tipo;
            dCliente.Estado = estado;

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

            return dCliente.Insertar(dDireccions, dNums, dPersona, dCliente);
        }

        public static DataTable Mostrar()
        {
            return new DCliente().Mostrar();
        }
    }
}
