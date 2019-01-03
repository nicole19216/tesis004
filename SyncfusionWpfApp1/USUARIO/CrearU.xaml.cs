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

        ROL.CrearR CrearR = new ROL.CrearR();

        

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

        private void preparar_telefono_celular()
        {
            int a; if (combotelefono.Items.Count >= combocelular.Items.Count)
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

        private void preparar_rol_usuario()
        {
            for (int i = 0; i < comborol.Items.Count; i++)
            {
                DataRow row = dtrolusuario.NewRow();
                row["id"] = Convert.ToInt32(comborol.SelectedValue);
                row["estado"] = true;
                dtrolusuario.Rows.Add(row);
            }
        }

        public CrearU()
        {
            InitializeComponent();
            Crear_tabla();
        }

        #region botones telefono/celular
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

        private void Buttonagregarcelular_Click(object sender, RoutedEventArgs e)
        {
            combocelular.Items.Add(combocelular.Text);
            combocelular.Text = null;
            combocelular.IsDropDownOpen = true;
        }

        private void Buttonquitarcelular_Click(object sender, RoutedEventArgs e)
        {
            combocelular.Items.Remove(combocelular.SelectedItem);
            combocelular.Text = null;
            combocelular.IsDropDownOpen = true;
        }
        #endregion

        private void Buttonguardar1_Click(object sender, RoutedEventArgs e)
        {
            preparar_telefono_celular();
        }

        private void Buttonguardar2_Click(object sender, RoutedEventArgs e)
        {
            preparar_telefono_celular();
        }

        private void Buttonagregarrol_Click(object sender, RoutedEventArgs e)
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


            //ROL.CrearR CrearR = new ROL.CrearR();
            //CrearR.ShowDialog();
        }

        private void CrearR_AgregarEventHandler(object sender, RoutedEventArgs e)
        {
            //ComboBoxItem item = new ComboBoxItem();
            //item.Content = CrearR.comborol.Text;
            //item.

            //comborol.Items.Add(CrearR.comborol.SelectedItem);
            //MessageBox.Show(CrearR.comborol.SelectedValue.ToString());


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

        private void Buttonquitarrol_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
