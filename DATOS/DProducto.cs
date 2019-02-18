using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DProducto
    {
        private int idp;
        private bool estado;
        private string clave;
        private string claverapida;
        private string nombre;
        private long stock_minimo;
        private long stock_maximo;
        private long stock_actual;
        private int id_m;
        private int id_u;
        private int id_i;
        private int idpbuscar;

        public DProducto() { }

        public DProducto(int idpbuscar, int idp, bool estado, string clave, string claverapida, string nombre, long stock_minimo, long stock_maximo, long stock_actual, int id_m, int id_u, int id_i)
        {
            Idp = idp;
            Estado = estado;
            Clave = clave;
            Claverapida = claverapida;
            Nombre = nombre;
            Stock_minimo = stock_minimo;
            Stock_maximo = stock_maximo;
            Stock_actual = stock_actual;
            Id_m = id_m;
            Id_u = id_u;
            Id_i = id_i;
            Idpbuscar = idpbuscar;
        }

        public int Idp { get => idp; set => idp = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Claverapida { get => claverapida; set => claverapida = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public long Stock_minimo { get => stock_minimo; set => stock_minimo = value; }
        public long Stock_maximo { get => stock_maximo; set => stock_maximo = value; }
        public long Stock_actual { get => stock_actual; set => stock_actual = value; }
        public int Id_m { get => id_m; set => id_m = value; }
        public int Id_u { get => id_u; set => id_u = value; }
        public int Id_i { get => id_i; set => id_i = value; }
        public int Idpbuscar { get => idpbuscar; set => idpbuscar = value; }

        public string Insertar(DProducto dProducto, List<DCategoriaProducto> dCategoriaProductos, List<DDetalleCompra> dDetalleCompras,
            List<DImagenes> dImagenes)
        {
            int flag = 0;
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCon.Open();
                //Establecer la trasacción
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "pInsertarProducto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idp";
                pid.SqlDbType = SqlDbType.Int;
                pid.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(pid);

                SqlParameter pidubicacion = new SqlParameter();
                pidubicacion.ParameterName = "@id_u";
                pidubicacion.SqlDbType = SqlDbType.Int;
                pidubicacion.Value = dProducto.Id_u;
                SqlCmd.Parameters.Add(pidubicacion);

                SqlParameter pidmarca = new SqlParameter();
                pidmarca.ParameterName = "@id_m";
                pidmarca.SqlDbType = SqlDbType.Int;
                pidmarca.Value = dProducto.Id_m;
                SqlCmd.Parameters.Add(pidmarca);

                SqlParameter pidimpuesto = new SqlParameter();
                pidimpuesto.ParameterName = "@id_i";
                pidimpuesto.SqlDbType = SqlDbType.Int;
                pidimpuesto.Value = dProducto.Id_i;
                SqlCmd.Parameters.Add(pidimpuesto);

                SqlParameter pcodigo_barras = new SqlParameter();
                pcodigo_barras.ParameterName = "@codigo_barras";
                pcodigo_barras.SqlDbType = SqlDbType.VarChar;
                pcodigo_barras.Size = 30;
                pcodigo_barras.Value = dProducto.Clave;
                SqlCmd.Parameters.Add(pcodigo_barras);

                SqlParameter pcodigo_rapido = new SqlParameter();
                pcodigo_rapido.ParameterName = "@codigo_rapido";
                pcodigo_rapido.SqlDbType = SqlDbType.VarChar;
                pcodigo_rapido.Size = 20;
                pcodigo_rapido.Value = dProducto.Claverapida;
                SqlCmd.Parameters.Add(pcodigo_rapido);

                SqlParameter pnombre = new SqlParameter();
                pnombre.ParameterName = "@nombre";
                pnombre.SqlDbType = SqlDbType.VarChar;
                pnombre.Size = 20;
                pnombre.Value = dProducto.Nombre;
                SqlCmd.Parameters.Add(pnombre);

                SqlParameter pstock_minimo = new SqlParameter();
                pstock_minimo.ParameterName = "@stock_minimo";
                pstock_minimo.SqlDbType = SqlDbType.BigInt;
                pstock_minimo.Value = dProducto.Stock_minimo;
                SqlCmd.Parameters.Add(pstock_minimo);

                SqlParameter pstock_maximo = new SqlParameter();
                pstock_maximo.ParameterName = "@stock_maximo";
                pstock_maximo.SqlDbType = SqlDbType.BigInt;
                pstock_maximo.Value = dProducto.Stock_maximo;
                SqlCmd.Parameters.Add(pstock_maximo);

                SqlParameter pstock_actual = new SqlParameter();
                pstock_actual.ParameterName = "@stock_actual";
                pstock_actual.SqlDbType = SqlDbType.BigInt;
                pstock_actual.Value = dProducto.Stock_actual;
                SqlCmd.Parameters.Add(pstock_actual);

                SqlParameter pestado = new SqlParameter();
                pestado.ParameterName = "@estado";
                pestado.SqlDbType = SqlDbType.Bit;
                pestado.Size = 1;
                pestado.Value = dProducto.Estado;
                SqlCmd.Parameters.Add(pestado);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado
                    this.Idp = Convert.ToInt32(SqlCmd.Parameters["@idp"].Value);

                    foreach (DCategoriaProducto cp in dCategoriaProductos)
                    {
                        cp.Id_p = this.Idp;
                        rpta = cp.Insertar(cp, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag==0)
                    {
                        foreach (DDetalleCompra dc in dDetalleCompras)
                        {
                            dc.Id_p = this.Idp;
                            rpta = dc.Insertar(dc,ref SqlCon,ref SqlTra);
                            if (!rpta.Equals("OK"))
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        foreach (DImagenes di in dImagenes)
                        {
                            di.Id_p = this.Idp;
                            rpta = di.Insertar(di, ref SqlCon, ref SqlTra);
                            if (!rpta.Equals("OK"))
                            {
                                break;
                            }
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
            DataTable DtResultado = new DataTable("producto");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pListarProducto";
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

        public DataTable Buscar(DProducto dpro)
        {
            DataTable DtResultado = new DataTable("buscarproducto");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.CadCon;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "pBuscarProducto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pidpbuscar = new SqlParameter();
                pidpbuscar.ParameterName = "@idp";
                pidpbuscar.SqlDbType = SqlDbType.Int;
                pidpbuscar.Value = dpro.Idpbuscar;
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
