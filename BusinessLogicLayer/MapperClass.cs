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

        public MapperClass(Repository repository, BusinessLogicClass _businessLogicClass, ILogger<Repository> logger)
        {
            _repository = repository;
            _logger = logger;
            _businessLogicClass = _businessLogicClass;
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
    }
}
