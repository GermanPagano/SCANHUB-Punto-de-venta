using SCANHUB___INVENTARIO_Y_CAJA.Database;
using SCANHUB___INVENTARIO_Y_CAJA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class Dashboard : Form
    {
        private MenuStrip mainMenu;

        public Dashboard()
        {
            InitializeComponent();
            CustomizeForm();
            InitializeCustomComponents();
            ApplyPanelStyles(); // Llamada para aplicar los estilos a los paneles
        }

        private void CustomizeForm()
        {
            this.Text = "SCANHUB - DASHBOARD"; // Establece el título del formulario
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Evita que el formulario sea redimensionable
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
            this.MaximizeBox = false; // Deshabilita el botón de maximizar
            this.FormBorderStyle = FormBorderStyle.None; // Elimina cualquier borde, haciendo que no se pueda redimensionar
            this.BackColor = ColorTranslator.FromHtml("#fff");
        }

        private void InitializeCustomComponents()
        {
            // Configurar el MenuStrip
            mainMenu = new MenuStrip();
            mainMenu.BackColor = ColorTranslator.FromHtml("#ACD4E3"); // Color de fondo
            mainMenu.ForeColor = ColorTranslator.FromHtml("black"); // Color de la tipografía
            mainMenu.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Cambia el tamaño de la fuente a 16
            mainMenu.Dock = DockStyle.Top;
            this.Controls.Add(mainMenu);

            // Agregar items al menú
            ToolStripMenuItem miCuenta = new ToolStripMenuItem("mi cuenta");
            ToolStripMenuItem soporte = new ToolStripMenuItem("soporte");
            ToolStripMenuItem cerrarSesion = new ToolStripMenuItem("cerrar sesión");
            mainMenu.Items.Add(miCuenta);
            mainMenu.Items.Add(soporte);
            mainMenu.Items.Add(cerrarSesion);

            // Asignar manejador de eventos al boton click 
            cerrarSesion.Click += CerrarSesion_Click;
            soporte.Click += Soporte_Click;
            miCuenta.Click += MiCuenta_Click;

            // Configurar el estilo de los paneles (sin bordes y con fondo blanco)
            panelCaja.BackColor = Color.White; // Fondo blanco para el panel Caja
            panelCaja.BorderStyle = BorderStyle.None; // Sin borde para el panel Caja
            panelStock.BackColor = Color.White; // Fondo blanco para el panel Stock
            panelStock.BorderStyle = BorderStyle.None; // Sin borde para el panel Stock

            // Aplicar sombra y bordes redondeados
            SetPanelStyle(panelCaja);
            SetPanelStyle(panelStock);
        }


        private void ApplyPanelStyles()
        {
            // Aplicar el estilo a los paneles de Caja y Stock
            SetPanelStyle(panelCaja);
            SetPanelStyle(panelStock);
        }

        private void SetPanelStyle(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                // Dibujar los bordes redondeados
                Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                int radius = 20; // Radio para esquinas redondeadas
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Dibujar borde grueso
                Pen pen = new Pen(Color.LightGray, 2);
                e.Graphics.DrawPath(pen, path);

                // Sombra alrededor del panel (simulación)
                for (int i = 1; i <= 10; i++) // Varias capas de sombra para suavizar
                {
                    System.Drawing.Drawing2D.GraphicsPath shadowPath = new System.Drawing.Drawing2D.GraphicsPath();
                    shadowPath.AddArc(rect.X + i, rect.Y + i, radius, radius, 180, 90);
                    shadowPath.AddArc(rect.X + rect.Width - radius - i, rect.Y + i, radius, radius, 270, 90);
                    shadowPath.AddArc(rect.X + rect.Width - radius - i, rect.Y + rect.Height - radius - i, radius, radius, 0, 90);
                    shadowPath.AddArc(rect.X + i, rect.Y + rect.Height - radius - i, radius, radius, 90, 90);
                    shadowPath.CloseFigure();

                    e.Graphics.DrawPath(new Pen(Color.FromArgb(30 - i * 2, Color.Black), 2), shadowPath);
                }
            };
        }





        private void CerrarSesion_Click(object sender, EventArgs e)
        {
            // Mostrar un mensaje de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Cerrar el formulario principal y terminar la aplicación
                this.Close();
            }
        }

        private void Soporte_Click(object sender, EventArgs e)
        {
            // Información de contacto
            string message = "Para soporte técnico, por favor contacta a:\n\n" +
                             "Email: contact@codewiseglobal.com\n" +
                             "Teléfono: +54 9 11 2389-7849";
            string caption = "Información de Soporte";

            // Mostrar el mensaje
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MiCuenta_Click(object sender, EventArgs e)
        {
            // Obtener los datos desde la base de datos
            UserAccount currentUser = DatabaseConfig.GetUserAccount();

            // Verificar si se obtuvieron datos
            if (currentUser != null)
            {
                // Crear y mostrar el formulario de la cuenta, pasándole el usuario actual
                Account accountForm = new Account(currentUser);
                accountForm.ShowDialog(); // Usar ShowDialog para que sea un formulario modal
            }
            else
            {
                MessageBox.Show("No se encontraron datos de la cuenta en la base de datos.");
            }
        }

        private void PanelCaja_Click(object sender, EventArgs e)
        {
            // Abrir el formulario de Caja
            CajaForm cajaForm = new CajaForm();
            cajaForm.ShowDialog();
        }

        private void PanelStock_Click(object sender, EventArgs e)
        {
            // Abrir el formulario de Stock
            StockForm stockForm = new StockForm();
            stockForm.ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Centramos el logo después de que el formulario se haya cargado
            logoPictureBox.Location = new System.Drawing.Point((this.ClientSize.Width - logoPictureBox.Width) / 2, 80);
        }
    }
}

