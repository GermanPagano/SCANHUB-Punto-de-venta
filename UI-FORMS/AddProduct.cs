using SCANHUB.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Database;
using System;
using System.Drawing; // Para los colores y estilos
using System.Linq;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            CargarProveedores();
            CargarCategorias();
        }

        private void InitializeComponent()
        {
            this.lblCodigo = new Label();
            this.txtProductCode = new TextBox();
            this.lblDescripcion = new Label();
            this.txtDescription = new TextBox();
            this.lblStockDisponible = new Label();
            this.txtStockDisponible = new TextBox();
            this.lblStockMinimo = new Label();
            this.txtStockMin = new TextBox();
            this.lblPrecioUnitario = new Label();
            this.txtUnitPrice = new TextBox();
            this.lblDescuento = new Label();
            this.txtDiscount = new TextBox();
            this.lblProveedor = new Label();
            this.cmbSupplier = new ComboBox(); // Cambié a ComboBox
            this.lblCategoria = new Label();
            this.cmbCategory = new ComboBox(); // Cambié a ComboBox
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            // 
            // Form Properties
            // 
            this.ClientSize = new System.Drawing.Size(450, 500);
            this.Text = "Añadir Producto";
            this.BackColor = Color.WhiteSmoke; // Fondo suave
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen; // Centrar el formulario

            // 
            // Labels & Textboxes (Mejor estilo)
            // 
            int inputHeight = 30;
            int labelWidth = 120;
            int inputWidth = 250;

            Font defaultFont = new Font("Segoe UI", 10);

            // Código
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.Font = defaultFont;
            this.lblCodigo.Location = new System.Drawing.Point(20, 30);
            this.lblCodigo.Size = new Size(labelWidth, inputHeight);

            this.txtProductCode.Font = defaultFont;
            this.txtProductCode.Location = new System.Drawing.Point(150, 30);
            this.txtProductCode.Size = new Size(inputWidth, inputHeight);

            // Descripción
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.Font = defaultFont;
            this.lblDescripcion.Location = new System.Drawing.Point(20, 80);
            this.lblDescripcion.Size = new Size(labelWidth, inputHeight);

            this.txtDescription.Font = defaultFont;
            this.txtDescription.Location = new System.Drawing.Point(150, 80);
            this.txtDescription.Size = new Size(inputWidth, inputHeight);

            // Stock Disponible
            this.lblStockDisponible.Text = "Stock Disponible:";
            this.lblStockDisponible.Font = defaultFont;
            this.lblStockDisponible.Location = new System.Drawing.Point(20, 130);
            this.lblStockDisponible.Size = new Size(labelWidth, inputHeight);

            this.txtStockDisponible.Font = defaultFont;
            this.txtStockDisponible.Location = new System.Drawing.Point(150, 130);
            this.txtStockDisponible.Size = new Size(inputWidth, inputHeight);

            // Stock Mínimo
            this.lblStockMinimo.Text = "Stock Mínimo:";
            this.lblStockMinimo.Font = defaultFont;
            this.lblStockMinimo.Location = new System.Drawing.Point(20, 180);
            this.lblStockMinimo.Size = new Size(labelWidth, inputHeight);

            this.txtStockMin.Font = defaultFont;
            this.txtStockMin.Location = new System.Drawing.Point(150, 180);
            this.txtStockMin.Size = new Size(inputWidth, inputHeight);

            // Precio Unitario
            this.lblPrecioUnitario.Text = "Precio Unitario:";
            this.lblPrecioUnitario.Font = defaultFont;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(20, 230);
            this.lblPrecioUnitario.Size = new Size(labelWidth, inputHeight);

            this.txtUnitPrice.Font = defaultFont;
            this.txtUnitPrice.Location = new System.Drawing.Point(150, 230);
            this.txtUnitPrice.Size = new Size(inputWidth, inputHeight);

            // Descuento
            this.lblDescuento.Text = "Descuento:";
            this.lblDescuento.Font = defaultFont;
            this.lblDescuento.Location = new System.Drawing.Point(20, 280);
            this.lblDescuento.Size = new Size(labelWidth, inputHeight);

            this.txtDiscount.Font = defaultFont;
            this.txtDiscount.Location = new System.Drawing.Point(150, 280);
            this.txtDiscount.Size = new Size(inputWidth, inputHeight);

            // Proveedor
            this.lblProveedor.Text = "Proveedor:";
            this.lblProveedor.Font = defaultFont;
            this.lblProveedor.Location = new System.Drawing.Point(20, 330);
            this.lblProveedor.Size = new Size(labelWidth, inputHeight);

            this.cmbSupplier.Font = defaultFont;
            this.cmbSupplier.Location = new System.Drawing.Point(150, 330);
            this.cmbSupplier.Size = new Size(inputWidth, inputHeight);

            // Categoría
            this.lblCategoria.Text = "Categoría:";
            this.lblCategoria.Font = defaultFont;
            this.lblCategoria.Location = new System.Drawing.Point(20, 380);
            this.lblCategoria.Size = new Size(labelWidth, inputHeight);

            this.cmbCategory.Font = defaultFont;
            this.cmbCategory.Location = new System.Drawing.Point(150, 380);
            this.cmbCategory.Size = new Size(inputWidth, inputHeight);

            // 
            // Botones Guardar y Cancelar (Estilo Moderno)
            // 
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = defaultFont;
            this.btnGuardar.BackColor = Color.LightSkyBlue;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(80, 430);
            this.btnGuardar.Size = new Size(120, 40);
            this.btnGuardar.Click += new EventHandler(this.BtnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = defaultFont;
            this.btnCancelar.BackColor = Color.LightCoral;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(230, 430);
            this.btnCancelar.Size = new Size(120, 40);
            this.btnCancelar.Click += new EventHandler(this.BtnCancelar_Click);

            // 
            // Añadir los controles al formulario
            // 
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblStockDisponible);
            this.Controls.Add(this.txtStockDisponible);
            this.Controls.Add(this.lblStockMinimo);
            this.Controls.Add(this.txtStockMin);
            this.Controls.Add(this.lblPrecioUnitario);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
        }

        // Función para cargar proveedores
        private void CargarProveedores()
        {
            var suppliers = DatabaseConfig.GetSuppliers();

            if (suppliers != null && suppliers.Any())
            {
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "SupplierName"; // Mostrar el nombre del proveedor
                cmbSupplier.ValueMember = "SupplierID"; // El valor seleccionado será el ID del proveedor
            }
            else
            {
                MessageBox.Show("No se encontraron proveedores para cargar.");
            }
        }


        // Función para cargar categorías
        private void CargarCategorias()
        {
            // Cargar las categorías de la base de datos
            var categories = DatabaseConfig.GetCategories();

            // Asegúrate de que 'categories' tenga elementos y las propiedades correctas
            if (categories != null && categories.Any())
            {
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "CategoryName"; // Mostrar el nombre de la categoría
                cmbCategory.ValueMember = "CategoryID"; // El valor seleccionado será el ID
            }
            else
            {
                MessageBox.Show("No se encontraron categorías para cargar.");
            }
        }






        // Función para el botón Cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            double descuento = 0;
            if (!string.IsNullOrEmpty(txtDiscount.Text))
            {
                descuento = double.Parse(txtDiscount.Text);
            }

            // Validar campos, aquí puedes agregar validaciones según lo que necesitas (no vacíos, tipo de dato, etc.)
            Product newProduct = new Product
            {
                Code = txtProductCode.Text,
                Description = txtDescription.Text,
                StockDisponible = int.Parse(txtStockDisponible.Text),
                StockMin = int.Parse(txtStockMin.Text),
                PrecioUnitario = double.Parse(txtUnitPrice.Text),
                Descuento = descuento,
                SupplierID = int.Parse(cmbSupplier.SelectedValue.ToString()), // Si es un ComboBox con proveedores
                CategoryID = Convert.ToInt32(cmbCategory.SelectedValue) // Convierte el valor seleccionado a entero
            };

            // Validar y agregar producto...
            bool success = DatabaseConfig.AddProduct(newProduct);

            if (success)
            {
                MessageBox.Show("Producto agregado con éxito.");
                this.DialogResult = DialogResult.OK;  // Devuelve OK si se guardó correctamente
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al agregar el producto.");
            }
        }







        private Label lblCodigo;
        private TextBox txtProductCode;
        private Label lblDescripcion;
        private TextBox txtDescription;
        private Label lblStockDisponible;
        private TextBox txtStockDisponible;
        private Label lblStockMinimo;
        private TextBox txtStockMin;
        private Label lblPrecioUnitario;
        private TextBox txtUnitPrice;
        private Label lblDescuento;
        private TextBox txtDiscount;
        private Label lblProveedor;
        private ComboBox cmbSupplier; // Cambié a ComboBox
        private Label lblCategoria;
        private ComboBox cmbCategory; // Cambié a ComboBox
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
