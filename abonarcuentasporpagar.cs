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
using System.Configuration; // Importar el espacio de nombres para ConfigurationManager

namespace WOLFSFITNESSMARKET
{
    public partial class abonarcuentasporpagar : Form
    {
        // Definir la cadena de conexión utilizando ConfigurationManager
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public abonarcuentasporpagar()
        {
            InitializeComponent();
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
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de registrar el abono.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal montoAbono;
            if (!decimal.TryParse(textBox3.Text, out montoAbono) || montoAbono <= 0)
            {
                MessageBox.Show("El monto del abono debe ser un número mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Validar que el monto del abono no exceda el saldo pendiente
                    string queryValidar = "SELECT SaldoPendiente FROM CuentasPorPagar WHERE CuentaPagarID = @CuentaPagarID";
                    SqlCommand cmdValidar = new SqlCommand(queryValidar, conn, transaction);
                    cmdValidar.Parameters.AddWithValue("@CuentaPagarID", int.Parse(textBox2.Text));

                    object saldoObj = cmdValidar.ExecuteScalar();
                    if (saldoObj == null)
                    {
                        MessageBox.Show("La cuenta por pagar no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return;
                    }

                    decimal saldoPendiente = Convert.ToDecimal(saldoObj);

                    if (montoAbono > saldoPendiente)
                    {
                        MessageBox.Show("El monto del abono no puede ser mayor al saldo pendiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return;
                    }

                    // Insertar el abono
                    string queryAbono = @"
            INSERT INTO AbonosCuentasPorPagar (CuentaPagarID, MontoAbono, FechaAbono)
            VALUES (@CuentaPagarID, @MontoAbono, GETDATE())";

                    SqlCommand cmdAbono = new SqlCommand(queryAbono, conn, transaction);
                    cmdAbono.Parameters.AddWithValue("@CuentaPagarID", int.Parse(textBox2.Text));
                    cmdAbono.Parameters.AddWithValue("@MontoAbono", montoAbono);
                    cmdAbono.ExecuteNonQuery();

                    // Confirmar la transacción
                    transaction.Commit();

                    MessageBox.Show("Abono registrado exitosamente.");
                    LimpiarFormularioAbono();
                    CargarDatosAbonosCuentasPorPagar();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al registrar el abono: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            vistacuentasporpagar vistaceuntaporpagarsbcs = new vistacuentasporpagar
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaceuntaporpagarsbcs.ShowDialog();
        }

        private void LimpiarFormularioAbono()
        {
            textBox2.Clear();
            textBox3.Clear();
        }

        private void abonarcuentasporpagar_Load(object sender, EventArgs e)
        {
            CargarDatosAbonosCuentasPorPagar();
        }

        private void CargarDatosAbonosCuentasPorPagar()
        {
            // Consulta base para obtener todos los registros
            string query = "SELECT * FROM AbonosCuentasPorPagar";

            // Si el textBox2 tiene un valor, agregamos un filtro para CuentaPagarID
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                query += " WHERE CuentaPagarID = @CuentaPagarID";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    // Si el textBox2 tiene un valor, agregar el parámetro
                    if (!string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        command.Parameters.AddWithValue("@CuentaPagarID", int.Parse(textBox2.Text));
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Establecer los datos en el DataGridView
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
            // Sin implementación aquí, pero se puede agregar según sea necesario
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LimpiarFormularioAbono();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Cadena de conexión a la base de datos
            // Ya se utiliza la cadena de conexión global
            string selectedField = metroComboBox1.Text;

            string[] validFields = { "AbonoID", "CuentaPagarID", "MontoAbono", "FechaAbono" };

            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                query = @"
SELECT TOP (1000) 
    AbonoID, CuentaPagarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorPagar;";
            }
            else
            {
                if (selectedField == "FechaAbono")
                {
                    query = $@"
SELECT TOP (1000) 
    AbonoID, CuentaPagarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorPagar
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    query = $@"
SELECT TOP (1000) 
    AbonoID, CuentaPagarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorPagar
WHERE CAST({selectedField} AS NVARCHAR) LIKE '%' + @Busqueda + '%';";
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

        private void button2_Click(object sender, EventArgs e)
        {
            vistacuentasporpagar vistaceuntaporpagarsbcs = new vistacuentasporpagar
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaceuntaporpagarsbcs.ShowDialog();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Implementación si es necesario
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CargarDatosAbonosCuentasPorPagar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Implementación si es necesario
        }
    }
}
