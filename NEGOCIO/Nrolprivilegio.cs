using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DATOS;

namespace NEGOCIO
{
    public class NRolPrivilegio
    {
        public static string insertar(DataTable dtrolpri)
        {
            DRolPrivilegio drp = new DRolPrivilegio();
            List<DRolPrivilegio> dRolprivilegios = new List<DRolPrivilegio>();
            foreach (DataRow fila in dtrolpri.Rows)
            {
                DRolPrivilegio dRolprivilegio = new DRolPrivilegio();
                dRolprivilegio.Id_rol = Convert.ToInt32(fila["rol"]);
                dRolprivilegio.Id_privilegio = Convert.ToInt32(fila["privilegio"]);
                dRolprivilegio.Estado = Convert.ToBoolean(fila["estado"]);
                dRolprivilegios.Add(dRolprivilegio);
            }
            return drp.Insertar(dRolprivilegios);
        }

        public static DataTable Mostrar()
        {
            return new DRolPrivilegio().Mostrar();
        }
    }
}
