using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WOLFSFITNESSMARKET
{
    public partial class DetallesVenta : Form
    {
        // Obtención de la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public DetallesVenta()
        {
            InitializeComponent();
        }

        private void DetallesVenta_Load(object sender, EventArgs e)
        {
            CargarDatosDetalleventa();
        }

        private void CargarDatosDetalleventa()
        {
            string query = "SELECT * FROM DetalleVentas";

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

        private void label5_Click(object sender, EventArgs e)
        {
            // Evento vacío según el código proporcionado
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // Obtener el nombre del campo seleccionado en el ComboBox
            string selectedField = metroComboBox1.Text;

            // Lista de campos válidos para la tabla DetalleVentas
            string[] validFields = { "DetalleVentaID", "VentaID", "ProductoID", "Cantidad", "PrecioUnitario", "Subtotal" };

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
                query = @"SELECT TOP (1000) DetalleVentaID, VentaID, ProductoID, Cantidad, PrecioUnitario, Subtotal FROM DetalleVentas;";
            }
            else
            {
                // Consulta general para búsqueda
                query = $@"SELECT TOP (1000) DetalleVentaID, VentaID, ProductoID, Cantidad, PrecioUnitario, Subtotal FROM DetalleVentas WHERE CAST({selectedField} AS NVARCHAR) LIKE '%' + @Busqueda + '%';";
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
