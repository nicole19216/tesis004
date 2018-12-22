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

namespace SyncfusionWpfApp1.USUARIO
{
    /// <summary>
    /// Lógica de interacción para CrearU.xaml
    /// </summary>
    public partial class CrearU : Window
    {

        private DataTable dtnumero;
        private DataTable dtdireccion;
        private DataTable dtrolusuario;

        private void Crear_tabla()
        {
            dtnumero = new DataTable("Dtnumero");
            dtnumero.Columns.Add("celular", System.Type.GetType("System.Int64"));
            dtnumero.Columns.Add("telefono", System.Type.GetType("System.Int64"));

            dtdireccion = new DataTable("Dtdireccion");
            dtdireccion.Columns.Add("pais", System.Type.GetType("System.String"));
            dtdireccion.Columns.Add("departamento", System.Type.GetType("System.String"));
            dtdireccion.Columns.Add("provincia", System.Type.GetType("System.String"));
            dtdireccion.Columns.Add("municipio", System.Type.GetType("System.String"));
            dtdireccion.Columns.Add("direccion", System.Type.GetType("System.String"));
            

            dtrolusuario = new DataTable("Dtrol");
            dtrolusuario.Columns.Add("id", Type.GetType("System.Int32"));
            dtrolusuario.Columns.Add("rol", Type.GetType("System.String"));
            dtrolusuario.Columns.Add("estado", Type.GetType("System.Boolean"));
        }

        public CrearU()
        {
            InitializeComponent();
        }

        private void Buttonagregartelefono_Click(object sender, RoutedEventArgs e)
        {
            combotelefono.Items.Add(combotelefono.Text);
            combotelefono.Text = null;
            combotelefono.IsDropDownOpen = true;
        }

        private void Buttonquitartelefono__Click(object sender, RoutedEventArgs e)
        {
            combotelefono.Items.Remove(combotelefono.SelectedItem);
            combotelefono.Text = null;
            combotelefono.IsDropDownOpen = true;
        }
    }
}
