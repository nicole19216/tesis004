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

namespace SyncfusionWpfApp1.ROL
{
    /// <summary>
    /// Lógica de interacción para CrearR.xaml
    /// </summary>
    public partial class CrearR : Window
    {

        List<CheckBox> lcb = new List<CheckBox>();
        List<CheckBox> lcb1 = new List<CheckBox>();

        public CrearR()
        {
            InitializeComponent();
            comborol.ItemsSource= NEGOCIO.NRol.Mostrar().DefaultView;
            comborol.SelectionChanged += Comborol_SelectionChanged;
            llenarprivilegios();
        }

        private void llenarprivilegios()
        {
            foreach (DataRow per in NEGOCIO.NPrivilegio.Mostrar().Rows)
            {
                if (per[1].Equals(3))
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Content = per[2].ToString();
                    checkBox.Name = "a" + per[0].ToString();
                    checkBox.FontSize = 15;
                    checkBox.FontWeight = FontWeights.Medium;
                    checkBox.IsEnabled = false;
                    lcb.Add(checkBox);
                    wrapusuario.Children.Add(checkBox);
                }
            }
        }

        public event RoutedEventHandler AgregarEventHandler;

        private void Buttonseleccionar_Click(object sender, RoutedEventArgs e)
        {
            //string n = comborol.Text;
            //string nom = comborol.SelectedItem.ToString();
            if (comborol.SelectedItem != null)
            {
                string n = comborol.Text;
                string nom = comborol.SelectedItem.ToString();
                if (AgregarEventHandler != null)
                {
                    //Disparamos el evento que hará que el dataGridView de la Ventana
                    //padre se actualice
                    AgregarEventHandler(sender, e);
                }
            }
            else
                MessageBox.Show("Seleccione un rol de la Lista");
        }

        private void ButtonNuevorol_Click(object sender, RoutedEventArgs e)
        {
            buttonseleccionar.Visibility = Visibility.Collapsed;
            buttonNuevorol.Visibility = Visibility.Collapsed;
            buttonguardar.Visibility = Visibility.Visible;
            buttoncancelar.Visibility = Visibility.Visible;
            textnombre.Visibility = Visibility.Visible;
            comborol.Visibility = Visibility.Collapsed;
            checrol.IsEnabled = true;
        }

        private void Comborol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRow item in NEGOCIO.NRol.Mostrar().Rows)
            {
                if (Convert.ToInt32(item[0]) == Convert.ToInt32(comborol.SelectedValue))
                {
                    checrol.IsChecked = Convert.ToBoolean(item[2]);
                }
            }

            foreach (System.Data.DataRow item in NEGOCIO.NRolPrivilegio.Mostrar().Rows)
            {
                if (Convert.ToInt32(item[0]) == Convert.ToInt32(comborol.SelectedValue))
                {
                    foreach (CheckBox box in lcb)
                    {
                        string nc = box.Name.Remove(0, 1);
                        int a = Convert.ToInt32(nc);
                        if (a == Convert.ToInt32(item[1]))
                        {
                            box.IsChecked = Convert.ToBoolean(item[2]);
                        }
                    }
                }
            }
        }

        private void Buttonguardar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textnombre.Text))
            {
                bool estado;
                if (checrol.IsChecked == true)
                    estado = true;
                else
                    estado = false;

                NEGOCIO.NRol.Insertar(textnombre.Text, estado);
                comborol.ItemsSource = null;
                comborol.ItemsSource = NEGOCIO.NRol.Mostrar().DefaultView;

                buttonseleccionar.Visibility = Visibility.Visible;
                buttonNuevorol.Visibility = Visibility.Visible;
                buttonguardar.Visibility = Visibility.Collapsed;
                buttoncancelar.Visibility = Visibility.Collapsed;
                textnombre.Visibility = Visibility.Collapsed;
                comborol.Visibility = Visibility.Visible;
                checrol.IsEnabled = false;
            }
        }

        private void Buttoncancelar_Click(object sender, RoutedEventArgs e)
        {
            buttonseleccionar.Visibility = Visibility.Visible;
            buttonNuevorol.Visibility = Visibility.Visible;
            buttonguardar.Visibility = Visibility.Collapsed;
            buttoncancelar.Visibility = Visibility.Collapsed;
            textnombre.Visibility = Visibility.Collapsed;
            comborol.Visibility = Visibility.Visible;
            checrol.IsEnabled = false;
        }

        private void Checrol_Click(object sender, RoutedEventArgs e)
        {
            if (checrol.IsChecked == true)
            {
                checrol.Content = "Habilitado";
            }
            if (checrol.IsChecked == false)
            {
                checrol.Content = "Deshabilitado";
            }
        }
    }
}
