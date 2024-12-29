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
using System.Configuration; // Asegúrate de importar esta biblioteca

namespace WOLFSFITNESSMARKET
{
    public partial class vistacuentasporpagar : Form
    {
        // Usamos la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistacuentasporpagar()
        {
            InitializeComponent();
        }

        private void vistacuentasporpagar_Load(object sender, EventArgs e)
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
    cp.Estado = 'Pendiente' AND -- Filtrar solo filas con estado 'Pendiente'
    (
        (CAST(cp.CuentaPagarID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.ProveedorID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (p.Nombre LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.CompraID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.Monto AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CAST(cp.SaldoPendiente AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cp.FechaEmision, 120) LIKE '%' + @Busqueda + '%') OR
        (CONVERT(NVARCHAR, cp.FechaVencimiento, 120) LIKE '%' + @Busqueda + '%')
    );
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para buscar en la tabla CuentasPorPagar y Proveedores
                string query = @"
SELECT 
    cp.CuentaPagarID,
    cp.ProveedorID,
    p.Nombre AS Proveedor,
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que la fila seleccionada sea válida
            {
                // Obtener el formulario principal
                abonarcuentasporpagar formularioPrincipal = (abonarcuentasporpagar)Owner;

                // Obtener el valor del ID de la cuenta por pagar
                string cuentaPagarID = dataGridView1.Rows[e.RowIndex].Cells["CuentaPagarID"].Value.ToString();

                // Pasar el valor al textBox correspondiente en el formulario principal
                formularioPrincipal.textBox2.Text = cuentaPagarID;

                // Cerrar el formulario de búsqueda
                this.Close();
            }
        }
    }
}
