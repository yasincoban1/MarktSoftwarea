using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity
{
    public class Comment:MyEntityBase
    {
        [Required, StringLength(300)]
        [DisplayName("Yorum")]
        public string Text { get; set; }

        public virtual Product Product { get; set; }
        public virtual MarktUser Owner { get; set; }
    }
}
