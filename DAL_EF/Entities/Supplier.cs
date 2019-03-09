using System.Collections.Generic;

namespace DAL_EF.Entities
{
    public partial class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
