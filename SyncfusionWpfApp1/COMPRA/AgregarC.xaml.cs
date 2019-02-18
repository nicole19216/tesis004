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
        public int idp = 0;

        public AgregarC()
        {
            InitializeComponent();
            DataTable data = NProducto.BuscarProducto(idp);
            DataSet ds = new DataSet("dsproducto");
            ds.Tables.Add(data);
            this.DataContext = ds.Tables[0];

            combounidadcompra.ItemsSource=

            //combounidadcompra.Items.CurrentChanged
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
