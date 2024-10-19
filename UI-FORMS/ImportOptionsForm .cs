using OfficeOpenXml;
using SCANHUB.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Database;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class ImportOptionsForm : Form
    {
        public bool UpdateStock { get; private set; }
        public bool ReplaceStock { get; private set; }

        // Evento para notificar que se ha actualizado el stock
        public event Action OnStockUpdated;

        public ImportOptionsForm()
        {
            InitializeComponent();
            InicializarControles();
        }

        private void InicializarControles()
        {
            this.Controls.Clear();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Opciones de Importación";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.White;

            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Stock";
            btnActualizar.Size = new Size(150, 40);
            btnActualizar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(30, 30);
            btnActualizar.Click += btnUpdateStock_Click;
            this.Controls.Add(btnActualizar);

            Button btnReemplazar = new Button();
            btnReemplazar.Text = "Reemplazar Stock";
            btnReemplazar.Size = new Size(150, 40);
            btnReemplazar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnReemplazar.BackColor = Color.FromArgb(231, 76, 60);
            btnReemplazar.ForeColor = Color.White;
            btnReemplazar.Location = new Point(30, 90);
            btnReemplazar.Click += btnReplaceStock_Click;
            this.Controls.Add(btnReemplazar);

            this.ClientSize = new Size(220, 180);
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ActualizarStockDesdeExcel(filePath);

                    // Notificar que se ha actualizado el stock
                    OnStockUpdated?.Invoke();
                }
            }
        }

        private void ActualizarStockDesdeExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int totalRows = worksheet.Dimension.Rows;

                    for (int i = 2; i <= totalRows; i++)
                    {
                        string codigoProducto = worksheet.Cells[i, 1].Text;
                        var stockValue = worksheet.Cells[i, 3].Value;
                        int stockDisponible;

                        if (stockValue != null && int.TryParse(stockValue.ToString(), out stockDisponible))
                        {
                            var producto = DatabaseConfig.GetProductByCode(codigoProducto);

                            if (producto != null)
                            {
                                DatabaseConfig.UpdateStockInDatabase(codigoProducto, stockDisponible);
                            }
                            else
                            {
                                string descripcion = worksheet.Cells[i, 2].Text;
                                string stockMinStr = worksheet.Cells[i, 4].Text;
                                string precioUnitarioStr = worksheet.Cells[i, 5].Text;
                                string descuentoStr = worksheet.Cells[i, 6].Text;

                                if (string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(precioUnitarioStr))
                                {
                                    MessageBox.Show($"El producto con código {codigoProducto} no tiene suficientes datos para ser añadido.");
                                    continue;
                                }

                                int stockMin = int.TryParse(stockMinStr, out int sm) ? sm : 0;
                                double precioUnitario = double.TryParse(precioUnitarioStr, out double pu) ? pu : 0.0;
                                double descuento = double.TryParse(descuentoStr, out double d) ? d : 0.0;

                                var nuevoProducto = new Product
                                {
                                    Code = codigoProducto,
                                    Description = descripcion,
                                    StockDisponible = stockDisponible,
                                    StockMin = stockMin,
                                    PrecioUnitario = precioUnitario,
                                    Descuento = descuento,
                                    SupplierID = 0,
                                    CategoryID = 0
                                };

                                bool success = DatabaseConfig.AddProduct(nuevoProducto);
                                if (success)
                                {
                                    MessageBox.Show($"Producto con código {codigoProducto} añadido exitosamente.");
                                }
                                else
                                {
                                    MessageBox.Show($"No se pudo añadir el producto con código {codigoProducto}.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"El valor de stock en la fila {i} no es válido. Verifica el archivo Excel.");
                        }
                    }

                    MessageBox.Show("Proceso de importación completado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo Excel: {ex.Message}");
            }
        }

        private void btnReplaceStock_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ReemplazarStockDesdeExcel(filePath);

                    // Notificar que se ha actualizado el stock
                    OnStockUpdated?.Invoke();
                }
            }
        }

        // Método para reemplazar el stock completamente desde un Excel
        private void ReemplazarStockDesdeExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int totalRows = worksheet.Dimension.Rows;
                    int totalColumns = worksheet.Dimension.Columns;

                    // Verificamos si el número de columnas es el esperado (7)
                    if (totalColumns != 7)
                    {
                        MessageBox.Show($"El archivo Excel no tiene el formato correcto. Columnas detectadas: {totalColumns}");
                        return;
                    }

                    // Borramos todos los productos del stock
                    DatabaseConfig.ClearStock();

                    // Iteramos por las filas del Excel
                    for (int i = 2; i <= totalRows; i++) // Saltamos el encabezado
                    {
                        string codigoProducto = worksheet.Cells[i, 1].Text;
                        string descripcion = worksheet.Cells[i, 2].Text;
                        int stockDisponible = int.Parse(worksheet.Cells[i, 3].Text);
                        int stockMin = int.Parse(worksheet.Cells[i, 4].Text);
                        double precioUnitario = double.Parse(worksheet.Cells[i, 5].Text);
                        double descuento = double.Parse(worksheet.Cells[i, 6].Text);
                        string categoria = worksheet.Cells[i, 7].Text; // CategoryName del Excel

                        // Verificamos si la categoría ya existe, si no, asignamos una categoría por defecto
                        int categoryID = DatabaseConfig.GetCategoryIDByName(categoria);
                        if (categoryID == 0)
                        {
                            categoryID = DatabaseConfig.CreateCategory(categoria); // Creamos una nueva categoría si no existe
                        }

                        // Creamos el objeto Producto
                        var nuevoProducto = new Product
                        {
                            Code = codigoProducto,
                            Description = descripcion,
                            StockDisponible = stockDisponible,
                            StockMin = stockMin,
                            PrecioUnitario = precioUnitario,
                            Descuento = descuento,
                            SupplierID = 0,  // Por ahora sin proveedor
                            CategoryID = categoryID
                        };

                        // Insertamos el producto en la base de datos
                        DatabaseConfig.AddProduct(nuevoProducto);
                    }

                    MessageBox.Show("Stock reemplazado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo Excel: {ex.Message}");
            }
        }




    }
}

