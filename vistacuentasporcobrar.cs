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
using System.Configuration; // Añadir esta referencia para usar ConfigurationManager

namespace WOLFSFITNESSMARKET
{
    public partial class vistacuentasporcobrar : Form
    {
        // Cambiar la declaración de la cadena de conexión
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistacuentasporcobrar()
        {
            InitializeComponent();
        }

        private void vistacuentasporcobrar_Load(object sender, EventArgs e)
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
                // Consulta SQL para buscar en la tabla CuentasPorCobrar y Clientes
                string query = @"
SELECT 
    cc.CuentaCobrarID,
    cc.ClienteID,
    c.Nombre AS Cliente,
    cc.VentaID,
    cc.Monto,
    cc.SaldoPendiente,
    cc.FechaEmision,
    cc.FechaVencimiento,
    cc.Estado
FROM CuentasPorCobrar cc
INNER JOIN Clientes c ON cc.ClienteID = c.ClienteID
WHERE 
    (CAST(cc.CuentaCobrarID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (c.Nombre LIKE '%' + @Busqueda + '%') OR
    (CAST(cc.VentaID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CAST(cc.Monto AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CAST(cc.SaldoPendiente AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CONVERT(NVARCHAR, cc.FechaEmision, 120) LIKE '%' + @Busqueda + '%') OR
    (CONVERT(NVARCHAR, cc.FechaVencimiento, 120) LIKE '%' + @Busqueda + '%') OR
    (cc.Estado LIKE '%' + @Busqueda + '%');
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
                abonarcuentasporcobra formularioPrincipal = (abonarcuentasporcobra)Owner;

                // Obtener el valor del ID de la cuenta por pagar
                string cuentaPagarID = dataGridView1.Rows[e.RowIndex].Cells["CuentaCobrarID"].Value.ToString();

                // Pasar el valor al textBox correspondiente en el formulario principal
                formularioPrincipal.textBox2.Text = cuentaPagarID;

                // Cerrar el formulario de búsqueda
                this.Close();
            }
        }
    }
}
