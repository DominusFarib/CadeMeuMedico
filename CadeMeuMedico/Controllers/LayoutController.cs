﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedico.Controllers
{
    public class LayoutController : BaseController
    {
        //
        // GET: /Layout/

        public ActionResult Index()
        {
            return View();
        }

    }
}