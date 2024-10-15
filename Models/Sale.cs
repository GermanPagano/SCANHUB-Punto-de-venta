using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCANHUB.Models
{
    public class Sale
    {
        public int SaleID { get; set; }          // Identificador único de la venta
        public DateTime SaleDate { get; set; }   // Fecha de la venta
        public double TotalAmount { get; set; }  // Monto total de la venta
        public string PaymentMethod { get; set; } // Método de pago (efectivo, tarjeta, etc.)
        public int TicketID { get; set; }        // Relacionado al Ticket
    }
}
