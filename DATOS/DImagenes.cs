using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DImagenes
    {
        private int idi;
        private int id_p;
        private byte[] imagen;
        private int idpbuscar;

        public DImagenes() { }

        public DImagenes(int idi, int id_p, byte[] imagen, int idpbuscar)
        {
            Idi = idi;
            Id_p = id_p;
            Imagen = imagen;
            Idpbuscar = idpbuscar;
        }

        public int Idi { get => idi; set => idi = value; }
        public int Id_p { get => id_p; set => id_p = value; }
        public byte[] Imagen { get => imagen; set => imagen = value; }
        public int Idpbuscar { get => idpbuscar; set => idpbuscar = value; }

        public string Insertar(DImagenes dImagenes, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarImagen";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idi";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.Int;
                pidpersona.Value = dImagenes.Id_p;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter pimagen = new SqlParameter();
                pimagen.ParameterName = "@imagen";
                pimagen.SqlDbType = SqlDbType.Image;
                pimagen.Value = dImagenes.Imagen;
                SqlCmd.Parameters.Add(pimagen);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }

        public DataTable Mostrar( DImagenes dima)
        {
            DataTable DtResultado = new DataTable("imagenes");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarImagen";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pidpbuscar = new SqlParameter();
                pidpbuscar.ParameterName = "@idp";
                pidpbuscar.SqlDbType = SqlDbType.Int;
                pidpbuscar.Value = dima.Idpbuscar;
                SqlCmd.Parameters.Add(pidpbuscar);

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
