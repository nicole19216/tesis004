using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NEGOCIO;

namespace SyncfusionWpfApp1.COTIZACION
{
    /// <summary>
    /// Lógica de interacción para CrearC.xaml
    /// </summary>
    public partial class CrearCo : Page
    {

        private DataTable item;
        private DataTable aux;

        private decimal total;
        private int id_pro;

        private void crear_tablas()
        {
            item = new DataTable("ITEM");
            item.Columns.Add("id", Type.GetType("System.Int32"));
            item.Columns.Add("codigo", Type.GetType("System.String"));
            item.Columns.Add("producto", Type.GetType("System.String"));
            item.Columns.Add("unidad", Type.GetType("System.String"));
            item.Columns.Add("cantidad", Type.GetType("System.String"));
            item.Columns.Add("piezas", Type.GetType("System.String"));
            item.Columns.Add("precioa", Type.GetType("System.Decimal"));
            item.Columns.Add("precion", Type.GetType("System.Decimal"));
            item.Columns.Add("costo", Type.GetType("System.Decimal"));

            aux = new DataTable("SEL");
            aux.Columns.Add("id", Type.GetType("System.Int32"));
            aux.Columns.Add("codigo", Type.GetType("System.String"));
            aux.Columns.Add("producto", Type.GetType("System.String"));
            aux.Columns.Add("unidad", Type.GetType("System.String"));
            aux.Columns.Add("cantidad", Type.GetType("System.String"));
            aux.Columns.Add("piezas", Type.GetType("System.String"));
            aux.Columns.Add("precioa", Type.GetType("System.Decimal"));
            aux.Columns.Add("precion", Type.GetType("System.Decimal"));
            aux.Columns.Add("costo", Type.GetType("System.Decimal"));
        }

