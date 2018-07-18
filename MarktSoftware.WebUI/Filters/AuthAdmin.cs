﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarktSoftware.WebUI.Models;


namespace MarktSoftware.WebUI.Filters
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.IsAdmin == false)
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}