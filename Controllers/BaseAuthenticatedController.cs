﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barker.Controllers
{
    [RequireHttps]
    [Authorize]
    public class BaseAuthenticatedController : Controller
    {
       
    }
}