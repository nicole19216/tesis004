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

namespace SyncfusionWpfApp1.USUARIO
{
    /// <summary>
    /// Lógica de interacción para EditarU.xaml
    /// </summary>
    public partial class EditarU : Window
    {
        public EditarU(string nom)
        {
            InitializeComponent();
            nombre.Text = nom;
        }
    }
}
