using OfficeOpenXml;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class Informes : Form
    {
        public Informes()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = @"
            SELECT 
                cpp.CuentaPagarID,
                p.Nombre AS Proveedor,
                cpp.Monto AS MontoInicial,
                cpp.SaldoPendiente AS SaldoPendiente,
                cpp.FechaEmision,
                cpp.FechaVencimiento,
                cpp.Estado
            FROM CuentasPorPagar cpp
            INNER JOIN Proveedores p ON cpp.ProveedorID = p.ProveedorID
            WHERE cpp.SaldoPendiente > 0
            ORDER BY cpp.FechaVencimiento ASC";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Cuentas por Pagar");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Informe de Cuentas por Pagar";
                    worksheet.Cells[1, 1, 1, 6].Merge = true; // Fusionar celdas A1:F1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "ID Cuenta";
                    worksheet.Cells[3, 2].Value = "Proveedor";
                    worksheet.Cells[3, 3].Value = "Monto Inicial (DOP)";
                    worksheet.Cells[3, 4].Value = "Saldo Pendiente (DOP)";
                    worksheet.Cells[3, 5].Value = "Fecha Emisión";
                    worksheet.Cells[3, 6].Value = "Fecha Vencimiento";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["CuentaPagarID"];
                        worksheet.Cells[row, 2].Value = reader["Proveedor"];
                        worksheet.Cells[row, 3].Value = Convert.ToDecimal(reader["MontoInicial"]);
                        worksheet.Cells[row, 4].Value = Convert.ToDecimal(reader["SaldoPendiente"]);
                        worksheet.Cells[row, 5].Value = Convert.ToDateTime(reader["FechaEmision"]);
                        worksheet.Cells[row, 6].Value = Convert.ToDateTime(reader["FechaVencimiento"]);

                        // Formatear las columnas
                        worksheet.Cells[row, 3].Style.Numberformat.Format = "DOP #,##0.00";
                        worksheet.Cells[row, 4].Style.Numberformat.Format = "DOP #,##0.00";
                        worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                        worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MM/yyyy";

                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar informe de cuentas por pagar";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Informe de cuentas por pagar generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                

            }
            }
            }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = @"
            SELECT ProveedorID, Nombre, Direccion, Telefono, Correo, FechaRegistro
            FROM Proveedores
            ORDER BY FechaRegistro DESC";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Proveedores");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Listado de Proveedores";
                    worksheet.Cells[1, 1, 1, 6].Merge = true; // Fusionar celdas A1:F1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "Proveedor ID";
                    worksheet.Cells[3, 2].Value = "Nombre";
                    worksheet.Cells[3, 3].Value = "Dirección";
                    worksheet.Cells[3, 4].Value = "Teléfono";
                    worksheet.Cells[3, 5].Value = "Correo Electrónico";
                    worksheet.Cells[3, 6].Value = "Fecha de Registro";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["ProveedorID"];
                        worksheet.Cells[row, 2].Value = reader["Nombre"];
                        worksheet.Cells[row, 3].Value = reader["Direccion"];
                        worksheet.Cells[row, 4].Value = reader["Telefono"];
                        worksheet.Cells[row, 5].Value = reader["Correo"];
                        worksheet.Cells[row, 6].Value = Convert.ToDateTime(reader["FechaRegistro"]);

                        // Formatear columna de fecha
                        worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar listado de proveedores";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Listado de proveedores generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
    }
}

        private void Informes_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = "SELECT ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso FROM Inventario";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Productos");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Listado de Productos";
                    worksheet.Cells[1, 1, 1, 8].Merge = true; // Fusionar celdas A1:H1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "Producto ID";
                    worksheet.Cells[3, 2].Value = "Nombre";
                    worksheet.Cells[3, 3].Value = "Descripción";
                    worksheet.Cells[3, 4].Value = "Precio Costo";
                    worksheet.Cells[3, 5].Value = "Precio Venta";
                    worksheet.Cells[3, 6].Value = "Stock Actual";
                    worksheet.Cells[3, 7].Value = "Stock Mínimo";
                    worksheet.Cells[3, 8].Value = "Fecha Ingreso";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["ProductoID"];
                        worksheet.Cells[row, 2].Value = reader["Nombre"];
                        worksheet.Cells[row, 3].Value = reader["Descripcion"];
                        worksheet.Cells[row, 4].Value = Convert.ToDecimal(reader["PrecioCosto"]);
                        worksheet.Cells[row, 5].Value = Convert.ToDecimal(reader["PrecioVenta"]);
                        worksheet.Cells[row, 6].Value = Convert.ToInt32(reader["StockActual"]);
                        worksheet.Cells[row, 7].Value = Convert.ToInt32(reader["StockMinimo"]);
                        worksheet.Cells[row, 8].Value = Convert.ToDateTime(reader["FechaIngreso"]);

                        // Formatear celdas específicas
                        worksheet.Cells[row, 4, row, 5].Style.Numberformat.Format = "[$RD$-416] #,##0.00"; // Formato de moneda DOP
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "dd/MM/yyyy"; // Formato de fecha
                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar el archivo Excel";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Archivo Excel generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            
        }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = "SELECT ProductoID, Nombre, Descripcion, PrecioCosto, PrecioVenta, StockActual, StockMinimo, FechaIngreso FROM Inventario WHERE StockActual <= 0";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Productos Agotados");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Informe de Productos Agotados";
                    worksheet.Cells[1, 1, 1, 8].Merge = true; // Fusionar celdas A1:H1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "Producto ID";
                    worksheet.Cells[3, 2].Value = "Nombre";
                    worksheet.Cells[3, 3].Value = "Descripción";
                    worksheet.Cells[3, 4].Value = "Precio Costo";
                    worksheet.Cells[3, 5].Value = "Precio Venta";
                    worksheet.Cells[3, 6].Value = "Stock Actual";
                    worksheet.Cells[3, 7].Value = "Stock Mínimo";
                    worksheet.Cells[3, 8].Value = "Fecha Ingreso";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["ProductoID"];
                        worksheet.Cells[row, 2].Value = reader["Nombre"];
                        worksheet.Cells[row, 3].Value = reader["Descripcion"];
                        worksheet.Cells[row, 4].Value = Convert.ToDecimal(reader["PrecioCosto"]);
                        worksheet.Cells[row, 5].Value = Convert.ToDecimal(reader["PrecioVenta"]);
                        worksheet.Cells[row, 6].Value = Convert.ToInt32(reader["StockActual"]);
                        worksheet.Cells[row, 7].Value = Convert.ToInt32(reader["StockMinimo"]);
                        worksheet.Cells[row, 8].Value = Convert.ToDateTime(reader["FechaIngreso"]);

                        // Formatear celdas específicas
                        worksheet.Cells[row, 4, row, 5].Style.Numberformat.Format = "[$RD$-416] #,##0.00"; // Formato de moneda DOP
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "dd/MM/yyyy"; // Formato de fecha
                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar el informe de productos agotados";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Informe generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = @"
            SELECT c.CompraID, p.Nombre AS Proveedor, c.FechaCompra, c.TotalCompra, c.TipoPago, c.Estado, 
                   dc.ProductoID, i.Nombre AS Producto, dc.Cantidad, dc.PrecioUnitario, dc.Subtotal
            FROM Compras c
            INNER JOIN Proveedores p ON c.ProveedorID = p.ProveedorID
            INNER JOIN DetalleCompras dc ON c.CompraID = dc.CompraID
            INNER JOIN Inventario i ON dc.ProductoID = i.ProductoID
            WHERE c.FechaCompra >= DATEADD(DAY, -7, GETDATE())
            ORDER BY c.FechaCompra DESC";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Compras Últimos 7 Días");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Informe de Compras de los Últimos 7 Días";
                    worksheet.Cells[1, 1, 1, 11].Merge = true; // Fusionar celdas A1:K1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "Compra ID";
                    worksheet.Cells[3, 2].Value = "Proveedor";
                    worksheet.Cells[3, 3].Value = "Fecha de Compra";
                    worksheet.Cells[3, 4].Value = "Total Compra";
                    worksheet.Cells[3, 5].Value = "Tipo de Pago";
                    worksheet.Cells[3, 6].Value = "Estado";
                    worksheet.Cells[3, 7].Value = "Producto ID";
                    worksheet.Cells[3, 8].Value = "Producto";
                    worksheet.Cells[3, 9].Value = "Cantidad";
                    worksheet.Cells[3, 10].Value = "Precio Unitario";
                    worksheet.Cells[3, 11].Value = "Subtotal";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["CompraID"];
                        worksheet.Cells[row, 2].Value = reader["Proveedor"];
                        worksheet.Cells[row, 3].Value = Convert.ToDateTime(reader["FechaCompra"]);
                        worksheet.Cells[row, 4].Value = Convert.ToDecimal(reader["TotalCompra"]);
                        worksheet.Cells[row, 5].Value = reader["TipoPago"];
                        worksheet.Cells[row, 6].Value = reader["Estado"];
                        worksheet.Cells[row, 7].Value = reader["ProductoID"];
                        worksheet.Cells[row, 8].Value = reader["Producto"];
                        worksheet.Cells[row, 9].Value = Convert.ToInt32(reader["Cantidad"]);
                        worksheet.Cells[row, 10].Value = Convert.ToDecimal(reader["PrecioUnitario"]);
                        worksheet.Cells[row, 11].Value = Convert.ToDecimal(reader["Subtotal"]);

                        // Formatear celdas específicas
                        worksheet.Cells[row, 3].Style.Numberformat.Format = "dd/MM/yyyy"; // Formato de fecha
                        worksheet.Cells[row, 4, row, 4].Style.Numberformat.Format = "[$RD$-416] #,##0.00"; // Formato moneda
                        worksheet.Cells[row, 10, row, 11].Style.Numberformat.Format = "[$RD$-416] #,##0.00"; // Formato moneda
                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar informe de compras";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Informe generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=JEFFERSON\SQLEXPRESS;Database=WOLFSFITNESSMARKET;Integrated Security=True;";
            string query = @"
            SELECT ClienteID, Nombre, Direccion, Telefono, Correo, FechaRegistro
            FROM Clientes
            ORDER BY FechaRegistro DESC";

            // Crear la conexión y el comando
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Crear el archivo Excel
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Clientes");

                    // Insertar título y formato
                    worksheet.Cells[1, 1].Value = "Listado de Clientes";
                    worksheet.Cells[1, 1, 1, 6].Merge = true; // Fusionar celdas A1:F1
                    worksheet.Cells[1, 1].Style.Font.Size = 18;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Insertar encabezados de columnas
                    worksheet.Cells[3, 1].Value = "Cliente ID";
                    worksheet.Cells[3, 2].Value = "Nombre";
                    worksheet.Cells[3, 3].Value = "Dirección";
                    worksheet.Cells[3, 4].Value = "Teléfono";
                    worksheet.Cells[3, 5].Value = "Correo Electrónico";
                    worksheet.Cells[3, 6].Value = "Fecha de Registro";

                    worksheet.Row(3).Style.Font.Bold = true;

                    int row = 4;

                    // Llenar el Excel con los datos
                    while (reader.Read())
                    {
                        worksheet.Cells[row, 1].Value = reader["ClienteID"];
                        worksheet.Cells[row, 2].Value = reader["Nombre"];
                        worksheet.Cells[row, 3].Value = reader["Direccion"];
                        worksheet.Cells[row, 4].Value = reader["Telefono"];
                        worksheet.Cells[row, 5].Value = reader["Correo"];
                        worksheet.Cells[row, 6].Value = Convert.ToDateTime(reader["FechaRegistro"]);

                        // Formatear columna de fecha
                        worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                        row++;
                    }

                    // Insertar la imagen
                    string imagePath = @"C:\Users\jeffe\OneDrive\Escritorio\SISTEMA-DE-VENTAS-c\Resources\WhatsApp Image 2024-12-02 at 3.24.52 PM.jpeg";
                    if (File.Exists(imagePath))
                    {
                        var tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                        File.Copy(imagePath, tempImagePath, true);

                        var excelImage = worksheet.Drawings.AddPicture("Logo", tempImagePath);
                        excelImage.SetPosition(0, 0, 0, 0); // Posición de la imagen en la celda
                        excelImage.SetSize(100, 100); // Tamaño de la imagen

                        File.Delete(tempImagePath); // Eliminar imagen temporal
                    }

                    // Ajustar ancho de columnas automáticamente
                    worksheet.Cells.AutoFitColumns();

                    // Guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar listado de clientes";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Listado de clientes generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            }
    }
}
