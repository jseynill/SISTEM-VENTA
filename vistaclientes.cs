using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class vistaclientes : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistaclientes()
        {
            InitializeComponent();
        }

        private void vistaclientes_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }

        private void CargarDatosClientes()
        {
            // Usar la cadena de conexión del archivo de configuración
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para seleccionar los datos de la tabla Clientes
                string query = "SELECT * FROM Clientes";

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL dinámica para la tabla Clientes
                string query = @"
SELECT *
FROM Clientes
WHERE 
    (CAST(ClienteID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (Nombre LIKE '%' + @Busqueda + '%') OR
    (Direccion LIKE '%' + @Busqueda + '%') OR
    (Telefono LIKE '%' + @Busqueda + '%') OR
    (Correo LIKE '%' + @Busqueda + '%') OR
    (CONVERT(NVARCHAR, FechaRegistro, 120) LIKE '%' + @Busqueda + '%');
";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parámetro para la búsqueda
                    command.Parameters.AddWithValue("@Busqueda", textBox8.Text);

                    // Abrir la conexión
                    connection.Open();

                    // Ejecutar el comando y llenar el DataTable con los resultados
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable results = new DataTable();
                    adapter.Fill(results);

                    // Mostrar los resultados en un DataGridView
                    dataGridView1.DataSource = results;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que la fila seleccionada sea válida
            {
                // Obtener el formulario principal
                Venta formularioPrincipal = (Venta)Owner;

                // Obtener los valores de las celdas seleccionadas
                string clienteID = dataGridView1.Rows[e.RowIndex].Cells["ClienteID"].Value.ToString();
                string nombreCliente = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Pasar los valores a los controles del formulario principal
                formularioPrincipal.txtClienteID.Text = clienteID;
                formularioPrincipal.txtCliente.Text = nombreCliente;

                // Cerrar el formulario de búsqueda
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
