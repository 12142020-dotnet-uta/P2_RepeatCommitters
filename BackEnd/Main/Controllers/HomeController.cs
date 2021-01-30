using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using ModelLayer.Models;
using Microsoft.AspNetCore.Hosting;
using ModelLayer.ViewModels;

namespace WhatsThatSong.Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : ControllerBase
    {
        //private IHostingEnvironment _env;
        //private readonly ILogger<HomeController> _logger;
        //private readonly BusinessLogicClass _businessLogicClass;



        //public HomeController(ILogger<HomeController> logger, BusinessLogicClass businessLogicLayer, IHostingEnvironment env)
        //{
        //    _logger = logger;
        //    _businessLogicClass = businessLogicLayer;
        //    _env = env;
        //}
       // BusinessLogicClass _businessLogicClass = new BusinessLogicClass();

        //[HttpGet]
        //[Route("Index")]
        //public IActionResult Index()
        //{
        //   //_businessLogicClass.PopulateDb();
        //    //_businessLogicClass.CreatNewBC("ronald", "mcdonald", "ronald@mcdonald.com");

        //    return View("login");
        //}

        [HttpPost]
        [Route("SongEditHC")]
        public IActionResult SongEditHC(int Id, string ArtistName)
        {
            //_businessLogicClass.PopulateDb();
            //_businessLogicClass.CreatNewBC("ronald", "mcdonald", "ronald@mcdonald.com");

            return RedirectToAction("UploadSongWithFile","Song");
        }


        //public IActionResult GotoUsercontroller(string userName, string email, string password)
        //{
        //    User user = new User(userName, password, email);
        //    return RedirectToAction("User", user);
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
