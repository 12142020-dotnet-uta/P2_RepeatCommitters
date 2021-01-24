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
        //private readonly Repository _repository;
        ////private readonly ILogger _logger;
        //private readonly BusinessLogicClass _businessLogicClass;

        //public MapperClass(Repository repository, BusinessLogicClass businessLogicClass)
        //{
        //    //_repository = repository;
        //    //_logger = logger;
        //    _businessLogicClass = businessLogicClass;
        //}


        public async Task<UserProfileViewModel> BuildUserProfileViewModel(int Id, User u, int numOfFriend, string pending)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.userId = Id;
            User user = u;
            model.userName = user.UserName;
            model.numberOfFriends = numOfFriend;
            model.FirendStatus = pending;
            
            return model;
        }

        internal async Task<MessagingViewModel> GetMessagingViewModel(int UserToMessageId, User loggedInUser, User usertomessage, List<Message> Messages)
        {
            MessagingViewModel viewModel = new MessagingViewModel();
            User LoggedInUser = loggedInUser;
            User UserToMessage = usertomessage;
            viewModel.CurrentUserId = LoggedInUser.Id;
            viewModel.currentUserName = LoggedInUser.UserName;
            viewModel.friendToMessageUserId = UserToMessage.Id;
            viewModel.friendToMessageUserName = UserToMessage.UserName;
            viewModel.messages = Messages;

            return viewModel;
        }
    }
}
