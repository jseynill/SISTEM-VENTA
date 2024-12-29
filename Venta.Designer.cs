namespace WOLFSFITNESSMARKET
{
    partial class Venta
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtpreciodeventa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsuarioNombre = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductoID = new System.Windows.Forms.TextBox();
            this.dgvDetalleVentas = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.TipodePago = new System.Windows.Forms.Label();
            this.txtUsuario1 = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.Label();
            this.txtClienteID = new System.Windows.Forms.TextBox();
            this.Proveedor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtpreciocosto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1215, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 73;
            this.button2.Text = "Buscar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 72;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(911, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 16);
            this.label6.TabIndex = 71;
            this.label6.Text = "Precio de venta";
            // 
            // txtpreciodeventa
            // 
            this.txtpreciodeventa.Enabled = false;
            this.txtpreciodeventa.Location = new System.Drawing.Point(912, 227);
            this.txtpreciodeventa.Margin = new System.Windows.Forms.Padding(4);
            this.txtpreciodeventa.Name = "txtpreciodeventa";
            this.txtpreciodeventa.Size = new System.Drawing.Size(132, 22);
            this.txtpreciodeventa.TabIndex = 70;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(1152, 593);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 29);
            this.label4.TabIndex = 69;
            this.label4.Text = "Total:";
            // 
            // txtUsuarioNombre
            // 
            this.txtUsuarioNombre.Enabled = false;
            this.txtUsuarioNombre.Location = new System.Drawing.Point(125, 227);
            this.txtUsuarioNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuarioNombre.Name = "txtUsuarioNombre";
            this.txtUsuarioNombre.Size = new System.Drawing.Size(132, 22);
            this.txtUsuarioNombre.TabIndex = 68;
            this.txtUsuarioNombre.Text = "Administrador";
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(132, 162);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(105, 22);
            this.txtCliente.TabIndex = 67;
            this.txtCliente.Text = " ";
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
            this.cmbTipoPago.Location = new System.Drawing.Point(382, 160);
            this.cmbTipoPago.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(147, 24);
            this.cmbTipoPago.TabIndex = 66;
            this.cmbTipoPago.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPago_SelectedIndexChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(194, 598);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 65;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(73, 598);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 28);
            this.btnGuardar.TabIndex = 64;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(1236, 52);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 39);
            this.btnEliminar.TabIndex = 63;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(1128, 52);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 39);
            this.btnAgregar.TabIndex = 62;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(909, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "Nombre ";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(913, 165);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(132, 22);
            this.txtNombre.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(755, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(759, 227);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(132, 22);
            this.txtCantidad.TabIndex = 58;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(755, 144);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 57;
            this.label1.Text = "ID de Producto";
            // 
            // txtProductoID
            // 
            this.txtProductoID.Location = new System.Drawing.Point(759, 164);
            this.txtProductoID.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductoID.Name = "txtProductoID";
            this.txtProductoID.Size = new System.Drawing.Size(132, 22);
            this.txtProductoID.TabIndex = 56;
            // 
            // dgvDetalleVentas
            // 
            this.dgvDetalleVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleVentas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDetalleVentas.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVentas.Location = new System.Drawing.Point(74, 275);
            this.dgvDetalleVentas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDetalleVentas.Name = "dgvDetalleVentas";
            this.dgvDetalleVentas.ReadOnly = true;
            this.dgvDetalleVentas.RowHeadersWidth = 51;
            this.dgvDetalleVentas.Size = new System.Drawing.Size(1335, 314);
            this.dgvDetalleVentas.TabIndex = 55;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(1228, 593);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(62, 29);
            this.lblTotal.TabIndex = 54;
            this.lblTotal.Text = "0.00";
            // 
            // TipodePago
            // 
            this.TipodePago.AutoSize = true;
            this.TipodePago.Location = new System.Drawing.Point(381, 140);
            this.TipodePago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TipodePago.Name = "TipodePago";
            this.TipodePago.Size = new System.Drawing.Size(90, 16);
            this.TipodePago.TabIndex = 53;
            this.TipodePago.Text = "Tipo de Pago";
            // 
            // txtUsuario1
            // 
            this.txtUsuario1.Enabled = false;
            this.txtUsuario1.Location = new System.Drawing.Point(73, 227);
            this.txtUsuario1.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario1.Name = "txtUsuario1";
            this.txtUsuario1.Size = new System.Drawing.Size(44, 22);
            this.txtUsuario1.TabIndex = 52;
            this.txtUsuario1.Text = "4";
            // 
            // Usuario
            // 
            this.Usuario.AutoSize = true;
            this.Usuario.Location = new System.Drawing.Point(70, 205);
            this.Usuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(54, 16);
            this.Usuario.TabIndex = 51;
            this.Usuario.Text = "Usuario";
            // 
            // txtClienteID
            // 
            this.txtClienteID.Enabled = false;
            this.txtClienteID.Location = new System.Drawing.Point(74, 162);
            this.txtClienteID.Margin = new System.Windows.Forms.Padding(4);
            this.txtClienteID.Name = "txtClienteID";
            this.txtClienteID.Size = new System.Drawing.Size(50, 22);
            this.txtClienteID.TabIndex = 50;
            // 
            // Proveedor
            // 
            this.Proveedor.AutoSize = true;
            this.Proveedor.Location = new System.Drawing.Point(70, 142);
            this.Proveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.Size = new System.Drawing.Size(123, 16);
            this.Proveedor.TabIndex = 49;
            this.Proveedor.Text = "Seleccionar Cliente";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 38);
            this.label5.TabIndex = 47;
            this.label5.Text = "Realizar venta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1074, 207);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 75;
            this.label7.Text = "Desuento";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1078, 227);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 74;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1229, 207);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 16);
            this.label8.TabIndex = 77;
            this.label8.Text = "Margen de Ganancias";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(1233, 227);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 76;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1074, 146);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 16);
            this.label9.TabIndex = 79;
            this.label9.Text = "Precio de compra";
            // 
            // txtpreciocosto
            // 
            this.txtpreciocosto.Enabled = false;
            this.txtpreciocosto.Location = new System.Drawing.Point(1077, 166);
            this.txtpreciocosto.Margin = new System.Windows.Forms.Padding(4);
            this.txtpreciocosto.Name = "txtpreciocosto";
            this.txtpreciocosto.Size = new System.Drawing.Size(132, 22);
            this.txtpreciocosto.TabIndex = 78;
            // 
            // Venta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 792);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtpreciocosto);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpreciodeventa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsuarioNombre);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProductoID);
            this.Controls.Add(this.dgvDetalleVentas);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.TipodePago);
            this.Controls.Add(this.txtUsuario1);
            this.Controls.Add(this.Usuario);
            this.Controls.Add(this.txtClienteID);
            this.Controls.Add(this.Proveedor);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Venta";
            this.Text = "Ventas";
            this.Load += new System.EventHandler(this.Venta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsuarioNombre;
        public System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtProductoID;
        private System.Windows.Forms.DataGridView dgvDetalleVentas;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label TipodePago;
        private System.Windows.Forms.TextBox txtUsuario1;
        private System.Windows.Forms.Label Usuario;
        public System.Windows.Forms.TextBox txtClienteID;
        private System.Windows.Forms.Label Proveedor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox txtpreciodeventa;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtpreciocosto;
    }
}