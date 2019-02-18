using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DNumero
    {
        private int idceltel;
        private int idpersona;
        private long celular;
        private long telefono;

        public int Idceltel { get => idceltel; set => idceltel = value; }
        public int Idpersona { get => idpersona; set => idpersona = value; }
        public long Celular { get => celular; set => celular = value; }
        public long Telefono { get => telefono; set => telefono = value; }

        public DNumero()
        {
        }

        public DNumero(int idceltel, int idpersona, long celular,long telefono)
        {
            Idceltel = idceltel;
            Idpersona = idpersona;
            Celular = celular;
            Telefono = telefono;
        }

        public string Insertar(DNumero dNum, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarNumero";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idn";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidpersona = new SqlParameter();
                pidpersona.ParameterName = "@id_p";
                pidpersona.SqlDbType = SqlDbType.BigInt;
                pidpersona.Value = dNum.Idpersona;
                SqlCmd.Parameters.Add(pidpersona);

                SqlParameter pcelular = new SqlParameter();
                pcelular.ParameterName = "@celular";
                pcelular.SqlDbType = SqlDbType.BigInt;
                pcelular.Value = dNum.Celular;
                SqlCmd.Parameters.Add(pcelular);

                SqlParameter ptelefono = new SqlParameter();
                ptelefono.ParameterName = "@telefono";
                ptelefono.SqlDbType = SqlDbType.BigInt;
                ptelefono.Value = dNum.Telefono;
                SqlCmd.Parameters.Add(ptelefono);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;

        }
    }
}
