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

namespace SyncfusionWpfApp1.PROVEEDOR
{
    /// <summary>
    /// Lógica de interacción para CrearP.xaml
    /// </summary>
    public partial class CrearP : Window
    {
        private DataTable dtnumero;
        private DataTable dtdireccion;

        private int validar_campos()
        {
            int contador = 0;

            if (string.IsNullOrEmpty(textnombreentidad.Text) || string.IsNullOrWhiteSpace(textnombreentidad.Text))
            {
                textnombreentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textnitentidad.Text) || string.IsNullOrWhiteSpace(textnitentidad.Text))
            {
                textnitentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textemailentidad.Text) || string.IsNullOrWhiteSpace(textemailentidad.Text))
            {
                textemailentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combopaisentidad.Items.Count <= 0)
            {
                combopaisentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combomunientidad.Items.Count <= 0)
            {
                combomunientidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combodirentidad.Items.Count <= 0)
            {
                combodirentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combotelentidad.Items.Count <= 0)
            {
                combotelentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combocelentidad.Items.Count <= 0)
            {
                combocelentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            return contador;
        }

        private int validar_campospersona()
        {
            int contador = 0;

            if (string.IsNullOrEmpty(textnombreper.Text) || string.IsNullOrWhiteSpace(textnombreper.Text))
            {
                textnombreper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textnitper.Text) || string.IsNullOrWhiteSpace(textnitper.Text))
            {
                textnitper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textemailper.Text) || string.IsNullOrWhiteSpace(textemailper.Text))
            {
                textemailper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textciper.Text) || string.IsNullOrWhiteSpace(textciper.Text))
            {
                textciper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textdirper.Text) || string.IsNullOrWhiteSpace(textdirper.Text))
            {
                textdirper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (string.IsNullOrEmpty(textciper.Text) || string.IsNullOrWhiteSpace(textciper.Text))
            {
                textciper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combotelper.Items.Count <= 0)
            {
                combotelper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            if (combocelper.Items.Count <= 0)
            {
                combocelper.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            return contador;
        }

        private void tabs()
        {
            if (tabentidad.IsChecked == true)
            {
                borderper.Visibility = Visibility.Collapsed;
                borderemp.Visibility = Visibility.Visible;
            }
            if (tabpersona.IsChecked == true)
            {
                cargar_comboproveedor();
                borderper.Visibility = Visibility.Visible;
                borderemp.Visibility = Visibility.Collapsed;
            }
        }

        private void crear_tablas()
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
        }

        private void preparar_direccion()
        {
            if (combopaisentidad.Items.Count > 0 && combomunientidad.Items.Count > 0 && combodirentidad.Items.Count > 0)
            {
                for (int i = 0; i < combopaisentidad.Items.Count; i++)
                {
                    DataRow row = dtdireccion.NewRow();
                    row["pais"] = combopaisentidad.Items[i];

                    if (combodepentidad.Items[i] == null || string.IsNullOrWhiteSpace(combodepentidad.Items[i].ToString()) || string.IsNullOrEmpty(combodepentidad.Items[i].ToString()))
                        row["departamento"] = "CLIENTE";
                    else
                        row["departamento"] = combodepentidad.Items[i];

                    if (comboprovinentidad.Items[i] == null || string.IsNullOrWhiteSpace(comboprovinentidad.Items[i].ToString()) || string.IsNullOrEmpty(comboprovinentidad.Items[i].ToString()))
                        row["provincia"] = "CLIENTE";
                    else
                        row["provincia"] = comboprovinentidad.Items[i];

                    row["municipio"] = combomunientidad.Items[i];
                    row["direccion"] = combodirentidad.Items[i];

                    dtdireccion.Rows.Add(row);
                }
            }
        }

        private void preparar_telefono_celular()
        {
            int a;

            if (combotelentidad.Items.Count > 0 || combocelentidad.Items.Count > 0)
            {
                if (combotelentidad.Items.Count >= combocelentidad.Items.Count)
                    a = combotelentidad.Items.Count;
                else
                    a = combocelentidad.Items.Count;

                for (int i = 0; i < a; i++)
                {
                    DataRow row = dtnumero.NewRow();

                    if (combotelentidad.Items.Count > i)
                        row["telefono"] = Convert.ToInt64(combotelentidad.Items[i]);
                    else
                        row["telefono"] = 01;

                    if (combocelentidad.Items.Count > i)
                        row["celular"] = Convert.ToInt64(combocelentidad.Items[i]);
                    else
                        row["celular"] = 01;

                    dtnumero.Rows.Add(row);
                }
            }
        }

        private void preparar_direccionpersona()
        {
            DataRow row = dtdireccion.NewRow();
            row["pais"] = "CLIENTE";
            row["departamento"] = "CLIENTE";
            row["provincia"] = "CLIENTE";
            row["municipio"] = textciudadper.Text; ;
            row["direccion"] = textdirper.Text;
            dtdireccion.Rows.Add(row);
        }

        private void preparar_telefono_celular_persona()
        {
            int a;

            if (combotelper.Items.Count > 0 || combocelper.Items.Count > 0)
            {
                if (combotelper.Items.Count >= combocelper.Items.Count)
                    a = combotelper.Items.Count;
                else
                    a = combocelper.Items.Count;

                for (int i = 0; i < a; i++)
                {
                    DataRow row = dtnumero.NewRow();

                    if (combotelper.Items.Count > i)
                        row["telefono"] = Convert.ToInt64(combotelper.Items[i]);
                    else
                        row["telefono"] = 01;

                    if (combocelper.Items.Count > i)
                        row["celular"] = Convert.ToInt64(combocelper.Items[i]);
                    else
                        row["celular"] = 01;

                    dtnumero.Rows.Add(row);
                }
            }
        }

        private void cargar_comboproveedor()
        {
            comboproveedor.ItemsSource = NEGOCIO.NProveedor.Mostrar().DefaultView;
        }

        public CrearP()
        {
            InitializeComponent();
            crear_tablas();
        }

        private void Tabentidad_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }

        private void Tabpersona_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }

        private void Listdireccionentidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listdireccionentidad.SelectedItem == itemdirentadd)
            {
                if (!string.IsNullOrEmpty(combopaisentidad.Text) && !string.IsNullOrWhiteSpace(combopaisentidad.Text) &&
                    !string.IsNullOrEmpty(combomunientidad.Text) && !string.IsNullOrWhiteSpace(combomunientidad.Text) &&
                        !string.IsNullOrEmpty(combodirentidad.Text) && !string.IsNullOrWhiteSpace(combodirentidad.Text))
                {
                    combopaisentidad.Items.Add(combopaisentidad.Text);
                    combodepentidad.Items.Add(combodepentidad.Text);
                    comboprovinentidad.Items.Add(comboprovinentidad.Text);
                    combomunientidad.Items.Add(combomunientidad.Text);
                    combodirentidad.Items.Add(combodirentidad.Text);

                    combopaisentidad.BorderBrush = new SolidColorBrush(Colors.Gray);
                    combomunientidad.BorderBrush = new SolidColorBrush(Colors.Gray);
                    combodirentidad.BorderBrush = new SolidColorBrush(Colors.Gray);

                    borderdireccion.IsEnabled = false;
                    buttondialogdir.Content = "Añadido!!";
                    buttondialogdir.Visibility = Visibility.Visible;
                }
                else
                {
                    combopaisentidad.BorderBrush = new SolidColorBrush(Colors.Red);
                    combomunientidad.BorderBrush = new SolidColorBrush(Colors.Red);
                    combodirentidad.BorderBrush = new SolidColorBrush(Colors.Red);

                    borderdireccion.IsEnabled = false;
                    buttondialogdir.Content = "Campos Obligatorios!!";
                    buttondialogdir.Visibility = Visibility.Visible;
                }
            }
            if (listdireccionentidad.SelectedItem == itemdirentquit)
            {
                combopaisentidad.Items.RemoveAt(combopaisentidad.Items.Count - 1);
                combodepentidad.Items.RemoveAt(combodepentidad.Items.Count - 1);
                comboprovinentidad.Items.RemoveAt(comboprovinentidad.Items.Count - 1);
                combomunientidad.Items.RemoveAt(combomunientidad.Items.Count - 1);
                combodirentidad.Items.RemoveAt(combodirentidad.Items.Count - 1);

                borderdireccion.IsEnabled = false;
                buttondialogdir.Content = "Removido!!";
                buttondialogdir.Visibility = Visibility.Visible;
            }
        }

        private void Buttondialogdir_Click(object sender, RoutedEventArgs e)
        {
            buttondialogdir.Visibility = Visibility.Collapsed;
            borderdireccion.IsEnabled = true;

            combopaisentidad.Text = null;
            combodepentidad.Text = null;
            comboprovinentidad.Text = null;
            combomunientidad.Text = null;
            combodirentidad.Text = null;
        }

        private void Listtelefonoentidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listtelefonoentidad.SelectedItem == itemtelentadd)
            {
                if (!string.IsNullOrEmpty(combotelentidad.Text) && !string.IsNullOrWhiteSpace(combotelentidad.Text))
                {
                    if (long.TryParse(combotelentidad.Text, out long r))
                    {
                        combotelentidad.Items.Add(combotelentidad.Text);
                        buttondialogtel.Content = "Añadido!!!";
                        buttondialogtel.Visibility = Visibility.Visible;
                        combotelentidad.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        buttondialogtel.Content = "Solo Numeros!!!";
                        buttondialogtel.Visibility = Visibility.Visible;
                        combotelentidad.Visibility = Visibility.Collapsed;
                    }
                }
            }
            if (listtelefonoentidad.SelectedItem == itemtelentquit)
            {
                combotelentidad.Items.Remove(combotelentidad.SelectedItem);
                combotelentidad.Text = null;
                combotelentidad.IsDropDownOpen = true;
            }
        }

        private void Buttondialogtel_Click(object sender, RoutedEventArgs e)
        {
            buttondialogtel.Visibility = Visibility.Collapsed;
            combotelentidad.Visibility = Visibility.Visible;
            combotelentidad.IsDropDownOpen = true;
        }

        private void Buttondialogcel_Click(object sender, RoutedEventArgs e)
        {
            buttondialogcel.Visibility = Visibility.Collapsed;
            combocelentidad.Visibility = Visibility.Visible;
            combocelentidad.IsDropDownOpen = true;
        }

        private void Listcelularntidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcelularntidad.SelectedItem == itemcelentadd)
            {
                if (!string.IsNullOrEmpty(combocelentidad.Text) && !string.IsNullOrWhiteSpace(combocelentidad.Text))
                {
                    if (long.TryParse(combocelentidad.Text, out long r))
                    {
                        combocelentidad.Items.Add(combocelentidad.Text);
                        buttondialogcel.Content = "Añadido!!!";
                        buttondialogcel.Visibility = Visibility.Visible;
                        combocelentidad.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        buttondialogcel.Content = "Solo Numeros!!!";
                        buttondialogcel.Visibility = Visibility.Visible;
                        combocelentidad.Visibility = Visibility.Collapsed;
                    }
                }
            }
            if (listcelularntidad.SelectedItem == itemcelentquit)
            {
                combocelentidad.Items.Remove(combocelentidad.SelectedItem);
                combocelentidad.Text = null;
                combocelentidad.IsDropDownOpen = true;
            }
        }

        private void Buttonguardad_Click(object sender, RoutedEventArgs e)
        {
            bool b;

            if (tabentidad.IsChecked == true)
            {
                if (toggleent.IsChecked == true)
                    b = true;
                else
                    b = false;

                if (validar_campos() <= 0)
                {
                    preparar_telefono_celular();
                    preparar_direccion();

                    string res = NEGOCIO.NProveedor.Insertar("PROVEEDOR", textnombreentidad.Text, textemailentidad.Text, System.DateTime.Today,
                        textnitentidad.Text, dtnumero, dtdireccion, textdescripcion.Text, b);

                    if (res.Equals("OK"))
                    {
                        MessageBox.Show("PROVEEDOR REGISTRADO CORRECTAMENTE");
                    }
                    else
                    {
                        MessageBox.Show("error");
                    }
                }
                else
                {
                    MessageBox.Show("Llene los campos remarkados");
                }
            }

            if (tabpersona.IsChecked == true)
            {
                if (toggleper.IsChecked == true)
                    b = true;
                else
                    b = false;

                if (validar_campospersona() <= 0)
                {
                    preparar_telefono_celular_persona();
                    preparar_direccionpersona();

                    string res = NEGOCIO.NRepresentante.Insertar(textciper.Text, textnombreper.Text, textemailper.Text, datefecha.DateTime.Value,
                        textnitper.Text, dtnumero, dtdireccion,Convert.ToInt32(comboproveedor.SelectedValue), b);

                    if (res.Equals("OK"))
                    {
                        MessageBox.Show("REPRESENTANTE REGISTRADO CORRECTAMENTE");
                    }
                    else
                    {
                        MessageBox.Show("error");
                    }
                }
                else
                {
                    MessageBox.Show("Llene los campos remarkados");
                }
            }
        }

        private void Buttondialogcelper_Click(object sender, RoutedEventArgs e)
        {
            buttondialogcelper.Visibility = Visibility.Collapsed;
            combocelper.Visibility = Visibility.Visible;
            combocelper.IsDropDownOpen = true;
        }

        private void Buttondialogtelper_Click(object sender, RoutedEventArgs e)
        {
            buttondialogtelper.Visibility = Visibility.Collapsed;
            combotelper.Visibility = Visibility.Visible;
            combotelper.IsDropDownOpen = true;
        }

        private void Listtelefonoper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listtelefonoper.SelectedItem == itemperadd)
            {
                if (!string.IsNullOrEmpty(combotelper.Text) && !string.IsNullOrWhiteSpace(combotelper.Text))
                {
                    if (long.TryParse(combotelper.Text, out long r))
                    {
                        combotelper.Items.Add(combotelper.Text);
                        buttondialogtelper.Content = "Añadido!!!";
                        buttondialogtelper.Visibility = Visibility.Visible;
                        combotelper.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        buttondialogtelper.Content = "Solo Numeros!!!";
                        buttondialogtelper.Visibility = Visibility.Visible;
                        combotelper.Visibility = Visibility.Collapsed;
                    }
                }
            }
            if (listtelefonoper.SelectedItem == itemperquit)
            {
                combotelper.Items.Remove(combotelper.SelectedItem);
                combotelper.Text = null;
                combotelper.IsDropDownOpen = true;
            }
        }

        private void Listcelularper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcelularper.SelectedItem == itemcelperadd)
            {
                if (!string.IsNullOrEmpty(combocelper.Text) && !string.IsNullOrWhiteSpace(combocelper.Text))
                {
                    if (long.TryParse(combocelper.Text, out long r))
                    {
                        combocelper.Items.Add(combocelper.Text);
                        buttondialogcelper.Content = "Añadido!!!";
                        buttondialogcelper.Visibility = Visibility.Visible;
                        combocelper.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        buttondialogcelper.Content = "Solo Numeros!!!";
                        buttondialogcelper.Visibility = Visibility.Visible;
                        combocelper.Visibility = Visibility.Collapsed;
                    }
                }
            }
            if (listcelularper.SelectedItem == itemcelperquit)
            {
                combocelper.Items.Remove(combocelper.SelectedItem);
                combocelper.Text = null;
                combocelper.IsDropDownOpen = true;
            }
        }
    }
}
