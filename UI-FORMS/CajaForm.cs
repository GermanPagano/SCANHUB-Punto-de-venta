using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SCANHUB.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Database;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class CajaForm : Form
    {
        private Label lblFecha;
        private Label lblHora;
        private Timer timer;
        private TextBox txtCodigo;
        private DataGridView dataGridView;
        private int multiplicador = 1; // Multiplicador para escaneos
        private Label lblSubtotal; // Declarar el campo de clase

        public CajaForm()
        {
            InitializeComponent();
            ConfigurarInterfaz();
            IniciarTimer();
            this.Shown += (s, e) => txtCodigo.Focus();
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
                Margin = new Padding(0, 0, 20, 0)
            };

            lblHora = new Label
            {
                Text = "HORA: ",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true
            };

            lblFechaHora.Controls.Add(lblFecha);
            lblFechaHora.Controls.Add(lblHora);

            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 5, 0, 0),
                Font = new Font("Arial", 12),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                RowTemplate = { Height = 40 }
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
            timer = new Timer { Interval = 1000 };
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
                BackColor = Color.Orange,
                Padding = new Padding(10)
            };

            var lblCodigo = new Label
            {
                Text = "CÓDIGO:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 5)
            };

            txtCodigo = new TextBox
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Arial", 40),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 5, 0, 10)
            };

            txtCodigo.TextChanged += TxtCodigo_TextChanged;

            // Asignar a lblSubtotal el control de subtotal
            lblSubtotal = new Label
            {
                Text = "Subtotal\n$0.00",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };

            panelSuperior.Controls.Add(lblSubtotal);
            panelSuperior.Controls.Add(txtCodigo);
            panelSuperior.Controls.Add(lblCodigo);

            return panelSuperior;

        }
        private void ActualizarSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["Total"].Value != null && row.Cells["Descuento"].Value != null)
                {
                    // Obtiene la cantidad, descuento y precio unitario
                    var cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    var descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);

                    // Calcula el precio unitario sin modificar el total directamente
                    var precioUnitario = Convert.ToDecimal(row.Cells["Total"].Value) / cantidad;

                    // Calcula el total considerando el descuento y la cantidad
                    var totalConDescuento = (precioUnitario - descuento) * cantidad;

                    // Solo actualizamos el subtotal; no modificamos la celda "Total"
                    subtotal += totalConDescuento;
                }
            }

            // Actualiza el texto del subtotal
            lblSubtotal.Text = $"Subtotal\n${subtotal:0.00}";
        }

        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text.StartsWith("*") && int.TryParse(txtCodigo.Text.Substring(1), out int nuevoMultiplicador))
            {
                multiplicador = nuevoMultiplicador;
                txtCodigo.Clear();
                return;
            }

            if (txtCodigo.Text.Length == 13)
            {
                var codigo = txtCodigo.Text;
                var producto = DatabaseConfig.GetProductByCode(codigo);

                if (producto != null)
                {
                    AgregarProductoAlGrid(producto);
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtCodigo.Clear();
            }
        }

        private void AgregarProductoAlGrid(Product producto)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["Producto"].Value?.ToString() == producto.Description)
                {
                    int cantidadActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    int nuevaCantidad = cantidadActual + multiplicador;

                    row.Cells["Cantidad"].Value = nuevaCantidad;
                    row.Cells["Total"].Value = producto.PrecioUnitario * nuevaCantidad;

                    multiplicador = 1;

                    // Actualiza el subtotal
                    ActualizarSubtotal();
                    return;
                }
            }

            dataGridView.Rows.Add(
                producto.Description,
                multiplicador,
                producto.Descuento,
                producto.PrecioUnitario * multiplicador
            );

            multiplicador = 1;

            // Actualiza el subtotal
            ActualizarSubtotal();
        }

        private FlowLayoutPanel CrearPanelMedio()
        {
            var panelMedio = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            panelMedio.Controls.Add(CrearBoton("CUPÓN", Color.DarkGray, (s, e) => { }));
            panelMedio.Controls.Add(CrearBoton("EXTRAS", Color.DarkGray, (s, e) => { }));
            panelMedio.Controls.Add(CrearBoton("BORRAR", Color.DarkGray, (s, e) => BorrarProducto()));
            panelMedio.Controls.Add(CrearBoton("RESET", Color.DarkGray, ResetGrid));
            panelMedio.Controls.Add(CrearBoton("SALIR", Color.DarkGray, BtnSalir_Click));
            panelMedio.Controls.Add(CrearBoton("RESTAR 1", Color.DarkGray, (s, e) => DescontarProducto()));


            return panelMedio;
        }
        private void DescontarProducto()
        {
            if (dataGridView.CurrentRow != null) // Verifica que haya una fila seleccionada
            {
                // Obtiene los valores del producto
                var cantidadActual = Convert.ToInt32(dataGridView.CurrentRow.Cells["Cantidad"].Value);
                var totalActual = Convert.ToDecimal(dataGridView.CurrentRow.Cells["Total"].Value);

                if (cantidadActual > 1)
                {
                    // Calcula el precio unitario directamente desde el total
                    var precioUnitario = totalActual / cantidadActual;

                    // Resta 1 a la cantidad
                    cantidadActual -= 1;
                    dataGridView.CurrentRow.Cells["Cantidad"].Value = cantidadActual;

                    // Actualiza el total
                    var nuevoTotal = precioUnitario * cantidadActual;
                    dataGridView.CurrentRow.Cells["Total"].Value = nuevoTotal;
                }
                else
                {
                    // Si la cantidad llega a 0, elimina la fila
                    if (MessageBox.Show("La cantidad será 0, ¿deseas eliminar este producto?",
                                        "Confirmación",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dataGridView.Rows.Remove(dataGridView.CurrentRow);
                        MessageBox.Show("Producto eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Recalcula el subtotal después de actualizar el grid
                ActualizarSubtotal();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para descontar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BorrarProducto()
        {
            if (dataGridView.CurrentRow != null)
            {
                if (MessageBox.Show("¿Estás seguro de que deseas borrar este producto?",
                                    "Confirmación",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataGridView.Rows.Remove(dataGridView.CurrentRow);

                    // Actualiza el subtotal
                    ActualizarSubtotal();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para borrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetGrid(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas borrar todos los productos?",
                                "Confirmación",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView.Rows.Clear();  // Eliminar todas las filas del grid
                ActualizarSubtotal();       // Reiniciar el subtotal a $0.00
                MessageBox.Show("Se ha limpiado la lista de productos.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (!VerificarGridVacio())
            {
                MessageBox.Show("Hay productos en la lista. Finaliza la operación antes de salir.");
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private bool VerificarGridVacio()
        {
            return dataGridView.Rows.Cast<DataGridViewRow>().All(row => string.IsNullOrWhiteSpace(row.Cells["Producto"].Value?.ToString()));
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



        private Button CrearBoton(string texto, Color color, EventHandler onClick)
        {
            var boton = new Button
            {
                Text = texto,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                Width = 100,
                Height = 40,
                Margin = new Padding(5)
            };
            boton.Click += onClick;  // Asignar el evento de clic
            return boton;
        }

        private void CajaForm_Load(object sender, EventArgs e)
        {

        }
    }


}
