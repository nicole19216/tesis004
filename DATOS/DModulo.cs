using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DModulo
    {
        private long id_modulo;
        private string nombre;

        public DModulo()
        {
        }

        public DModulo(long id_modulo, string nombre, string descripcion)
        {
            Id_modulo = id_modulo;
            Nombre = nombre;
        }

        public long Id_modulo { get => id_modulo; set => id_modulo = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("modulo");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarModulo";
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
