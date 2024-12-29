using System;
using System.Data.SqlClient;
using System.Configuration; // Necesario para usar ConfigurationManager
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class chart : Form
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


        public chart()
        {
            InitializeComponent();
        }

        private void chart_Load(object sender, EventArgs e)
        {
         


        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

       

        private void ObtenerReporte(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
WITH Gastos AS (
    SELECT 
        ISNULL(SUM(C.TotalCompra), 0) AS GastosContado,
        ISNULL(SUM(A.MontoAbono), 0) AS AbonosCredito,
        ISNULL(SUM(C.TotalCompra), 0) + ISNULL(SUM(A.MontoAbono), 0) AS GastosTotales
    FROM 
        Compras C
    LEFT JOIN 
        CuentasPorPagar CPP ON C.CompraID = CPP.CompraID
    LEFT JOIN 
        AbonosCuentasPorPagar A ON CPP.CuentaPagarID = A.CuentaPagarID
    WHERE 
        (C.FechaCompra BETWEEN @FechaInicio AND @FechaFin AND C.Estado = 'Pagado') OR 
        (A.FechaAbono BETWEEN @FechaInicio AND @FechaFin AND C.Estado = 'Pendiente')
),
VentasResumen AS (
    SELECT 
        ISNULL(SUM(V.TotalVenta), 0) AS VentasContado,
        ISNULL(SUM(A.MontoAbono), 0) AS AbonosCredito,
        ISNULL(SUM(V.TotalVenta), 0) + ISNULL(SUM(A.MontoAbono), 0) AS VentasTotales
    FROM 
        Ventas V
    LEFT JOIN 
        CuentasPorCobrar CPC ON V.VentaID = CPC.VentaID
    LEFT JOIN 
        AbonosCuentasPorCobrar A ON CPC.CuentaCobrarID = A.CuentaCobrarID
    WHERE 
        (V.FechaVenta BETWEEN @FechaInicio AND @FechaFin AND V.Estado = 'Pagado') OR 
        (A.FechaAbono BETWEEN @FechaInicio AND @FechaFin AND V.Estado = 'Pendiente')
),
Ganancias AS (
    SELECT 
        ISNULL(SUM(V.Ganancia), 0) AS GananciaRealizada,
        ISNULL(SUM(A.MontoAbono * (V.GananciaTotalProyectada / V.TotalVenta)), 0) AS GananciaAbonos,
        ISNULL(SUM(V.Ganancia), 0) + ISNULL(SUM(A.MontoAbono * (V.GananciaTotalProyectada / V.TotalVenta)), 0) AS GananciaTotal
    FROM 
        Ventas V
    LEFT JOIN 
        CuentasPorCobrar CPC ON V.VentaID = CPC.VentaID
    LEFT JOIN 
        AbonosCuentasPorCobrar A ON CPC.CuentaCobrarID = A.CuentaCobrarID
    WHERE 
        (V.FechaVenta BETWEEN @FechaInicio AND @FechaFin AND V.Estado = 'Pagado') OR 
        (A.FechaAbono BETWEEN @FechaInicio AND @FechaFin AND V.Estado = 'Pendiente')
)
SELECT 
    G.GastosTotales,
    VR.VentasTotales,
    GN.GananciaTotal
FROM 
    Gastos G, VentasResumen VR, Ganancias GN;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Convertir fechas al formato SQL Server
                        string fechaInicioSQL = fechaInicio.ToString("yyyy-MM-dd");
                        string fechaFinSQL = fechaFin.ToString("yyyy-MM-dd");

                        // Agregar los parámetros de fecha
                        command.Parameters.AddWithValue("@FechaInicio", fechaInicioSQL);
                        command.Parameters.AddWithValue("@FechaFin", fechaFinSQL);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label1.Text = reader["GastosTotales"] != DBNull.Value ? reader["GastosTotales"].ToString() : "0";
                                label2.Text = reader["VentasTotales"] != DBNull.Value ? reader["VentasTotales"].ToString() : "0";
                                label3.Text = reader["GananciaTotal"] != DBNull.Value ? reader["GananciaTotal"].ToString() : "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

      private void button1_Click(object sender, EventArgs e)
{
    // Obtener las fechas seleccionadas por el usuario desde los DateTimePicker
    DateTime fechaInicio = dateTimePicker1.Value.Date;
    DateTime fechaFin = dateTimePicker2.Value.Date;

    // Validar que la fecha inicial no sea mayor que la final
    if (fechaInicio > fechaFin)
    {
        MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // Validar que las fechas no estén vacías (opcional, por si hay más controles que alteren su estado)
    if (fechaInicio == DateTime.MinValue || fechaFin == DateTime.MinValue)
    {
        MessageBox.Show("Por favor, selecciona un rango de fechas válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    // Llamar al método para obtener el reporte y actualizar los labels
    ObtenerReporte(fechaInicio, fechaFin);
}

    }
}
