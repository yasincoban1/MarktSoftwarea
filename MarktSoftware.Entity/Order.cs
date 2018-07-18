
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktSoftware.Entity.Enum;

namespace MarktSoftware.Entity
{
    public class Order
    {
        public int Id { get; set; }
        [DisplayName("Sipariş Numarası")]
        public string OrderNumber { get; set; }
        [DisplayName("Toplam Tutar")]
        public double Total { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Siparişin Durumu")]
        public EnumOrderState OrderState { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [DisplayName("Adres Başlığı")]
        public string AdresBasligi { get; set; }

        public string Adres { get; set; }
        [DisplayName("Şehir")]
        public string Sehir { get; set; }

        public string Semt { get; set; }

        public string Mahalle { get; set; }

        [DisplayName("Posta Kodu")]
        public string PostaKodu { get; set; }

        public virtual List<OrderLine> Orderlines { get; set; }
    }
}
