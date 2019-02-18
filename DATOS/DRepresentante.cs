using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DRepresentante
    {
        private int idr;
        private int id_p;
        private int id_pp;
        private bool estado;

        public DRepresentante()
        {
        }

        public DRepresentante(int idr, int id_p, int id_pp, bool estado)
        {
            Idr = idr;
            Id_p = id_p;
            Id_pp = id_pp;
            Estado = estado;
        }

        public int Idr { get => idr; set => idr = value; }
        public int Id_p { get => id_p; set => id_p = value; }
        public int Id_pp { get => id_pp; set => id_pp = value; }
        public bool Estado { get => estado; set => estado = value; }

        public string Insertar(List<DDireccion> dDireccions, List<DNumero> dNums, DPersona dPersona, DRepresentante dRepresentante)
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
                SqlCmd.CommandText = "pInsertarRepresentante";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idr";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.Int;
                pidpersona.Value = dp.Id_persona;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter pusuario = new SqlParameter();
                pusuario.ParameterName = "@id_pp";
                pusuario.SqlDbType = SqlDbType.Int;
                pusuario.Value = dRepresentante.Id_pp;
                SqlCmd.Parameters.Add(pusuario);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dRepresentante.Estado;
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
            DataTable DtResultado = new DataTable("representante");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarRepresentante";
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
