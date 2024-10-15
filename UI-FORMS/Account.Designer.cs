namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    partial class Account
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtCuil;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.Label lblCuil;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblEmail;

        // Agregar un ProgressBar y un Label
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;

        private void InitializeComponent()
        {
            // Definir una fuente más grande para los controles
            var defaultFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtCuil = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblCuil = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Ajuste a pantalla completa
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // Label para Razón Social
            this.lblRazonSocial.Location = new System.Drawing.Point(100, 50);
            this.lblRazonSocial.Size = new System.Drawing.Size(120, 30);
            this.lblRazonSocial.Text = "Razón Social:";
            this.lblRazonSocial.Font = defaultFont;

            // TextBox para Razón Social
            this.txtRazonSocial.Location = new System.Drawing.Point(250, 50);
            this.txtRazonSocial.Size = new System.Drawing.Size(400, 40);  // Más ancho y alto
            this.txtRazonSocial.Font = defaultFont;  // Cambiar tamaño de tipografía

            // Label para Cuil
            this.lblCuil.Location = new System.Drawing.Point(100, 100);
            this.lblCuil.Size = new System.Drawing.Size(120, 30);
            this.lblCuil.Text = "CUIL:";
            this.lblCuil.Font = defaultFont;

            // TextBox para Cuil
            this.txtCuil.Location = new System.Drawing.Point(250, 100);
            this.txtCuil.Size = new System.Drawing.Size(400, 40);  // Más ancho y alto
            this.txtCuil.Font = defaultFont;

            // Label para Dirección
            this.lblDireccion.Location = new System.Drawing.Point(100, 150);
            this.lblDireccion.Size = new System.Drawing.Size(120, 30);
            this.lblDireccion.Text = "Dirección:";
            this.lblDireccion.Font = defaultFont;

            // TextBox para Dirección
            this.txtDireccion.Location = new System.Drawing.Point(250, 150);
            this.txtDireccion.Size = new System.Drawing.Size(400, 40);  // Más ancho y alto
            this.txtDireccion.Font = defaultFont;

            // Label para Teléfono
            this.lblTelefono.Location = new System.Drawing.Point(100, 200);
            this.lblTelefono.Size = new System.Drawing.Size(120, 30);
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.Font = defaultFont;

            // TextBox para Teléfono
            this.txtTelefono.Location = new System.Drawing.Point(250, 200);
            this.txtTelefono.Size = new System.Drawing.Size(400, 40);  // Más ancho y alto
            this.txtTelefono.Font = defaultFont;

            // Label para Email
            this.lblEmail.Location = new System.Drawing.Point(100, 250);
            this.lblEmail.Size = new System.Drawing.Size(120, 30);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = defaultFont;

            // TextBox para Email
            this.txtEmail.Location = new System.Drawing.Point(250, 250);
            this.txtEmail.Size = new System.Drawing.Size(400, 40);  // Más ancho y alto
            this.txtEmail.Font = defaultFont;

            // Botón Guardar
            this.btnSave.Location = new System.Drawing.Point(250, 350);
            this.btnSave.Size = new System.Drawing.Size(150, 50);  // Botón más grande
            this.btnSave.Font = defaultFont;
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Botón Cancelar
            this.btnCancel.Location = new System.Drawing.Point(450, 350);
            this.btnCancel.Size = new System.Drawing.Size(150, 50);  // Botón más grande
            this.btnCancel.Font = defaultFont;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ProgressBar
            this.progressBar.Location = new System.Drawing.Point(250, 420);
            this.progressBar.Size = new System.Drawing.Size(400, 30);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 100;

            // Label Status
            this.lblStatus.Location = new System.Drawing.Point(250, 460);
            this.lblStatus.Size = new System.Drawing.Size(400, 30);
            this.lblStatus.Text = "";
            this.lblStatus.Font = defaultFont;

            // Añadir controles al formulario
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.txtCuil);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.lblCuil);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);

            this.Name = "Account";
            this.Text = "Información de la Cuenta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}



