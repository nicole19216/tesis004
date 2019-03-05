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
        public int idp;
        private DataTable datacombo;

        public AgregarC(int p)
        {
            InitializeComponent();
            CargarInfoProducto(p);
            CargarInfoCosto(p);
            //combounidadcompra.Items.CurrentChanged
        }

        private void CargarInfoProducto(int id)
        {
            DataTable data = NProducto.BuscarProducto(id);
            DataSet ds = new DataSet("dsproducto");
            ds.Tables.Add(data);
            gridproducto.DataContext = ds.Tables[0];
        }

        private void CargarInfoCosto(int id)
        {
            combounidadcompra.ItemsSource = NCompra.BuscarCosto(id).DefaultView;
            combounidadcompra.SelectionChanged += Combounidadcompra_SelectionChanged;
            datacombo = NCompra.BuscarCosto(id);
        }

        private void Combounidadcompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRow item in datacombo.Rows)
            {
                if (Convert.ToInt32(item[0]) == Convert.ToInt32(combounidadcompra.SelectedValue))
                {
                    textcostocompra.Text =(decimal.Round((decimal)item[2],2)).ToString();
                    textcantidadpieza.Text = item[3].ToString();
                    textcostopieza.Text = (decimal.Round((decimal)item[2] / (int)item[3],2)).ToString();
                }
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
