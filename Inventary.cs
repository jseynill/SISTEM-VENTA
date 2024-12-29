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
using System.Configuration; // Para manejar ConfigurationManager
using Guna.UI2.WinForms;

namespace WOLFSFITNESSMARKET
{
    public partial class Inventary : Form
    {
        // Leer cadena de conexión del archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Inventary()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Inventary_Load(object sender, EventArgs e)
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

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                 string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text))

            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string query = "INSERT INTO Inventario (Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo) " +
                               "VALUES (@Nombre, @Descripcion, @PrecioCosto, @PrecioVenta, @StockActual, @StockMinimo)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", textBox2.Text);
                            command.Parameters.AddWithValue("@Descripcion", textBox3.Text);
                            command.Parameters.AddWithValue("@PrecioCosto", decimal.Parse(textBox4.Text));
                            command.Parameters.AddWithValue("@PrecioVenta", decimal.Parse(textBox5.Text));
                            command.Parameters.AddWithValue("@StockActual", int.Parse(textBox6.Text));
                            command.Parameters.AddWithValue("@StockMinimo", int.Parse(textBox7.Text));

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                CargarDatosInventario();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo guardar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = selectedRow.Cells["ProductoID"].Value.ToString();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int productoId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductoID"].Value);
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string descripcion = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                decimal precioCosto = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["PrecioCosto"].Value);
                decimal precioVenta = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["PrecioVenta"].Value);
                int stockActual = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StockActual"].Value);
                int stockMinimo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StockMinimo"].Value);

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar este producto?",
                                                      "Confirmar actualización",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "UPDATE Inventario SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioCosto = @PrecioCosto, " +
                                   "PrecioVenta = @PrecioVenta, StockActual = @StockActual, StockMinimo = @StockMinimo WHERE ProductoID = @ProductoID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ProductoID", productoId);
                                command.Parameters.AddWithValue("@Nombre", nombre);
                                command.Parameters.AddWithValue("@Descripcion", descripcion);
                                command.Parameters.AddWithValue("@PrecioCosto", precioCosto);
                                command.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                                command.Parameters.AddWithValue("@StockActual", stockActual);
                                command.Parameters.AddWithValue("@StockMinimo", stockMinimo);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDatosInventario();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    CargarDatosInventario();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int productoId = Convert.ToInt32(textBox1.Text);
                string query = "DELETE FROM Inventario WHERE ProductoID = @ProductoID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ProductoID", productoId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Producto eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatosInventario();
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string selectedField = metroComboBox1.Text;
            string[] validFields = { "ProductoID", "Nombre", "Descripcion", "PrecioCosto", "PrecioVenta", "StockActual", "StockMinimo", "FechaIngreso" };

            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el combobox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                // Si el campo de búsqueda está vacío, mostrar los primeros 1000 registros
                query = "SELECT TOP (1000) ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso FROM Inventario;";
            }
            else
            {
                // Asegúrate de construir la consulta correctamente
                query = $"SELECT TOP (1000) ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso " +
                        $"FROM Inventario WHERE CAST({selectedField} AS NVARCHAR) LIKE '%' + @Busqueda + '%';";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(textBox8.Text))
                        {
                            command.Parameters.AddWithValue("@Busqueda", textBox8.Text.Trim());
                        }

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable results = new DataTable();
                        adapter.Fill(results);

                        dataGridView1.DataSource = results;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarDatosInventario();
        }
    }
}
