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
using Microsoft.Win32;
using NEGOCIO;

namespace SyncfusionWpfApp1.PRODUCTO
{
    /// <summary>
    /// Lógica de interacción para CrearP.xaml
    /// </summary>
    public partial class CrearP : Window
    {
        #region variables
        private List<byte[]> limagen=new List<byte[]>();
        private DataTable dtcosto, dtcopycosto;
        private DataTable dtcategoria;
        private DataTable dtventa, dtcopyventa;
        CrearCategoriaP crearCategoria = new CrearCategoriaP();

        byte[] imag;
        string strname, imgname;
        string valimg = null;
        #endregion

        private int validar_costo()
        {
            int contador = 0;

            if (dtcosto.Rows.Count<=0)
            {
                contador++;
            }

            return contador;
        }
        private int validar_venta()
        {
            int contador = 0;

            if (dtventa.Rows.Count <= 0)
            {
                contador++;
            }

            return contador;
        }
        private int validar_campos()
        {
            int contador = 0;

            if (string.IsNullOrEmpty(textbarras.Text) || string.IsNullOrWhiteSpace(textbarras.Text))
            {
                textbarras.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textbarras.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textrapido.Text) || string.IsNullOrWhiteSpace(textrapido.Text))
            {
                textrapido.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textrapido.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textnombre.Text) || string.IsNullOrWhiteSpace(textnombre.Text))
            {
                textnombre.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textnombre.BorderBrush = new SolidColorBrush(Colors.Black);

            if (combocategoriap.Items.Count <= 0)
            {
                combocategoriap.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                combocategoriap.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(combomarca.Text))
            {
                combomarca.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                combomarca.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(comboimpuesto.Text))
            {
                comboimpuesto.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                comboimpuesto.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(comboubicacion.Text))
            {
                comboubicacion.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                comboubicacion.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textstockminimo.Text) || string.IsNullOrWhiteSpace(textstockminimo.Text))
            {
                textstockminimo.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textstockminimo.BorderBrush = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrEmpty(textstockmaximo.Text) || string.IsNullOrWhiteSpace(textstockmaximo.Text))
            {
                textstockmaximo.BorderBrush = new SolidColorBrush(Colors.Red);
                contador++;
            }
            else
                textstockmaximo.BorderBrush = new SolidColorBrush(Colors.Black);

