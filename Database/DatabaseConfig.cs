using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using SCANHUB.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Models;

namespace SCANHUB___INVENTARIO_Y_CAJA.Database
{
    public static class DatabaseConfig
    {
        private static SQLiteConnection connection;
        private static string dbPath = "Database\\MyDatabase.sqlite";

        public static void InitializeDatabase()
        {
            string folder = "Database";

            // Verificar si la carpeta existe, si no, crearla
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            // Crear el archivo de la base de datos si no existe
            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            // Abrir conexión
            connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            connection.Open();

            // Crear tabla de Usuarios si no existe
            string sqlUsers = @"CREATE TABLE IF NOT EXISTS Users (
                             ID INTEGER PRIMARY KEY AUTOINCREMENT,
                             NOMBRE TEXT NOT NULL,
                             PASSWORD TEXT NOT NULL)";

            ExecuteNonQuery(sqlUsers);

            // Crear tabla Bussines_Info si no existe
            string sqlBussinesInfo = @"CREATE TABLE IF NOT EXISTS Bussines_Info (
                  CUIT TEXT PRIMARY KEY,
                  RazonSocial TEXT,
                  Direccion TEXT,
                  Telefono TEXT,
                  Email TEXT)";
            ExecuteNonQuery(sqlBussinesInfo);

