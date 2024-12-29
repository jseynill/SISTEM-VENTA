-- Tabla de Usuarios: almacena la información de los usuarios que pueden acceder al sistema.
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada usuario.
    Nombre NVARCHAR(100) NOT NULL, -- Nombre completo del usuario.
    Correo NVARCHAR(100) UNIQUE NOT NULL, -- Correo electrónico único para identificar al usuario.
    Contrasena NVARCHAR(100) NOT NULL, -- Contraseña de acceso al sistema.
    Rol NVARCHAR(50) NOT NULL, -- Rol del usuario (Ej.: 'Administrador', 'Vendedor').
    FechaCreacion DATETIME DEFAULT GETDATE() -- Fecha de creación del registro.
);

-- Tabla de Proveedores: almacena los datos de los proveedores de productos.
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada proveedor.
    Nombre NVARCHAR(100) NOT NULL, -- Nombre del proveedor.
    Direccion NVARCHAR(200), -- Dirección del proveedor.
    Telefono NVARCHAR(15), -- Teléfono de contacto del proveedor.
    Correo NVARCHAR(100) UNIQUE, -- Correo electrónico único del proveedor.
    FechaRegistro DATETIME DEFAULT GETDATE() -- Fecha en que se registra el proveedor en el sistema.
);

-- Tabla de Clientes: almacena los datos de los clientes que realizan compras.
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada cliente.
    Nombre NVARCHAR(100) NOT NULL, -- Nombre del cliente.
    Direccion NVARCHAR(200), -- Dirección del cliente.
    Telefono NVARCHAR(15), -- Teléfono de contacto del cliente.
    Correo NVARCHAR(100) UNIQUE, -- Correo electrónico único del cliente.
    FechaRegistro DATETIME DEFAULT GETDATE() -- Fecha en que se registra el cliente en el sistema.
);

-- Tabla de Inventario: almacena los productos disponibles en el inventario.
CREATE TABLE Inventario (
    ProductoID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada producto.
    Nombre NVARCHAR(100) NOT NULL, -- Nombre del producto.
    Descripcion NVARCHAR(255), -- Descripción del producto.
    PrecioCosto DECIMAL(18, 2) NOT NULL, -- Precio de costo del producto.
    PrecioVenta DECIMAL(18, 2) NOT NULL, -- Precio de venta al cliente.
    StockActual INT DEFAULT 0, -- Cantidad actual en el inventario.
    StockMinimo INT DEFAULT 0, -- Cantidad mínima requerida para evitar desabastecimiento.
    FechaIngreso DATETIME DEFAULT GETDATE() -- Fecha en que el producto ingresa al inventario.
);

-- Tabla de Compras: registra las compras realizadas a proveedores.
CREATE TABLE Compras (
    CompraID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada compra.
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID), -- Relación con el proveedor.
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID), -- Relación con el usuario que realiza la compra.
    FechaCompra DATETIME DEFAULT GETDATE(), -- Fecha de la compra.
    TotalCompra DECIMAL(18, 2) NOT NULL, -- Monto total de la compra.
    TipoPago NVARCHAR(50) NOT NULL, -- Tipo de pago (Ej.: 'Contado', 'Crédito').
    Estado NVARCHAR(50) DEFAULT 'Pendiente' -- Estado de la compra (Ej.: 'Pendiente', 'Pagado').
);

-- Tabla de DetalleCompras: detalle de los productos comprados en cada compra.
CREATE TABLE DetalleCompras (
    DetalleCompraID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada detalle de compra.
    CompraID INT FOREIGN KEY REFERENCES Compras(CompraID) ON DELETE CASCADE, -- Relación con la compra.
    ProductoID INT FOREIGN KEY REFERENCES Inventario(ProductoID), -- Relación con el producto comprado.
    Cantidad INT NOT NULL, -- Cantidad de producto comprado.
    PrecioUnitario DECIMAL(18, 2) NOT NULL, -- Precio unitario de cada producto.
    Subtotal AS (Cantidad * PrecioUnitario), -- Subtotal calculado automáticamente.
	PrecioVenta DECIMAL(18, 2)
);

