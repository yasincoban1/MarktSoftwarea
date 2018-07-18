
using MarktSoftware.Common;
using MarktSoftware.Entity;
using MarktSoftware.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace MarktSoftware.WebUI.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            MarktUser user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }
}