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
using System.Configuration; // Importante para ConfigurationManager

namespace WOLFSFITNESSMARKET
{
    public partial class Usuario : Form
    {
        // Usar la cadena de conexión desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Usuario()
        {
            InitializeComponent();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            CargarDatosUsuarios();
        }

        private void CargarDatosUsuarios()
        {
            string query = "SELECT * FROM Usuarios";

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
                comboBox1.SelectedIndex == -1)
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
            comboBox1.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox2.Text;
            string correo = textBox3.Text;
            string contrasena = textBox4.Text;
            string rol = comboBox1.SelectedItem?.ToString();

            if (ValidarCampos())
            {
                if (string.IsNullOrEmpty(rol))
                {
                    MessageBox.Show("Por favor, seleccione un rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "INSERT INTO Usuarios (Nombre, Correo, Contrasena, Rol) VALUES (@Nombre, @Correo, @Contrasena, @Rol)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", nombre);
                            command.Parameters.AddWithValue("@Correo", correo);
                            command.Parameters.AddWithValue("@Contrasena", contrasena);
                            command.Parameters.AddWithValue("@Rol", rol);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Usuario guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                CargarDatosUsuarios();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo guardar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int usuarioId = Convert.ToInt32(textBox1.Text);

                string query = "DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UsuarioID", usuarioId);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatosUsuarios();
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                textBox1.Text = selectedRow.Cells["UsuarioID"].Value.ToString();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int usuarioId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UsuarioID"].Value);
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string correo = dataGridView1.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                string contrasena = dataGridView1.Rows[e.RowIndex].Cells["Contrasena"].Value.ToString();
                string rol = dataGridView1.Rows[e.RowIndex].Cells["Rol"].Value.ToString();

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar este usuario?",
                                                      "Confirmar actualización",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "UPDATE Usuarios SET Nombre = @Nombre, Correo = @Correo, Contrasena = @Contrasena, Rol = @Rol WHERE UsuarioID = @UsuarioID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UsuarioID", usuarioId);
                                command.Parameters.AddWithValue("@Nombre", nombre);
                                command.Parameters.AddWithValue("@Correo", correo);
                                command.Parameters.AddWithValue("@Contrasena", contrasena);
                                command.Parameters.AddWithValue("@Rol", rol);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDatosUsuarios();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    dataGridView1.CancelEdit();
                    CargarDatosUsuarios();
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarDatosUsuarios();
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            string selectedField = metroComboBox1.Text;
            string[] validFields = { "UsuarioID", "Nombre", "Correo", "Contrasena", "Rol", "FechaCreacion" };

            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                query = @"SELECT TOP (1000) UsuarioID, Nombre, Correo, Contrasena, Rol, FechaCreacion FROM Usuarios;";
            }
            else
            {
                if (selectedField == "FechaCreacion")
                {
                    query = $"SELECT TOP (1000) UsuarioID, Nombre, Correo, Contrasena, Rol, FechaCreacion FROM Usuarios WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%'+ @Busqueda +'%';";
                }
                else
                {
                    query = $"SELECT TOP (1000) UsuarioID, Nombre, Correo, Contrasena, Rol, FechaCreacion FROM Usuarios WHERE CAST({selectedField} AS NVARCHAR) LIKE '%'+ @Busqueda +'%';";
                }
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
