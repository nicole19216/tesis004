using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DImpuesto
    {
        private int idi;
        private string nombre;
        private decimal valor;

        public DImpuesto() { }

        public DImpuesto(int idi, string nombre, decimal valor)
        {
            Idi = idi;
            Nombre = nombre;
            Valor = valor;
        }

        public int Idi { get => idi; set => idi = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal Valor { get => valor; set => valor = value; }

        public string Insertar(DImpuesto dImpuesto)
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
                SqlCmd.CommandText = "pInsertarImpuesto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idi";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pnombre = new SqlParameter();
                pnombre.ParameterName = "@nombre";
                pnombre.SqlDbType = SqlDbType.VarChar;
                pnombre.Size = 30;
                pnombre.Value = dImpuesto.Nombre;
                SqlCmd.Parameters.Add(pnombre);

                SqlParameter pvalor = new SqlParameter();
                pvalor.ParameterName = "@valor";
                pvalor.SqlDbType = SqlDbType.Decimal;
                pvalor.Precision = 4;
                pvalor.Scale = 2;
                pvalor.Value = dImpuesto.Valor;
                SqlCmd.Parameters.Add(pvalor);

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
            DataTable DtResultado = new DataTable("impuesto");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarImpuesto";
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
