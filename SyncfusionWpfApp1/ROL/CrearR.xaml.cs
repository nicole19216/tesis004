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
        public CrearR()
        {
            InitializeComponent();
            comborol.ItemsSource = NEGOCIO.NRol.Mostrar().DefaultView;
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
                    checkBox.FontSize = 15;
                    checkBox.FontWeight = FontWeights.Medium;
                    wrapusuario.Children.Add(checkBox);
                }
            }
        }

        public event RoutedEventHandler AgregarEventHandler;

        private void Buttonseleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (AgregarEventHandler != null)
            {
                //Disparamos el evento que hará que el dataGridView de la Ventana
                //padre se actualice
                AgregarEventHandler(sender, e);
            }
        }

        private void ButtonNuevorol_Click(object sender, RoutedEventArgs e)
        {
            buttonseleccionar.Visibility = Visibility.Collapsed;
            buttonNuevorol.Visibility = Visibility.Collapsed;
            buttonguardar.Visibility = Visibility.Visible;
            gridocultar.Visibility = Visibility.Visible;
            textnombre.Visibility = Visibility.Visible;
            comborol.Visibility = Visibility.Collapsed;
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
        }

        private void Buttonguardar_Click(object sender, RoutedEventArgs e)
        {
            buttonseleccionar.Visibility = Visibility.Visible;
            buttonNuevorol.Visibility = Visibility.Visible;
            buttonguardar.Visibility = Visibility.Collapsed;
            gridocultar.Visibility = Visibility.Collapsed;
            textnombre.Visibility = Visibility.Collapsed;
            comborol.Visibility = Visibility.Visible;
        }
    }
}
