using ETrade.Entities;
using System.Collections.Generic;

namespace ETrade.Web.Models
{
    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

    }
}
