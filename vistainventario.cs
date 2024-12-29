using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class vistainventario : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistainventario()
        {
            InitializeComponent();
        }

        private void vistainventario_Load(object sender, EventArgs e)
        {
            CargarDatosInventario();
        }

        private void CargarDatosInventario()
        {
            string query = "SELECT * FROM Inventario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
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
            // Este evento está vacío, así que lo dejamos tal cual
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {
            // Obtener el nombre del campo seleccionado en el ComboBox
            string selectedField = metroComboBox1.Text;

            // Lista de campos válidos
            string[] validFields = { "ProductoID", "Nombre", "Descripcion", "PrecioCosto", "PrecioVenta", "StockActual", "StockMinimo", "FechaIngreso" };

            // Validar que el ComboBox tenga un campo seleccionado
            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el TextBox está vacío
            string query;
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                // Si el TextBox está vacío, mostrar todos los registros
                query = @"
SELECT TOP (1000) 
    ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso
FROM Inventario;";
            }
            else
            {
                // Si el campo es FechaIngreso, realizar búsqueda exacta o por patrón
                if (selectedField == "FechaIngreso")
                {
                    query = $@"
SELECT TOP (1000) 
    ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso
FROM Inventario
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    // Consulta general para otros campos
                    query = $@"
SELECT TOP (1000) 
    ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso
FROM Inventario
WHERE CAST({selectedField} AS NVARCHAR) LIKE '%' + @Busqueda + '%';";
                }
            }

            try
            {
                // Establecer la conexión con la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar el parámetro solo si el TextBox tiene texto
                        if (!string.IsNullOrWhiteSpace(textBox8.Text))
                        {
                            command.Parameters.AddWithValue("@Busqueda", textBox8.Text.Trim());
                        }

                        // Abrir la conexión
                        connection.Open();

                        // Ejecutar la consulta y llenar el DataTable con los resultados
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable results = new DataTable();
                        adapter.Fill(results);

                        // Mostrar los resultados en el DataGridView
                        dataGridView1.DataSource = results;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de conexión o consulta
                MessageBox.Show($"Ocurrió un error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
