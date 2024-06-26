using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AppPrueba
{
    internal class Conexion
    {
        public static SqlConnection Conectar()  //Esta clase realiza la conexión con
        {                                       //SQL para poder utilizar los datos de la base de datos
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-3DM810J; " +   //Esta linea realiza la conexión con el servidor
                                                 "DATABASE=App_Prueba;" +       //Esta linea realiza la conexión con la base de datos
                                                 "integrated security=true;");  //Esta linea realiza la conexión con la base de datos
            cn.Open();                                                          //sin importar si la base de datos pide usuario y pw
            return cn;
        }
    }
}
