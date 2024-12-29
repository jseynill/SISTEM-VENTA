using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace WOLFSFITNESSMARKET
{
    public partial class Compras : Form
    {
        public Compras()
        {
            InitializeComponent();

            // Permitir que el formulario capture teclas
            this.KeyPreview = true;

            // Asociar el evento KeyDown al formulario
            this.KeyDown += new KeyEventHandler(Compras_KeyDown);
        }

        public class DetalleCompra
        {
            public int ProductoID { get; set; }
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal PrecioVenta { get; set; } // Nuevo campo
            public decimal Subtotal => Cantidad * PrecioUnitario;
        }

       

        // Variables globales
        private List<DetalleCompra> detalleCompras1 = new List<DetalleCompra>();
        private decimal totalCompra = 0;
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        private void Compras_Load(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione un tipo de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar ID de producto
                if (string.IsNullOrWhiteSpace(txtProductoID.Text) || !int.TryParse(txtProductoID.Text, out int productoID))
                {
                    MessageBox.Show("Por favor, ingrese un ID de producto válido.");
                    txtProductoID.Focus();
                    return;
                }

                // Validar cantidad
                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Por favor, ingrese una cantidad válida.");
                    txtCantidad.Focus();
                    return;
                }

                // Validar precio unitario
                if (string.IsNullOrWhiteSpace(txtPrecioUnitario.Text) || !decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario) || precioUnitario <= 0)
                {
                    MessageBox.Show("Por favor, ingrese un precio unitario válido.");
                    txtPrecioUnitario.Focus();
                    return;
                }

                // Validar precio de venta
                if (string.IsNullOrWhiteSpace(txtpreciodeventa.Text) || !decimal.TryParse(txtpreciodeventa.Text, out decimal precioVenta) || precioVenta <= 0)
                {
                    MessageBox.Show("Por favor, ingrese un precio de venta válido.");
                    txtpreciodeventa.Focus();
                    return;
                }

                // Validar que el precio de venta sea mayor o igual al precio de costo
                if (precioVenta < precioUnitario)
                {
                    MessageBox.Show("El precio de venta no puede ser menor al precio de costo.");
                    txtpreciodeventa.Focus();
                    return;
                }

                // Obtener nombre del producto
                string nombreProducto = ObtenerNombreProducto(productoID);
                if (string.IsNullOrEmpty(nombreProducto))
                {
                    MessageBox.Show("El producto no existe.");
                    txtProductoID.Focus();
                    return;
                }

                // Verificar si el producto ya existe en la lista
                var productoExistente = detalleCompras1.FirstOrDefault(p => p.ProductoID == productoID);

                if (productoExistente != null)
                {
                    // Si ya existe, sumar la cantidad y actualizar los precios
                    productoExistente.Cantidad += cantidad;
                    productoExistente.PrecioUnitario = precioUnitario;
                    productoExistente.PrecioVenta = precioVenta;
                }
                else
                {
                    // Si no existe, agregar un nuevo detalle
                    var detalle = new DetalleCompra
                    {
                        ProductoID = productoID,
                        Nombre = nombreProducto,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnitario,
                        PrecioVenta = precioVenta
                    };
                    detalleCompras1.Add(detalle);
                }

                // Actualizar el total de la compra
                totalCompra = detalleCompras1.Sum(p => p.Subtotal);

                // Actualizar interfaz
                ActualizarDataGrid();

                // Formatear y mostrar el total en el Label
                lblTotal.Text = "$" + totalCompra.ToString("#,##0.00", System.Globalization.CultureInfo.InvariantCulture);

                // Limpiar los campos de entrada
                LimpiarCamposProducto();
                cmbTipoPago.Enabled = false;
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message);
            }
        }

        private void LimpiarCamposProducto()
        {
            txtProductoID.Clear();
            txtCantidad.Clear();
            txtPrecioUnitario.Clear();
            txtpreciodeventa.Clear();
            txtProductoID.Focus();
        }

        private string ObtenerNombreProducto(int productoID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Nombre FROM Inventario WHERE ProductoID = @ProductoID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductoID", productoID);
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private void ActualizarDataGrid()
        {
            dgvDetalleCompras.DataSource = null;  // Limpia el DataSource para evitar problemas de duplicado
            dgvDetalleCompras.DataSource = detalleCompras1;  // Asigna la lista correcta
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleCompras.CurrentRow != null)
            {
                var detalle = (DetalleCompra)dgvDetalleCompras.CurrentRow.DataBoundItem;
                detalleCompras1.Remove(detalle);
                totalCompra -= detalle.Subtotal;

                ActualizarDataGrid();
                lblTotal.Text = totalCompra.ToString("C2");
            }
        }

        private void LimpiarFormulario()
        {
            txtProveedorID.Clear();
            txtProveedor.Clear();
            cmbTipoPago.SelectedIndex = -1;
            lblTotal.Text = "0.00";
            detalleCompras1.Clear();
            ActualizarDataGrid();
            totalCompra = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProveedorID.Text) || string.IsNullOrEmpty(txtUsuario1.Text) || cmbTipoPago.SelectedIndex == -1 || detalleCompras1.Count == 0)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de finalizar la compra.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Cambiar la consulta para manejar la tabla temporal
                    string queryCompra = @"
                        DECLARE @InsertedIds TABLE (CompraID INT);
                        INSERT INTO Compras (ProveedorID, UsuarioID, FechaCompra, TotalCompra, TipoPago, Estado)
                        OUTPUT INSERTED.CompraID INTO @InsertedIds
                        VALUES (@ProveedorID, @UsuarioID, GETDATE(), @TotalCompra, @TipoPago, 'Pendiente');
                        SELECT CompraID FROM @InsertedIds;";

                    SqlCommand cmdCompra = new SqlCommand(queryCompra, conn, transaction);
                    cmdCompra.Parameters.AddWithValue("@ProveedorID", int.Parse(txtProveedorID.Text));
                    cmdCompra.Parameters.AddWithValue("@UsuarioID", int.Parse(txtUsuario1.Text));
                    cmdCompra.Parameters.AddWithValue("@TotalCompra", totalCompra);
                    cmdCompra.Parameters.AddWithValue("@TipoPago", cmbTipoPago.SelectedItem.ToString());

                    // Obtener el ID de la compra desde el SELECT final
                    int compraID = (int)cmdCompra.ExecuteScalar();

                    // Insertar detalles de la compra
                    foreach (var detalle in detalleCompras1)
                    {
                        string queryDetalle = "INSERT INTO DetalleCompras (CompraID, ProductoID, Cantidad, PrecioUnitario, PrecioVenta) VALUES (@CompraID, @ProductoID, @Cantidad, @PrecioUnitario, @PrecioVenta)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conn, transaction);
                        cmdDetalle.Parameters.AddWithValue("@CompraID", compraID);
                        cmdDetalle.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                        cmdDetalle.Parameters.AddWithValue("@PrecioVenta", detalle.PrecioVenta);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Compra registrada exitosamente.");
                    cmbTipoPago.Enabled = true;
                    button1.Enabled = true;
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al guardar la compra: " + ex.Message);
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cmbTipoPago.Enabled = true;
        }

        private void Compras_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Si un cuadro de mensaje está activo, no ejecutar el botón Guardar
                if (ActiveForm != this)
                {
                    e.Handled = true; // Marcar como manejado
                    e.SuppressKeyPress = true; // Suprimir el evento
                    return;
                }

                // Simular clic en el botón "Guardar"
                btnGuardar.PerformClick();

                // Marcar el evento como manejado
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
          

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vistabuscarproveedores vistaprovedoresbcs = new vistabuscarproveedores
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaprovedoresbcs.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vistabuscarproducto vistaproductobcs = new vistabuscarproducto
            {
                Owner = this // Establecer el formulario principal como propietario
            };
            vistaproductobcs.ShowDialog();
        }
    }
}
