using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;

namespace BusinessLogicLayer
{
    public class MapperClass
    {
        private readonly Repository _repository;
        private readonly ILogger _logger;
        private readonly BusinessLogicClass _businessLogicClass;

        public MapperClass(Repository repository, ILogger<Repository> logger, BusinessLogicClass businessLogicClass)
        {
            _repository = repository;
            _logger = logger;
            _businessLogicClass = businessLogicClass;
        }


        public async Task<UserProfileViewModel> BuildUserProfileViewModel(int Id)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.userId = Id;
            User user = await _repository.GetUserByIdAsync(Id);
            model.userName = user.UserName;
            model.numberOfFriends = await _repository.GetNumOfFriendsByUserId(Id);
            model.FirendStatus = await _repository.HasPendingFrinedRequest(Id);
            
            return model;
        }

        internal async Task<MessagingViewModel> GetMessagingViewModel(int UserToMessageId)
        {
            MessagingViewModel viewModel = new MessagingViewModel();
            User LoggedInUser = await _businessLogicClass.GetLoggedInUser();
            User UserToMessage = await _businessLogicClass.GetUserByIdAsync(UserToMessageId);
            viewModel.CurrentUserId = LoggedInUser.Id;
            viewModel.currentUserName = LoggedInUser.UserName;
            viewModel.friendToMessageUserId = UserToMessage.Id;
            viewModel.friendToMessageUserName = UserToMessage.UserName;
            viewModel.messages = await _repository.GetMessages2users(LoggedInUser.Id, UserToMessageId);

            return viewModel;
        }
    }
}
