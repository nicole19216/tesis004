using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NCompra
    {
        public static string Insertar(int idp, int idu, string factura, DateTime fecha, DataTable dtdetallecompra)
        {
            DCompra compra = new DCompra();
            compra.Id_p = idp;
            compra.Id_u = idu;
            compra.Factura = factura;
            compra.Fecha = fecha;

            List<DDetalleCompra> dDetalleCompras = new List<DDetalleCompra>();
            foreach (DataRow fila in dtdetallecompra.Rows)
            {
                DDetalleCompra detalleCompra = new DDetalleCompra();
                detalleCompra.Id_c = 1;
                detalleCompra.Unidad_compra = Convert.ToString(fila["unidad_compra"]);
                detalleCompra.Cantidad = 0;
                detalleCompra.Cantidad_piezas = Convert.ToInt32(fila["cantidad_piezas"]);
                detalleCompra.Precio = Convert.ToDecimal(fila["precio"]);
                dDetalleCompras.Add(detalleCompra);
            }

            return compra.Insertar(compra, dDetalleCompras);
        }

        public static DataTable BuscarCosto(int producto)
        {
            DCompra dCompra = new DCompra();
            dCompra.Idpbuscar = producto;
            return dCompra.BuscarCosto(dCompra);
        }
    }
}
