using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AppPrueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar(); //Se llama a la clase conecion() para ejecutarla al abrir la aplicacion
            //MessageBox.Show("Conexion Exitosa"); //Muestra un mensaje antes de abrir la app

            dataGridView1.DataSource = llenar_grid();   //Esta parte llama a la clase llenar_grid() para ver el contenido de la base
                                                        //de datos en el gridview
        }

        public DataTable llenar_grid() //Clase para mostrar el contenido en la DataGridView
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM TPeluches"; //Comandos de SQL para mostrar una tabla
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        private void btAgregar_Click(object sender, EventArgs e) //BOTON AGREGAR
        {
            Conexion.Conectar();

           string buscarc = "SELECT * FROM TPeluches WHERE Codigo = '" + txtcodigo.Text + "'"; //Lectura del texbox
           SqlCommand cmdbuscar = new SqlCommand(buscarc, Conexion.Conectar());
           SqlDataReader consulta = cmdbuscar.ExecuteReader();  //Este metodo crea un objeto y lo almacena en consulta

            string insertar = "INSERT INTO TPeluches (Codigo, Producto, Descripcion, Existencia, Precio) " +  //Se insertan comandos
                              "VALUES (@Codigo, @Producto, @Descripcion, @Existencia, @Precio)";              //en sql
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());    //Ejecuta el comando y realiza la conexion
            cmd1.Parameters.AddWithValue("@Codigo", txtcodigo.Text);            //El texto de los Texbox Se recibe y envía a SQL
            cmd1.Parameters.AddWithValue("@Producto", txtproducto.Text);        //El texto de los Texbox Se recibe y envía a SQL
            cmd1.Parameters.AddWithValue("@Descripcion", txtdescripcion.Text);  //El texto de los Texbox Se recibe y envía a SQL
            cmd1.Parameters.AddWithValue("@Existencia", txtexistencia.Text);    //El texto de los Texbox Se recibe y envía a SQL
            cmd1.Parameters.AddWithValue("@Precio", txtprecio.Text);            //El texto de los Texbox Se recibe y envía a SQL

            if (string.IsNullOrWhiteSpace(txtcodigo.Text))
            {
                MessageBox.Show("No hay datos para agregar");
            }
            else
            {
                if (consulta.Read())    //Si el select devuelve una fila devuelve verdadero
                {
                    MessageBox.Show("Los datos ya existen");
                }
                else //Si es falso
                {
                    cmd1.ExecuteNonQuery(); //Se ejecuta el comando en SQL
                }
               
            }
            //MessageBox.Show("Los datos fueron agregados con exito"); //Envia un mensaje de confirmacion de datos

            dataGridView1.DataSource = llenar_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtcodigo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtproducto.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtdescripcion.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtexistencia.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtprecio.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch { }
        }

        private void btModificar_Click(object sender, EventArgs e) //BOTON MODIFICAR
        {
            Conexion.Conectar();
            string actualizar = "UPDATE TPeluches SET Codigo=@Codigo, " +
                                "Producto=@Producto, " +
                                "Descripcion=@Descripcion, " +
                                "Existencia=@Existencia, " +
                                "Precio=@Precio " +
                                "WHERE Codigo=@Codigo";

            SqlCommand cmd2 = new SqlCommand(actualizar, Conexion.Conectar()); //Ejecuta el comando actualizar y lo guarda en un comando llamado cmd2

            cmd2.Parameters.AddWithValue("@Codigo", txtcodigo.Text);
            cmd2.Parameters.AddWithValue("@Producto", txtproducto.Text);
            cmd2.Parameters.AddWithValue("@Descripcion", txtdescripcion.Text);
            cmd2.Parameters.AddWithValue("@Existencia", txtexistencia.Text);
            cmd2.Parameters.AddWithValue("@Precio", txtprecio.Text);

            cmd2.ExecuteNonQuery();

            //MessageBox.Show("Los datos se han actualizado");
            dataGridView1.DataSource = llenar_grid(); //Actualiza los datos del gridview
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();

            string eliminar = "DELETE FROM TPeluches WHERE Codigo=@Codigo";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());
            cmd3.Parameters.AddWithValue("@Codigo", txtcodigo.Text);

            cmd3.ExecuteNonQuery();

            MessageBox.Show("Los datos fueron eliminados");
            dataGridView1.DataSource= llenar_grid();
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txtcodigo.Clear();
            txtproducto.Clear();
            txtdescripcion.Clear();
            txtexistencia.Clear();
            txtprecio.Clear();
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            if(txtcodigo.Text != "") //Validacion para cuando no hay nada en el texbox
            {
                Conexion.Conectar();

                string buscar = "SELECT * FROM TPeluches WHERE Codigo = '" + txtcodigo.Text + "'"; //Concatenacion para buscar en el texbox
                SqlCommand cmd4 = new SqlCommand(buscar, Conexion.Conectar());
                SqlDataAdapter dato = new SqlDataAdapter(cmd4);

                DataTable tabla = new DataTable();
                dato.Fill(tabla);
                dataGridView1.DataSource = tabla; //Escribe en la DataGriedView
            }
            if (txtproducto.Text != "")
            {
                Conexion.Conectar();

                string buscarproducto = "SELECT * FROM TPeluches WHERE Producto = '" + txtproducto.Text + "'";
                SqlCommand cmd5 = new SqlCommand(buscarproducto, Conexion.Conectar());
                SqlDataAdapter dato = new SqlDataAdapter(cmd5);

                DataTable tabla = new DataTable();
                dato.Fill(tabla);
                dataGridView1.DataSource = tabla; //Escribe en la DataGriedView
            }
            
        }

        private void btMostrarTodo_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            dataGridView1.DataSource = llenar_grid();
        }
    }
}
