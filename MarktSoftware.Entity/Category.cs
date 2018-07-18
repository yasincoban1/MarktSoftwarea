using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity
{
    public class Category:MyEntityBase
    {       
        [DisplayName("Kategori Adı")]
        [StringLength(maximumLength: 20, ErrorMessage = "en fazla 20 karakter girebilirsiniz.")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
      
    }
}
