using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DPresentacion
    {
        private int idpre;
        private int idpro;
        private string nombre;
        private int cantidad_pieza;
        private decimal utilidad_pieza;
        private decimal precio_pieza;
        private decimal utilidad_venta;
        private decimal precio_venta;

        public DPresentacion()
        {
        }

        public DPresentacion(int idpre, int idpro, string nombre, int cantidad_pieza, decimal utilidad_pieza, decimal precio_pieza, decimal utilidad_venta, decimal precio_venta)
        {
            Idpre = idpre;
            Idpro = idpro;
            Nombre = nombre;
            Cantidad_pieza = cantidad_pieza;
            Utilidad_pieza = utilidad_pieza;
            Precio_pieza = precio_pieza;
            Utilidad_venta = utilidad_venta;
            Precio_venta = precio_venta;
        }

        public int Idpre { get => idpre; set => idpre = value; }
        public int Idpro { get => idpro; set => idpro = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Cantidad_pieza { get => cantidad_pieza; set => cantidad_pieza = value; }
        public decimal Utilidad_pieza { get => utilidad_pieza; set => utilidad_pieza = value; }
        public decimal Precio_pieza { get => precio_pieza; set => precio_pieza = value; }
        public decimal Utilidad_venta { get => utilidad_venta; set => utilidad_venta = value; }
        public decimal Precio_venta { get => precio_venta; set => precio_venta = value; }

        public string Insertar(DPresentacion dPresentacion, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarPresentacion";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idp";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidproducto = new SqlParameter();
                pidproducto.ParameterName = "@id_p";
                pidproducto.SqlDbType = SqlDbType.Int;
                pidproducto.Value = dPresentacion.Idpro;
                SqlCmd.Parameters.Add(pidproducto);

                SqlParameter pnombre = new SqlParameter();
                pnombre.ParameterName = "@nombre";
                pnombre.SqlDbType = SqlDbType.VarChar;
                pnombre.Size = 20;
                pnombre.Value = dPresentacion.Nombre;
                SqlCmd.Parameters.Add(pnombre);

                SqlParameter pcantidadpieza = new SqlParameter();
                pcantidadpieza.ParameterName = "@cantidad_piezas";
                pcantidadpieza.SqlDbType = SqlDbType.Int;
                pcantidadpieza.Value = dPresentacion.Cantidad_pieza;
                SqlCmd.Parameters.Add(pcantidadpieza);

                SqlParameter putilidadpieza = new SqlParameter();
                putilidadpieza.ParameterName = "@utilidad_pieza";
                putilidadpieza.SqlDbType = SqlDbType.Decimal;
                putilidadpieza.Precision = 4;
                putilidadpieza.Scale = 2;
                putilidadpieza.Value = dPresentacion.Utilidad_pieza;
                SqlCmd.Parameters.Add(putilidadpieza);

                SqlParameter ppreciopieza = new SqlParameter();
                ppreciopieza.ParameterName = "@precio_pieza";
                ppreciopieza.SqlDbType = SqlDbType.Money;
                ppreciopieza.Value = dPresentacion.Precio_pieza;
                SqlCmd.Parameters.Add(ppreciopieza);

                SqlParameter putilidadventa = new SqlParameter();
                putilidadventa.ParameterName = "@utilidad_venta";
                putilidadventa.SqlDbType = SqlDbType.Decimal;
                putilidadventa.Precision = 4;
                putilidadventa.Scale = 2;
                putilidadventa.Value = dPresentacion.Utilidad_venta;
                SqlCmd.Parameters.Add(putilidadventa);

                SqlParameter pprecioventa = new SqlParameter();
                pprecioventa.ParameterName = "@precio_venta";
                pprecioventa.SqlDbType = SqlDbType.Money;
                pprecioventa.Value = dPresentacion.Precio_venta;
                SqlCmd.Parameters.Add(pprecioventa);

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
            DataTable DtResultado = new DataTable("presentacion");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarPresentacion";
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
