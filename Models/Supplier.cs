using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCANHUB.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }  // ID único del proveedor
        public string SupplierName { get; set; }  // Nombre del proveedor
        public string ContactName { get; set; }  // Nombre del contacto en el proveedor
        public string Phone { get; set; }  // Teléfono del proveedor
        public string Email { get; set; }  // Correo electrónico del proveedor
        public string Address { get; set; }  // Dirección del proveedor
    }
}