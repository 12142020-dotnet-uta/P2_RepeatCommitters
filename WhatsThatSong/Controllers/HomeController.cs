using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WhatsThatSong.Models;
using BusinessLogicLayer;
using ModelLayer.Models;

namespace WhatsThatSong.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly BusinessLogicClass _businessLogicClass;



        //public HomeController(ILogger<HomeController> logger, BusinessLogicClass businessLogicLayer)
        //{
        //    _logger = logger;
        //    _businessLogicClass = businessLogicLayer;
        //}
        BusinessLogicClass _businessLogicClass = new BusinessLogicClass();

        public IActionResult Index()
        {
            //businessLogicClass.PopulateDb();
            //_businessLogicClass.CreatNewBC("ronald", "mcdonald", "ronald@mcdonald.com");

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
