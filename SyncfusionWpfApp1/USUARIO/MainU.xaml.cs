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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncfusionWpfApp1.USUARIO
{
    /// <summary>
    /// Lógica de interacción para MainU.xaml
    /// </summary>
    public partial class MainU : Page
    {
        public MainU()
        {
            InitializeComponent();
            
            var table = NEGOCIO.NUsuario.Mostrar();
            DataSet dataSet = new DataSet("ListBox");
            dataSet.Tables.Add(table);
            tileview.DataContext = dataSet;

            sfgrid.DataContext = NEGOCIO.NUsuario.Mostrar();

            tileview.SelectionChanged += Tileview_SelectionChanged;

            sfgrid.CellTapped += Sfgrid_CellTapped;

        }

        private void Sfgrid_CellTapped(object sender, Syncfusion.UI.Xaml.Grid.GridCellTappedEventArgs e)
        {
            var g = sfgrid.CurrentItem;
        }

        private void Tileview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = tileview.SelectedItem;
            int o = 0;
            DataRowView dataRow = (DataRowView)tileview.SelectedItem;
            EditarU editarU = new EditarU(dataRow[3].ToString());
            editarU.ShowDialog();

            foreach (DataRowView datos in tileview.Items)
            {
                string res = datos[o].ToString();
                o++;
            }
        }
    }
}
