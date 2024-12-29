using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WOLFSFITNESSMARKET.Venta;

namespace WOLFSFITNESSMARKET
{
    public partial class Venta : Form
    {
        public Venta()
        {
            InitializeComponent();

        }

        public class DetalleVenta
        {
            public int ProductoID { get; set; }
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal => Cantidad * PrecioUnitario; // Calcula el subtotal automáticamente.
        }


        private void Venta_Load(object sender, EventArgs e)
        {

        }

        // Variables globales
        private List<DetalleVenta> detalleVentas = new List<DetalleVenta>();
        private decimal totalVenta = 0;
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;



        private void CalcularMargenGanancia()
        {
            try
            {
                // Captura los valores de los campos
                decimal precioCompra = decimal.Parse(txtpreciocosto.Text);
                decimal precioVenta = decimal.Parse(txtpreciodeventa.Text);
                decimal descuento = string.IsNullOrEmpty(textBox1.Text) ? 0 : decimal.Parse(textBox1.Text);
                int cantidad = string.IsNullOrEmpty(txtCantidad.Text) ? 0 : int.Parse(txtCantidad.Text);

                // Calcula el margen de ganancia
                decimal margenGanancia = (precioVenta - precioCompra - descuento) * cantidad;

                // Muestra el margen de ganancia en el campo correspondiente
                textBox2.Text = margenGanancia.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los cálculos: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            vistaclientes vistaclientebus = new vistaclientes
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaclientebus.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            vistaproductoventas vistaproductobcs = new vistaproductoventas
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaproductobcs.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CalcularMargenGanancia();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isSaving)
            {
                CalcularMargenGanancia();
            }

        }

        private bool isSaving = false;


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar tipo de venta
                if (cmbTipoPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de venta antes de agregar un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTipoPago.Focus();
                    return;
                }