-- Tabla de Ventas: registra las ventas realizadas a los clientes.
CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada venta.
    ClienteID INT FOREIGN KEY REFERENCES Clientes(ClienteID), -- Relación con el cliente.
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID), -- Relación con el usuario que realiza la venta.
    FechaVenta DATETIME DEFAULT GETDATE(), -- Fecha de la venta.
    TotalVenta DECIMAL(18, 2) NOT NULL, -- Monto total de la venta.
    Descuento DECIMAL(5, 2) DEFAULT 0.00, -- Porcentaje de descuento aplicado.
    TipoPago NVARCHAR(50) NOT NULL, -- Tipo de pago (Ej.: 'Contado', 'Crédito').
    Estado NVARCHAR(50) DEFAULT 'Pendiente',-- Estado de la venta (Ej.: 'Pendiente', 'Cerrado').
	Ganancia DECIMAL(18, 2) DEFAULT 0.00,
	 GananciaTotalProyectada DECIMAL(18, 2) DEFAULT 0

);




-- Tabla de DetalleVentas: detalle de los productos vendidos en cada venta.
CREATE TABLE DetalleVentas (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada detalle de venta.
    VentaID INT FOREIGN KEY REFERENCES Ventas(VentaID) ON DELETE CASCADE, -- Relación con la venta.
    ProductoID INT FOREIGN KEY REFERENCES Inventario(ProductoID), -- Relación con el producto vendido.
    Cantidad INT NOT NULL, -- Cantidad de producto vendido.
    PrecioUnitario DECIMAL(18, 2) NOT NULL, -- Precio unitario de cada producto.
    Subtotal AS (Cantidad * PrecioUnitario) -- Subtotal calculado automáticamente.
);

-- Tabla de CuentasPorPagar: gestiona las cuentas pendientes de las compras a crédito.
CREATE TABLE CuentasPorPagar (
    CuentaPagarID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada cuenta por pagar.
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID), -- Relación con el proveedor.
    CompraID INT FOREIGN KEY REFERENCES Compras(CompraID), -- Relación con la compra a crédito.
    Monto DECIMAL(18, 2) NOT NULL, -- Monto total de la cuenta.
    SaldoPendiente DECIMAL(18, 2) NOT NULL, -- Saldo pendiente de pago.
    FechaEmision DATETIME DEFAULT GETDATE(), -- Fecha de emisión de la cuenta.
    FechaVencimiento DATETIME NOT NULL, -- Fecha límite para el pago.
    Estado NVARCHAR(50) DEFAULT 'Pendiente' -- Estado de la cuenta (Ej.: 'Pendiente', 'Pagado').
);

-- Tabla de AbonosCuentasPorPagar: registra los abonos realizados en cuentas por pagar.
CREATE TABLE AbonosCuentasPorPagar (
    AbonoID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada abono.
    CuentaPagarID INT FOREIGN KEY REFERENCES CuentasPorPagar(CuentaPagarID) ON DELETE CASCADE, -- Relación con la cuenta por pagar.
    MontoAbono DECIMAL(18, 2) NOT NULL, -- Monto del abono realizado.
    FechaAbono DATETIME DEFAULT GETDATE() -- Fecha en que se realiza el abono.
);

-- Tabla de CuentasPorCobrar: gestiona las cuentas pendientes de las ventas a crédito.
CREATE TABLE CuentasPorCobrar (
    CuentaCobrarID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada cuenta por cobrar.
    ClienteID INT FOREIGN KEY REFERENCES Clientes(ClienteID), -- Relación con el cliente.
    VentaID INT FOREIGN KEY REFERENCES Ventas(VentaID), -- Relación con la venta a crédito.
    Monto DECIMAL(18, 2) NOT NULL, -- Monto total de la cuenta.
    SaldoPendiente DECIMAL(18, 2) NOT NULL, -- Saldo pendiente de cobro.
    FechaEmision DATETIME DEFAULT GETDATE(), -- Fecha de emisión de la cuenta.
    FechaVencimiento DATETIME NOT NULL, -- Fecha límite para el cobro.
    Estado NVARCHAR(50) DEFAULT 'Pendiente' -- Estado de la cuenta (Ej.: 'Pendiente', 'Pagado').
);

