using System;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    partial class StockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView miDataGridView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.topPanel = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.miDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.miDataGridView)).BeginInit();
            this.SuspendLayout();

            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 60; // Aumentamos el espacio para los botones y la barra de búsqueda
            this.topPanel.BackColor = System.Drawing.Color.LightGray;
            this.topPanel.Controls.Add(this.btnAgregar);
            this.topPanel.Controls.Add(this.btnModificar);
            this.topPanel.Controls.Add(this.btnEliminar);
            this.topPanel.Controls.Add(this.txtBuscar);
            this.topPanel.Controls.Add(this.btnBuscar);
            this.topPanel.Controls.Add(this.btnImprimir);
            this.topPanel.Controls.Add(this.btnExportar);
            this.topPanel.Controls.Add(this.btnImportar);

            // 
            // btnAgregar
            // 
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnAgregar.Size = new System.Drawing.Size(120, 40); // Ajustamos el tamaño del botón
            this.btnAgregar.Location = new System.Drawing.Point(10, 10);
            this.btnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click); // Asignar manejador de evento

            // 
            // btnModificar
            // 
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnModificar.Size = new System.Drawing.Size(120, 40); // Ajustamos el tamaño del botón
            this.btnModificar.Location = new System.Drawing.Point(140, 10);
            this.btnModificar.Click += new System.EventHandler(this.BtnModificar_Click); // Asignar manejador de evento

            // 
            // btnEliminar
            // 
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnEliminar.Size = new System.Drawing.Size(120, 40); // Ajustamos el tamaño del botón
            this.btnEliminar.Location = new System.Drawing.Point(270, 10);
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click); // Asignar manejador de evento

            // 
            // txtBuscar
            // 
            this.txtBuscar.Width = 200;
            this.txtBuscar.Height = 40; // Ajustamos la altura del TextBox
            this.txtBuscar.Location = new System.Drawing.Point(400, 10);
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular); // Tipografía similar
            this.txtBuscar.Text = "Buscar";
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Enter += TxtBuscar_Enter;
            this.txtBuscar.Leave += TxtBuscar_Leave;
            txtBuscar.KeyDown += TxtBuscar_KeyDown;

            // 
            // btnBuscar
            // 
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnBuscar.Size = new System.Drawing.Size(120, 40); // Ajustamos el tamaño del botón
            this.btnBuscar.Location = new System.Drawing.Point(610, 10);
            this.btnBuscar.Click += BtnBuscar_Click;

            // 
            // btnImprimir
            // 
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnImprimir.Size = new System.Drawing.Size(120, 40); // Ajustamos el tamaño del botón
            this.btnImprimir.Location = new System.Drawing.Point(740, 10);
            this.btnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click); // Asignar manejador de evento

            // 
            // btnExportar
            // 
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnExportar.Size = new System.Drawing.Size(120, 40); // Tamaño del botón
            this.btnExportar.Location = new System.Drawing.Point(870, 10);
            this.btnExportar.Click += new System.EventHandler(this.BtnExportar_Click); // Asignar manejador de evento

            // 
            // btnImportar
            // 
            this.btnImportar.Text = "Importar";
            this.btnImportar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.btnImportar.Size = new System.Drawing.Size(120, 40); // Tamaño del botón
            this.btnImportar.Location = new System.Drawing.Point(1000, 10);
            this.btnImportar.Click += new System.EventHandler(this.BtnImportar_Click); // Asignar manejador de evento

            // 
            // miDataGridView
            // 
            this.miDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.miDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.miDataGridView.Location = new System.Drawing.Point(0, 60); // Empieza justo debajo del topPanel
            this.miDataGridView.Name = "miDataGridView";
            this.miDataGridView.RowTemplate.Height = 24;
            this.miDataGridView.Size = new System.Drawing.Size(800, 400);
            this.miDataGridView.TabIndex = 0;

            // 
            // StockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.miDataGridView);
            this.Controls.Add(this.topPanel);
            this.Name = "StockForm";
            this.Text = "Gestión de Stock";
            this.Load += new System.EventHandler(this.StockForm_Load);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.BackColor = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.miDataGridView)).EndInit();
            this.ResumeLayout(false);



            // funciones para buscar

 


        }

        #endregion
    }
}



