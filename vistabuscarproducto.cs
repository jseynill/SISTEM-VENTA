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
    public partial class vistabuscarproducto : Form
    {
        // Obtener la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public vistabuscarproducto()
        {
            InitializeComponent();
        }

        private void vistabuscarproducto_Load(object sender, EventArgs e)
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
                    command.Parameters.AddWithValue("@Busqueda", textBox8.Text);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable results = new DataTable();
                        adapter.Fill(results);

                        dataGridView1.DataSource = results;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Compras formularioPrincipal = (Compras)Owner;

                string productoID = dataGridView1.Rows[e.RowIndex].Cells["ProductoID"].Value.ToString();

                formularioPrincipal.txtProductoID.Text = productoID;

                this.Close();
            }
        }
    }
}