-- Tabla de AbonosCuentasPorCobrar: registra los abonos realizados en cuentas por cobrar.
CREATE TABLE AbonosCuentasPorCobrar (
    AbonoID INT PRIMARY KEY IDENTITY(1,1), -- Identificador único para cada abono.
    CuentaCobrarID INT FOREIGN KEY REFERENCES CuentasPorCobrar(CuentaCobrarID) ON DELETE CASCADE, -- Relación con la cuenta por cobrar.
    MontoAbono DECIMAL(18, 2) NOT NULL, -- Monto del abono realizado.
    FechaAbono DATETIME DEFAULT GETDATE() -- Fecha en que se realiza el abono.
);

CREATE TRIGGER tr_AfterInsertDetalleCompras
ON DetalleCompras
AFTER INSERT
AS
BEGIN
    -- Incrementa el stock actual del producto y actualiza el precio de costo y precio de venta.
    UPDATE Inventario
    SET 
        StockActual = StockActual + i.Cantidad,
        PrecioCosto = i.PrecioUnitario,
        PrecioVenta = i.PrecioVenta
    FROM Inventario AS inv
    INNER JOIN inserted AS i 
        ON inv.ProductoID = i.ProductoID;
END;






-- Trigger para actualizar el saldo y estado de una cuenta por pagar al realizar un abono, y actualizar el estado en Compras.
CREATE  TRIGGER tr_AfterInsertAbonoCuentasPorPagar
ON AbonosCuentasPorPagar
AFTER INSERT
AS
BEGIN
    DECLARE @CuentaPagarID INT, @MontoAbono DECIMAL(18,2), @CompraID INT;

    -- Obtiene los valores insertados.
    SELECT @CuentaPagarID = CuentaPagarID, @MontoAbono = MontoAbono FROM inserted;

    -- Actualiza el saldo pendiente en la cuenta por pagar.
    UPDATE CuentasPorPagar
    SET SaldoPendiente = SaldoPendiente - @MontoAbono
    WHERE CuentaPagarID = @CuentaPagarID;

    -- Si el saldo pendiente llega a cero, actualiza el estado a 'Pagado' en CuentasPorPagar.
    UPDATE CuentasPorPagar
    SET Estado = 'Pagado'
    WHERE CuentaPagarID = @CuentaPagarID AND SaldoPendiente <= 0;

    -- Obtiene el ID de la compra asociada para actualizar el estado en la tabla Compras.
    SELECT @CompraID = CompraID FROM CuentasPorPagar WHERE CuentaPagarID = @CuentaPagarID;

    -- Actualiza el estado en la tabla Compras si el saldo pendiente es cero.
    IF EXISTS (SELECT 1 FROM CuentasPorPagar WHERE CuentaPagarID = @CuentaPagarID AND SaldoPendiente <= 0)
    BEGIN
        UPDATE Compras
        SET Estado = 'Pagado'
        WHERE CompraID = @CompraID;
    END
    ELSE
    BEGIN
        UPDATE Compras
        SET Estado = 'Pendiente'
        WHERE CompraID = @CompraID;
    END
END;






