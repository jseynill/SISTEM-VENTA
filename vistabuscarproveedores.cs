using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class vistabuscarproveedores : Form
    {
        // Usamos la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistabuscarproveedores()
        {
            InitializeComponent();
        }

        private void vistabuscarproveedores_Load(object sender, EventArgs e)
        {
            CargarDatosProveedores();
        }

        private void CargarDatosProveedores()
        {
            string query = "SELECT * FROM Proveedores";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
SELECT *
FROM Proveedores
WHERE 
    (CAST(ProveedorID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (Nombre LIKE '%' + @Busqueda + '%') OR
    (Direccion LIKE '%' + @Busqueda + '%') OR
    (Telefono LIKE '%' + @Busqueda + '%') OR
    (Correo LIKE '%' + @Busqueda + '%') OR
    (CONVERT(NVARCHAR, FechaRegistro, 120) LIKE '%' + @Busqueda + '%');
";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Busqueda", textBox8.Text);

                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable results = new DataTable();
                    adapter.Fill(results);

                    dataGridView1.DataSource = results;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener el formulario principal
                Compras formularioPrincipal = (Compras)Owner;

                // Obtener los valores de las celdas seleccionadas
                string proveedorID = dataGridView1.Rows[e.RowIndex].Cells["ProveedorID"].Value.ToString();
                string nombreProveedor = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Pasar los valores a los controles del formulario principal
                formularioPrincipal.txtProveedorID.Text = proveedorID;
                formularioPrincipal.txtProveedor.Text = nombreProveedor;

                // Cerrar el formulario de búsqueda
                this.Close();
            }
        }
    }
}
