using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarktSoftware.WebUI.ViewModels
{
    public class WarningViewModel : NotifyViewModel<string>
    {
        public WarningViewModel()
        {
            Title = "Uyarı!";
        }
    }
}