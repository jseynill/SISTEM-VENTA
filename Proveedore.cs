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
using Guna.UI2.WinForms;
using System.Configuration;

namespace WOLFSFITNESSMARKET
{
    public partial class Proveedore : Form
    {
        // Cadena de conexión obtenida desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Proveedore()
        {
            InitializeComponent();
        }

        private void Proveedore_Load(object sender, EventArgs e)
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
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string query = "INSERT INTO Proveedores (Nombre, Direccion, Telefono, Correo) VALUES (@Nombre, @Direccion, @Telefono, @Correo)";

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
                                MessageBox.Show("Proveedor guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                CargarDatosProveedores();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo guardar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este proveedor?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int proveedorId = Convert.ToInt32(textBox1.Text);
                string query = "DELETE FROM Proveedores WHERE ProveedorID = @ProveedorID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ProveedorID", proveedorId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Proveedor eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatosProveedores();
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int proveedorId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProveedorID"].Value);
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string direccion = dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
                string telefono = dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                string correo = dataGridView1.Rows[e.RowIndex].Cells["Correo"].Value.ToString();

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar este proveedor?",
                                                      "Confirmar actualización",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "UPDATE Proveedores SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo WHERE ProveedorID = @ProveedorID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ProveedorID", proveedorId);
                                command.Parameters.AddWithValue("@Nombre", nombre);
                                command.Parameters.AddWithValue("@Direccion", direccion);
                                command.Parameters.AddWithValue("@Telefono", telefono);
                                command.Parameters.AddWithValue("@Correo", correo);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDatosProveedores();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    CargarDatosProveedores();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells["ProveedorID"].Value.ToString();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarDatosProveedores();
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            string selectedField = metroComboBox1.Text;
            string[] validFields = { "ProveedorID", "Nombre", "Direccion", "Telefono", "Correo", "FechaRegistro" };

            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                query = @"SELECT TOP (1000) ProveedorID, Nombre, Direccion, Telefono, Correo, FechaRegistro FROM Proveedores;";
            }
            else if (selectedField == "FechaRegistro")
            {
                query = $@"SELECT TOP (1000) ProveedorID, Nombre, Direccion, Telefono, Correo, FechaRegistro FROM Proveedores WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
            }
            else
            {
                query = $@"SELECT TOP (1000) ProveedorID, Nombre, Direccion, Telefono, Correo, FechaRegistro FROM Proveedores WHERE CAST({selectedField} AS NVARCHAR) LIKE '%' + @Busqueda + '%';";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(textBox6.Text))
                        {
                            command.Parameters.AddWithValue("@Busqueda", textBox6.Text.Trim());
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
    }
}
