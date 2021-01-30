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
       

        public async Task<Song> GetSongById(int id)
        {
            Song song = await _repository.GetSongById(id);
            return song;
        }

        public async Task AddSongToFavorites(int songid, int userId)
        {
            List<FavoriteList> AllUsersIdfavoriteLists = await _repository.GetUsersFavorites(userId);
            foreach (var item in AllUsersIdfavoriteLists)
            {
                if (item.SongId == songid)
                {
                    return;
                }
                else
                {
                    FavoriteList fSong = new FavoriteList(songid, userId);
                    await _repository.AddSongToFavorites(fSong);
                }
            }
        }


        public async Task<int> GetNumOfFriendsByUserId(int id)
        {
            return await _repository.GetNumOfFriendsByUserId(id);
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
            FriendList friendsWithPending = await _repository.HasPendingFrinedRequest(id);
            string pending = null;
            if (friendsWithPending != null)
            {
                pending = "pending";
            }
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

        public async Task SaveSong(Song song)
        {
            Song s = await _repository.GetSongById(song.Id);
            if(s == null)
            {
                await _repository.SaveSongToDb(song);
            }
        }

        public async Task<List<User>> GetFriendsToUserList(int id)
        {
            List<User> u = new List<User>();
            List<FriendList> friends = await _repository.GetListOfFriendsByUserId(id);
            foreach(var item in friends)
            {
                if(item.FriendId == id)
                {
                    User x = await _repository.GetUserByIdAsync(item.RequestedFriendId);
                    u.Add(x);
                }
                else if(item.RequestedFriendId == id)
                {
                    User x = await _repository.GetUserByIdAsync(item.FriendId);
                    u.Add(x);
                }
            }
            return u;
        }

        public async Task<List<Song>> GetallSongsByAUser(int userId)
        {
            User user = await GetUserByIdAsync(userId);
            List<Song> songlistByUserName = await _repository.GetAllSongsByUserName(user.UserName);
            return songlistByUserName; 
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
                return newUser;
            }
        }

        public async Task<User> SaveUserToDb(User userToEdit)
        {
           return await _repository.SaveUserToDb(userToEdit);
        }

        public async Task<List<Song>> GetSongsBySearhGenre(string genre)
        {
            List<Song> originalSongs = await _repository.GetOriginalSongsByGenre(genre);
            return originalSongs;
        }

        public async Task AcceptFriend(FriendList friend)
        {
            FriendList friendToEdit = await _repository.GetFriendListFriend(friend.FriendId, friend.RequestedFriendId);
            friendToEdit.status = friend.status;
            await _repository.AcceptRequest(friendToEdit);
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

        public async Task<FriendList> RequestFriend(int userid, int RerequestedFriendId)
        {
            return await _repository.RequestFriend(userid, RerequestedFriendId);
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

        public async Task<List<FriendList>> GetAllFriendRequestsOUserId(int userId)
        {
            List<FriendList> list = await _repository.GetAllFRiendRequestsByUserId(userId);
            return list;
        }

        public async Task<bool> AreWeFriends(int userId, int friendId)
        {
            FriendList f = await _repository.GetFriendByBothIds(userId, friendId);
            FriendList fReverseIds = await _repository.GetFriendByBothIds(friendId, userId);

            if (f != null || fReverseIds != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public async Task DeleteFriend(int friendListId)
        {
            await _repository.DeleteFriendByFreindListId(friendListId);
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

        public async Task SaveNewUser(User user)
        {
            await _repository.SaveNewUser(user);
        }
    }
}
