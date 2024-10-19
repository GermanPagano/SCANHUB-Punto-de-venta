using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class CajaForm : Form
    {
        private Label lblFecha;
        private Label lblHora;
        private Timer timer;
        private TextBox txtCodigo; // Variable de clase para el TextBox

        public CajaForm()
        {
            InitializeComponent();
            ConfigurarInterfaz();
            IniciarTimer(); // Inicia el timer para actualizar la fecha y hora

            this.Shown += (s, e) => txtCodigo.Focus(); // Coloca el foco en el TextBox al mostrar el formulario
        }

        private void ConfigurarInterfaz()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            var layoutPrincipal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(10)
            };

            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            this.Controls.Add(layoutPrincipal);

            layoutPrincipal.Controls.Add(CrearPanelIzquierdo(), 0, 0);
            layoutPrincipal.Controls.Add(CrearPanelDerecho(), 1, 0);
        }

        private Panel CrearPanelIzquierdo()
        {
            var panelIzquierdo = new Panel { Dock = DockStyle.Fill };

            var lblFechaHora = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 30,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 0, 10, 0)
            };

            lblFecha = new Label
            {
                Text = "FECHA: ",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 0, 20, 0) // Espacio entre fecha y hora
            };

            lblHora = new Label
            {
                Text = "HORA: ",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true
            };

            lblFechaHora.Controls.Add(lblFecha);
            lblFechaHora.Controls.Add(lblHora);

            var dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 5, 0, 0)
            };

            dataGridView.Columns.Add("Producto", "PRODUCTO");
            dataGridView.Columns.Add("Cantidad", "CANT");
            dataGridView.Columns.Add("Descuento", "DESC");
            dataGridView.Columns.Add("Total", "TOTAL");

            panelIzquierdo.Controls.Add(dataGridView);
            panelIzquierdo.Controls.Add(lblFechaHora);
            return panelIzquierdo;
        }

        private void IniciarTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 segundo
            timer.Tick += (s, e) => ActualizarFechaHora();
            timer.Start();
        }

        private void ActualizarFechaHora()
        {
            lblFecha.Text = $"FECHA: {DateTime.Now:dd/MM/yyyy}";
            lblHora.Text = $"HORA: {DateTime.Now:HH:mm:ss}";
        }

        private TableLayoutPanel CrearPanelDerecho()
        {
            var panelDerecho = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };

            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            panelDerecho.Controls.Add(CrearPanelSuperior(), 0, 0);
            panelDerecho.Controls.Add(CrearPanelMedio(), 0, 1);
            panelDerecho.Controls.Add(CrearPanelInferior(), 0, 2);

            return panelDerecho;
        }

        private Panel CrearPanelSuperior()
        {
            var panelSuperior = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.LightYellow,
                Padding = new Padding(10)
            };

            var lblCodigo = new Label
            {
                Text = "CÓDIGO:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 5)
            };

            txtCodigo = new TextBox // Instanciamos la variable de clase
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Arial", 40),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 5, 0, 10)
            };

            var lblSubtotal = new Label
            {
                Text = "Subtotal\n$0.00",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };

            panelSuperior.Controls.Add(lblSubtotal);
            panelSuperior.Controls.Add(txtCodigo);
            panelSuperior.Controls.Add(lblCodigo);

            return panelSuperior;
        }

        private FlowLayoutPanel CrearPanelMedio()
        {
            var panelMedio = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                BackColor = Color.LightYellow
            };

            panelMedio.Controls.Add(CrearBoton("CUPÓN", Color.LightBlue));
            panelMedio.Controls.Add(CrearBoton("EXTRAS", Color.SkyBlue));
            panelMedio.Controls.Add(CrearBoton("BORRAR", Color.LightCoral));
            panelMedio.Controls.Add(CrearBoton("RESET", Color.Salmon));

            var btnSalir = new Button
            {
                Text = "SALIR",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Width = 100,
                Height = 40
            };
            btnSalir.Click += (s, e) => Application.Exit();

            panelMedio.Controls.Add(btnSalir);
            return panelMedio;
        }

        private Panel CrearPanelInferior()
        {
            var panelInferior = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var btnPagar = new Button
            {
                Text = "PAGAR",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };

            btnPagar.Click += (s, e) => MessageBox.Show("Pago realizado.");

            panelInferior.Controls.Add(btnPagar);
            return panelInferior;
        }

        private Button CrearBoton(string texto, Color color)
        {
            return new Button
            {
                Text = texto,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                Width = 100,
                Height = 40,
                Margin = new Padding(5)
            };
        }
    }
}


