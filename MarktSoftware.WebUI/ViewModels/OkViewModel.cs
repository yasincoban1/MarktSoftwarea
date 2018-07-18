using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarktSoftware.WebUI.ViewModels
{
    public class OkViewModel : NotifyViewModel<string>
    {
        public OkViewModel()
        {
            Title = "İşlem Başarılı.";
        }
    }
}