namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    partial class ImportOptionsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnUpdateStock;
        private System.Windows.Forms.Button btnReplaceStock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.btnReplaceStock = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnUpdateStock
            this.btnUpdateStock.Text = "Actualizar Stock";
            this.btnUpdateStock.Size = new System.Drawing.Size(150, 40);
            this.btnUpdateStock.Location = new System.Drawing.Point(30, 30);
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);

            // btnReplaceStock
            this.btnReplaceStock.Text = "Reemplazar Stock";
            this.btnReplaceStock.Size = new System.Drawing.Size(150, 40);
            this.btnReplaceStock.Location = new System.Drawing.Point(30, 80);
            this.btnReplaceStock.Click += new System.EventHandler(this.btnReplaceStock_Click);

            // ImportOptionsForm
            this.ClientSize = new System.Drawing.Size(220, 150);
            this.Controls.Add(this.btnUpdateStock);
            this.Controls.Add(this.btnReplaceStock);
            this.Name = "ImportOptionsForm";
            this.Text = "Opciones de Importación";
            this.ResumeLayout(false);
        }
    }
}
