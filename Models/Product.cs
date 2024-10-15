namespace SCANHUB.Models
{
    public class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int StockDisponible { get; set; }
        public int StockMin { get; set; }
        public double PrecioUnitario { get; set; }
        public double Descuento { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }  // Cambiado a int para coincidir con el tipo en la base de datos
        public string CategoryName { get; set; }
    }
}
