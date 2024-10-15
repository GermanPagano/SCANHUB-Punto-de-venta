using System;
using System.Windows.Forms;
using SCANHUB___INVENTARIO_Y_CAJA.Database;
using SCANHUB___INVENTARIO_Y_CAJA.EmailServices;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class ModifiedProduct : Form
    {
        // Definición de controles (TextBoxes, ComboBoxes, CheckBox, Labels y Botones)
        private TextBox txtProductCode;
        private TextBox txtDescription;
        private TextBox txtStockDisponible;
        private TextBox txtStockMin;
        private TextBox txtUnitPrice;
        private TextBox txtDiscount;
        private ComboBox cmbSupplier;
        private ComboBox cmbCategory;
        private CheckBox chkSendEmail; // Checkbox para enviar correo
        private Button btnGuardar;
        private Button btnCancelar;
        private Label lblProductCode, lblDescription, lblStockDisponible, lblStockMin, lblUnitPrice, lblDiscount, lblSupplier, lblCategory;

        // Variables para almacenar los valores originales (para detectar cambios)
        private string originalDescription;
        private int originalStockDisponible;
        private int originalStockMin;
        private double originalPrecioUnitario;
        private double originalDescuento;

        private string productCode;

        public ModifiedProduct(string productCode, string description, int stockDisponible, int stockMin, double precioUnitario, double descuento, int supplierID, int categoryID)
        {
            InitializeComponent();

            // Inicializamos y configuramos los controles
            InicializarControles();

            // Asignamos los valores recibidos a los TextBoxes o ComboBoxes
            this.productCode = productCode;
            txtProductCode.Text = productCode;
            txtDescription.Text = description;
            txtStockDisponible.Text = stockDisponible.ToString();
            txtStockMin.Text = stockMin.ToString();
            txtUnitPrice.Text = precioUnitario.ToString();
            txtDiscount.Text = descuento.ToString();

            // Guardamos los valores originales para comparación
            originalDescription = description;
            originalStockDisponible = stockDisponible;
            originalStockMin = stockMin;
            originalPrecioUnitario = precioUnitario;
            originalDescuento = descuento;

            // Cargamos los ComboBoxes con las categorías y los proveedores
            CargarSuppliers();
            CargarCategorias();

            // Seleccionamos los valores correctos
            cmbSupplier.SelectedValue = supplierID;
            cmbCategory.SelectedValue = categoryID;
        }

        // Inicialización de controles (TextBoxes, ComboBoxes, Labels, CheckBox y Botones)
        private void InicializarControles()
        {
            int labelX = 20, inputX = 120, currentY = 20, incrementY = 35;

            // Labels descriptivos
            lblProductCode = new Label { Text = "Código:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtProductCode = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30), ReadOnly = true };
            currentY += incrementY;

            lblDescription = new Label { Text = "Descripción:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtDescription = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblStockDisponible = new Label { Text = "Disponible:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtStockDisponible = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblStockMin = new Label { Text = "Stock Mín:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtStockMin = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblUnitPrice = new Label { Text = "Precio Unitario:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtUnitPrice = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblDiscount = new Label { Text = "Descuento:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            txtDiscount = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblSupplier = new Label { Text = "Proveedor:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            cmbSupplier = new ComboBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            lblCategory = new Label { Text = "Categoría:", Location = new System.Drawing.Point(labelX, currentY), AutoSize = true };
            cmbCategory = new ComboBox { Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 30) };
            currentY += incrementY;

            chkSendEmail = new CheckBox { Text = "Enviar correo de confirmación", Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(250, 20) };
            currentY += incrementY + 10;

            btnGuardar = new Button { Text = "Guardar", Location = new System.Drawing.Point(inputX, currentY), Size = new System.Drawing.Size(100, 30) };
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button { Text = "Cancelar", Location = new System.Drawing.Point(inputX + 120, currentY), Size = new System.Drawing.Size(100, 30) };
            btnCancelar.Click += BtnCancelar_Click;

            // Añadimos los controles al formulario
            this.Controls.Add(lblProductCode);
            this.Controls.Add(txtProductCode);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblStockDisponible);
            this.Controls.Add(txtStockDisponible);
            this.Controls.Add(lblStockMin);
            this.Controls.Add(txtStockMin);
            this.Controls.Add(lblUnitPrice);
            this.Controls.Add(txtUnitPrice);
            this.Controls.Add(lblDiscount);
            this.Controls.Add(txtDiscount);
            this.Controls.Add(lblSupplier);
            this.Controls.Add(cmbSupplier);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(chkSendEmail);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnCancelar);

            // Configuraciones adicionales del formulario
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.StartPosition = FormStartPosition.CenterScreen; // Centrar el formulario
            this.Text = "Modificar Producto";
        }

        // Método para cargar proveedores
        private void CargarSuppliers()
        {
            var suppliers = DatabaseConfig.GetSuppliers();
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
        }

        // Método para cargar categorías
        private void CargarCategorias()
        {
            var categories = DatabaseConfig.GetCategories();
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }

        // Método que se ejecuta al presionar el botón Guardar
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtStockDisponible.Text) || string.IsNullOrEmpty(txtStockMin.Text) || string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            // Obtenemos los valores modificados
            string newDescription = txtDescription.Text;
            int newStockDisponible = int.Parse(txtStockDisponible.Text);
            int newStockMin = int.Parse(txtStockMin.Text);
            double newPrecioUnitario = double.Parse(txtUnitPrice.Text);
            double newDescuento = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : double.Parse(txtDiscount.Text);
            int newSupplierID = int.Parse(cmbSupplier.SelectedValue.ToString());
            int newCategoryID = int.Parse(cmbCategory.SelectedValue.ToString());

            // Llamamos al método para actualizar el producto en la base de datos
            bool success = DatabaseConfig.UpdateProduct(productCode, newDescription, newStockDisponible, newStockMin, newPrecioUnitario, newDescuento, newSupplierID, newCategoryID);

            if (success)
            {
                // Verificar si el checkbox para enviar correo está tildado
                if (chkSendEmail.Checked)
                {
                    // Obtener el email del negocio desde la base de datos
                    string emailTo = DatabaseConfig.GetBusinessEmail();

                    // Enviar correo
                    if (!string.IsNullOrEmpty(emailTo))
                    {
                        EmailService emailService = new EmailService();
                        string subject = "Confirmación de modificación del producto";

                        // Mensaje detallando los cambios
                        string messageBody = $"El producto {newDescription} ha sido modificado exitosamente.\n\nCambios realizados:";
                        if (originalDescription != newDescription)
                            messageBody += $"\n- Descripción cambiada de '{originalDescription}' a '{newDescription}'";
                        if (originalStockDisponible != newStockDisponible)
                            messageBody += $"\n- Stock disponible cambiado de {originalStockDisponible} a {newStockDisponible}";
                        if (originalStockMin != newStockMin)
                            messageBody += $"\n- Stock mínimo cambiado de {originalStockMin} a {newStockMin}";
                        if (originalPrecioUnitario != newPrecioUnitario)
                            messageBody += $"\n- Precio unitario cambiado de {originalPrecioUnitario} a {newPrecioUnitario}";
                        if (originalDescuento != newDescuento)
                            messageBody += $"\n- Descuento cambiado de {originalDescuento} a {newDescuento}";

                        emailService.SendEmail(emailTo, subject, messageBody);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo enviar el correo: no se encontró la dirección de email del negocio.");
                    }
                }

                MessageBox.Show("Producto modificado con éxito.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al modificar el producto.");
            }
        }

        // Método que se ejecuta al presionar el botón Cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
