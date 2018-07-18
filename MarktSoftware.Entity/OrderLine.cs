using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity
{
    public class OrderLine
    {
        public int Id { get; set; }
        [DisplayName("Sipariş Numarası")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [DisplayName("Miktarı")]
        public int Quantity { get; set; }
        [DisplayName("Ücreti")]
        public double Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
