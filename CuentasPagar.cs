using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class CuentasPagar : Form
    {
        // Cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public CuentasPagar()
        {
            InitializeComponent();
        }

        private void CuentasPagar_Load(object sender, EventArgs e)
        {
            CargarDatosCuentasporpagar();
        }

        private void CargarDatosCuentasporpagar()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener datos de CuentasPorPagar junto con el ID y nombre del proveedor
                string query = @"
    SELECT 
        cp.CuentaPagarID,
        cp.ProveedorID, -- Incluye el ID del proveedor
        p.Nombre AS Proveedor, -- Incluye el nombre del proveedor
        cp.CompraID,
        cp.Monto,
        cp.SaldoPendiente,
        cp.FechaEmision,
        cp.FechaVencimiento,
        cp.Estado
    FROM CuentasPorPagar cp
    INNER JOIN Proveedores p ON cp.ProveedorID = p.ProveedorID
    WHERE 
        (CAST(cp.CuentaPagarID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.ProveedorID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (p.Nombre LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.CompraID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.Monto AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.SaldoPendiente AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cp.FechaEmision, 120) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cp.FechaVencimiento, 120) LIKE '%' + @Busqueda + '%') OR
        (cp.Estado LIKE '%' + @Busqueda + '%');
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

                    // Mostrar los resultados en el DataGridView
                    dataGridView1.DataSource = results;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // Evento no implementado
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
            string[] validFields = { "CuentaPagarID", "ProveedorID", "CompraID", "Monto", "SaldoPendiente", "FechaEmision", "FechaVencimiento", "Estado" };

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
    CuentaPagarID, ProveedorID, CompraID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorPagar;";
            }
            else
            {
                // Si el campo es de tipo Fecha, realizar búsqueda exacta o por patrón
                if (selectedField == "FechaEmision" || selectedField == "FechaVencimiento")
                {
                    query = $@"
SELECT TOP (1000) 
    CuentaPagarID, ProveedorID, CompraID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorPagar
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    // Consulta general para otros campos
                    query = $@"
SELECT TOP (1000) 
    CuentaPagarID, ProveedorID, CompraID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorPagar
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

        private void button6_Click(object sender, EventArgs e)
        {
            // Evento no implementado
        }
    }
}
