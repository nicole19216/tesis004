using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DUsuariorol
    {
        private int id;
        private int idusuario;
        private int idrol;
        private bool estado;

        public DUsuariorol()
        {
        }

        public DUsuariorol(int id, int idusuario, int idrol, bool estado)
        {
            Id = id;
            Idusuario = idusuario;
            Idrol = idrol;
            Estado = estado;
        }

        public int Id { get => id; set => id = value; }
        public int Idusuario { get => idusuario; set => idusuario = value; }
        public int Idrol { get => idrol; set => idrol = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(DUsuariorol dUsuariorol, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarRolUsuario";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idur";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidusuario = new SqlParameter();
                pidusuario.ParameterName = "@id_u";
                pidusuario.SqlDbType = SqlDbType.Int;
                pidusuario.Value = dUsuariorol.Idusuario;
                SqlCmd.Parameters.Add(pidusuario);

                SqlParameter pidrol = new SqlParameter();
                pidrol.ParameterName = "@id_r";
                pidrol.SqlDbType = SqlDbType.Int;
                pidrol.Value = dUsuariorol.Idrol;
                SqlCmd.Parameters.Add(pidrol);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dUsuariorol.Estado;
                SqlCmd.Parameters.Add(pestado);

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
            DataTable DtResultado = new DataTable("usuariorol");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "metmostrar_usurol";
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