            // Crear tabla Stock
            string sqlStock = @"CREATE TABLE IF NOT EXISTS Stock (
                  StockID INTEGER PRIMARY KEY AUTOINCREMENT,
                  ProductCode TEXT NOT NULL,
                  ProductDescription TEXT NOT NULL,
                  StockAvailable INTEGER,
                  StockMin INTEGER,
                  UnitPrice REAL,
                  Discount REAL DEFAULT 0,
                  SupplierID INTEGER,
                  CategoryID INTEGER,
                  FOREIGN KEY(SupplierID) REFERENCES Suppliers(SupplierID),
                  FOREIGN KEY(CategoryID) REFERENCES ProductCategory(CategoryID)
                )";
            ExecuteNonQuery(sqlStock);

            // Crear tabla Suppliers
            string sqlSuppliers = @"CREATE TABLE IF NOT EXISTS Suppliers (
                  SupplierID INTEGER PRIMARY KEY AUTOINCREMENT,
                  SupplierName TEXT NOT NULL,
                  ContactInfo TEXT
                )";
            ExecuteNonQuery(sqlSuppliers);

            // Crear tabla ProductCategory
            string sqlProductCategory = @"CREATE TABLE IF NOT EXISTS ProductCategory (
                  CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                  CategoryName TEXT NOT NULL
                )";
            ExecuteNonQuery(sqlProductCategory);

            // Crear tabla Sales
            string sqlSales = @"CREATE TABLE IF NOT EXISTS Sales (
                  SaleID INTEGER PRIMARY KEY AUTOINCREMENT,
                  ProductID INTEGER,
                  Quantity INTEGER,
                  SalePrice REAL,
                  SaleDate TEXT,
                  FOREIGN KEY(ProductID) REFERENCES Stock(StockID)
                )";
            ExecuteNonQuery(sqlSales);

            // Crear tabla Tickets
            string sqlTickets = @"CREATE TABLE IF NOT EXISTS Tickets (
                  TicketID INTEGER PRIMARY KEY AUTOINCREMENT,
                  SaleID INTEGER,
                  TicketNumber TEXT,
                  IssueDate TEXT,
                  FOREIGN KEY(SaleID) REFERENCES Sales(SaleID)
                )";
            ExecuteNonQuery(sqlTickets);

            connection.Close();
        }


        // Método para ejecutar comandos SQL que no retornan datos
        private static void ExecuteNonQuery(string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Validar usuario y contraseña
        public static bool ValidateUser(string NOMBRE, string password)
        {
            string dbPath = "Database\\MyDatabase.sqlite";  // Asegúrate de que la ruta a tu base de datos es correcta
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();  // Abrir conexión aquí

                // Consulta que verifica el nombre de usuario y recupera la contraseña
                // Consulta que verifica el nombre de usuario y recupera la contraseña
                string sql = "SELECT PASSWORD FROM Users WHERE \"NOMBRE\" = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    // Agregar el parámetro @username
                    command.Parameters.AddWithValue("@username", NOMBRE);

                    // Ejecutar el comando y leer el resultado
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader.GetString(0);
                            return storedPassword == password;  // Retorna true si las contraseñas coinciden
                        }
                    }
                }

                connection.Close();  // Cerrar conexión aquí
            }
            return false;  // Retorna false si no se encuentra el usuario o la contraseña no coincide
        }


        // Obtener los datos de la cuenta de usuario desde la tabla Bussines_Info
        public static UserAccount GetUserAccount()
        {
            UserAccount userAccount = null;
            string dbPath = "Database\\MyDatabase.sqlite";  // Asegúrate de que la ruta a tu base de datos es correcta

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                // Consulta SQL para obtener los datos de la tabla
                string sql = "SELECT CUIT, RazonSocial, Direccion, Telefono, Email FROM Bussines_Info";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Si encuentra un registro, lo asigna al objeto
                        {
                            userAccount = new UserAccount
                            {
                                Cuil = reader["CUIT"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
                connection.Close();
            }
            return userAccount;  // Devuelve el objeto con los datos de la base de datos
        }


        // Método para actualizar los datos de la cuenta en la base de datos
        public static bool UpdateUserAccount(UserAccount userAccount)
        {
            try
            {
                string dbPath = "Database\\MyDatabase.sqlite";
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())  // Iniciar una transacción
                    {
                        // Consulta SQL para actualizar los datos
                        string sql = @"UPDATE Bussines_Info 
                               SET RazonSocial = @razonSocial, 
                                   Direccion = @direccion, 
                                   Telefono = @telefono, 
                                   Email = @email 
                               WHERE CUIT = @cuit";

                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@razonSocial", userAccount.RazonSocial);
                            command.Parameters.AddWithValue("@direccion", userAccount.Direccion);
                            command.Parameters.AddWithValue("@telefono", userAccount.Telefono);
                            command.Parameters.AddWithValue("@email", userAccount.Email);
                            command.Parameters.AddWithValue("@cuit", userAccount.Cuil);

                            int rowsAffected = command.ExecuteNonQuery();

                            transaction.Commit();  // Confirmar la transacción

                            return rowsAffected > 0;  // Devuelve true si se actualizó correctamente
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la información: {ex.Message}");
                return false;
            }
        }


        // Método para traer los productos al grid view 
        public static List<Product> GetStockProducts()
        {
            List<Product> products = new List<Product>();
            string dbPath = "Database\\MyDatabase.sqlite"; // Ruta de la base de datos

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                // Consulta con LEFT JOIN para incluir productos sin categoría
                string query = @"SELECT s.ProductCode, s.ProductDescription, s.StockAvailable, 
                                s.StockMin, s.UnitPrice, s.Discount, s.SupplierID, 
                                s.CategoryID, c.CategoryName 
                         FROM Stock s
                         LEFT JOIN ProductCategory c ON s.CategoryID = c.CategoryID";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Code = reader["ProductCode"].ToString(),
                                Description = reader["ProductDescription"].ToString(),
                                StockDisponible = int.TryParse(reader["StockAvailable"].ToString(), out int stockDisponible) ? stockDisponible : 0,
                                StockMin = int.TryParse(reader["StockMin"].ToString(), out int stockMinimo) ? stockMinimo : 0,
                                PrecioUnitario = double.TryParse(reader["UnitPrice"].ToString(), out double precioUnitario) ? precioUnitario : 0.0,
                                Descuento = double.TryParse(reader["Discount"].ToString(), out double descuento) ? descuento : 0.0,
                                SupplierID = int.TryParse(reader["SupplierID"].ToString(), out int proveedorID) ? proveedorID : 0,
                                CategoryID = int.TryParse(reader["CategoryID"].ToString(), out int categoryID) ? categoryID : 0,
                                CategoryName = reader["CategoryName"]?.ToString() ?? "Sin Categoría" // Si es NULL, asigna "Sin Categoría"
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }


        // Método para guardar un producto en el stock
        public static bool AddProduct(Product product)
        {
            try
            {
                string dbPath = "Database\\MyDatabase.sqlite";
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    string sql = @"INSERT INTO Stock (ProductCode, ProductDescription, StockAvailable, StockMin, UnitPrice, Discount, SupplierID, CategoryID) 
                           VALUES (@Code, @Description, @StockDisponible, @StockMin, @PrecioUnitario, @Descuento, @SupplierID, @CategoryID)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        // Asignamos los valores de los parámetros
                        command.Parameters.AddWithValue("@Code", product.Code);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@StockDisponible", product.StockDisponible);
                        command.Parameters.AddWithValue("@StockMin", product.StockMin);
                        command.Parameters.AddWithValue("@PrecioUnitario", product.PrecioUnitario);
                        command.Parameters.AddWithValue("@Descuento", product.Descuento);
                        command.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                        command.Parameters.AddWithValue("@CategoryID", product.CategoryID);  // Corregido aquí

                        // Ejecutamos el comando y verificamos si se insertó
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Si afectó filas, es éxito
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
                return false;
            }
        }

        // Método para Eliminar un producto en el stock
        public static bool DeleteProduct(string productCode)
        {
            try
            {
                string dbPath = "Database\\MyDatabase.sqlite";
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    string sql = "DELETE FROM Stock WHERE ProductCode = @ProductCode";  // Elimina el producto por código

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", productCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;  // Retorna true si se eliminó el producto
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}");
                return false;
            }
        }

        // Método para Modificar un producto en el stock
        public static bool UpdateProduct(string productCode, string description, int stockDisponible, int stockMin, double precioUnitario, double descuento, int supplierID, int categoryID)
        {
            try
            {
                string dbPath = "Database\\MyDatabase.sqlite";
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    string sql = @"UPDATE Stock 
                           SET ProductDescription = @Description, 
                               StockAvailable = @StockDisponible, 
                               StockMin = @StockMin, 
                               UnitPrice = @PrecioUnitario, 
                               Discount = @Descuento, 
                               SupplierID = @SupplierID, 
                               CategoryID = @CategoryID 
                           WHERE ProductCode = @ProductCode";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@StockDisponible", stockDisponible);
                        command.Parameters.AddWithValue("@StockMin", stockMin);
                        command.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                        command.Parameters.AddWithValue("@Descuento", descuento);
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.Parameters.AddWithValue("@CategoryID", categoryID);
                        command.Parameters.AddWithValue("@ProductCode", productCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el producto: {ex.Message}");
                return false;
            }
        }


        // Método para obtener la lista de proveedores ( que ya debe existir en la carga )
        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT SupplierID, SupplierName FROM Suppliers"; // Asegúrate de que estos campos existan
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierID = reader.GetInt32(0),
                                SupplierName = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return suppliers;
        }


        //Método para obtener Categorías ( que ya debe existir en la carga )
        public static List<ProductCategory> GetCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            string dbPath = "Database\\MyDatabase.sqlite";  // Asegúrate de que la ruta a tu base de datos es correcta

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT CategoryID, CategoryName FROM ProductCategory";
                // Asume que tienes la tabla 'ProductCategory' con estos campos
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new ProductCategory
                            {
                                CategoryID = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                }
                connection.Close();
            }

            return categories;
        }


        // Método para traer el correo del usuario
        public static string GetBusinessEmail()
        {
            string email = null;
            string dbPath = "Database\\MyDatabase.sqlite";  // Ruta de la base de datos

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT Email FROM Bussines_Info LIMIT 1";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            email = reader["Email"].ToString();
                        }
                    }
                }
            }

            return email;
        }


        //metodos para actualizar stock


        //metodo para traer el producto segun el codigo ( en el update )
        public static Product GetProductByCode(string codigoProducto)
        {
            Product product = null;
            string dbPath = "Database\\MyDatabase.sqlite";  // Asegúrate de que la ruta a tu base de datos es correcta

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM Stock WHERE ProductCode = @codigoProducto";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codigoProducto", codigoProducto);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Code = reader["ProductCode"].ToString(),
                                StockDisponible = int.Parse(reader["StockAvailable"].ToString())
                                // Puedes obtener más campos si es necesario
                            };
                        }
                    }
                }
            }

            return product;
        }
        public static void UpdateStockInDatabase(string codigoProducto, int stockSumar)
        {
            try
            {
                // Abrir conexión a la base de datos
                string dbPath = "Database\\MyDatabase.sqlite";  // Asegúrate de que la ruta a tu base de datos es correcta
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    // Obtener el stock actual del producto
                    string query = "SELECT StockAvailable FROM Stock WHERE ProductCode = @ProductCode";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", codigoProducto);

                        int stockActual = Convert.ToInt32(command.ExecuteScalar());

                        // Sumar el stock del Excel al stock actual
                        int nuevoStock = stockActual + stockSumar;

                        // Actualizar el stock en la base de datos
                        string updateQuery = "UPDATE Stock SET StockAvailable = @NuevoStock WHERE ProductCode = @ProductCode";
                        using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@NuevoStock", nuevoStock);
                            updateCommand.Parameters.AddWithValue("@ProductCode", codigoProducto);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Stock actualizado para el producto con código {codigoProducto}: nuevo stock {nuevoStock}.");
                            }
                            else
                            {
                                MessageBox.Show($"No se pudo actualizar el stock para el producto con código {codigoProducto}.");
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el stock: {ex.Message}");
            }
        }

        //metodos para reemplazar stock

        // Obtener el ID de la categoría según su nombre
        public static int GetCategoryIDByName(string categoryName)
        {
            string query = "SELECT CategoryID FROM ProductCategory WHERE CategoryName = @CategoryName";
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        // Crear una nueva categoría si no existe
        public static int CreateCategory(string categoryName)
        {
            string insertQuery = "INSERT INTO ProductCategory (CategoryName) VALUES (@CategoryName)";
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    command.ExecuteNonQuery();
                    return (int)connection.LastInsertRowId; // Devuelve el ID de la nueva categoría
                }
            }
        }

        // Método para limpiar la tabla Stock
        public static void ClearStock()
        {
            string query = "DELETE FROM Stock";
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
















        // Método para cerrar la conexión a la base de datos
        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
