using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DProveedor
    {
        private int idp;
        private int id_p;
        private string descripcion;
        private bool estado;

        public DProveedor()
        {
        }

        public DProveedor(int idp, int id_p, string descripcion, bool estado)
        {
            Idp = idp;
            Id_p = id_p;
            Descripcion = descripcion;
            Estado = estado;
        }

        public int Idp { get => idp; set => idp = value; }
        public int Id_p { get => id_p; set => id_p = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(List<DDireccion> dDireccions, List<DNumero> dNums, DPersona dPersona, DProveedor dProveedor)
        {
            DPersona dp = new DPersona();
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                dp.Insertar(dPersona, dNums, dDireccions, ref SqlCon, ref SqlTra);
                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarCliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idc";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.Int;
                pidpersona.Value = dp.Id_persona;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter pusuario = new SqlParameter();
                pusuario.ParameterName = "@descripcion";
                pusuario.SqlDbType = SqlDbType.VarChar;
                pusuario.Size = 80;
                pusuario.Value = dProveedor.Descripcion;
                SqlCmd.Parameters.Add(pusuario);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dProveedor.Estado;
                SqlCmd.Parameters.Add(pestado);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }


        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("proveedor");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarProveedor";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }
    }
}
