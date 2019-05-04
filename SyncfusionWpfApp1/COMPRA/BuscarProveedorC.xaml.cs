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
    /// Lógica de interacción para BuscarProveedorC.xaml
    /// </summary>
    public partial class BuscarProveedorC : Window
    {
        public BuscarProveedorC()
        {
            InitializeComponent();
            sfdtgrid.DataContext = NProveedor.Mostrar();
        }

        private void Textbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            sfdtgrid.SearchHelper.SearchBrush = Brushes.Yellow;
            sfdtgrid.SearchHelper.Search(textbuscar.Text);
        }

        private int id;
        private string nombre;
        private Interface1 interface1;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Interface1 Interface1 { get => interface1; set => interface1 = value; }

        private void Sfdtgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sfdtgrid.SelectedItem == null)
                return;

            DataRowView dr = sfdtgrid.SelectedItem as DataRowView;
            Id = (int)dr["ID"];
            Nombre = (string)dr["NOMBRE"];

            Interface1.pasarproveedor(Id, Nombre);

            this.Close();
        }
    }
}
