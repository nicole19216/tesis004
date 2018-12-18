using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DPersona
    {
        private long id_persona;
        private string ci;
        private string nombre;
        private string email;
        private DateTime nacimiento;
        private long nit;
        private byte[] imagen;

        public DPersona()
        {
        }

        public DPersona(long id_persona, string ci, string nombre, string email, DateTime nacimiento, long nit, byte[] imagen)
        {
            Id_persona = id_persona;
            Ci = ci;
            Nombre = nombre;
            Email = email;
            Nacimiento = nacimiento;
            Nit = nit;
            Imagen = imagen;
        }

        public long Id_persona { get => id_persona; set => id_persona = value; }
        public string Ci { get => ci; set => ci = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Nacimiento { get => nacimiento; set => nacimiento = value; }
        public long Nit { get => nit; set => nit = value; }
        public byte[] Imagen { get => imagen; set => imagen = value; }

        public string Insertar(DPersona Persona, List<DNmero> Numtel, List<DDireccion> Direccion
            , ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            int flag = 0;
            string rpta = "";
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = SqlCon;
                sqlCommand.Transaction = SqlTra;
                sqlCommand.CommandText = "pInsertarPersona";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@idp";
                pid.SqlDbType = SqlDbType.BigInt;
                pid.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(pid);

                SqlParameter pci = new SqlParameter();
                pci.ParameterName = "@ci";
                pci.SqlDbType = SqlDbType.VarChar;
                pci.Size = 15;
                pci.Value = Persona.Ci;
                sqlCommand.Parameters.Add(pci);

                SqlParameter pnombre = new SqlParameter();
                pnombre.ParameterName = "@nombre";
                pnombre.SqlDbType = SqlDbType.VarChar;
                pnombre.Size = 50;
                pnombre.Value = Persona.Nombre;
                sqlCommand.Parameters.Add(pnombre);

                SqlParameter pemail = new SqlParameter();
                pemail.ParameterName = "@email";
                pemail.SqlDbType = SqlDbType.VarChar;
                pemail.Size = 50;
                pemail.Value = Persona.Email;
                sqlCommand.Parameters.Add(pemail);

                SqlParameter pfecha = new SqlParameter();
                pfecha.ParameterName = "@fecha";
                pfecha.SqlDbType = SqlDbType.Date;
                pfecha.Value = Persona.Nacimiento;
                sqlCommand.Parameters.Add(pfecha);

                SqlParameter pnit = new SqlParameter();
                pnit.ParameterName = "@nit";
                pnit.SqlDbType = SqlDbType.BigInt;
                pnit.Value = Persona.Nit;
                sqlCommand.Parameters.Add(pnit);

                SqlParameter pimagen = new SqlParameter();
                pimagen.ParameterName = "@imagen";
                pimagen.SqlDbType = SqlDbType.Image;
                pimagen.Value = Persona.Imagen;
                sqlCommand.Parameters.Add(pimagen);

                //Ejecutamos nuestro comando

                rpta = sqlCommand.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado
                    this.Id_persona = Convert.ToInt64(sqlCommand.Parameters["@id_persona"].Value);

                    foreach (DNmero nt in Numtel)
                    {
                        nt.Idpersona = this.Id_persona;
                        rpta = nt.Insertar(nt, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        foreach (DDireccion dir in Direccion)
                        {
                            dir.Id_persona = this.Id_persona;
                            rpta = dir.Insertar(dir, ref SqlCon, ref SqlTra);
                            if (!rpta.Equals("OK"))
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;

        }
    }
}
