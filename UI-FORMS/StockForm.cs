using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using OfficeOpenXml; // Librería EPPlus
using System.IO;
using SCANHUB.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Database;
using System.Collections.Generic;
using System.Linq;
using iTextRectangle = iTextSharp.text.Rectangle;


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class StockForm : Form
    {
        public StockForm()
        {
            InitializeComponent();
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            // Cargar los datos en el GridView desde la base de datos
            CargarDatosEnGridView(miDataGridView);
        }


        // carga inicial de stock en el grid view
        private void CargarDatosEnGridView(DataGridView gridView)
        {
            try
            {
                List<Product> products = DatabaseConfig.GetStockProducts(); // Llamada al método centralizado
                DataTable dataTable = new DataTable();

                // Agregar columnas
                dataTable.Columns.Add("Codigo");
                dataTable.Columns.Add("Descripcion");
                dataTable.Columns.Add("StockDisponible");
                dataTable.Columns.Add("StockMin");
                dataTable.Columns.Add("PrecioUnitario");
                dataTable.Columns.Add("Descuento");
                dataTable.Columns.Add("Categoria"); // Cambiado para que muestre el CategoryName

                // Rellenar filas
                foreach (var product in products)
                {
                    dataTable.Rows.Add(product.Code, product.Description, product.StockDisponible, product.StockMin, product.PrecioUnitario, product.Descuento, product.CategoryName);
                }

                gridView.DataSource = dataTable;
                gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        // funcion que ejecuta el boton Agregar 
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            AddProduct addProductForm = new AddProduct();
            if (addProductForm.ShowDialog() == DialogResult.OK) // Esto espera el resultado de guardar
            {
                AddNewProducttoGrid(miDataGridView); // Aquí llamas a tu método para recargar el DataGridView
            }
        }

        private void AddNewProducttoGrid(DataGridView gridView)
        {
            try
            {
                List<Product> products = DatabaseConfig.GetStockProducts(); // Llamada al método centralizado
                DataTable dataTable = new DataTable();

                // Agregar columnas
                dataTable.Columns.Add("Codigo");
                dataTable.Columns.Add("Descripcion");
                dataTable.Columns.Add("StockDisponible");
                dataTable.Columns.Add("StockMin");
                dataTable.Columns.Add("PrecioUnitario");
                dataTable.Columns.Add("Descuento");
                dataTable.Columns.Add("Categoria"); // Cambiado para que muestre el CategoryName

                // Rellenar filas
                foreach (var product in products)
                {
                    dataTable.Rows.Add(product.Code, product.Description, product.StockDisponible, product.StockMin, product.PrecioUnitario, product.Descuento, product.CategoryName);
                }

                gridView.DataSource = dataTable;
                gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }             // Asigna los productos al DataGridView
        }

        // funcion que ejecuta el boton eliminar 
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (miDataGridView.SelectedRows.Count > 0)  // Verifica que se haya seleccionado un producto
            {
                // Obtén el código del producto seleccionado
                string productCode = miDataGridView.SelectedRows[0].Cells["Codigo"].Value.ToString();

                // Pregunta de confirmación
                var confirmResult = MessageBox.Show($"¿Estás seguro de eliminar el producto con código {productCode}?",
                                                    "Confirmar eliminación",
                                                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Llama al método para eliminar el producto
                    bool success = DatabaseConfig.DeleteProduct(productCode);

                    if (success)
                    {
                        MessageBox.Show("Producto eliminado correctamente.");

                        // Actualiza el GridView para reflejar los cambios
                        CargarDatosEnGridView(miDataGridView);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.");
            }
        }


        // funcion que ejecuta el boton modificar 
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (miDataGridView.SelectedRows.Count > 0)  // Verifica que se haya seleccionado un producto
            {
                // Obtén el índice del producto seleccionado
                int rowIndex = miDataGridView.SelectedRows[0].Index;

                // Ahora puedes acceder a los datos del producto almacenado en la lista
                var product = DatabaseConfig.GetStockProducts()[rowIndex];

                // Abre el formulario de modificar y pasa los datos
                ModifiedProduct modifiedForm = new ModifiedProduct(
                    product.Code,
                    product.Description,
                    product.StockDisponible,
                    product.StockMin,
                    product.PrecioUnitario,
                    product.Descuento,
                    product.SupplierID,
                    product.CategoryID); // Asegúrate de pasar los datos correctos

                modifiedForm.ShowDialog();

                // Luego de que se cierre el formulario, puedes recargar los datos del GridView
                CargarDatosEnGridView(miDataGridView);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para modificar.");
            }
        }

        // Evento para cuando el TextBox obtiene el foco
        private void TxtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar")
            {
                txtBuscar.Text = "";  // Borrar el texto placeholder
                txtBuscar.ForeColor = System.Drawing.Color.Black;  // Cambiar el color de texto cuando se escribe
            }
        }

        // Evento para cuando el TextBox pierde el foco
        private void TxtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.ForeColor = System.Drawing.Color.Gray;  // Cambiar el color de texto al placeholder
            }
        }

        // Evento para el botón de búsqueda
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProducto(txtBuscar.Text);  // Ejecutar la búsqueda al hacer clic en el botón
        }

        // Función para realizar la búsqueda
        // Función para realizar la búsqueda
        private void BuscarProducto(string terminoBusqueda)
        {
            if (!string.IsNullOrEmpty(terminoBusqueda) && terminoBusqueda != "Buscar por nombre, código o categoría")
            {
                // Filtrar la lista de productos basada en la descripción, código o categoría del producto
                var productosFiltrados = DatabaseConfig.GetStockProducts()
                    .Where(p => p.Description.ToLower().Contains(terminoBusqueda.ToLower())
                             || p.Code.Contains(terminoBusqueda)
                             || p.CategoryName.ToLower().Contains(terminoBusqueda.ToLower())) // Añadimos la búsqueda por categoría
                    .ToList();

                // Actualizar el DataGridView con los resultados filtrados
                CargarDatosEnGridViewConResultadosFiltrados(productosFiltrados);
            }
            else
            {
                // Si no hay búsqueda, cargar todos los productos
                CargarDatosEnGridView(miDataGridView);
            }
        }

        // Función para cargar los resultados filtrados en el DataGridView
        private void CargarDatosEnGridViewConResultadosFiltrados(List<Product> productosFiltrados)
        {
            DataTable dataTable = new DataTable();

            // Agregar columnas
            dataTable.Columns.Add("Codigo");
            dataTable.Columns.Add("Descripcion");
            dataTable.Columns.Add("StockDisponible");
            dataTable.Columns.Add("StockMin");
            dataTable.Columns.Add("PrecioUnitario");
            dataTable.Columns.Add("Descuento");
            dataTable.Columns.Add("Categoria");

            // Rellenar filas con los productos filtrados
            foreach (var product in productosFiltrados)
            {
                dataTable.Rows.Add(product.Code, product.Description, product.StockDisponible, product.StockMin, product.PrecioUnitario, product.Descuento, product.CategoryName);
            }

            miDataGridView.DataSource = dataTable;
            miDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Evento para capturar la tecla Enter en el TextBox de búsqueda
        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarProducto(txtBuscar.Text);  // Ejecutar la búsqueda al presionar Enter
                e.Handled = true;
                e.SuppressKeyPress = true;  // Evitar que el Enter inserte una nueva línea en el TextBox
            }
        }


        // Evento para imprimir el stock
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirPDF(miDataGridView); // Pasamos el DataGridView actual al método para generar el PDF
        }


        public void ImprimirPDF(DataGridView dataGridView)
    {
        // Crear el diálogo para que el usuario elija la ubicación y el nombre del archivo
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf"; // Filtrar para que solo se muestren archivos PDF
        saveFileDialog.Title = "Guardar archivo PDF";
        saveFileDialog.FileName = "Stock_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf"; // Nombre por defecto

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            // Obtener la ruta seleccionada por el usuario
            string rutaArchivo = saveFileDialog.FileName;

            // Obtener la información de la empresa usando GetUserAccount
            var businessInfo = DatabaseConfig.GetUserAccount();

                // Crear el documento PDF
                Document documento = new Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));

            // Abrir el documento para agregar contenido
            documento.Open();

            // Estilo de fuentes
            iTextSharp.text.Font fuenteEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f, BaseColor.BLACK);
            iTextSharp.text.Font fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.BLACK);
            iTextSharp.text.Font fuenteTablaEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.WHITE);

            // Agregar la información de la empresa como encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.DefaultCell.Border = iTextRectangle.NO_BORDER; // Usar iTextRectangle para las celdas
            tablaEncabezado.AddCell(new PdfPCell(new Phrase(businessInfo.RazonSocial, fuenteEncabezado)) { Border = iTextRectangle.NO_BORDER });
            tablaEncabezado.AddCell(new PdfPCell(new Phrase("CUIT: " + businessInfo.Cuil, fuenteNormal)) { Border = iTextRectangle.NO_BORDER });
            tablaEncabezado.AddCell(new PdfPCell(new Phrase("Email: " + businessInfo.Email, fuenteNormal)) { Border = iTextRectangle.NO_BORDER });
            tablaEncabezado.SpacingAfter = 20;  // Espacio después de la cabecera
            documento.Add(tablaEncabezado);

            // Crear una tabla para el stock (con solo las columnas deseadas)
            PdfPTable tabla = new PdfPTable(4); // Solo incluimos 4 columnas: Código, Descripción, Precio Unitario y Categoría
            tabla.WidthPercentage = 100; // Ancho de la tabla al 100%
            tabla.SpacingBefore = 10f;
            tabla.SpacingAfter = 10f;
            tabla.SetWidths(new float[] { 2f, 5f, 2f, 3f });

            // Encabezados de la tabla con estilo
            string[] encabezados = { "Código", "Descripción", "Precio Unitario", "Categoría" };
            foreach (string encabezado in encabezados)
            {
                PdfPCell celdaEncabezado = new PdfPCell(new Phrase(encabezado, fuenteTablaEncabezado))
                {
                    BackgroundColor = BaseColor.DARK_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                tabla.AddCell(celdaEncabezado);
            }

            // Recorrer las filas del DataGridView y agregar solo las columnas relevantes al PDF
            foreach (DataGridViewRow fila in dataGridView.Rows)
            {
                if (fila.IsNewRow) continue; // Evitar fila vacía al final

                tabla.AddCell(new PdfPCell(new Phrase(fila.Cells["Codigo"].Value?.ToString() ?? "", fuenteNormal))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                tabla.AddCell(new PdfPCell(new Phrase(fila.Cells["Descripcion"].Value?.ToString() ?? "", fuenteNormal))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tabla.AddCell(new PdfPCell(new Phrase(fila.Cells["PrecioUnitario"].Value?.ToString() ?? "", fuenteNormal))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                tabla.AddCell(new PdfPCell(new Phrase(fila.Cells["Categoria"].Value?.ToString() ?? "", fuenteNormal))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
            }

            // Agregar la tabla al documento
            documento.Add(tabla);

            // Cerrar el documento
            documento.Close();

            // Mostrar mensaje de éxito
            MessageBox.Show("PDF generado exitosamente en: " + Path.GetFullPath(rutaArchivo));
        }
        else
        {
            // Si el usuario cancela, puedes mostrar un mensaje o simplemente no hacer nada
            MessageBox.Show("Se canceló la generación del PDF.");
        }
    }



    // Botón para exportar datos desde Excel
    private void BtnExportar_Click(object sender, EventArgs e)
        {
            // Configurar la licencia para EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Crear un SaveFileDialog para que el usuario seleccione dónde guardar el archivo
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Guardar archivo Excel";
                saveFileDialog.FileName = "StockExport.xlsx"; // Nombre predeterminado del archivo

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Crear un paquete Excel
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            // Crear una nueva hoja de trabajo en el archivo Excel
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Stock");

                            // Obtener la tabla de datos desde el DataGridView
                            DataTable dataTable = (DataTable)miDataGridView.DataSource;

                            // Agregar encabezados de columna
                            for (int col = 0; col < dataTable.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col + 1].Value = dataTable.Columns[col].ColumnName;
                            }

                            // Agregar los datos de las filas
                            for (int row = 0; row < dataTable.Rows.Count; row++)
                            {
                                for (int col = 0; col < dataTable.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col].ToString();
                                }
                            }

                            // Guardar el archivo en la ubicación seleccionada por el usuario
                            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                            package.SaveAs(excelFile);

                            MessageBox.Show("Datos exportados exitosamente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar los datos: " + ex.Message);
                    }
                }
            }
        }

        // Botón para importar datos desde Excel
        private void BtnImportar_Click(object sender, EventArgs e)
        {
            // Abrir el formulario de opciones de importación
            using (ImportOptionsForm importOptionsForm = new ImportOptionsForm())
            {
                if (importOptionsForm.ShowDialog() == DialogResult.OK)
                {
                    // Aquí podrías manejar el retorno de opciones si fuera necesario.
                    // Por ejemplo, dependiendo de si elige actualizar o reemplazar.
                    if (importOptionsForm.UpdateStock)
                    {
                        // Lógica para actualizar stock se moverá a ImportOptionsForm
                        MessageBox.Show("Actualizar stock seleccionado.");
                    }
                    else if (importOptionsForm.ReplaceStock)
                    {
                        // Lógica para reemplazar stock se moverá a ImportOptionsForm
                        MessageBox.Show("Reemplazar stock seleccionado.");
                    }
                }
            }
        }






    }



}

