using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DRolPrivilegio
    {
        private int id_rolprivilegio;
        private int id_rol;
        private int id_privilegio;
        private bool estado;

        public DRolPrivilegio()
        {
        }

        public DRolPrivilegio(int id_rolprivilegio, int id_rol, int id_privilegio, bool estado)
        {
            Id_rolprivilegio = id_rolprivilegio;
            Id_rol = id_rol;
            Id_privilegio = id_privilegio;
            Estado = estado;
        }

        public int Id_rolprivilegio { get => id_rolprivilegio; set => id_rolprivilegio = value; }
        public int Id_rol { get => id_rol; set => id_rol = value; }
        public int Id_privilegio { get => id_privilegio; set => id_privilegio = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(List<DRolPrivilegio> dRolprivilegios)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection(); ;
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                //recorrer objetos de la lista
                foreach (DRolPrivilegio rolpri in dRolprivilegios)
                {
                    try
                    {
                        //Establecer el Comando
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = SqlCon;
                        SqlCmd.CommandText = "pInsertarRolPrivilegio";
                        SqlCmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter pid = new SqlParameter();
                        pid.ParameterName = "@idrp";
                        pid.SqlDbType = SqlDbType.Int;
                        pid.Direction = ParameterDirection.Output;
                        SqlCmd.Parameters.Add(pid);

                        SqlParameter pidrol = new SqlParameter();
                        pidrol.ParameterName = "@id_r";
                        pidrol.SqlDbType = SqlDbType.Int;
                        pidrol.Value = rolpri.Id_rol;
                        SqlCmd.Parameters.Add(pidrol);

                        SqlParameter pidprivilegio = new SqlParameter();
                        pidprivilegio.ParameterName = "@id_p";
                        pidprivilegio.SqlDbType = SqlDbType.Int;
                        pidprivilegio.Value = rolpri.Id_privilegio;
                        SqlCmd.Parameters.Add(pidprivilegio);

                        SqlParameter pestado = new SqlParameter();
                        pestado.ParameterName = "@estado";
                        pestado.SqlDbType = SqlDbType.Bit;
                        pestado.Size = 1;
                        pestado.Value = rolpri.Estado;
                        SqlCmd.Parameters.Add(pestado);

                        //Ejecutamos nuestro comando

                        rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
                    }
                    catch (Exception ex)
                    {
                        rpta = ex.Message;
                        break;
                    }
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
            DataTable DtResultado = new DataTable("rolprivilegio");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarRolPrivilegio";
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
