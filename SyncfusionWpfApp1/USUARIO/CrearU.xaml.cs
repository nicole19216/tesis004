using Microsoft.Win32;
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

        ROL.CrearR CrearR = new ROL.CrearR();

        //string direccion = "";
        byte[] imag;
        //BitmapDecoder bitCoder;

        string strname, imgname;
        string valimg = null;
        


        private void validarcampos()
        {
            bool val; Int64 b;

            if (string.IsNullOrWhiteSpace(textnombre.Text))
                textnombre.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textciudad.Text))
                textciudad.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textemail.Text))
                textemail.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textdireccion.Text))
                textdireccion.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textci.Text))
                textci.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textusuario.Text))
                textci.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textcontraseña.Password))
                textci.BorderBrush = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(textcontraseñaconfirmar.Password))
                textci.BorderBrush = new SolidColorBrush(Colors.Red);
            foreach (var item in combotelefono.Items)
            {
                val = Int64.TryParse(item.ToString(), out b);
                if (val == false)
                {
                    combotelefono.Background = new SolidColorBrush(Colors.Red);
                    break;
                }
            }
            foreach (var item in combocelular.Items)
            {
                val = Int64.TryParse(item.ToString(), out b);
                if (val == false)
                {
                    combocelular.BorderBrush = new SolidColorBrush(Colors.Red);
                    break;
                }
            }
            if (comborol.Items.Count == 0)
            {
                comborol.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private bool preparar_save()
        {
            bool valor = true; bool valor1 = true; bool valor2 = true; bool valor3 = true; bool valor4 = true; bool valor5 = true;
            bool val;
            Int64 b;

            if (string.IsNullOrWhiteSpace(textnombre.Text) || string.IsNullOrWhiteSpace(textci.Text) ||
                string.IsNullOrWhiteSpace(textciudad.Text) || string.IsNullOrWhiteSpace(textemail.Text) ||
                    string.IsNullOrWhiteSpace(textdireccion.Text) || string.IsNullOrWhiteSpace(textusuario.Text) ||
                    string.IsNullOrWhiteSpace(textcontraseña.Password) || string.IsNullOrWhiteSpace(textcontraseñaconfirmar.Password) ||
                    textcontraseña.Password != textcontraseñaconfirmar.Password)
                valor = false;

            if (valimg != null)
            {
                valor5 = false;
            }

            if (combotelefono.Items.Count == 0 && combocelular.Items.Count == 0)
            {
                MessageBox.Show("Se requiere al menos un numero telefonico o de celular", "Telefono/Celular",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                valor3 = false;
            }

            if (comborol.Items.Count == 0)
            {
                MessageBox.Show("Se requiere al menos un rol designado", "Telefono/Celular",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
                valor4 = false;
            }

            foreach (var item in combotelefono.Items)
            {
                val = Int64.TryParse(item.ToString(), out b);
                if (val == false)
                {
                    valor1 = false;
                    break;
                }
            }

            foreach (var item in combocelular.Items)
            {
                val = Int64.TryParse(item.ToString(), out b);
                if (val == false)
                {
                    valor2 = false;
                    break;
                }
            }

            if (valor == false || valor1 == false || valor2 == false || valor3 == false || valor4 == false || valor5 == false)
                val = false;
            else
                val = true;

            return val;
        }

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
            dtrolusuario.Columns.Add("nombre", Type.GetType("System.String"));
            dtrolusuario.Columns.Add("estado", Type.GetType("System.Boolean"));
        }

        private void preparar_direccion()
        {
            DataRow dataRow = dtdireccion.NewRow();
            dataRow["pais"] = "USUARIO";
            dataRow["departamento"] = "USUARIO";
            dataRow["provincia"] = "USUARIO";
            dataRow["municipio"] = textciudad.Text.ToString();
            dataRow["direccion"] = textdireccion.Text.ToString();
            dtdireccion.Rows.Add(dataRow);
        }

        private void preparar_telefono_celular()
        {
            int a;

            if (combotelefono.Items.Count > 0 || combocelular.Items.Count > 0)
            {
                if (combotelefono.Items.Count >= combocelular.Items.Count)
                    a = combotelefono.Items.Count;
                else
                    a = combocelular.Items.Count;

                for (int i = 0; i < a; i++)
                {
                    DataRow row = dtnumero.NewRow();

                    if (combotelefono.Items.Count > i)
                        row["telefono"] = Convert.ToInt64(combotelefono.Items[i]);
                    else
                        row["telefono"] = 01;

                    if (combocelular.Items.Count > i)
                        row["celular"] = Convert.ToInt64(combocelular.Items[i]);
                    else
                        row["celular"] = 01;

                    dtnumero.Rows.Add(row);
                }
            }
        }

        private void preparar_imagen()
        {
            try
            {
                if (imgname != null)
                {
                    FileStream fs = new FileStream(imgname, FileMode.Open, FileAccess.Read);
                    imag = new byte[fs.Length];
                    fs.Read(imag, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public CrearU()
        {
            InitializeComponent();
            var table = NEGOCIO.NUsuario.Mostrar();
            DataSet dataSet = new DataSet("ListBox");
            dataSet.Tables.Add(table);
            this.DataContext = dataSet.Tables[0];
            Crear_tabla();
        }

        #region Botones
        private void CrearR_AgregarEventHandler(object sender, RoutedEventArgs e)
        {
            bool agregar = true;
            foreach (DataRow item in dtrolusuario.Rows)
            {
                if (item["nombre"].Equals(CrearR.comborol.Text))
                {
                    MessageBox.Show("Rol ya agregado");
                    agregar = false;
                }
            }
            if (agregar)
            {
                bool valor = false;
                DataRow row = dtrolusuario.NewRow();
                row["id"] = Convert.ToInt32(CrearR.comborol.SelectedValue);
                row["nombre"] = CrearR.comborol.Text;
                if (CrearR.checrol.IsChecked == true)
                    valor = true;
                else
                    valor = false;
                row["estado"] = valor;
                dtrolusuario.Rows.Add(row);
                comborol.ItemsSource = dtrolusuario.DefaultView;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listimagen.SelectedItem==item1)
            {
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image File (*.jpg)|*.jpg";
                    fileDialog.ShowDialog();
                    {
                        strname = fileDialog.SafeFileName;
                        imgname = fileDialog.FileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        img.SetValue(Image.SourceProperty, isc.ConvertFromString(imgname));
                    }
                    fileDialog = null;
                }
                catch (Exception ex)
                {

                    valimg = ex.Message.ToString();
                }
            }
            if (listimagen.SelectedItem==item2)
            {
                if (imgname!=null)
                {
                    img.Source = null;
                }
            }
        }

        private void Listtelefono_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listtelefono.SelectedItem==itemtel1)
            {
                combotelefono.Items.Add(combotelefono.Text);
                combotelefono.Text = null;
                combotelefono.IsDropDownOpen = true;
            }
            if (listtelefono.SelectedItem==itemtel2)
            {
                combotelefono.Items.Remove(combotelefono.SelectedItem);
                combotelefono.Text = null;
                combotelefono.IsDropDownOpen = true;
            }
        }

        private void Listcelular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcelular.SelectedItem == itemcel1)
            {
                combocelular.Items.Add(combocelular.Text);
                combocelular.Text = null;
                combocelular.IsDropDownOpen = true;
            }
            if (listcelular.SelectedItem == itemcel2)
            {
                combocelular.Items.Remove(combocelular.SelectedItem);
                combocelular.Text = null;
                combocelular.IsDropDownOpen = true;
            }
        }

        private void Checkestado_Click(object sender, RoutedEventArgs e)
        {
            if (checkestado.IsChecked == true)
            {
                checkestado.Content = "Habilitado";
            }
            if (checkestado.IsChecked == false)
            {
                checkestado.Content = "Deshabilitado";
            }
        }

        private void Listrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listrol.SelectedItem==itemrol1)
            {
                try
                {
                    //Si la ventana hija -Agregar- existe, le añadimos un evento y mostramos la Ventana
                    //El evento disparará cuando el usuario presione el Boton_Agregar de la Ventana Hija
                    //a continuación se ejecutará el método ag_AgregarEventHandler que actualizara el Grid
                    CrearR.AgregarEventHandler += new RoutedEventHandler(CrearR_AgregarEventHandler);
                    CrearR.ShowDialog();
                }
                catch
                {
                    //Si la ventana hija -Agregar- no existe, la volvemos a referenciar y le añadimos un 
                    //evento y mostramos la Ventana. El evento disparará cuando el usuario presione el 
                    //Boton_Agregar de la Ventana Hija, a continuación se ejecutará el método 
                    //ag_AgregarEventHandler que actualizara el Grid
                    CrearR = new ROL.CrearR();
                    CrearR.AgregarEventHandler += new RoutedEventHandler(CrearR_AgregarEventHandler);
                    CrearR.ShowDialog();
                }
            }

            if (listrol.SelectedItem == itemrol2)
            {
                foreach (DataRow item in dtrolusuario.Rows)
                {
                    if ((int)item[0] == Convert.ToInt32(comborol.SelectedValue))
                    {
                        dtrolusuario.Rows.Remove(item);
                        break;
                    }
                }
                comborol.ItemsSource = dtrolusuario.DefaultView;
            }
        }

        private void Buttonguardar1_Click(object sender, RoutedEventArgs e)
        {
            var i= img;
            bool b;

            if (checkestado.IsChecked == true)
                b = true;
            else
                b = false;

            if (preparar_save() == false)
                validarcampos();
            else
            {
                preparar_telefono_celular();
                preparar_direccion();
                preparar_imagen();

                string res = NEGOCIO.NUsuario.Insertar(textci.Text, textnombre.Text, textemail.Text, datefecha.DateTime.Value, "01", dtnumero, dtdireccion,
                textusuario.Text, textcontraseña.Password, b, dtrolusuario, imag);

                if (res.Equals("OK"))
                {
                    MessageBox.Show("guardado exitosamente");
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }
        #endregion
    }
}
