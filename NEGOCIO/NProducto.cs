using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;

namespace NEGOCIO
{
    public class NProducto
    {

        public static string Insertar(int id_m, int id_u, int id_i, string codigo_barras, string codigo_rapido, string nombre, long stock_minimo, long stock_maximo,
            bool estado, DataTable dtcategoriaproducto, List<byte[]> listimagen)
        {

            DProducto dProducto = new DProducto();
            dProducto.Id_m = id_m;
            dProducto.Id_u = id_u;
            dProducto.Id_i = id_i;
            dProducto.Clave = codigo_barras;
            dProducto.Claverapida = codigo_rapido;
            dProducto.Nombre = nombre;
            dProducto.Stock_minimo = stock_minimo;
            dProducto.Stock_maximo = stock_maximo;
            dProducto.Stock_actual = 0;
            dProducto.Estado = estado;

            List<DCategoriaProducto> dCategoriaProductos = new List<DCategoriaProducto>();
            foreach (DataRow fila in dtcategoriaproducto.Rows)
            {
                DCategoriaProducto categoriaProducto = new DCategoriaProducto();
                categoriaProducto.Id_c = Convert.ToInt32(fila["idc"]);
                dCategoriaProductos.Add(categoriaProducto);
            }

            List<DImagenes> dImagenes2 = new List<DImagenes>();
            foreach (byte[] item in listimagen)
            {
                DImagenes imagenes2 = new DImagenes();
                imagenes2.Imagen = item;
                dImagenes2.Add(imagenes2);
            }

            return dProducto.Insertar(dProducto, dCategoriaProductos, dImagenes2);
        }
        public static string InsertarC(int id_m, int id_u, int id_i, string codigo_barras, string codigo_rapido, string nombre, long stock_minimo, long stock_maximo,
            bool estado, DataTable dtcategoriaproducto, DataTable dtdetallecompra, List<byte[]> listimagen)
        {

            DProducto dProducto = new DProducto();
            dProducto.Id_m = id_m;
            dProducto.Id_u = id_u;
            dProducto.Id_i = id_i;
            dProducto.Clave = codigo_barras;
            dProducto.Claverapida = codigo_rapido;
            dProducto.Nombre = nombre;
            dProducto.Stock_minimo = stock_minimo;
            dProducto.Stock_maximo = stock_maximo;
            dProducto.Stock_actual = 0;
            dProducto.Estado = estado;

            List<DCategoriaProducto> dCategoriaProductos = new List<DCategoriaProducto>();
            foreach (DataRow fila in dtcategoriaproducto.Rows)
            {
                DCategoriaProducto categoriaProducto = new DCategoriaProducto();
                categoriaProducto.Id_c = Convert.ToInt32(fila["idc"]);
                dCategoriaProductos.Add(categoriaProducto);
            }

            List<DImagenes> dImagenes2 = new List<DImagenes>();
            foreach (byte[] item in listimagen)
            {
                DImagenes imagenes2 = new DImagenes();
                imagenes2.Imagen = item;
                dImagenes2.Add(imagenes2);
            }

            List<DDetalleCompra> dDetalleCompras = new List<DDetalleCompra>();
            foreach (DataRow fila in dtdetallecompra.Rows)
            {
                DDetalleCompra detalleCompra = new DDetalleCompra();
                detalleCompra.Id_c = 1;
                detalleCompra.Unidad_compra = Convert.ToString(fila["unidad_compra"]);
                detalleCompra.Cantidad = 0;
                detalleCompra.Cantidad_piezas= Convert.ToInt32(fila["cantidad_piezas"]);
                detalleCompra.Precio = Convert.ToDecimal(fila["precio"]);
                dDetalleCompras.Add(detalleCompra);
            }

            return dProducto.Insertar(dProducto, dCategoriaProductos,dDetalleCompras, dImagenes2);
        }

        public static string InsertarV(int id_m, int id_u, int id_i, string codigo_barras, string codigo_rapido, string nombre, long stock_minimo, long stock_maximo,
            bool estado, DataTable dtcategoriaproducto, /*DataTable dtdetallecompra,*/DataTable dtventa, List<byte[]> listimagen)
        {

            DProducto dProducto = new DProducto();
            dProducto.Id_m = id_m;
            dProducto.Id_u = id_u;
            dProducto.Id_i = id_i;
            dProducto.Clave = codigo_barras;
            dProducto.Claverapida = codigo_rapido;
            dProducto.Nombre = nombre;
            dProducto.Stock_minimo = stock_minimo;
            dProducto.Stock_maximo = stock_maximo;
            dProducto.Stock_actual = 0;
            dProducto.Estado = estado;

            List<DCategoriaProducto> dCategoriaProductos = new List<DCategoriaProducto>();
            foreach (DataRow fila in dtcategoriaproducto.Rows)
            {
                DCategoriaProducto categoriaProducto = new DCategoriaProducto();
                categoriaProducto.Id_c = Convert.ToInt32(fila["idc"]);
                dCategoriaProductos.Add(categoriaProducto);
            }

            List<DImagenes> dImagenes2 = new List<DImagenes>();
            foreach (byte[] item in listimagen)
            {
                DImagenes imagenes2 = new DImagenes();
                imagenes2.Imagen = item;
                dImagenes2.Add(imagenes2);
            }

            List<DPresentacion> dPresentacions = new List<DPresentacion>();
            foreach (DataRow fila in dtventa.Rows)
            {
                DPresentacion dPresentacion = new DPresentacion();
                dPresentacion.Nombre = Convert.ToString(fila["unidad_venta"]);
                dPresentacion.Cantidad_pieza = Convert.ToInt32(fila["cantidad_pieza"]);
                dPresentacion.Precio_pieza= Convert.ToDecimal(fila["precio_pieza"]);
                dPresentacion.Precio_venta= Convert.ToDecimal(fila["precio_venta"]);
                dPresentacions.Add(dPresentacion);
            }

            return dProducto.Insertar(dProducto, dCategoriaProductos, dPresentacions, dImagenes2);
        }

        public static DataTable Mostrar()
        {
            return new DProducto().Mostrar();
        }

        public static DataTable BuscarImagen(int producto)
        {
            DImagenes Obj = new DImagenes();
            Obj.Idpbuscar = producto;
            return Obj.Mostrar(Obj);
        }

        public static DataTable BuscarProducto(int producto)
        {
            DProducto dProducto = new DProducto();
            dProducto.Idpbuscar=producto;
            return dProducto.Buscar(dProducto);
        }
    }
}
