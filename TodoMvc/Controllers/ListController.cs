﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoMvc.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}