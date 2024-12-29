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
using System.Configuration; // Necesario para ConfigurationManager

namespace WOLFSFITNESSMARKET
{
    public partial class CuentasCobrar : Form
    {
        // Cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public CuentasCobrar()
        {
            InitializeComponent();
        }

        private void CuentasCobrar_Load(object sender, EventArgs e)
        {
            CargarDatosCuentasporCobrar();
        }

        private void CargarDatosCuentasporCobrar()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener datos de CuentasPorCobrar junto con el ID y nombre del cliente
                string query = @"
SELECT 
    cc.CuentaCobrarID,
    cc.ClienteID, -- Incluye el ID del cliente
    c.Nombre AS Cliente, -- Incluye el nombre del cliente
    cc.VentaID,
    cc.Monto,
    cc.SaldoPendiente,
    cc.FechaEmision,
    cc.FechaVencimiento,
    cc.Estado
FROM CuentasPorCobrar cc
INNER JOIN Clientes c ON cc.ClienteID = c.ClienteID
WHERE 
    cc.Estado = 'Pendiente' AND -- Filtrar solo filas con estado 'Pendiente'
    (
        (CAST(cc.CuentaCobrarID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cc.ClienteID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (c.Nombre LIKE '%' + @Busqueda + '%') OR
        (CAST(cc.VentaID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cc.Monto AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cc.SaldoPendiente AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cc.FechaEmision, 120) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cc.FechaVencimiento, 120) LIKE '%' + @Busqueda + '%')
    );";

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

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // Obtener el nombre del campo seleccionado en el ComboBox
            string selectedField = metroComboBox1.Text;

            // Lista de campos válidos
            string[] validFields = { "CuentaCobrarID", "ClienteID", "VentaID", "Monto", "SaldoPendiente", "FechaEmision", "FechaVencimiento", "Estado" };

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
    CuentaCobrarID, ClienteID, VentaID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorCobrar;";
            }
            else
            {
                // Si el campo es de tipo Fecha, realizar búsqueda exacta o por patrón
                if (selectedField == "FechaEmision" || selectedField == "FechaVencimiento")
                {
                    query = $@"
SELECT TOP (1000) 
    CuentaCobrarID, ClienteID, VentaID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorCobrar
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    // Consulta general para otros campos
                    query = $@"
SELECT TOP (1000) 
    CuentaCobrarID, ClienteID, VentaID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado
FROM CuentasPorCobrar
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
