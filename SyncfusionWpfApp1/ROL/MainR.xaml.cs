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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncfusionWpfApp1.ROL
{
    /// <summary>
    /// Lógica de interacción para MainR.xaml
    /// </summary>
    public partial class MainR : Page
    {
        List<CheckBox> lcb = new List<CheckBox>();
        List<CheckBox> lcb1 = new List<CheckBox>();

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
                    checkBox.IsEnabled = true;
                    lcb.Add(checkBox);
                    wrapprivilegios.Children.Add(checkBox);
                }
            }
        }

        public MainR()
        {
            InitializeComponent();
            var table = NEGOCIO.NRol.Mostrar();
            DataSet dataSet = new DataSet("ListBox");
            dataSet.Tables.Add(table);
            listrol.DataContext = dataSet;

            llenarprivilegios();
        }

        private void Listrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