----------------------------------------------------------------------------------------------------------------------------------------
-- este trigger no es activo 
CREATE TRIGGER trg_ActualizarEstadoCompra
ON Compras
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar registros de cuentas por pagar para compras a crédito
    INSERT INTO CuentasPorPagar (ProveedorID, CompraID, Monto, SaldoPendiente, FechaEmision, FechaVencimiento, Estado)
    SELECT 
        i.ProveedorID, 
        i.CompraID,
        i.TotalCompra,
        i.TotalCompra,
        i.FechaCompra,
        DATEADD(DAY, 
                CASE 
                    WHEN i.TipoPago = '15 Dias' THEN 15
                    WHEN i.TipoPago = '30 Dias' THEN 30
                    WHEN i.TipoPago = '45 Dias' THEN 45
                    ELSE 0 -- Para evitar errores si no se especifica un tipo válido
                END, 
                i.FechaCompra),
        'Pendiente'
    FROM 
        inserted i
    WHERE 
        i.TipoPago IN ('15 Dias', '30 Dias', '45 Dias');

    -- Actualizar el estado de compras a 'Cerrado' para pagos de contado
    UPDATE Compras
    SET Estado = 'Pagado'
    FROM Compras c
    INNER JOIN inserted i ON c.CompraID = i.CompraID
    WHERE i.TipoPago = 'Contado';
END;
------------------------------------------------------------------------------------------------------



CREATE  TRIGGER trg_AfterInsert_Compra
ON Compras
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inserta en CuentasPorPagar si el TipoPago es diferente de 'Contado'
    INSERT INTO CuentasPorPagar (ProveedorID, CompraID, Monto, SaldoPendiente, FechaVencimiento, Estado)
    SELECT 
        i.ProveedorID,
        i.CompraID,
        i.TotalCompra,
        i.TotalCompra,
        CASE 
		    WHEN i.TipoPago = 'Credito' THEN DATEADD(DAY, 01, GETDATE())
            WHEN i.TipoPago = '15 Dias' THEN DATEADD(DAY, 15, GETDATE())
            WHEN i.TipoPago = '30 Dias' THEN DATEADD(DAY, 30, GETDATE())
            WHEN i.TipoPago = '40 Dias' THEN DATEADD(DAY, 40, GETDATE())
        END AS FechaVencimiento,
        'Pendiente'
    FROM INSERTED i
    WHERE i.TipoPago IN ('Credito', '15 Dias', '30 Dias', '40 Dias');

    -- Actualiza el estado de la compra según el TipoPago
    UPDATE Compras
    SET Estado = CASE 
                    WHEN i.TipoPago = 'Contado' THEN 'Pagado'
                    ELSE 'Pendiente'
                 END
    FROM Compras c
    INNER JOIN INSERTED i ON c.CompraID = i.CompraID;
END;


CREATE TRIGGER trg_ActualizarCuentasPorPagar
ON AbonosCuentasPorPagar
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el saldo pendiente en las cuentas afectadas
    UPDATE CuentasPorPagar
    SET SaldoPendiente = SaldoPendiente - Abonos.MontoAbono
    FROM CuentasPorPagar AS Cuentas
    INNER JOIN inserted AS Abonos
    ON Cuentas.CuentaPagarID = Abonos.CuentaPagarID;

    -- Verificar y marcar como saldadas las cuentas con saldo pendiente cero
    UPDATE CuentasPorPagar
    SET Estado = 'Pagado'
    FROM CuentasPorPagar
    WHERE SaldoPendiente = 0 AND CuentaPagarID IN (SELECT CuentaPagarID FROM inserted);

    -- Marcar como cerradas las compras relacionadas con cuentas saldadas
    UPDATE Compras
    SET Estado = 'Pagado'
    FROM Compras
    INNER JOIN CuentasPorPagar
    ON Compras.CompraID = CuentasPorPagar.CompraID
    WHERE CuentasPorPagar.SaldoPendiente = 0 
    AND CuentasPorPagar.CuentaPagarID IN (SELECT CuentaPagarID FROM inserted);
END;



