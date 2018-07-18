using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktSoftware.Entity.Enum
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Kargoda")]
        İn_Cargo,
        [Display(Name = "Tamamlandı")]
        Completed
    }
}
