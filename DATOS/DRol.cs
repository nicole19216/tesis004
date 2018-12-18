using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DRol
    {
        private int id;
        private string nombre;
        private bool estado;

        public DRol()
        {
        }

        public DRol(int id, string nombre, bool estado)
        {
            Id = id;
            Nombre = nombre;
            Estado = estado;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(DRol dRol)
        {

            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {

                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pInsertarRol";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idr";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pnombre = new SqlParameter();
                pnombre.ParameterName = "@nombre";
                pnombre.SqlDbType = SqlDbType.VarChar;
                pnombre.Size = 30;
                pnombre.Value = dRol.Nombre;
                SqlCmd.Parameters.Add(pnombre);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dRol.Estado;
                SqlCmd.Parameters.Add(pestado);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
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
            DataTable DtResultado = new DataTable("rol");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarRol";
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
