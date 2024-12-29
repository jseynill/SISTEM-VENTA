using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOLFSFITNESSMARKET
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

          
          

            // Crear y mostrar el formulario hijo
            Logofijocs hijo = new Logofijocs            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            hijo.Show();
        }
        

        private void Form131_Load(object sender, EventArgs e)
        {
           
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear y mostrar el formulario hijo
          
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void inventarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void agregarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void agregarNuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear y mostrar el formulario hijo
            Inventary inventario = new Inventary
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            inventario.Show();
        }

        private void comprarProductoExistenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras comapra = new Compras
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            comapra.Show();
        }

        private void inventarioToolStripMenuItem3_Click(object sender, EventArgs e)
        {
             vistainventario vsintentario = new vistainventario
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            vsintentario.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cuentasPorPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
             CuentasPagar Cuentaspagar = new CuentasPagar
             {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            Cuentaspagar.Show();
        }

        private void cuentasPorPagarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            abonarcuentasporpagar abonaaruentaspagar = new abonarcuentasporpagar
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            abonaaruentaspagar.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void infromesGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Informes informeg = new Informes
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            informeg.Show();
        }

        private void cuentasPorCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CuentasCobrar Cuentascobrar = new CuentasCobrar
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            Cuentascobrar.Show();
        }

        private void cuentasPorPagarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            abonarcuentasporcobra abonaaruentascobrar = new abonarcuentasporcobra
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            abonaaruentascobrar.Show();
        }

        private void graficosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart chRT = new chart
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            chRT.Show();
        }

        private void detallesDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetallesVenta detaventa = new DetallesVenta
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            detaventa.Show();
        }

        private void detallesDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetallesCompra detacompra = new DetallesCompra
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            detacompra.Show();
        }

        private void realizarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Venta ventap = new Venta
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            ventap.Show();
        }

        private void gestionarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cliente clientes = new Cliente
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            clientes.Show();
        }

        private void gestionarProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear y mostrar el formulario hijo
            Proveedore proveedores = new Proveedore
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            proveedores.Show();
        }

        private void gestionarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario usuarios = new Usuario
            {
                MdiParent = this, // Establecer el formulario principal como contenedor
                StartPosition = FormStartPosition.Manual,
                Location = new Point(0, this.MainMenuStrip != null ? this.MainMenuStrip.Height : 0) // Posicionarlo debajo del menú
            };

            usuarios.Show();
        }
    }
}
