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

namespace SyncfusionWpfApp1.COMPRA
{
    /// <summary>
    /// Lógica de interacción para AgregarC.xaml
    /// </summary>
    public partial class AgregarC : Window
    {
        private string unidad;
        private decimal precio;
        private decimal precio_nuevo;
        private int piezas;
        private int cantidad;
        private Interface1 inter;

        int idp;
        private DataTable datacombo;
        private DataTable dtcosto;

        public string Unidad { get => unidad; set => unidad = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Piezas { get => piezas; set => piezas = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public Interface1 Inter { get => inter; set => inter = value; }
        public decimal Precio_nuevo { get => precio_nuevo; set => precio_nuevo = value; }

        private int validar_campos()
        {
            int contador = 0;

            if (string.IsNullOrEmpty(textnuevocantidadpieza.Text) || string.IsNullOrWhiteSpace(textnuevocantidadpieza.Text))
            {
                textnuevocantidadpieza.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textnuevocantidadpieza.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textnuevocostocompra.Text) || string.IsNullOrWhiteSpace(textnuevocostocompra.Text))
            {
                textnuevocostocompra.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textnuevocostocompra.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textnuevocostopieza.Text) || string.IsNullOrWhiteSpace(textnuevocostopieza.Text))
            {
                textnuevocostopieza.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textnuevocostopieza.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textnuevounidadcompra.Text) || string.IsNullOrWhiteSpace(textnuevounidadcompra.Text))
            {
                textnuevounidadcompra.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textnuevounidadcompra.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textcantidad.Text) || string.IsNullOrWhiteSpace(textcantidad.Text))
            {
                textcantidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textcantidad.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(combounidadcompra.Text)|| string.IsNullOrWhiteSpace(combounidadcompra.Text))
            {
                combounidadcompra.BorderBrush=new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                combounidadcompra.BorderBrush= new SolidColorBrush(Colors.Black);

            return contador;
        }

        private void Crear_tabla()
        {
            dtcosto = new DataTable("Dtcosto");
            dtcosto.Columns.Add("unidad_compra", Type.GetType("System.String"));
            dtcosto.Columns.Add("cantidad_piezas", Type.GetType("System.Int32"));
            dtcosto.Columns.Add("precio", Type.GetType("System.Decimal"));
            dtcosto.Columns.Add("costo_pieza", Type.GetType("System.Decimal"));
            dtcosto.Columns.Add("cantidad", Type.GetType("System.Int32"));
            BuscarProductoC buscarProductoC = new BuscarProductoC();
        }

        public AgregarC(int p)
        {
            InitializeComponent();
            CargarInfoProducto(p);
            CargarInfoCosto(p);
            Crear_tabla();
            idp = p;
            //combounidadcompra.Items.CurrentChanged
        }

        private void CargarInfoProducto(int id)
        {
            DataTable data = NProducto.BuscarProducto(id);
            DataSet ds = new DataSet("dsproducto");
            ds.Tables.Add(data);
            gridproducto.DataContext = ds.Tables[0];
            combonuevounidadcompra.SelectionChanged += Combonuevounidadcompra_SelectionChanged;
        }

        private void CargarInfoCosto(int id)
        {
            combounidadcompra.ItemsSource = NCompra.BuscarCosto(id).DefaultView;
            combounidadcompra.SelectionChanged += Combounidadcompra_SelectionChanged;
            datacombo = NCompra.BuscarCosto(id);
        }

        private void Combonuevounidadcompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRow item in datacombo.Rows)
            {
                if (Convert.ToInt32(item[0]) == Convert.ToInt32(combounidadcompra))
                {
                    textnuevocostocompra.Text = (decimal.Round((decimal)item[2], 2)).ToString();
                    textnuevocantidadpieza.Text = item[3].ToString();
                    textnuevocostopieza.Text = (decimal.Round((decimal)item[2] / (int)item[3], 2)).ToString();
                }
            }
        }

        private void Combounidadcompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRow item in datacombo.Rows)
            {
                if (item[1].ToString().Equals(combounidadcompra.SelectedValue.ToString()))
                {
                    textcostocompra.Text =(decimal.Round((decimal)item[2],2)).ToString();
                    textcantidadpieza.Text = item[3].ToString();
                    textcostopieza.Text = (decimal.Round((decimal)item[2] / (int)item[3],2)).ToString();
                    break;
                }
            }
        }

        private void Listnuevacompramenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listnuevacompramenu.SelectedItem==itemdowncompra)
            {
                textnuevounidadcompra.Text = combounidadcompra.Text;
                textnuevocantidadpieza.Text = textcantidadpieza.Text;
                textnuevocostocompra.Text = textcostocompra.Text;
                textnuevocostopieza.Text = textcostopieza.Text;

                textnuevounidadcompra.Background = new SolidColorBrush(Colors.LightGray);
                textnuevocantidadpieza.Background = new SolidColorBrush(Colors.LightGray);
                textnuevocostopieza.Background = new SolidColorBrush(Colors.LightGray);

                textnuevounidadcompra.Focusable = false;
                textnuevocantidadpieza.Focusable = false;
                textnuevocostopieza.Focusable = false;

                textcantidad.Focus();
            }
            if (listnuevacompramenu.SelectedItem==itemnuevo)
            {
                textnuevounidadcompra.Text = string.Empty;
                textnuevocantidadpieza.Text = string.Empty;
                textnuevocostocompra.Text = string.Empty;
                textnuevocostopieza.Text = string.Empty;

                textnuevounidadcompra.Background = new SolidColorBrush(Colors.Transparent);
                textnuevocantidadpieza.Background = new SolidColorBrush(Colors.Transparent);
                textnuevocostopieza.Background = new SolidColorBrush(Colors.Transparent);

                textnuevounidadcompra.Focusable = true;
                textnuevocantidadpieza.Focusable = true;
                textnuevocostopieza.Focusable = true;

                textnuevounidadcompra.Focus();
            }
        }

        private void prepararcompra()
        {
            DataRow row = dtcosto.NewRow();
            row["unidad_compra"] = combonuevounidadcompra.Text;
            row["precio"] = Convert.ToDecimal(textnuevocostocompra.Text);
            row["cantidad_piezas"] = Convert.ToInt32(textnuevocantidadpieza.Text);
            row["costo_pieza"] = Convert.ToDecimal(textnuevocostopieza.Text);
            row["cantidad"] = Convert.ToInt32(textcantidad.Text);
            dtcosto.Rows.Add(row);
        }

        private void Buttonagregar_Click(object sender, RoutedEventArgs e)
        {
            if (validar_campos()==0)
            {
                prepararcompra();
                Unidad = textnuevounidadcompra.Text;
                Precio = Convert.ToDecimal(textcostocompra.Text);
                Precio_nuevo = Convert.ToDecimal(textnuevocostocompra.Text);
                Piezas = Convert.ToInt32(textnuevocantidadpieza.Text);
                Cantidad = Convert.ToInt32(textcantidad.Text);
                Inter.agregar_crear(Unidad, Precio, Precio_nuevo, Piezas, Cantidad);
                MessageBox.Show("Agregado Correctamente", "Mensaje del Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Llene los campos correctamente");
            }
        }
        //private void Textbarras_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key==Key.Return)
        //    {
        //        textbarras.Text = "enter";
        //    }
        //}
    }
}
