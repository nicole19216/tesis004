using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncfusionWpfApp1.COMPRA
{
    public interface Interface1
    {
        void pasaritem(int uno, string dos, string tres);
        void agregar_crear(string nombre, decimal precio, decimal precio_nuevo, int piezas, int cantidad);

        void pasarproveedor(int id, string nombre);
    }
}
