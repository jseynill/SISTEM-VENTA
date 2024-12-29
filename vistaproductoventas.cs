using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class vistaproductoventas : Form
    {
        // Usamos la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistaproductoventas()
        {
            InitializeComponent();
        }

        private void vistaproductoventas_Load(object sender, EventArgs e)
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL dinámica
                string query = @"
SELECT *
FROM Inventario
WHERE 
    (CAST(ProductoID AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (Nombre LIKE '%' + @Busqueda + '%') OR
    (Descripcion LIKE '%' + @Busqueda + '%') OR
    (CAST(PrecioCosto AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CAST(PrecioVenta AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CAST(StockActual AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CAST(StockMinimo AS NVARCHAR) LIKE '%' + @Busqueda + '%') OR
    (CONVERT(NVARCHAR, FechaIngreso, 120) LIKE '%' + @Busqueda + '%');
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

                    // Mostrar los resultados en un DataGridView (ajusta el nombre del control según tu diseño)
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
                string productoID = dataGridView1.Rows[e.RowIndex].Cells["ProductoID"].Value.ToString();
                string nombreProducto = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string precioVenta = dataGridView1.Rows[e.RowIndex].Cells["PrecioVenta"].Value.ToString();
                string preciocosto = dataGridView1.Rows[e.RowIndex].Cells["PrecioCosto"].Value.ToString();

                // Pasar los valores a los controles del formulario principal
                formularioPrincipal.txtProductoID.Text = productoID;
                formularioPrincipal.txtNombre.Text = nombreProducto;
                formularioPrincipal.txtpreciodeventa.Text = precioVenta;
                formularioPrincipal.txtpreciocosto.Text = preciocosto;

                // Cerrar el formulario de búsqueda
                this.Close();
            }
        }
    }
}
