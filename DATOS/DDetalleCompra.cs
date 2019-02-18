using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DDetalleCompra
    {
        private int iddc;
        private int id_c;
        private int id_p;
        private string unidad_compra;
        private int cantidad;
        private decimal precio;
        private int cantidad_piezas;

        public DDetalleCompra() { }

        public DDetalleCompra(int iddc, int id_c, int id_p, string unidad_compra, int cantidad, decimal precio, int cantidad_piezas)
        {
            Iddc = iddc;
            Id_c = id_c;
            Id_p = id_p;
            Unidad_compra = unidad_compra;
            Cantidad = cantidad;
            Precio = precio;
            Cantidad_piezas = cantidad_piezas;
        }

        public int Iddc { get => iddc; set => iddc = value; }
        public int Id_c { get => id_c; set => id_c = value; }
        public int Id_p { get => id_p; set => id_p = value; }
        public string Unidad_compra { get => unidad_compra; set => unidad_compra = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Cantidad_piezas { get => cantidad_piezas; set => cantidad_piezas = value; }

        public string Insertar(DDetalleCompra dDetalleCompra, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarDetalleCompra";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter piddc = new SqlParameter();
                piddc.ParameterName = "@iddc";
                piddc.SqlDbType = SqlDbType.Int;
                piddc.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(piddc);

                SqlParameter pidc = new SqlParameter();
                pidc.ParameterName = "@id_c";
                pidc.SqlDbType = SqlDbType.Int;
                pidc.Value = dDetalleCompra.Id_c;
                SqlCmd.Parameters.Add(pidc);

                SqlParameter pidp = new SqlParameter();
                pidp.ParameterName = "@id_p";
                pidp.SqlDbType = SqlDbType.Int;
                pidp.Value = dDetalleCompra.Id_p;
                SqlCmd.Parameters.Add(pidp);

                SqlParameter punidad_compra = new SqlParameter();
                punidad_compra.ParameterName = "@unidad_compra";
                punidad_compra.SqlDbType = SqlDbType.VarChar;
                punidad_compra.Size = 30;
                punidad_compra.Value = dDetalleCompra.Unidad_compra;
                SqlCmd.Parameters.Add(punidad_compra);

                SqlParameter pprecio = new SqlParameter();
                pprecio.ParameterName = "@precio_compra";
                pprecio.SqlDbType = SqlDbType.Money;
                pprecio.Value = dDetalleCompra.Precio;
                SqlCmd.Parameters.Add(pprecio);

                SqlParameter pcantidad = new SqlParameter();
                pcantidad.ParameterName = "@cantidad";
                pcantidad.SqlDbType = SqlDbType.Int;
                pcantidad.Value = dDetalleCompra.Cantidad;
                SqlCmd.Parameters.Add(pcantidad);

                SqlParameter ppiezas = new SqlParameter();
                ppiezas.ParameterName = "@cantidad_pieza";
                ppiezas.SqlDbType = SqlDbType.Int;
                ppiezas.Value = dDetalleCompra.Cantidad_piezas;
                SqlCmd.Parameters.Add(ppiezas);

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
