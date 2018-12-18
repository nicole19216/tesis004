using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DDireccion
    {
        private long id_domicilio;
        private long id_persona;
        private string pais;
        private string departamento;
        private string provincia;
        private string municipio;
        private string direccion;

        public DDireccion()
        {
        }

        public DDireccion(int id_domicilio, int id_direccion, string pais, string departamento,
            string provincia, string municipio, string direccion)
        {
            Id_domicilio = id_domicilio;
            Id_persona = id_direccion;
            Pais = pais;
            Departamento = departamento;
            Provincia = provincia;
            Municipio = municipio;
            Direccion = direccion;
        }

        public long Id_domicilio { get => id_domicilio; set => id_domicilio = value; }
        public long Id_persona { get => id_persona; set => id_persona = value; }
        public string Pais { get => pais; set => pais = value; }
        public string Departamento { get => departamento; set => departamento = value; }
        public string Provincia { get => provincia; set => provincia = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public string Insertar(DDireccion dDireccion, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarDireccion";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idd";
                pid.SqlDbType = SqlDbType.BigInt;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.BigInt;
                pidpersona.Value = dDireccion.Id_persona;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter ppais = new SqlParameter();
                ppais.ParameterName = "@pais";
                ppais.SqlDbType = SqlDbType.VarChar;
                ppais.Size = 30;
                ppais.Value = dDireccion.Pais;
                SqlCmd.Parameters.Add(ppais);

                SqlParameter pdepartamento = new SqlParameter();
                pdepartamento.ParameterName = "@departamento";
                pdepartamento.SqlDbType = SqlDbType.VarChar;
                pdepartamento.Size = 30;
                pdepartamento.Value = dDireccion.Departamento;
                SqlCmd.Parameters.Add(pdepartamento);

                SqlParameter pciudad = new SqlParameter();
                pciudad.ParameterName = "@provincia";
                pciudad.SqlDbType = SqlDbType.VarChar;
                pciudad.Size = 30;
                pciudad.Value = dDireccion.Provincia;
                SqlCmd.Parameters.Add(pciudad);

                SqlParameter pdireccion = new SqlParameter();
                pdireccion.ParameterName = "@municipio";
                pdireccion.SqlDbType = SqlDbType.VarChar;
                pdireccion.Size = 30;
                pdireccion.Value = dDireccion.Municipio;
                SqlCmd.Parameters.Add(pdireccion);

                SqlParameter pdescripcion = new SqlParameter();
                pdescripcion.ParameterName = "@direccion";
                pdescripcion.SqlDbType = SqlDbType.VarChar;
                pdescripcion.Size = 50;
                pdescripcion.Value = dDireccion.Direccion;
                SqlCmd.Parameters.Add(pdescripcion);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;

        }
    }
}
