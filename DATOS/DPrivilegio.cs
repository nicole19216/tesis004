using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DPrivilegio
    {
        private int id_privilegio;
        private string nombre;
        private int id_modulo;

        public DPrivilegio()
        {
        }

        public DPrivilegio(int id_privilegio, string nombre, int id_modulo)
        {
            Id_privilegio = id_privilegio;
            Nombre = nombre;
            Id_modulo = id_modulo;
        }

        public int Id_privilegio { get => id_privilegio; set => id_privilegio = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Id_modulo { get => id_modulo; set => id_modulo = value; }

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("privilegio");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarPrivilegio";
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
