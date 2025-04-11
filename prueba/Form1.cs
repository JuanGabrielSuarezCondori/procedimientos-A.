using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
namespace prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static private string cadena = "server=dataepis.uandina.pe,49157;Database=BDAcademicoSuarez;Uid=suarezj;pwd=juan1999";
        static private SqlConnection conexion = new SqlConnection(cadena);
        private void button2_Click(object sender, EventArgs e)
        {
            {
                // Obtener los valores desde los TextBox
                string codAlumno = TBCodigo.Text;
                string aPaterno = TBPaterno.Text;
                string aMaterno = TBMaterno.Text;
                string nombres = TBNombre.Text;
                string usuario = TBUsuario.Text;
                string codCarrera = TB.Text;

                // Nombre del procedimiento almacenado
                string consulta = "spActualizarAlumno";

                try
                {
                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        SqlCommand cmd = new SqlCommand(consulta, conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros
                        cmd.Parameters.AddWithValue("@CodAlumno", codAlumno);
                        cmd.Parameters.AddWithValue("@APaterno", aPaterno);
                        cmd.Parameters.AddWithValue("@AMaterno", aMaterno);
                        cmd.Parameters.AddWithValue("@Nombres", nombres);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@CodCarrera", codCarrera);

                        // Ejecutar
                        conexion.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Alumno actualizado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un alumno con ese código.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el alumno: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //Generar la consulta
                SqlCommand comando = new SqlCommand("spListarAlumno", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //traer los datos de sql server en un contenedor 
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvResultado.DataSource = table;
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            {
                // Recoger los valores de los TextBox
                string codAlumno = TBCodigo.Text;
                string aPaterno = TBPaterno.Text;
                string aMaterno = TBMaterno.Text;
                string nombres = TBNombre.Text;
                string usuario = TBUsuario.Text;
                string codCarrera = TB.Text;

                // Definir la consulta para ejecutar el procedimiento almacenado
                string consulta = "spAgregarAlumno";

                try
                {
                    // Crear la conexión y el comando
                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        SqlCommand cmd = new SqlCommand(consulta, conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@CodAlumno", codAlumno);
                        cmd.Parameters.AddWithValue("@APaterno", aPaterno);
                        cmd.Parameters.AddWithValue("@AMaterno", aMaterno);
                        cmd.Parameters.AddWithValue("@Nombres", nombres);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@CodCarrera", codCarrera);

                        // Abrir la conexión y ejecutar el comando
                        conexion.Open();
                        cmd.ExecuteNonQuery();  // Ejecutar el procedimiento almacenado

                        // Mostrar un mensaje de éxito
                        MessageBox.Show("Alumno agregado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, mostrar un mensaje
                    MessageBox.Show("Error al agregar el alumno: " + ex.Message);
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            {
                // Obtener el código del alumno desde el TextBox
                string codAlumno = TBCodigo.Text;

                // Nombre del procedimiento almacenado
                string consulta = "spEliminarAlumno";

                try
                {
                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        SqlCommand cmd = new SqlCommand(consulta, conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetro del código de alumno
                        cmd.Parameters.AddWithValue("@CodAlumno", codAlumno);

                        // Abrir conexión y ejecutar
                        conexion.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        // Verificar si se eliminó
                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Alumno eliminado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un alumno con ese código.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el alumno: " + ex.Message);
                }
            }
        }
    }
}
