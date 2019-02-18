using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using System.Data;

namespace SyncfusionWpfApp1.PRODUCTO
{
    /// <summary>
    /// Lógica de interacción para CrearCategoriaP.xaml
    /// </summary>
    public partial class CrearCategoriaP : Window
    {
        public CrearCategoriaP()
        {
            InitializeComponent();
            combocategoria.ItemsSource = NCategoria.Mostrar().DefaultView;
        }
        public event RoutedEventHandler AgregarEventHandler;

        private void Buttonañadir_Click(object sender, RoutedEventArgs e)
        {
            if (combocategoria.SelectedItem != null)
            {
                if (AgregarEventHandler != null)
                {
                    //Disparamos el evento que hará que el dataGridView de la Ventana
                    //padre se actualice
                    AgregarEventHandler(sender, e);
                }
            }
            else
                MessageBox.Show("Seleccione un item de la Lista");
        }

        private void Buttoncrear_Click(object sender, RoutedEventArgs e)
        {
            combocategoria.Visibility = Visibility.Collapsed;
            buttonañadir.Visibility = Visibility.Collapsed;
            buttoncrear.Visibility = Visibility.Collapsed;
            textnombre.Visibility = Visibility.Visible;
            buttonaceptar.Visibility = Visibility.Visible;
            buttoncancelar.Visibility = Visibility.Visible;
        }

        private void Buttonaceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textnombre.Text)||!string.IsNullOrEmpty(textnombre.Text))
            {
                foreach (DataRow item in NCategoria.Mostrar().Rows)
                {
                    if (item[1].ToString()==textnombre.Text)
                    {
                        MessageBox.Show("Nombre ya registrado");
                        break;
                    }
                    else
                    {
                        NCategoria.Insertar(textnombre.Text);
                        combocategoria.ItemsSource = null;
                        combocategoria.ItemsSource = NCategoria.Mostrar().DefaultView;

                        combocategoria.Visibility = Visibility.Visible;
                        buttonañadir.Visibility = Visibility.Visible;
                        buttoncrear.Visibility = Visibility.Visible;
                        textnombre.Visibility = Visibility.Collapsed;
                        buttonaceptar.Visibility = Visibility.Collapsed;
                        buttoncancelar.Visibility = Visibility.Collapsed;
                        MessageBox.Show("correcto");
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Ingresar Nombre");
        }

        private void Buttoncancelar_Click(object sender, RoutedEventArgs e)
        {
            combocategoria.Visibility = Visibility.Visible;
            buttonañadir.Visibility = Visibility.Visible;
            buttoncrear.Visibility = Visibility.Visible;
            textnombre.Visibility = Visibility.Collapsed;
            buttonaceptar.Visibility = Visibility.Collapsed;
            buttoncancelar.Visibility = Visibility.Collapsed;
        }

        private void Buttoncerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
