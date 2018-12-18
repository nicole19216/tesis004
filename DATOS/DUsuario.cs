using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{

    public class DUsuario : DPersona
    {
        private int id_usuario;
        private int id_persona;
        private string usuario;
        private string contraseña;
        private bool estado;

        public DUsuario()
        {
        }

        public DUsuario(int id_usuario, int id_persona, string usuario, string contraseña, bool estado)
        {
            Id_usuario = id_usuario;
            Id_persona = id_persona;
            Usuario = usuario;
            Contraseña = contraseña;
            Estado = estado;
        }

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public int Id_persona { get => id_persona; set => id_persona = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(List<DDireccion> dDireccions, List<DNumero> dNums, DPersona dPersona,
            DUsuario dUsuario, List<DUsuariorol> dUsuariorols/*, ref SqlConnection SqlCon, ref SqlTransaction SqlTra*/)
        {
            DPersona dp = new DPersona();
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                //Establecer la trasacción
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                //Inserta persona
                dp.Insertar(dPersona, dNums, dDireccions, ref SqlCon, ref SqlTra);
                //Establecer el Comando
                //Inserta usuario
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarUsuario";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idu";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.Int;
                pidpersona.Value = dp.Id_persona;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter pusuario = new SqlParameter();
                pusuario.ParameterName = "@usuario";
                pusuario.SqlDbType = SqlDbType.VarChar;
                pusuario.Size = 20;
                pusuario.Value = dUsuario.Usuario;
                SqlCmd.Parameters.Add(pusuario);

                SqlParameter pcontraseña = new SqlParameter();
                pcontraseña.ParameterName = "@contraseña";
                pcontraseña.SqlDbType = SqlDbType.VarChar;
                pcontraseña.Size = 20;
                pcontraseña.Value = dUsuario.Contraseña;
                SqlCmd.Parameters.Add(pcontraseña);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dUsuario.Estado;
                SqlCmd.Parameters.Add(pestado);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado
                    this.Id_usuario = Convert.ToInt32(SqlCmd.Parameters["@id_usuario"].Value);
                    foreach (DUsuariorol ur in dUsuariorols)
                    {
                        ur.Idusuario = this.Id_usuario;
                        rpta = ur.Insertar(ur, ref SqlCon, ref SqlTra);
                    }
                }
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
            DataTable DtResultado = new DataTable("usuario");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarUsuario";
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