        private int validar_campos()
        {
            int contador = 0;

            if (string.IsNullOrEmpty(text_cotizacion.Text) || string.IsNullOrWhiteSpace(text_cotizacion.Text))
            {
                text_cotizacion.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(text_cliente.Text) || string.IsNullOrWhiteSpace(text_cliente.Text))
            {
                text_cliente.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (item.Rows.Count <= 0)
                contador++;

            return contador;
        }
        public CrearCo()
        {
            InitializeComponent();
        }

        public void pasaritem(int id, string codigo, string nombre)
        {
            int contador = 0;

            if (item.Rows.Count > 0)
            {
                foreach (DataRow dataRow in item.Rows)
                {
                    if (dataRow["id"].Equals(id))
                    {
                        contador++;
                        break;
                    }
                }
            }

            if (contador <= 0)
            {
                DataRow row = item.NewRow();
                row["id"] = id;
                row["codigo"] = codigo;
                row["producto"] = nombre;
                row["unidad"] = "-----";
                row["cantidad"] = 00000;
                row["piezas"] = 00000;
                row["precioa"] = 00000;
                row["precion"] = 00000;
                row["costo"] = 00000;
                item.Rows.Add(row);
                DataGridNumero.DataContext = item;
                DataGridNumero.GridColumnSizer.ResetAutoCalculationforAllColumns();
                DataGridNumero.GridColumnSizer.Refresh();
            }


            aux = item.Copy();
            //DataRow data = aux.NewRow();
            //data["id"] = id;
            //data["codigo"] = codigo;
            //data["producto"] = nombre;
            //data["unidad"] = "-----";
            //data["cantidad"] = 00000;
            //data["piezas"] = 00000;
            //data["precioa"] = 00000;
            //data["precion"] = 00000;
            //data["costo"] = 00000;
            //aux.Rows.Add(row);
        }

        public void pasarproveedor(int id, string nombre)
        {
            text_cliente.Text = nombre;
            id_pro = id;
        }

        public void agregar_crear(string nombre, decimal precio, decimal precio_nuevo, int cantidad_pieza, int cantidad)
        {
            DataRowView dr = DataGridNumero.SelectedItem as DataRowView;
            total = 0;

            foreach (DataRow item in item.Rows)
            {
                if (item["id"].Equals(dr["id"]))
                {
                    item["unidad"] = nombre;
                    item["cantidad"] = cantidad.ToString();
                    item["piezas"] = cantidad_pieza.ToString();
                    item["precioa"] = precio;
                    item["precion"] = precio_nuevo;
                    item["costo"] = Convert.ToDecimal(cantidad * precio_nuevo);

                    dr["unidad"] = nombre;
                    dr["cantidad"] = cantidad.ToString();
                    dr["piezas"] = cantidad_pieza.ToString();
                    dr["precioa"] = precio;
                    dr["precion"] = precio_nuevo;
                    dr["costo"] = Convert.ToDecimal(cantidad * precio_nuevo);

                    break;
                }
            }
            foreach (DataRow costo in item.Rows)
            {
                total = Convert.ToDecimal(costo["costo"]) + total;
            }

            lbltotal.Content = total;

            aux = item.Copy();
        }

        private void Buttonbuscar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    BuscarProductoC buscarProductoC = new BuscarProductoC();
            //    buscarProductoC.Inter = this;
            //    buscarProductoC.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void DataGridNumero_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    if (DataGridNumero.SelectedItem == null)
            //        return;

            //    DataRowView dr = DataGridNumero.SelectedItem as DataRowView;
            //    //MessageBox.Show(dr["MARCA"].ToString());
            //    AgregarC agregar = new AgregarC((int)dr["id"]);
            //    //agregar.idp = (int)dr["ID"];
            //    agregar.Inter = this;
            //    agregar.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Buttonborrar_Click(object sender, RoutedEventArgs e)
        {
            DataGridNumero.SelectionMode = Syncfusion.UI.Xaml.Grid.GridSelectionMode.Multiple;
            DataGridNumero.AllowDeleting = true;

            borrarok.Visibility = Visibility.Visible;
            borrarcancel.Visibility = Visibility.Visible;

            buttonborrar.IsEnabled = false;
        }

        private void Borrarok_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView drv in DataGridNumero.SelectedItems)
            {
                for (int i = aux.Rows.Count - 1; i >= 0; i--)
                {
                    if (drv["id"].Equals(aux.Rows[i]["id"]))
                    {
                        aux.Rows.RemoveAt(i);
                        break;
                    }
                }
            }

            item = aux.Copy();
            DataGridNumero.DataContext = item;
            DataGridNumero.GridColumnSizer.ResetAutoCalculationforAllColumns();
            DataGridNumero.GridColumnSizer.Refresh();

            total = 0;
            foreach (DataRow costo in item.Rows)
            {
                total = Convert.ToDecimal(costo["costo"]) + total;
            }

            lbltotal.Content = total;
        }

        private void Borrarcancel_Click(object sender, RoutedEventArgs e)
        {
            DataGridNumero.SelectionMode = Syncfusion.UI.Xaml.Grid.GridSelectionMode.Single;
            DataGridNumero.AllowDeleting = false;

            borrarok.Visibility = Visibility.Collapsed;
            borrarcancel.Visibility = Visibility.Collapsed;

            buttonborrar.IsEnabled = true;
        }

        private void Buttonguardar_Click(object sender, RoutedEventArgs e)
        {
            if (validar_campos() <= 0)
            {
                string resp = NCompra.Insertar(id_pro, 16, text_cotizacion.Text, fechaproduccion.DateTime.Value, item);

                if (resp.Equals("OK"))
                    MessageBox.Show("PROVEEDOR REGISTRADO CORRECTAMENTE");
                else
                    MessageBox.Show("error");
            }
            else
            {
                MessageBox.Show("Rellene los campos remarcados / Agrege al menos un item");
            }
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    BuscarProveedorC proveedorC = new BuscarProveedorC();
            //    proveedorC.Interface1 = this;
            //    proveedorC.ShowDialog();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}

        }
    }
}
