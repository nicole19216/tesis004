using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DCategoriaProducto
    {
        private int idcp;
        private int id_c;
        private int id_p;

        public DCategoriaProducto() { }

        public DCategoriaProducto(int idcp, int id_c, int id_p)
        {
            Idcp = idcp;
            Id_c = id_c;
            Id_p = id_p;
        }

        public int Idcp { get => idcp; set => idcp = value; }
        public int Id_c { get => id_c; set => id_c = value; }
        public int Id_p { get => id_p; set => id_p = value; }

        public string Insertar(DCategoriaProducto dCategoriaProducto, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {

            string rpta = "";

            try
            {
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarCategoriaProducto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idcp";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidc = new SqlParameter();
                pidc.ParameterName = "@id_c";
                pidc.SqlDbType = SqlDbType.Int;
                pidc.Value = dCategoriaProducto.Id_c;
                SqlCmd.Parameters.Add(pidc);

                SqlParameter pidp = new SqlParameter();
                pidp.ParameterName = "@id_p";
                pidp.SqlDbType = SqlDbType.Int;
                pidp.Value = dCategoriaProducto.Id_p;
                SqlCmd.Parameters.Add(pidp);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;

        }

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("categoriaproducto");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarCategoriaProducto";
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
