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
    /// Lógica de interacción para BuscarProductoCO.xaml
    /// </summary>
    public partial class BuscarProductoCO : Window
    {
        DataTable data;
        public BuscarProductoCO()
        {
            InitializeComponent();
            sfdtgrid.DataContext = NProducto.Mostrar();
        }
        private void Textbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            sfdtgrid.SearchHelper.SearchBrush = Brushes.Yellow;
            sfdtgrid.SearchHelper.Search(textbuscar.Text);
        }

        private int id;
        private string codigo;
        private string producto;
        private Interface1 inter;

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Producto { get => producto; set => producto = value; }
        public Interface1 Inter { get => inter; set => inter = value; }

        //public event RoutedEventHandler AgregarEventHandler;

        private void Sfdtgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (sfdtgrid.SelectedItem == null)
                return;

            DataRowView dr = sfdtgrid.SelectedItem as DataRowView;
            Id = (int)dr["ID"];
            Codigo = (string)dr["CODIGO_BARRAS"];
            Producto = (string)dr["NOMBRE"];

            Inter.pasaritem(Id, Codigo, Producto);

            //for (int i = data.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (dr["ID"].Equals(data.Rows[i]["ID"]))
            //    {
            //        data.Rows.RemoveAt(i);
            //        break;
            //    }
            //}

            //sfdtgrid.DataContext = data;
        }
    }
}
