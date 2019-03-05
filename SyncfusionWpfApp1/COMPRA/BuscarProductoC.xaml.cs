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
    /// Lógica de interacción para BuscarProductoC.xaml
    /// </summary>
    public partial class BuscarProductoC : Window
    {
        public BuscarProductoC()
        {
            InitializeComponent();
            sfdtgrid.DataContext = NProducto.Mostrar();
        }

        private void Textbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            sfdtgrid.SearchHelper.SearchBrush = Brushes.Yellow;
            sfdtgrid.SearchHelper.Search(textbuscar.Text);
        }

        private void Sfdtgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sfdtgrid.SelectedItem == null)
                    return;

                DataRowView dr = sfdtgrid.SelectedItem as DataRowView;
                //MessageBox.Show(dr["MARCA"].ToString());
                AgregarC agregar = new AgregarC((int)dr["ID"]);
                //agregar.idp = (int)dr["ID"];
                agregar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
