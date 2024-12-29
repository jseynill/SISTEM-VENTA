namespace WOLFSFITNESSMARKET
{
    partial class Compras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.Proveedor = new System.Windows.Forms.Label();
            this.txtProveedorID = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.Label();
            this.txtUsuario1 = new System.Windows.Forms.TextBox();
            this.TipodePago = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtProductoID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.dgvDetalleCompras = new System.Windows.Forms.DataGridView();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.txtUsuarioNombre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtpreciodeventa = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 38);
            this.label5.TabIndex = 16;
            this.label5.Text = "Compras";
            // 
            // Proveedor
            // 
            this.Proveedor.AutoSize = true;
            this.Proveedor.Location = new System.Drawing.Point(64, 97);
            this.Proveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.Size = new System.Drawing.Size(71, 16);
            this.Proveedor.TabIndex = 19;
            this.Proveedor.Text = "Proveedor";
            // 
            // txtProveedorID
            // 
            this.txtProveedorID.Enabled = false;
            this.txtProveedorID.Location = new System.Drawing.Point(68, 117);
            this.txtProveedorID.Margin = new System.Windows.Forms.Padding(4);
            this.txtProveedorID.Name = "txtProveedorID";
            this.txtProveedorID.Size = new System.Drawing.Size(50, 22);
            this.txtProveedorID.TabIndex = 20;
            // 
            // Usuario
            // 
            this.Usuario.AutoSize = true;
            this.Usuario.Location = new System.Drawing.Point(64, 160);
            this.Usuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(54, 16);
            this.Usuario.TabIndex = 21;
            this.Usuario.Text = "Usuario";
            // 
            // txtUsuario1
            // 
            this.txtUsuario1.Enabled = false;
            this.txtUsuario1.Location = new System.Drawing.Point(67, 182);
            this.txtUsuario1.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario1.Name = "txtUsuario1";
            this.txtUsuario1.Size = new System.Drawing.Size(44, 22);
            this.txtUsuario1.TabIndex = 22;
            this.txtUsuario1.Text = "4";
            // 
            // TipodePago
            // 
            this.TipodePago.AutoSize = true;
            this.TipodePago.Location = new System.Drawing.Point(375, 95);
            this.TipodePago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TipodePago.Name = "TipodePago";
            this.TipodePago.Size = new System.Drawing.Size(90, 16);
            this.TipodePago.TabIndex = 23;
            this.TipodePago.Text = "Tipo de Pago";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(1222, 548);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(62, 29);
            this.lblTotal.TabIndex = 25;
            this.lblTotal.Text = "0.00";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // txtProductoID
            // 
            this.txtProductoID.Location = new System.Drawing.Point(753, 119);
            this.txtProductoID.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductoID.Name = "txtProductoID";
            this.txtProductoID.Size = new System.Drawing.Size(132, 22);
            this.txtProductoID.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(749, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "ID de Producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(749, 162);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(753, 182);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(132, 22);
            this.txtCantidad.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(969, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Precio X Unidad";
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.Location = new System.Drawing.Point(973, 119);
            this.txtPrecioUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(132, 22);
            this.txtPrecioUnitario.TabIndex = 31;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(1122, 7);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 39);
            this.btnAgregar.TabIndex = 33;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(1230, 7);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 39);
            this.btnEliminar.TabIndex = 34;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(67, 553);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 28);
            this.btnGuardar.TabIndex = 35;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(188, 553);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 36;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Items.AddRange(new object[] {
            "Contado",
            "Credito",
            "15 Dias",
            "30 Dias",
            "45 Dias"});
            this.cmbTipoPago.Location = new System.Drawing.Point(376, 115);
            this.cmbTipoPago.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(147, 24);
            this.cmbTipoPago.TabIndex = 37;
            // 
            // dgvDetalleCompras
            // 
            this.dgvDetalleCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleCompras.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDetalleCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleCompras.Location = new System.Drawing.Point(68, 230);
            this.dgvDetalleCompras.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDetalleCompras.Name = "dgvDetalleCompras";
            this.dgvDetalleCompras.ReadOnly = true;
            this.dgvDetalleCompras.RowHeadersWidth = 51;
            this.dgvDetalleCompras.Size = new System.Drawing.Size(1335, 314);
            this.dgvDetalleCompras.TabIndex = 26;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Enabled = false;
            this.txtProveedor.Location = new System.Drawing.Point(126, 117);
            this.txtProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(105, 22);
            this.txtProveedor.TabIndex = 38;
            // 
            // txtUsuarioNombre
            // 
            this.txtUsuarioNombre.Enabled = false;
            this.txtUsuarioNombre.Location = new System.Drawing.Point(119, 182);
            this.txtUsuarioNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuarioNombre.Name = "txtUsuarioNombre";
            this.txtUsuarioNombre.Size = new System.Drawing.Size(132, 22);
            this.txtUsuarioNombre.TabIndex = 40;
            this.txtUsuarioNombre.Text = "Administrador";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(1146, 548);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 29);
            this.label4.TabIndex = 41;
            this.label4.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(972, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 16);
            this.label6.TabIndex = 44;
            this.label6.Text = "Precio de venta";
            // 
            // txtpreciodeventa
            // 
            this.txtpreciodeventa.Location = new System.Drawing.Point(973, 182);
            this.txtpreciodeventa.Margin = new System.Windows.Forms.Padding(4);
            this.txtpreciodeventa.Name = "txtpreciodeventa";
            this.txtpreciodeventa.Size = new System.Drawing.Size(132, 22);
            this.txtpreciodeventa.TabIndex = 43;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(891, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 46;
            this.button2.Text = "Buscar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Compras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 792);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpreciodeventa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsuarioNombre);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPrecioUnitario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProductoID);
            this.Controls.Add(this.dgvDetalleCompras);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.TipodePago);
            this.Controls.Add(this.txtUsuario1);
            this.Controls.Add(this.Usuario);
            this.Controls.Add(this.txtProveedorID);
            this.Controls.Add(this.Proveedor);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Compras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras";
            this.Load += new System.EventHandler(this.Compras_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Compras_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView detalleCompras;
        private System.Windows.Forms.Label Proveedor;
        private System.Windows.Forms.Label Usuario;
        private System.Windows.Forms.TextBox txtUsuario1;
        private System.Windows.Forms.Label TipodePago;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.DataGridView dgvDetalleCompras;
        private System.Windows.Forms.TextBox txtUsuarioNombre;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtProveedorID;
        public System.Windows.Forms.TextBox txtProveedor;
        public System.Windows.Forms.TextBox txtProductoID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtpreciodeventa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}