CREATE TRIGGER trg_AfterInsert_Venta
ON Ventas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inserta en CuentasPorCobrar si el TipoPago es diferente de 'Contado'
    INSERT INTO CuentasPorCobrar (ClienteID, VentaID, Monto, SaldoPendiente, FechaVencimiento, Estado)
    SELECT 
        i.ClienteID,
        i.VentaID,
        i.TotalVenta,
        i.TotalVenta,
        CASE 
            WHEN i.TipoPago = 'Credito' THEN DATEADD(DAY, 1, GETDATE())
            WHEN i.TipoPago = '15 Dias' THEN DATEADD(DAY, 15, GETDATE())
            WHEN i.TipoPago = '30 Dias' THEN DATEADD(DAY, 30, GETDATE())
            WHEN i.TipoPago = '40 Dias' THEN DATEADD(DAY, 40, GETDATE())
        END AS FechaVencimiento,
        'Pendiente'
    FROM INSERTED i
    WHERE i.TipoPago IN ('Credito', '15 Dias', '30 Dias', '40 Dias');

    -- Actualiza el estado de la venta según el TipoPago
    UPDATE Ventas
    SET Estado = CASE 
                    WHEN i.TipoPago = 'Contado' THEN 'Pagado'
                    ELSE 'Pendiente'
                 END
    FROM Ventas v
    INNER JOIN INSERTED i ON v.VentaID = i.VentaID;
END;

----------------------------------------------------------------



---------------------------------------------------------------------------------
---Nueva logica--

CREATE TRIGGER TRG_AbonoCuentasPorCobrar
ON AbonosCuentasPorCobrar
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declaración de variables
    DECLARE @CuentaCobrarID INT;
    DECLARE @MontoAbono DECIMAL(18, 2);
    DECLARE @SaldoPendiente DECIMAL(18, 2);
    DECLARE @VentaID INT;
    DECLARE @GananciaProyectada DECIMAL(18, 2);
    DECLARE @TotalVenta DECIMAL(18, 2);

    -- Obtener los datos del abono insertado
    SELECT 
        @CuentaCobrarID = CuentaCobrarID,
        @MontoAbono = MontoAbono
    FROM INSERTED;

    -- Verificar que el abono sea mayor que 0
    IF @MontoAbono <= 0
    BEGIN
        THROW 50000, 'El monto del abono debe ser mayor a 0.', 1;
    END;

    -- Obtener el saldo pendiente, VentaID, TotalVenta y GananciaProyectada asociados
    SELECT 
        @SaldoPendiente = SaldoPendiente,
        @VentaID = VentaID
    FROM CuentasPorCobrar
    WHERE CuentaCobrarID = @CuentaCobrarID;

    SELECT 
        @TotalVenta = TotalVenta,
        @GananciaProyectada = GananciaTotalProyectada
    FROM Ventas
    WHERE VentaID = @VentaID;

    -- Verificar que el abono no exceda el saldo pendiente
    IF @MontoAbono > @SaldoPendiente
    BEGIN
        THROW 50001, 'El monto del abono no puede exceder el saldo pendiente.', 1;
    END;

    -- Calcular la proporción del abono que corresponde a la ganancia
    DECLARE @GananciaAbono DECIMAL(18, 2) = (@MontoAbono / @TotalVenta) * @GananciaProyectada;

    -- Actualizar el saldo pendiente en la tabla CuentasPorCobrar
    UPDATE CuentasPorCobrar
    SET SaldoPendiente = SaldoPendiente - @MontoAbono
    WHERE CuentaCobrarID = @CuentaCobrarID;

    -- Actualizar la ganancia acumulada en la tabla Ventas
    UPDATE Ventas
    SET Ganancia = Ganancia + @GananciaAbono
    WHERE VentaID = @VentaID;

    -- Si el saldo pendiente llega a 0, marcar la cuenta y la venta como cerradas
    IF (@SaldoPendiente - @MontoAbono) = 0
    BEGIN
        UPDATE CuentasPorCobrar
        SET Estado = 'Pagado'
        WHERE CuentaCobrarID = @CuentaCobrarID;

        UPDATE Ventas
        SET Estado = 'Pagado'
        WHERE VentaID = @VentaID;
    END;
END;
GO


