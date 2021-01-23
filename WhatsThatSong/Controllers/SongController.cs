using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
//using System.Web.Abstractions;



namespace WhatsThatSong.Controllers
{
    public class SongController : ControllerBase
    {
        private IHostingEnvironment _env;
        private readonly BusinessLogicClass _businessLogicClass;
        private readonly ILogger<SongController> _logger;

        public SongController(BusinessLogicClass businessLogicClass, ILogger<SongController> logger, IHostingEnvironment env)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// gets a song based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getSong")]
        public async Task<Song> GetSongByIdAsync(int id)
        {
            return await _businessLogicClass.GetSongById(id);
        }

        /// <summary>
        /// adds a song to favorites. takes in the song id
        /// </summary>
        /// <param name="songid"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("addSongToFavorites")]
        public async Task addSongToFavorites(int songid)
        {
            await _businessLogicClass.AddSongToFavorites(songid);
        }

        /// <summary>
        /// gets all of a users favorite songs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllSongs")]
        public async Task<List<FavoriteList>> GetUsersFavorites(int userId)
        {
            List<FavoriteList> favs = await _businessLogicClass.GetUsersFavorites(userId);
            return favs;
        }

        [HttpPost]
        [Route("uploadSong")]
        public async void  UploadSong(IFormFile file)
        {
            //string path = @"\WhatsThatSong\wwwroot\Songs\";
            if (file != null)
            {
                var dir = _env.ContentRootPath;
                using (var fileStream = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }

                //file.CopyToAsync(path + file.FileName);
                
            }
        }
    }
}
