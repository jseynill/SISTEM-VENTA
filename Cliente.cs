using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class Cliente : Form
    {
        // Usar la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Cliente()
        {
            InitializeComponent();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            CargarDatosClientes();
        }

        private void CargarDatosClientes()
        {
            string query = "SELECT * FROM Clientes";

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
                string.IsNullOrWhiteSpace(textBox5.Text))
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string query = "INSERT INTO Clientes (Nombre, Direccion, Telefono, Correo) VALUES (@Nombre, @Direccion, @Telefono, @Correo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", textBox2.Text);
                        command.Parameters.AddWithValue("@Direccion", textBox3.Text);
                        command.Parameters.AddWithValue("@Telefono", textBox4.Text);
                        command.Parameters.AddWithValue("@Correo", textBox5.Text);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarDatosClientes();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            string query = "DELETE FROM Clientes WHERE ClienteID = @ClienteID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", Convert.ToInt32(textBox1.Text));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarDatosClientes();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar este cliente?", "Confirmar actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            string query = "UPDATE Clientes SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo WHERE ClienteID = @ClienteID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ClienteID"].Value));
                        command.Parameters.AddWithValue("@Nombre", dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString());
                        command.Parameters.AddWithValue("@Direccion", dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value.ToString());
                        command.Parameters.AddWithValue("@Telefono", dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString());
                        command.Parameters.AddWithValue("@Correo", dataGridView1.Rows[e.RowIndex].Cells["Correo"].Value.ToString());

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatosClientes();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Asignar los valores de las celdas de la fila a los TextBox
                textBox1.Text = selectedRow.Cells["ClienteID"].Value.ToString();  // Ajusta el nombre de la columna

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // Obtener el nombre del campo seleccionado en el ComboBox
            string selectedField = metroComboBox1.Text;

            // Lista de campos válidos
            string[] validFields = { "ClienteID", "Nombre", "Direccion", "Telefono", "Correo", "FechaRegistro" };

            // Validar que el ComboBox tenga un campo seleccionado
            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construir la consulta SQL
            string query;
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                // Si el TextBox está vacío, mostrar todos los registros
                query = @"
SELECT TOP (1000) 
    ClienteID, Nombre, Direccion, Telefono, Correo, FechaRegistro
FROM Clientes;";
            }
            else
            {
                // Si el campo es FechaRegistro, realizar búsqueda exacta o por patrón
                if (selectedField == "FechaRegistro")
                {
                    query = $@"
SELECT TOP (1000) 
    ClienteID, Nombre, Direccion, Telefono, Correo, FechaRegistro
FROM Clientes
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    // Consulta general para otros campos
                    query = $@"
SELECT TOP (1000) 
    ClienteID, Nombre, Direccion, Telefono, Correo, FechaRegistro
FROM Clientes
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
                        if (!string.IsNullOrWhiteSpace(textBox7.Text))
                        {
                            command.Parameters.AddWithValue("@Busqueda", textBox7.Text.Trim());
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
