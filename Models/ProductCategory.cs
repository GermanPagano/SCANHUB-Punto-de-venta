using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCANHUB___INVENTARIO_Y_CAJA.Models
{
    public class ProductCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        // Constructor opcional
        public ProductCategory() { }

        public ProductCategory(int productCategoryID, string categoryName)
        {
            CategoryID = productCategoryID;
            CategoryName = categoryName;
        }
    }
}
