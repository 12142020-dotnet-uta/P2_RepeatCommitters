using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.ViewModels;


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

        public BusinessLogicClass()
        {
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

        //public void PopulateDb()
        //{
        //    _repository.populateDb();

        //}

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<UserProfileViewModel> GetUserProfileViewModel(int id)
        {
            UserProfileViewModel model = await _mapperClass.BuildUserProfileViewModel(id);
            return model;
        }

        public async Task<List<FavoriteList>> GetUsersFavorites(int userId)
        {
            List<FavoriteList> favs = await _repository.GetUsersFavorites(userId);
            return favs; 
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
            if(!userExists)
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

        public async Task<List<Song>> GetOriginalsongsByLyrics(string phrase)
        {
            return await _repository.GetOriginalSongByLyrics(phrase);
        }

        public async Task RequesFriend(int userid, int RerequestedFriendId)
        {
            _repository.RequestFreind(userid, RerequestedFriendId);
           
        }

        public async Task<List<User>> SearchForUsersByPartialN(string searchstring)
        {
            List<User> ListOfUsers = await _repository.GetUsersByPartialN(searchstring);
            return ListOfUsers;
        }

        public async Task<List<FriendList>> GetListOfFriendsByUserId(int id)
        {
            List<FriendList> list = await _repository.GetListOfFriendsByUserId(id);
            return list; ;
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

        public async Task DeleteFriend(int id)
        {
            await _repository.DeletFriend(LoggedInUser.Id, id);
        }

        /// <summary>
        /// returns a messageviewModel for 2 users
        /// </summary>
        /// <param name="UserToMessageId"></param>
        /// <returns></returns>
        public async Task<MessagingViewModel> GetMessagesViewModel(int UserToMessageId)
        {
            MessagingViewModel viewModel = await _mapperClass.GetMessagingViewModel(UserToMessageId);
            return viewModel;
        }

        public async Task<MessagingViewModel> sendMessage(int userToMessageId, string content)
        {
            int fromUserId = LoggedInUser.Id;
            await  _repository.SaveMessage(userToMessageId, fromUserId, content);
            MessagingViewModel viewModel = await _mapperClass.GetMessagingViewModel(userToMessageId);
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
    }
}
