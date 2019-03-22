using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class SupplierModelView
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public ICollection<ProductModelView> Products { get; set; }
    }
}