using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace SyncfusionWpfApp1.PRODUCTO
{
    /// <summary>
    /// Lógica de interacción para MainU.xaml
    /// </summary>
    public partial class MainP : Page
    {
        private void cargarimagen(int idp)
        {
            DataTable dataimagen = NProducto.BuscarImagen(idp);
            DataSet ds = new DataSet("dslist");
            ds.Tables.Add(dataimagen);
            listaimagenes.DataContext = ds;
        }

        public MainP()
        {
            InitializeComponent();

            var dttable = NProducto.Mostrar();
            DataSet data = new DataSet("dstile");
            data.Tables.Add(dttable);
            tileviewp.DataContext = data;

            tileviewp.SelectionChanged += Tileviewp_SelectionChanged;
        }

        private void Tileviewp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)tileviewp.SelectedItem;
            cargarimagen((int)dataRow[0]);
        }
    }
}