                // Validar usuario y cliente
                if (string.IsNullOrWhiteSpace(txtUsuario1.Text) || string.IsNullOrWhiteSpace(txtClienteID.Text))
                {
                    MessageBox.Show("Complete los campos de Usuario y Cliente antes de agregar un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar ProductoID
                if (string.IsNullOrWhiteSpace(txtProductoID.Text) || !int.TryParse(txtProductoID.Text, out int productoID))
                {
                    MessageBox.Show("Ingrese un ID de producto válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductoID.Focus();
                    return;
                }

                // Validar cantidad
                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCantidad.Focus();
                    return;
                }

                // Verificar stock disponible
                if (!VerificarStockDisponible(productoID, cantidad))
                {
                    return;
                }

                // Validar precio unitario
                if (string.IsNullOrWhiteSpace(txtpreciodeventa.Text) || !decimal.TryParse(txtpreciodeventa.Text, out decimal precioUnitario) || precioUnitario <= 0)
                {
                    MessageBox.Show("Ingrese un precio unitario válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpreciodeventa.Focus();
                    return;
                }

                var productoExistente = detalleVentas.FirstOrDefault(p => p.ProductoID == productoID);

                if (productoExistente != null)
                {
                    productoExistente.Cantidad += cantidad;
                }
                else
                {
                    detalleVentas.Add(new DetalleVenta
                    {
                        ProductoID = productoID,
                        Nombre = txtNombre.Text,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnitario
                    });
                }

                totalVenta = detalleVentas.Sum(p => p.Subtotal);
                ActualizarDataGrid();
                lblTotal.Text = "$" + totalVenta.ToString("#,##0.00", System.Globalization.CultureInfo.InvariantCulture);
                cmbTipoPago.Enabled = false;

                LimpiarCamposProducto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ActualizarDataGrid()
        {
            dgvDetalleVentas.DataSource = null; // Limpia el DataSource para evitar problemas de duplicado
            dgvDetalleVentas.DataSource = detalleVentas; // Asigna la lista actualizada
        }

        private void LimpiarCamposProducto()
        {
            txtProductoID.Clear();
            txtCantidad.Clear();
            txtpreciodeventa.Clear();
            txtProductoID.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVentas.CurrentRow != null)
            {
                var detalle = (DetalleVenta)dgvDetalleVentas.CurrentRow.DataBoundItem;
                detalleVentas.Remove(detalle);
                totalVenta -= detalle.Subtotal;

                ActualizarDataGrid();
                lblTotal.Text = totalVenta.ToString("C2");
            }
        }


        private bool VerificarStockDisponible(int productoID, int cantidadRequerida)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta el stock actual del producto
                    string query = "SELECT StockActual FROM Inventario WHERE ProductoID = @ProductoID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductoID", productoID);

                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("El producto con ID " + productoID + " no existe en el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    int stockActual = Convert.ToInt32(result);

                    // Validar si el stock actual es suficiente
                    if (stockActual < cantidadRequerida)
                    {
                        MessageBox.Show($"Stock insuficiente para el producto con ID {productoID}.\n" +
                                        $"Stock disponible: {stockActual}, Cantidad requerida: {cantidadRequerida}.",
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    return true; // Hay suficiente stock
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbTipoPago.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtClienteID.Text) || string.IsNullOrWhiteSpace(txtUsuario1.Text) || detalleVentas.Count == 0)
            {
                MessageBox.Show("Complete todos los campos antes de finalizar la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var detalle in detalleVentas)
            {
                if (!VerificarStockDisponible(detalle.ProductoID, detalle.Cantidad))
                {
                    return;
                }
            }

            isSaving = true;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Calcular ganancias
                    decimal gananciaInmediata = 0;
                    decimal gananciaProyectada = 0;

                    if (cmbTipoPago.SelectedItem.ToString().Trim().Equals("Contado", StringComparison.OrdinalIgnoreCase))
                    {
                        // Venta al contado: calcular ganancia inmediata
                        gananciaInmediata = detalleVentas.Sum(d => (d.PrecioUnitario - decimal.Parse(txtpreciocosto.Text)) * d.Cantidad);
                        gananciaProyectada = 0;
                    }
                    else
                    {
                        // Venta a crédito: proyectar ganancia futura
                        gananciaInmediata = 0;
                        gananciaProyectada = detalleVentas.Sum(d => (d.PrecioUnitario - decimal.Parse(txtpreciocosto.Text)) * d.Cantidad);
                    }

                    // Insertar venta en la tabla Ventas
                    string queryVenta = @"
                DECLARE @InsertedIds TABLE (VentaID INT);
                INSERT INTO Ventas (ClienteID, UsuarioID, FechaVenta, TotalVenta, Descuento, TipoPago, Estado, Ganancia, GananciaTotalProyectada)
                OUTPUT INSERTED.VentaID INTO @InsertedIds
                VALUES (@ClienteID, @UsuarioID, GETDATE(), @TotalVenta, @Descuento, @TipoPago, 'Pendiente', @Ganancia, @GananciaProyectada);
                SELECT VentaID FROM @InsertedIds;";

                    SqlCommand cmdVenta = new SqlCommand(queryVenta, conn, transaction);
                    cmdVenta.Parameters.AddWithValue("@ClienteID", int.Parse(txtClienteID.Text));
                    cmdVenta.Parameters.AddWithValue("@UsuarioID", int.Parse(txtUsuario1.Text));
                    cmdVenta.Parameters.AddWithValue("@TotalVenta", totalVenta);
                    cmdVenta.Parameters.AddWithValue("@Descuento", decimal.Parse(textBox1.Text));
                    cmdVenta.Parameters.AddWithValue("@TipoPago", cmbTipoPago.SelectedItem.ToString());
                    cmdVenta.Parameters.AddWithValue("@Ganancia", gananciaInmediata);
                    cmdVenta.Parameters.AddWithValue("@GananciaProyectada", gananciaProyectada);

                    int ventaID = (int)cmdVenta.ExecuteScalar();

                    // Insertar detalles de venta y actualizar inventario
                    foreach (var detalle in detalleVentas)
                    {
                        string queryDetalle = "INSERT INTO DetalleVentas (VentaID, ProductoID, Cantidad, PrecioUnitario) VALUES (@VentaID, @ProductoID, @Cantidad, @PrecioUnitario)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conn, transaction);
                        cmdDetalle.Parameters.AddWithValue("@VentaID", ventaID);
                        cmdDetalle.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                        cmdDetalle.ExecuteNonQuery();

                        string queryActualizarStock = "UPDATE Inventario SET StockActual = StockActual - @Cantidad WHERE ProductoID = @ProductoID";
                        SqlCommand cmdActualizarStock = new SqlCommand(queryActualizarStock, conn, transaction);
                        cmdActualizarStock.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdActualizarStock.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                        cmdActualizarStock.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Venta registrada exitosamente.");
                    GuardarFacturaEnArchivo(ventaID);
                    LimpiarFormularioVenta();
                    cmbTipoPago.Enabled = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al guardar la venta: " + ex.Message);
                    LimpiarFormularioVenta();
                    cmbTipoPago.Enabled = true;
                }
                finally
                {
                    isSaving = false;
                }
            }
        }


        private void LimpiarFormularioVenta()
        {
            txtClienteID.Clear();
            txtCliente.Clear();
            cmbTipoPago.SelectedIndex = -1;
            txtpreciocosto.Clear();
            txtNombre.Clear();
            textBox1.Clear(); // Descuento
            lblTotal.Text = "$0.00"; // Total de la venta
            textBox2.Clear(); // Margen de ganancia
            detalleVentas.Clear(); // Limpiar la lista de detalle de ventas
            ActualizarDataGrid(); // Refrescar el DataGridView con datos vacíos
            totalVenta = 0; // Reiniciar el total de la venta
        }




        private void GuardarFacturaEnArchivo(int ventaID)
        {
            try
            {
                // Obtener detalles de la venta
                DataTable detallesVenta = ObtenerDetallesVenta(ventaID);

                if (detallesVenta.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron detalles para esta venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construir el contenido de la factura
                StringBuilder factura = new StringBuilder();
                int anchoLinea = 40; // Ancho máximo para la línea de separación

                // Encabezado centrado
                factura.AppendLine("WOLFS FITNES MARKET".PadLeft((anchoLinea + "WOLFS FITNES MARKET".Length) / 2));
                factura.AppendLine(new string('-', anchoLinea));

                // Detalles de la venta alineados a la izquierda
                factura.AppendLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}".PadRight(anchoLinea));
                factura.AppendLine($"Venta ID: {ventaID}".PadRight(anchoLinea));
                factura.AppendLine(new string('-', anchoLinea));

                // Agregar los detalles de los productos
                decimal totalVenta = 0;
                foreach (DataRow row in detallesVenta.Rows)
                {
                    string producto = row["NombreProducto"].ToString().PadRight(15); // Nombre ajustado a 15 caracteres
                    string cantidad = $"x{Convert.ToInt32(row["Cantidad"])}".PadLeft(4); // Cantidad ajustada a 4 caracteres

                    // Formato de precios con 2 decimales, garantizando que el total no se desborde
                    string precio = $"{Convert.ToDecimal(row["PrecioUnitario"]):C2}".PadLeft(10); // Precio ajustado a 10 caracteres
                    string subtotal = $"{Convert.ToDecimal(row["Subtotal"]):C2}".PadLeft(10); // Subtotal ajustado a 10 caracteres

                    // Producto y precio en una línea
                    string lineaProducto = $"{producto}{cantidad} {precio}";
                    factura.AppendLine(lineaProducto);

                    // Subtotal en la siguiente línea
                    string lineaSubtotal = $"Subtotal: {subtotal}";
                    factura.AppendLine(lineaSubtotal);

                    // Sumar al total de la venta
                    totalVenta += Convert.ToDecimal(row["Subtotal"]);
                }

                // Calcular y agregar el total
                factura.AppendLine(new string('-', anchoLinea));
                factura.AppendLine($"Total: {totalVenta:C2}".PadLeft(anchoLinea - 5)); // Total alineado a la derecha
                factura.AppendLine();

                // Mensaje final centrado
                factura.AppendLine("Gracias por su compra!".PadLeft((anchoLinea + "Gracias por su compra!".Length) / 2));

                // Guardar la factura en un archivo de texto
                string rutaArchivo = $@"C:\Facturas\Factura_{ventaID}.txt";

                // Crear el directorio si no existe
                string directorio = System.IO.Path.GetDirectoryName(rutaArchivo);
                if (!System.IO.Directory.Exists(directorio))
                {
                    System.IO.Directory.CreateDirectory(directorio);
                }

                // Escribir el archivo
                System.IO.File.WriteAllText(rutaArchivo, factura.ToString(), Encoding.UTF8);

                // Mostrar mensaje de éxito
                MessageBox.Show($"Factura guardada exitosamente en:\n{rutaArchivo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private DataTable ObtenerDetallesVenta(int ventaID)
        {
            DataTable detallesVenta = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
            SELECT 
                dv.ProductoID,
                i.Nombre AS NombreProducto, 
                dv.Cantidad, 
                dv.PrecioUnitario,
                (dv.Cantidad * dv.PrecioUnitario) AS Subtotal
            FROM DetalleVentas dv
            INNER JOIN Inventario i ON dv.ProductoID = i.ProductoID
            WHERE dv.VentaID = @VentaID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", ventaID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(detallesVenta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los detalles de la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return detallesVenta;
        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
