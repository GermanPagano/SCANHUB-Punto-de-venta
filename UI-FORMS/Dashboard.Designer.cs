namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    partial class Dashboard
    {
        private System.Windows.Forms.Panel panelCaja;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.PictureBox pictureBoxCaja;
        private System.Windows.Forms.PictureBox pictureBoxStock;
        private System.Windows.Forms.Label labelCaja;
        private System.Windows.Forms.Label labelStock;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox logoPictureBox;

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panelCaja = new System.Windows.Forms.Panel();
            this.panelStock = new System.Windows.Forms.Panel();
            this.pictureBoxCaja = new System.Windows.Forms.PictureBox();
            this.pictureBoxStock = new System.Windows.Forms.PictureBox();
            this.labelCaja = new System.Windows.Forms.Label();
            this.labelStock = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();

            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1361, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = System.Drawing.Image.FromFile("Images/logo.png"); // Asegúrate de que la ruta sea correcta
            this.logoPictureBox.Size = new System.Drawing.Size(150, 80); // Tamaño del logo ajustado
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


            // 
            // panelCaja
            // 
            this.panelCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCaja.Controls.Add(this.pictureBoxCaja);
            this.panelCaja.Controls.Add(this.labelCaja);
            this.panelCaja.Location = new System.Drawing.Point(300, 300); // Ajustar la ubicación de la caja debajo del logo
            this.panelCaja.Size = new System.Drawing.Size(200, 200);
            this.panelCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCaja.Click += new System.EventHandler(this.PanelCaja_Click);

            // 
            // panelStock
            // 
            this.panelStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStock.Controls.Add(this.pictureBoxStock);
            this.panelStock.Controls.Add(this.labelStock);
            this.panelStock.Location = new System.Drawing.Point(600, 300); // Ajustar la ubicación del stock debajo del logo
            this.panelStock.Size = new System.Drawing.Size(200, 200);
            this.panelStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelStock.Click += new System.EventHandler(this.PanelStock_Click);

            // 
            // pictureBoxCaja
            // 
            this.pictureBoxCaja.Image = System.Drawing.Image.FromFile("Images/CAJA.png"); // Reemplaza con la ruta correcta si es necesario
            this.pictureBoxCaja.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCaja.Location = new System.Drawing.Point(50, 30);
            this.pictureBoxCaja.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxCaja.Click += new System.EventHandler(this.PanelCaja_Click); // Agregar evento para la imagen de Caja


            // 
            // pictureBoxStock
            // 
            this.pictureBoxStock.Image = System.Drawing.Image.FromFile("Images/STOCK.png"); // Reemplaza con la ruta correcta si es necesario
            this.pictureBoxStock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStock.Location = new System.Drawing.Point(50, 30);
            this.pictureBoxStock.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxStock.Click += new System.EventHandler(this.PanelStock_Click); // Agregar evento para la imagen de Stock


            // 
            // labelCaja
            // 
            this.labelCaja.Text = "Caja";
            this.labelCaja.Font = new System.Drawing.Font("Arial", 12F);
            this.labelCaja.Location = new System.Drawing.Point(75, 150);
            this.labelCaja.AutoSize = true;

            // 
            // labelStock
            // 
            this.labelStock.Text = "Stock";
            this.labelStock.Font = new System.Drawing.Font("Arial", 12F);
            this.labelStock.Location = new System.Drawing.Point(75, 150);
            this.labelStock.AutoSize = true;

            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 603);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.logoPictureBox); // Añadimos el logo en la parte superior
            this.Controls.Add(this.panelCaja);  // Añadimos el panel Caja al Dashboard
            this.Controls.Add(this.panelStock);  // Añadimos el panel Stock al Dashboard
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Dashboard";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
