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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace SyncfusionWpfApp1.CLIENTE
{
    /// <summary>
    /// Lógica de interacción para MainC.xaml
    /// </summary>
    public partial class MainC : Page
    {
        public MainC()
        {
            InitializeComponent();
            sfgrid.DataContext = NEGOCIO.NCliente.Mostrar();
        }
    }
}
