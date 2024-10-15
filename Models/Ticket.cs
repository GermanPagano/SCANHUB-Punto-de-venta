using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCANHUB.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }            // Identificador único del ticket
        public string ProductCode { get; set; }      // Código del producto vendido
        public int Quantity { get; set; }            // Cantidad de productos vendidos
        public double UnitPrice { get; set; }        // Precio unitario
        public double TotalPrice { get; set; }       // Precio total (cantidad x precio unitario)
        public int SaleID { get; set; }              // Relacionado con la venta (Sale)
    }
}

