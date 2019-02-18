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

namespace SyncfusionWpfApp1.PRODUCTO
{
    /// <summary>
    /// Lógica de interacción para CrearMarcaP.xaml
    /// </summary>
    public partial class CrearMarcaP : Window
    {
        public CrearMarcaP()
        {
            InitializeComponent();
        }

        private void Buttoncrear_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textnombre.Text) || !string.IsNullOrEmpty(textnombre.Text))
            {
                foreach (DataRow item in NMarca.Mostrar().Rows)
                {
                    if (item[1].ToString() == textnombre.Text)
                    {
                        MessageBox.Show("Nombre ya registrado");
                        break;
                    }
                    else
                    {
                        NMarca.Insertar(textnombre.Text);
                        break;
                    }
                }
                   
            }
            else
                MessageBox.Show("Ingrese Todos los Datos");
        }

        private void Buttoncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
