using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repository;
        private readonly MapperClass _mapperClass;
        private readonly ILogger _logger;

        public BusinessLogicClass(Repository repository, MapperClass mapperClass, ILogger<Repository> logger)
        {
            _repository = repository;
            _mapperClass = mapperClass;
            _logger = logger;
        }
       

        public User LoggedInUser = new User();

        public async Task<Song> GetSongById(int id)
        {
            Song song = await _repository.GetSongById(id);
            return song;
        }

        public async Task AddSongToFavorites(int sogid)
        {

            await _repository.AddSongToFavorites(sogid, LoggedInUser.Id);
        }

        public void PopulateDb()
        {

            //_repository.populateDb();

        }

        internal Task<int> GetNumOfFriendsByUserId(int id)
        {
            return _repository.GetNumOfFriendsByUserId(id);
        }

        internal Task<string> HasPendingFrinedRequest(int id)
        {
            return _repository.HasPendingFrinedRequest(id);
        }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = await _repository.GetAllUsersAsync();
            return users;
        }

        public async Task<UserProfileViewModel> GetUserProfileViewModel(int id)
        {
            User user = await _repository.GetUserByIdAsync(id);
            int num = await _repository.GetNumOfFriendsByUserId(id);
            string pending = await _repository.HasPendingFrinedRequest(id);
            UserProfileViewModel model = _mapperClass.BuildUserProfileViewModel(id, num, pending, user.UserName);

            return model;
        }

        public async Task<List<Message>> GetMessages2users(int id, int userToMessageId)
        {
            return await _repository.GetMessages2users(id, userToMessageId); 
        }

        public async Task<List<FavoriteList>> GetUsersFavorites(int userId)
        {
            List<FavoriteList> favs = await _repository.GetUsersFavorites(userId);
            return favs; 
        }

        public async Task ConvertFileToBitArray(Song newSong)
        {
            Song song = newSong;
            await _repository.SaveSongToDb(song);
        }
        public byte[] ConvertIformFileToByteArray(IFormFile iformFile)
        {
            using (var ms = new MemoryStream())
            {
                // convert the IFormFile into a byte[]
                iformFile.CopyTo(ms);

                if (ms.Length > 2097152)// if it's bigger that 2 MB
                {
                    return null;
                }
                else
                {
                    byte[] a = ms.ToArray(); // put the string into the Image property
                    return a;
                }
            }
        }

        public async Task sendSongToRepCLass(Song song)
        {
            await _repository.SaveSongToDb(song);
        }

        /// <summary>
        /// checks to see if a user with that info already exists and returns null if the user already exist. creates a new user if the ures does not already exist.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> CreatNewBC(string userName, string password, string email)
        {
            bool userExists = await _repository.DoesUserExist(userName, password);
            if(userExists)
            {
                return null;
            }
            else 
            {
                User newUser = await _repository.CreateNewUser(userName, password,email);
                LoggedInUser = newUser;
                return newUser;
            }
        }

        public async Task<User> SaveUserToDb(User userToEdit)
        {
           await _repository.SaveUserToDb(userToEdit);
           return null;
        }

        public async Task<List<Song>> GetSongsBySearhGenre(string genre)
        {
            List<Song> originalSongs = await _repository.GetOriginalSongsByGenre(genre);
            return originalSongs;
        }

        public async Task AcceptFriend(int loggedInId,int pendingFriendId)
        {
            await _repository.AcceptRequest(loggedInId,pendingFriendId);
        }

        /// <summary>
        /// checks to see if the user exists and logs them in if they do. returns null if they dont exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> LoginUser(string userName, string password)
        {
            bool userExists = await _repository.DoesUserExist(userName, password);

            if(userExists)

            {
                User user = await _repository.GetUserByNameAndPass(userName, password);
                LoggedInUser = user;
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Song>> GetTop5Originals()
        {
            List<Song> songs = await _repository.GetTop5Originals();
            return songs;
        }

        public async Task<List<Song>> GetOriginalsongsByLyrics(string phrase)
        {
            return await _repository.GetOriginalSongByLyrics(phrase);
        }

        public async Task RequesFriend(int userid, int RerequestedFriendId)
        {
            await _repository.RequestFreind(userid, RerequestedFriendId);
           
        }

        public async Task IncrementNUmPlays(int songId)
        {
            await _repository.IncrementNumPlays(songId);
        }

        public async Task<List<User>> SearchForUsersByPartialN(string searchstring)
        {
            List<User> ListOfUsers = await _repository.GetUsersByPartialN(searchstring);
            return ListOfUsers;
        }

        public async Task<List<FriendList>> GetListOfFriendsByUserId(int id)
        {
            List<FriendList> list = await _repository.GetListOfFriendsByUserId(id);
            return list;
        }

        /// <summary>
        /// Returns a User specified by their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task DeleteFriend(int LoggedInUserId, int friendToDeleteId)
        {
            await _repository.DeletFriend(LoggedInUserId, friendToDeleteId);
        }

        /// <summary>
        /// returns a messageviewModel for 2 users
        /// </summary>
        /// <param name="UserToMessageId"></param>
        /// <returns></returns>
        public async Task<MessagingViewModel> GetMessagesViewModel(int LoggedInUserIdint, int UserToMessageId)
        {
            User LoginUser = await GetUserByIdAsync(LoggedInUserIdint);
            User user = await GetUserByIdAsync(UserToMessageId);
            List<Message> Messages = await GetMessages2users(UserToMessageId, LoggedInUserIdint);
            MessagingViewModel viewModel = _mapperClass.GetMessagingViewModel(LoggedInUserIdint, user.Id, Messages, LoginUser.UserName, user.UserName);
            return viewModel;
        }

        public async Task<MessagingViewModel> sendMessage(string FromUserName, int LoggedInUserIdint, int UserToMessageId, string content)
        {
            await  _repository.SaveMessage(FromUserName, UserToMessageId, LoggedInUserIdint, content);
            User LoginUser = await GetUserByIdAsync(LoggedInUserIdint);
            User user = await GetUserByIdAsync(UserToMessageId);
            List<Message> Messages = await GetMessages2users(UserToMessageId, LoggedInUserIdint);
            MessagingViewModel viewModel = _mapperClass.GetMessagingViewModel(LoggedInUserIdint, user.Id, Messages, LoginUser.UserName, user.UserName);
            return viewModel;
        }



        /// <summary>
        /// Returns all messages for a user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _repository.GetAllMessagesAsync();
        }
        public async Task<User> GetLoggedInUser()
        {
            return LoggedInUser;
        }
        public async Task SaveNewUser(User user)
        {
            await _repository.SaveNewUser(user);
        }
    }
}
