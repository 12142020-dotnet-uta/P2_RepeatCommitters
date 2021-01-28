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
using Microsoft.Extensions.Hosting.Internal;
//using System.Web.Abstractions;



namespace WhatsThatSong.Controllers
{
    [ApiController]
    [Route("Song")]
    public class SongController : ControllerBase
    {
        private HostingEnvironment _env;
        private readonly BusinessLogicClass _businessLogicClass;
        private readonly ILogger<SongController> _logger;

        public SongController(BusinessLogicClass businessLogicClass, ILogger<SongController> logger, HostingEnvironment env)
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
        public async Task addSongToFavorites(int songid, int userId)
        {
            await _businessLogicClass.AddSongToFavorites(songid, userId);
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

        //[HttpPost]
        //[Route("uploadSongWithFile")]
        //public async void UploadSongWithFile(IFormFile file, int userid, string artist,
        //    string genre, string title, string lyrics, string urlPath, bool isOriginal)
        //{
        //    Song NewSong = new Song(artist, genre,title, lyrics, urlPath, isOriginal);
        //    byte[] songByte = _businessLogicClass.ConvertIformFileToByteArray(file);
        //    NewSong.ByteArrayImage = songByte;
        //    await _businessLogicClass.ConvertFileToBitArray(NewSong);

        //string path = @"\wwwroot\Songs\" + title;
        //if (file != null)
        //{
        //    var dir = _env.ContentRootPath;
        //    using (var fileStream = new FileStream(Path.Combine(dir, path), FileMode.Create, FileAccess.Write))
        //    {
        //        //string path = Path.Combine(dir, file.FileName);
        //        await file.CopyToAsync(fileStream);
        //        Song song = new Song(artist, genre, title, lyrics, path, isOriginal);
        //        await _businessLogicClass.sendSongToRepCLass(song);
        //    }

        //file.CopyToAsync(path + file.FileName);

        //}
        //}

        [HttpPost]
        [Route("uploadSong")]
        public async Task UploadSong(int userid, string userName,
            string genre, string title, string lyrics, string urlPath, bool isOriginal)
        {
            Song s = new Song(userName, genre, title, lyrics, urlPath, isOriginal);
            await _businessLogicClass.SaveSong(s);

        }

        /// <summary>
        /// return a list of original songs searched by genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getOriginalsongSearch")]
        public async Task<List<Song>> GetOriginalSongSearchByGenre(string genre)
        {
            List<Song> originalSongSearch = await _businessLogicClass.GetSongsBySearhGenre(genre);
            return originalSongSearch;

        }

        /// <summary>
        /// gets a list of original song serached by lyric phrase
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getOriginalsongsByLyrics")]
        public async Task<List<Song>> GetOriginalsongsByLyrics(string phrase)
        {
            List<Song> originalSongSearch = await _businessLogicClass.GetOriginalsongsByLyrics(phrase);
            return originalSongSearch;

        }

        /// <summary>
        /// return the top 5 songs basedon the number of plays
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getTop5Originals")]
        public async Task<List<Song>> GetTop5Originals()
        {
            List<Song> songs = await _businessLogicClass.GetTop5Originals();
            return songs;
        }

        /// <summary>
        /// increments the song number of plays property
        /// </summary>
        [HttpPost]
        [Route("incrementNumPlays")]
        public async Task IncrementNumPlays(int songId)
        {
            await _businessLogicClass.IncrementNUmPlays(songId);
        }
    }
}