            return contador;
        }
        private void Crear_tabla()
        {
            dtcategoria = new DataTable("Dtcategoria");
            dtcategoria.Columns.Add("idc", Type.GetType("System.Int32"));
            dtcategoria.Columns.Add("nombre", Type.GetType("System.String"));

            dtcosto = new DataTable("Dtcosto");
            dtcosto.Columns.Add("unidad_compra", Type.GetType("System.String"));
            dtcosto.Columns.Add("cantidad_piezas", Type.GetType("System.Int32"));
            dtcosto.Columns.Add("precio", Type.GetType("System.Decimal"));
            dtcosto.Columns.Add("costo_pieza",Type.GetType("System.Decimal"));

            dtventa = new DataTable("Dtventa");
            dtventa.Columns.Add("unidad_venta", Type.GetType("System.String"));
            dtventa.Columns.Add("cantidad_pieza", Type.GetType("System.Int32"));
            dtventa.Columns.Add("utilidad_pieza", Type.GetType("System.Decimal"));
            dtventa.Columns.Add("precio_pieza", Type.GetType("System.Decimal"));
            dtventa.Columns.Add("utilidad_venta", Type.GetType("System.Decimal"));
            dtventa.Columns.Add("precio_venta", Type.GetType("System.Decimal"));
        }
        private void tabs()
        {
            if (TabDatos.IsChecked == true)
            {
                borderimagen.Visibility = Visibility.Collapsed;
                borderventa.Visibility = Visibility.Collapsed;
                bordercosto.Visibility = Visibility.Collapsed;
                text_inicio.Visibility = Visibility.Collapsed;

                borderdatos.Visibility = Visibility.Visible;
            }
            if (TabImagenes.IsChecked == true)
            {
                borderdatos.Visibility = Visibility.Collapsed;
                borderventa.Visibility = Visibility.Collapsed;
                bordercosto.Visibility = Visibility.Collapsed;
                text_inicio.Visibility = Visibility.Collapsed;

                borderimagen.Visibility = Visibility.Visible;
            }
            if (TabVenta.IsChecked == true)
            {
                borderimagen.Visibility = Visibility.Collapsed;
                bordercosto.Visibility = Visibility.Collapsed;
                borderdatos.Visibility = Visibility.Collapsed;
                text_inicio.Visibility = Visibility.Collapsed;

                borderventa.Visibility = Visibility.Visible;
            }
            if (TabCosto.IsChecked == true)
            {
                borderdatos.Visibility = Visibility.Collapsed;
                borderimagen.Visibility = Visibility.Collapsed;
                borderventa.Visibility = Visibility.Collapsed;
                text_inicio.Visibility = Visibility.Collapsed;

                bordercosto.Visibility = Visibility.Visible;
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

                    limagen.Add(imag);
                    Image image = new Image();
                    image.Width = 100;image.Height = 100;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    image.SetValue(Image.SourceProperty, isc.ConvertFromString(imgname));
                    wrapimagen.Children.Add(image);

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public CrearP()
        {
            InitializeComponent();
            Crear_tabla();
            comboubicacion.ItemsSource = NUbicacion.Mostrar().DefaultView;
            combomarca.ItemsSource = NMarca.Mostrar().DefaultView;
            comboimpuesto.ItemsSource = NImpuesto.Mostrar().DefaultView;

            //var table = dtcosto;
            //DataSet dataSet = new DataSet();
            //dataSet.Tables.Add(table);
            //listcost.DataContext = dataSet;
            textcostopiezacompra.GotFocus += Textcostopieza_GotFocus;
            textpreciopieza.GotFocus += Textpreciopieza_GotFocus;
        }

        private void Textpreciopieza_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textcantidadpiezaventa.Text) && !string.IsNullOrWhiteSpace(textcantidadpiezaventa.Text)
               && !string.IsNullOrEmpty(textprecioventatotal.Text) && !string.IsNullOrWhiteSpace(textprecioventatotal.Text)
               && decimal.TryParse(textprecioventatotal.Text, out decimal d) && int.TryParse(textcantidadpiezaventa.Text, out int i))
            {
                decimal preciopieza = Convert.ToDecimal(textprecioventatotal.Text) / Convert.ToInt32(textcantidadpiezaventa.Text);
                textpreciopieza.Text = decimal.Round(preciopieza, 2).ToString();
            }
            else
            {
                textpreciopieza.Text = "Datos incorrectos para el calculo";
                textpreciopieza.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void Textcostopieza_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textcostocompra.Text) && !string.IsNullOrWhiteSpace(textcostocompra.Text) &&
                    !string.IsNullOrEmpty(textcantidadpiezacompra.Text) && !string.IsNullOrWhiteSpace(textcantidadpiezacompra.Text) &&
                    decimal.TryParse(textcostocompra.Text, out decimal d) && int.TryParse(textcantidadpiezacompra.Text, out int i))
            {
                decimal pre = Convert.ToDecimal(textcostocompra.Text) / Convert.ToInt32(textcantidadpiezacompra.Text);
                textcostopiezacompra.Text =decimal.Round(pre,2).ToString();
            }
            else
            {
                textcostopiezacompra.Text = "Datos incorrectos para el calculo";
                textcostopiezacompra.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void TabDatos_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }
        private void TabImagenes_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }
        private void TabVenta_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }
        private void TabCosto_Checked(object sender, RoutedEventArgs e)
        {
            tabs();
        }

        private void CrearC_AgregarEventHandler(object sender, RoutedEventArgs e)
        {
            bool agregar = true;
            foreach (DataRow item in dtcategoria.Rows)
            {
                if (item["nombre"].Equals(crearCategoria.combocategoria.Text))
                {
                    MessageBox.Show("Categoria ya agregado");
                    agregar = false;
                }
            }
            if (agregar)
            {
                DataRow row = dtcategoria.NewRow();
                row["idc"] = Convert.ToInt32(crearCategoria.combocategoria.SelectedValue);
                row["nombre"] = crearCategoria.combocategoria.Text;
                dtcategoria.Rows.Add(row);
                combocategoriap.ItemsSource = dtcategoria.DefaultView;
            }
        }
        private void Listcategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcategoria.SelectedItem == itemcatadd)
            {
                try
                {
                    //Si la ventana hija -Agregar- existe, le añadimos un evento y mostramos la Ventana
                    //El evento disparará cuando el usuario presione el Boton_Agregar de la Ventana Hija
                    //a continuación se ejecutará el método ag_AgregarEventHandler que actualizara el Grid
                    crearCategoria.AgregarEventHandler += new RoutedEventHandler(CrearC_AgregarEventHandler);
                    crearCategoria.ShowDialog();
                }
                catch
                {
                    //Si la ventana hija -Agregar- no existe, la volvemos a referenciar y le añadimos un 
                    //evento y mostramos la Ventana. El evento disparará cuando el usuario presione el 
                    //Boton_Agregar de la Ventana Hija, a continuación se ejecutará el método 
                    //ag_AgregarEventHandler que actualizara el Grid
                    crearCategoria = new CrearCategoriaP();
                    crearCategoria.AgregarEventHandler += new RoutedEventHandler(CrearC_AgregarEventHandler);
                    crearCategoria.ShowDialog();
                }
            }

            if (listcategoria.SelectedItem == itemcatquit)
            {
                foreach (DataRow item in dtcategoria.Rows)
                {
                    if ((int)item[0] == Convert.ToInt32(combocategoriap.SelectedValue))
                    {
                        dtcategoria.Rows.Remove(item);
                        break;
                    }
                }
                combocategoriap.ItemsSource = dtcategoria.DefaultView;
            }
        }

        private void Listdireccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listdireccion.SelectedItem==itemdiradd)
            {
                CrearUbicacionP crearUbicacionP = new CrearUbicacionP();
                crearUbicacionP.ShowDialog();
                comboubicacion.ItemsSource = null;
                comboubicacion.ItemsSource = NUbicacion.Mostrar().DefaultView;
            }
        }
        private void Listmarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listmarca.SelectedItem==itemmaradd)
            {
                CrearMarcaP crearMarcaP = new CrearMarcaP();
                crearMarcaP.ShowDialog();
                combomarca.ItemsSource = null;
                combomarca.ItemsSource = NMarca.Mostrar().DefaultView;
            }
        }
        private void Listimpuesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listimpuesto.SelectedItem==itemimpadd)
            {
                CrearImpuestoP crearImpuestoP = new CrearImpuestoP();
                crearImpuestoP.ShowDialog();
                comboimpuesto.ItemsSource = null;
                comboimpuesto.ItemsSource = NImpuesto.Mostrar().DefaultView;
            }
        }


        private void Listcosto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (listcosto.SelectedItem==itemcostadd)
            //{
            //    if (!string.IsNullOrEmpty(textcostocompra.Text)&&!string.IsNullOrWhiteSpace(textcostocompra.Text)&&
            //        !string.IsNullOrEmpty(textcantidadpieza.Text)&&!string.IsNullOrWhiteSpace(textcantidadpieza.Text)&&
            //        !string.IsNullOrEmpty(combounidadcompra.Text)&&!string.IsNullOrWhiteSpace(combounidadcompra.Text)&&
            //        decimal.TryParse(textcostocompra.Text,out decimal d)&&int.TryParse(textcantidadpieza.Text,out int i)&&
            //        decimal.TryParse(textcostopieza.Text,out decimal dd)&&!string.IsNullOrEmpty(textcostopieza.Text) &&
            //        !string.IsNullOrWhiteSpace(textcostopieza.Text))
            //    {
            //        combounidadcompra.Items.Add(combounidadcompra.Text);
            //        DataRow row = dtcosto.NewRow();
            //        row["unidad_compra"] = combounidadcompra.Text;
            //        row["precio"] = Convert.ToDecimal(textcostocompra.Text);
            //        row["cantidad_piezas"] = Convert.ToInt32(textcantidadpieza.Text);
            //        row["costo_pieza"] = Convert.ToDecimal(textcostopieza.Text);
            //        dtcosto.Rows.Add(row);

            //        combounidadcompra.Text = null;
            //        textcostocompra.Text = null;
            //        textcantidadpieza.Text = null;
            //        textcostopieza.Text = null;
            //    }
            //    else
            //    {
            //        gridshowcost.Visibility = Visibility.Visible;
            //        buttonshowcost.Visibility = Visibility.Visible;
            //    }
            //}
            //if (listcosto.SelectedItem==itemcostquit)
            //{
            //    if (!string.IsNullOrEmpty(combounidadcompra.Text)&&!string.IsNullOrWhiteSpace(combounidadcompra.Text))
            //    {
            //        foreach (DataRow dr in dtcosto.Rows)
            //        {
            //            if (dr["unidad_compra"].ToString() == combounidadcompra.Text)
            //            {
            //                dr.Delete();
            //                break;
            //            }
            //        }

            //        combounidadcompra.Items.Remove(combounidadcompra.Text);
            //        combounidadcompra.Text = null;
            //        textcostocompra.Text = null;
            //        textcostopieza.Text = null;
            //        textcantidadpieza.Text = null;
            //    }
            //}
            //if (listcosto.SelectedItem == itemcostshow)
            //{
            //    if (listcost.Visibility==Visibility.Collapsed)
            //    {
            //        combounidadcompra.Visibility = Visibility.Collapsed;
            //        combounidadcompra.Visibility = Visibility.Collapsed;
            //        textcostocompra.Visibility = Visibility.Collapsed;
            //        textcostopieza.Visibility = Visibility.Collapsed;
            //        textcantidadpieza.Visibility = Visibility.Collapsed;

            //        labelcantidadpiezas.Visibility = Visibility.Collapsed;
            //        labelcostocompra.Visibility = Visibility.Collapsed;
            //        labelcostopieza.Visibility = Visibility.Collapsed;
            //        labelunidadcompra.Visibility = Visibility.Collapsed;

            //        listcost.Visibility = Visibility.Visible;                    
            //    }
            //    else
            //    {
                    
            //        combounidadcompra.Visibility = Visibility.Visible;
            //        combounidadcompra.Visibility = Visibility.Visible;
            //        textcostocompra.Visibility = Visibility.Visible;
            //        textcostopieza.Visibility = Visibility.Visible;
            //        textcantidadpieza.Visibility = Visibility.Visible;

            //        labelcantidadpiezas.Visibility = Visibility.Visible;
            //        labelcostocompra.Visibility = Visibility.Visible;
            //        labelcostopieza.Visibility = Visibility.Visible;
            //        labelunidadcompra.Visibility = Visibility.Visible;

            //        listcost.Visibility = Visibility.Collapsed;
            //    }
            //}
        }

        private void Buttonguardar_Click(object sender, RoutedEventArgs e)
        {
            bool valor=true;

            if (toggleper.IsChecked == true)
                valor = true;
            else
                valor = false;

            if (togglecos.IsChecked==false && toggleven.IsChecked==false)
            {
                if (validar_campos() <= 0)
                {
                    if (limagen.Count <= 0)
                    {
                        imagen.Source = new BitmapImage(new Uri(@"/RECURSOS/IMAGENES/1A2.jpg", UriKind.RelativeOrAbsolute));
                        byte[] a = getJPGFromImageControl(imagen.Source as BitmapImage);

                        limagen.Add(a);

                        string resp = NProducto.Insertar(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                    else
                    {
                        string resp = NProducto.Insertar(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                }
            }

            if (togglecos.IsChecked==true && toggleven.IsChecked==false)
            {
                if (validar_campos() <= 0 && validar_costo() <=0)
                {
                    if (limagen.Count <= 0)
                    {
                        imagen.Source = new BitmapImage(new Uri(@"/RECURSOS/IMAGENES/1A2.jpg", UriKind.RelativeOrAbsolute));
                        byte[] a = getJPGFromImageControl(imagen.Source as BitmapImage);

                        limagen.Add(a);

                        string resp = NProducto.InsertarC(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, dtcosto, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                    else
                    {
                        string resp = NProducto.InsertarC(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, dtcosto, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                }
            }

            if (togglecos.IsChecked == false && toggleven.IsChecked == true)
            {
                if (validar_campos() <= 0 && validar_venta() <= 0)
                {
                    if (limagen.Count <= 0)
                    {
                        imagen.Source = new BitmapImage(new Uri(@"/RECURSOS/IMAGENES/1A2.jpg", UriKind.RelativeOrAbsolute));
                        byte[] a = getJPGFromImageControl(imagen.Source as BitmapImage);

                        limagen.Add(a);

                        string resp = NProducto.InsertarV(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, dtventa, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                    else
                    {
                        string resp = NProducto.InsertarV(Convert.ToInt32(combomarca.SelectedValue), Convert.ToInt32(comboubicacion.SelectedValue),
                        Convert.ToInt32(comboimpuesto.SelectedValue), textbarras.Text, textrapido.Text, textnombre.Text, Convert.ToInt64(textstockminimo.Text),
                        Convert.ToInt64(textstockmaximo.Text), valor, dtcategoria, dtventa, limagen);

                        if (resp.Equals("OK"))
                            MessageBox.Show("ENTIDAD/CLIENTE REGISTRADA CORRECTAMENTE");
                        else
                            MessageBox.Show("error");
                    }
                }
            }
        }

        private void Buttonquitarimagen_Click(object sender, RoutedEventArgs e)
        {
            if (limagen.Count!=0)
            {
                limagen.RemoveAt(limagen.Count - 1);
                wrapimagen.Children.RemoveAt(wrapimagen.Children.Count - 1);
            }
        }

        private void Buttonshowcost_Click(object sender, RoutedEventArgs e)
        {
            //gridshowcost.Visibility = Visibility.Collapsed;
            //buttonshowcost.Visibility = Visibility.Collapsed;
        }

        private void Togglecos_Checked(object sender, RoutedEventArgs e)
        {
                textunidadcompra.IsEnabled = true;
                textcantidadpiezacompra.IsEnabled = true;
                textcostocompra.IsEnabled = true;
               textcostopiezacompra.IsEnabled = true;
        }
        private void Togglecos_Unchecked(object sender, RoutedEventArgs e)
        {
            textunidadcompra.IsEnabled = false;
            textcantidadpiezacompra.IsEnabled = false;
            textcostocompra.IsEnabled = false;
            textcostopiezacompra.IsEnabled = false;
        }
        private void Toggleven_Checked(object sender, RoutedEventArgs e)
        {
            textunidadventa.IsEnabled = true;
            textcantidadpiezaventa.IsEnabled = true;
            textpreciopieza.IsEnabled = true;
            textprecioventatotal.IsEnabled = true;
        }
        private void Toggleven_Unchecked(object sender, RoutedEventArgs e)
        {
            textunidadventa.IsEnabled = false;
            textcantidadpiezaventa.IsEnabled = false;
            textpreciopieza.IsEnabled = false;
            textprecioventatotal.IsEnabled = false;
        }

        private void Button_añadir_venta_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textunidadventa.Text) && !string.IsNullOrWhiteSpace(textunidadventa.Text) 
               && !string.IsNullOrEmpty(textcantidadpiezaventa.Text) && !string.IsNullOrWhiteSpace(textcantidadpiezaventa.Text) 
               && !string.IsNullOrEmpty(textprecioventatotal.Text) && !string.IsNullOrWhiteSpace(textprecioventatotal.Text) 
               && decimal.TryParse(textprecioventatotal.Text, out decimal d) && int.TryParse(textcantidadpiezaventa.Text, out int i))
            {
                DataRow row = dtventa.NewRow();
                row["unidad_venta"] = textunidadventa.Text;
                row["cantidad_pieza"] = Convert.ToInt32(textcantidadpiezaventa.Text);
                row["precio_pieza"] = Convert.ToDecimal(textpreciopieza.Text);
                row["precio_venta"] = Convert.ToDecimal(textprecioventatotal.Text);
                dtventa.Rows.Add(row);

                dtgventa.DataContext = dtventa;
                dtcopyventa = dtventa.Copy();
            }
            else
            {
                MessageBox.Show("Llene los campos correctamente");
            }
        }

        private void Button_quitar_venta_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView drv in dtgventa.SelectedItems)
            {
                for (int i = dtcopyventa.Rows.Count - 1; i >= 0; i--)
                {
                    if (drv["unidad_venta"].Equals(dtcopyventa.Rows[i]["unidad_venta"]))
                    {
                        dtcopyventa.Rows.RemoveAt(i);
                        break;
                    }
                }
            }

            dtventa = dtcopyventa.Copy();
            dtgventa.DataContext = dtventa;
            dtgventa.GridColumnSizer.ResetAutoCalculationforAllColumns();
            dtgventa.GridColumnSizer.Refresh();
        }

        private void Button_añadir_costo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textcostocompra.Text) && !string.IsNullOrWhiteSpace(textcostocompra.Text) &&
                    !string.IsNullOrEmpty(textunidadcompra.Text) && !string.IsNullOrWhiteSpace(textunidadcompra.Text) &&
                    !string.IsNullOrEmpty(textcantidadpiezacompra.Text) && !string.IsNullOrWhiteSpace(textcantidadpiezacompra.Text) &&
                    decimal.TryParse(textcostocompra.Text, out decimal d) && int.TryParse(textcantidadpiezacompra.Text, out int i))
            {
                DataRow row = dtcosto.NewRow();
                row["unidad_compra"] = textunidadcompra.Text;
                row["precio"] = Convert.ToDecimal(textcostocompra.Text);
                row["cantidad_piezas"] = Convert.ToInt32(textcantidadpiezacompra.Text);
                row["costo_pieza"] = Convert.ToDecimal(textcostopiezacompra.Text);
                dtcosto.Rows.Add(row);

                dtgcosto.DataContext = dtcosto;
                dtcopycosto = dtcosto.Copy();
            }
            else
            {
                MessageBox.Show("Llene los campos correctamente");
            }
        }

        private void Button_quitar_costo_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView drv in dtgcosto.SelectedItems)
            {
                for (int i = dtcopycosto.Rows.Count - 1; i >= 0; i--)
                {
                    if (drv["unidad_compra"].Equals(dtcopycosto.Rows[i]["unidad_compra"]))
                    {
                        dtcopycosto.Rows.RemoveAt(i);
                        break;
                    }
                }
            }

            dtcosto = dtcopycosto.Copy();
            dtgcosto.DataContext = dtcosto;
            dtgcosto.GridColumnSizer.ResetAutoCalculationforAllColumns();
            dtgcosto.GridColumnSizer.Refresh();
        }

        private void Listimagen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listimagen.SelectedItem == itemimageadd)
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
                        imagen.SetValue(Image.SourceProperty, isc.ConvertFromString(imgname));
                    }
                    fileDialog = null;

                    preparar_imagen();
                }
                catch (Exception ex)
                {
                    valimg = ex.Message.ToString();
                }
            }
            if (listimagen.SelectedItem == itemimagequit)
            {
                if (imgname != null)
                {
                    imagen.Source = null;
                }
            }
        }
    }
}
