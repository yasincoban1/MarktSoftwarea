using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity
{
    public class Product:MyEntityBase
    {
      

        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Marka")]
        public string Brand { get; set; }
        [DisplayName("Model")]
        public string Model { get; set; }
        [DisplayName("Ürün Açıklama")]
        public string Description { get; set; }
        [DisplayName("Eski Fiyati")]
        public double OldPrice { get; set; }
        [DisplayName("Ürün Fiyatı")]
        public double Price { get; set; }
        
        [DisplayName("Stok Miktarı")]
        public int Stock { get; set; }
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }
        [DisplayName("Ana Resim")]
        public string Image { get; set; }
        [DisplayName("Alternatif Resim 1")]
        public string AlternativeImage1 { get; set; }
        [DisplayName("Alternatif Resim 2")]
        public string AlternativeImage2 { get; set; }
        [DisplayName("Alternatif Resim 3")]
        public string AlternativeImage3 { get; set; }
        [DisplayName("Ana Sayfada ?")]
        public bool IsHome { get; set; }
        [DisplayName("Stokta mı ?")]
        public bool IsApproved { get; set; }
        [DisplayName("Öne Çıkan Ürünlerde mi ?")]
        public bool IsFeatured { get; set; }
        [DisplayName("Carousel ?")]
        public bool IsCarousel { get; set; }
        [DisplayName("New Hot")]
        public string NewHot { get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }

        public virtual MarktUser Owner { get; set; }
        public virtual Category Categorys { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }


        public Product()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

    }
}
