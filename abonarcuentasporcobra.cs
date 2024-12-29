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
using System.Configuration; // Agregado para ConfigurationManager

namespace WOLFSFITNESSMARKET
{
    public partial class abonarcuentasporcobra : Form
    {
        // Cambiar la cadena de conexión a la configuración del app.config
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public abonarcuentasporcobra()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de registrar el abono.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el monto del abono sea un número válido mayor a cero
            if (!decimal.TryParse(textBox3.Text, out decimal montoAbono) || montoAbono <= 0)
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
                    // Validar que la cuenta por cobrar exista y obtener el saldo pendiente
                    string queryValidar = "SELECT SaldoPendiente FROM CuentasPorCobrar WHERE CuentaCobrarID = @CuentaCobrarID";
                    SqlCommand cmdValidar = new SqlCommand(queryValidar, conn, transaction);
                    cmdValidar.Parameters.AddWithValue("@CuentaCobrarID", int.Parse(textBox2.Text));

                    object saldoObj = cmdValidar.ExecuteScalar();
                    if (saldoObj == null)
                    {
                        MessageBox.Show("La cuenta por cobrar no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return;
                    }

                    decimal saldoPendiente = Convert.ToDecimal(saldoObj);

                    // Validar que el monto del abono no exceda el saldo pendiente
                    if (montoAbono > saldoPendiente)
                    {
                        MessageBox.Show("El monto del abono no puede ser mayor al saldo pendiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return;
                    }

                    // Insertar el abono
                    string queryAbono = @"
INSERT INTO AbonosCuentasPorCobrar (CuentaCobrarID, MontoAbono, FechaAbono)
VALUES (@CuentaCobrarID, @MontoAbono, GETDATE())";

                    SqlCommand cmdAbono = new SqlCommand(queryAbono, conn, transaction);
                    cmdAbono.Parameters.AddWithValue("@CuentaCobrarID", int.Parse(textBox2.Text));
                    cmdAbono.Parameters.AddWithValue("@MontoAbono", montoAbono);
                    cmdAbono.ExecuteNonQuery();

                    // El trigger en SQL Server manejará la actualización del saldo pendiente y la ganancia de la venta

                    // Confirmar la transacción
                    transaction.Commit();

                    MessageBox.Show("Abono registrado exitosamente.");
                    LimpiarFormularioAbono();
                    CargarDatosAbonosCuentasPorCobrar();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al registrar el abono: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vistacuentasporcobrar vistaceuntaporcobrarsbcs = new vistacuentasporcobrar
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaceuntaporcobrarsbcs.ShowDialog();
        }

        private void abonarcuentasporcobra_Load(object sender, EventArgs e)
        {
            CargarDatosAbonosCuentasPorCobrar();
        }

        private void LimpiarFormularioAbono()
        {
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void CargarDatosAbonosCuentasPorCobrar()
        {
            // Consulta base para obtener todos los registros
            string query = "SELECT * FROM AbonosCuentasPorCobrar";

            // Si el textBox2 tiene un valor, agregamos un filtro para CuentaPagarID
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                query += " WHERE CuentaCobrarID = @CuentaCobrarID";
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
                        command.Parameters.AddWithValue("@CuentaCobrarID", int.Parse(textBox2.Text));
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Obtener el nombre del campo seleccionado en el ComboBox
            string selectedField = metroComboBox1.Text;

            // Lista de campos válidos
            string[] validFields = { "AbonoID", "CuentaCobrarID", "MontoAbono", "FechaAbono" };

            // Validar que el ComboBox tenga un campo seleccionado
            if (string.IsNullOrWhiteSpace(selectedField) || Array.IndexOf(validFields, selectedField) == -1)
            {
                MessageBox.Show("Por favor, selecciona un campo válido en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el TextBox está vacío
            string query;
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                // Si el TextBox está vacío, mostrar todos los registros
                query = @"
SELECT TOP (1000) 
    AbonoID, CuentaCobrarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorCobrar;";
            }
            else
            {
                // Si el campo es de tipo Fecha, realizar búsqueda exacta o por patrón
                if (selectedField == "FechaAbono")
                {
                    query = $@"
SELECT TOP (1000) 
    AbonoID, CuentaCobrarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorCobrar
WHERE CONVERT(NVARCHAR, {selectedField}, 120) LIKE '%' + @Busqueda + '%';";
                }
                else
                {
                    // Consulta general para otros campos
                    query = $@"
SELECT TOP (1000) 
    AbonoID, CuentaCobrarID, MontoAbono, FechaAbono
FROM AbonosCuentasPorCobrar
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
                        if (!string.IsNullOrWhiteSpace(textBox6.Text))
                        {
                            command.Parameters.AddWithValue("@Busqueda", textBox6.Text.Trim());
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CargarDatosAbonosCuentasPorCobrar();
        }
    }
}
