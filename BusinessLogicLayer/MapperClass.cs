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


        public UserProfileViewModel BuildUserProfileViewModel(int Id, int numOfFriend, string pending, string username)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.userId = Id;
            //User user = await _businessLogicClass.GetUserByIdAsync(Id);
            model.userName = username;
            model.numberOfFriends = numOfFriend;
            model.FirendStatus = pending;
            
            return model;
        }

        public  MessagingViewModel GetMessagingViewModel(int loggedInUserId, int usertomessageId, List<Message> Messages, string LoggedInUserName, string userToMessageUserName)
        {
            MessagingViewModel viewModel = new MessagingViewModel();
            //User LoggedInUser = await _businessLogicClass.GetUserByIdAsync(loggedInUserId);
            //User UserToMessage = await _businessLogicClass.GetUserByIdAsync(usertomessageId);
            viewModel.CurrentUserId = loggedInUserId;
            viewModel.currentUserName = LoggedInUserName;
            viewModel.friendToMessageUserId = usertomessageId;
            viewModel.friendToMessageUserName = userToMessageUserName;
            viewModel.messages = Messages;

            return viewModel;
        }
    }
}
