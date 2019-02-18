using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DCompra
    {
        private int idc;
        private int id_p;
        private int id_u;
        private string factura;
        private DateTime fecha;
        private int idpbuscar;

        public DCompra() { }

        public DCompra(int idc, int id_p, int id_u, string factura, DateTime fecha,int idpbuscar)
        {
            Idc = idc;
            Id_p = id_p;
            Id_u = id_u;
            Factura = factura;
            Fecha = fecha;
            Idpbuscar = idpbuscar;
        }

        public int Idc { get => idc; set => idc = value; }
        public int Id_p { get => id_p; set => id_p = value; }
        public int Id_u { get => id_u; set => id_u = value; }
        public string Factura { get => factura; set => factura = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int Idpbuscar { get => idpbuscar; set => idpbuscar = value; }

        public string Insertar(DCompra dCompra, List<DDetalleCompra> dDetalleCompras)
        {

            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {

                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                //Establecer el Comando
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pInsertarCompra";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idc";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidp = new SqlParameter();
                pidp.ParameterName = "@id_p";
                pidp.SqlDbType = SqlDbType.Int;
                pidp.Value = dCompra.Id_p;
                SqlCmd.Parameters.Add(pidp);

                SqlParameter pidu = new SqlParameter();
                pidu.ParameterName = "@id_u";
                pidu.SqlDbType = SqlDbType.Int;
                pidu.Value = dCompra.Id_u;
                SqlCmd.Parameters.Add(pidu);

                SqlParameter pfactura = new SqlParameter();
                pfactura.ParameterName = "@factura";
                pfactura.SqlDbType = SqlDbType.VarChar;
                pfactura.Size = 30;
                pfactura.Value = dCompra.Factura;
                SqlCmd.Parameters.Add(pfactura);

                SqlParameter pfecha = new SqlParameter();
                pfecha.ParameterName = "@fecha";
                pfecha.SqlDbType = SqlDbType.Date;
                pfecha.Value = dCompra.Fecha;
                SqlCmd.Parameters.Add(pfecha);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado
                    this.Idc = Convert.ToInt32(SqlCmd.Parameters["@idc"].Value);

                    foreach (DDetalleCompra dc in dDetalleCompras)
                    {
                        dc.Id_c = this.Idc;
                        rpta = dc.Insertar(dc, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
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
            DataTable DtResultado = new DataTable("compra");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarCompra";
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

        public DataTable BuscarCosto(DCompra dc)
        {
            DataTable DtResultado = new DataTable("buscarcosto");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pBuscarCosto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pidpbuscar = new SqlParameter();
                pidpbuscar.ParameterName = "@idp";
                pidpbuscar.SqlDbType = SqlDbType.Int;
                pidpbuscar.Value = dc.Idpbuscar;
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
