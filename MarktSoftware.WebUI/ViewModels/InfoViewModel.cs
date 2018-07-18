using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarktSoftware.WebUI.ViewModels
{
    public class InfoViewModel : NotifyViewModel<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme";
        }
    }

}