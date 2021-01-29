using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsThatSong.Controllers;
using Xunit;

namespace WhatsThatTest
{
    public class ControllerTests
    {

        private readonly ILogger<Repository> _repositoryLogger;
        private readonly ILogger<UserController> _userControllerLogger;
        private readonly MapperClass _mapperClass = new MapperClass();

        [Fact]
        public void CreateUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<UserProfileViewModel> upvm = userController.CreateUser(user.UserName, user.Password, user.Email);
                Assert.NotNull(upvm);
            }
        }

        [Fact]
        public void LoginTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<UserProfileViewModel> upvm = userController.CreateUser(user.UserName, user.Password, user.Email);
                Task<UserProfileViewModel> upvm2 = userController.login(user.UserName, user.Password);
                Assert.NotNull(upvm2);
            }
        }

        [Fact]
        public void GetUserToEditTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.SaveNewUser(user).Wait();

                Task<User> u = userController.GetUserToEdit(user.Id);
                Assert.NotNull(u);
            }
        }

        /* TODO: Failing for multiple reasons. NullReferenceException, Assert evaluating to false, DatabaseUpdateConcurrency issue. Reason unknown, suspect async/await.*/
        [Fact]
        public void EditUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);

                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // add and save new user
                repository.SaveNewUser(user).Wait();

                // attempt to edit that user
                Task<User> u = userController.GetUserToEdit(user.Id);
                userController.EditUser(user.Id, u.Result.UserName, u.Result.Password, u.Result.Email, u.Result.FirstName, "Test2").Wait();

                // check that user was edited
                Assert.Equal("Test2", user.LastName);
            }
        }

        /* TODO: Assert inconsistently evaluating to false. Reason unknown, suspect async/await.*/
        [Fact]
        public void SearchForUsersTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);

                var userList = new List<User>();
                // create some dummy users
                for (int i = 0; i < 10; i++)
                {
                    // j0, j2, j4 ... j8
                    var user = new User
                    {
                        Id = int.MaxValue - i,
                        UserName = "jtest" + i,
                        Password = "Test1!",
                        FirstName = "Johnny" + i,
                        LastName = "Test",
                        Email = "johnnytest123" + i + "@email.com"
                    };

                    // g1, g3, g5 ... g9
                    var user2 = new User
                    {
                        Id = int.MaxValue - ++i,
                        UserName = "greg" + i,
                        Password = "Test1!",
                        FirstName = "Greg" + i,
                        LastName = "Smeg",
                        Email = "johnnytest123" + i + "@zmail.com"
                    };

                    repository.SaveNewUser(user).Wait();
                    repository.SaveNewUser(user2).Wait();
                }

                // with user populated db, let's search for the letter g
                var userList2 = userController.SearchForUsers("g");
                // we expect 5 users to return in the list
                Assert.Equal(5, userList2.Result.Count);
            }
        }

        [Fact]
        public void FriendRequestTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                userController.FriendRequest(user.Id, user2.Id).Wait();
                Assert.NotNull(repository.friendList);
            }
        }

        [Fact]
        public void AcceptFriendTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var fl = new FriendList { FriendId = user.Id, RequestedFriendId = user2.Id };
                repository.friendList.Add(fl);
                context.SaveChanges();

                userController.AcceptFriend(fl).Wait();
                Assert.NotNull(repository.friendList.FirstOrDefault(x => x.status == "accept"));
            }
        }

        [Fact]
        public void GetFriendsByUserIdTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var fl = new FriendList { FriendId = user.Id, RequestedFriendId = user2.Id, status = "accept" };
                repository.friendList.Add(fl);
                context.SaveChanges();

                Assert.NotNull(userController.GetFriendsByUserId(user.Id));
            }
        }

        [Fact]
        public void GetFriendsAsUsers()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var fl = new FriendList { FriendId = user.Id, RequestedFriendId = user2.Id, status = "accept" };
                repository.friendList.Add(fl);
                context.SaveChanges();

                Assert.NotNull(userController.GetFriendsAsUsers(user.Id));
            }
        }

        [Fact]
        public void DeleteFriendTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var fl = new FriendList { FriendId = user.Id, RequestedFriendId = user2.Id, status = "accept" };
                repository.friendList.Add(fl);
                context.SaveChanges();

                var list = userController.DeleteFriend(user.Id, user2.Id);

                Assert.Empty(list.Result);
            }
        }

        [Fact]
        public void SendMessageTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var message = new Message { ToUserId = user.Id, FromUserId = user2.Id, FromUserName = user2.UserName, Content = "Thicc dummy data" };

                var mvm = userController.SendMessage(message.FromUserName, message.FromUserId, message.ToUserId, message.Content);

                Assert.NotNull(mvm);
            }
        }

        [Fact]
        public void GetMessagesBetween2UsersTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var message = new Message { ToUserId = user.Id, FromUserId = user2.Id, FromUserName = user2.UserName, Content = "Thicc dummy data" };

                var mvm = userController.SendMessage(message.FromUserName, message.FromUserId, message.ToUserId, message.Content);

                Assert.NotNull(mvm);
            }
        }

        [Fact]
        public void BackToProfileTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.SaveNewUser(user).Wait();

                var upvm = userController.BackToProfile(user.Id);

                Assert.NotNull(upvm);
            }
        }

        [Fact]
        public void GetAllUsersAsyncTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                Assert.NotEmpty(userController.GetAllUsersAsync().Result);
            }
        }

        [Fact]
        public void GetUserByIdAsyncTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.SaveNewUser(user).Wait();

                Assert.NotNull(userController.GetUserByIdAsync(user.Id));
            }
        }

        [Fact]
        public void GetAllMessagesAsyncTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                Assert.Empty(userController.GetAllMessagesAsync().Result);

                var message = new Message { ToUserId = user.Id, FromUserId = user2.Id, FromUserName = user2.UserName, Content = "Thicc dummy data" };
                repository.messages.Add(message);
                context.SaveChanges();

                Assert.NotEmpty(userController.GetAllMessagesAsync().Result);
            }
        }

        [Fact]
        public void DisplayAllFriendRequestsTest()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestControllerDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // create a second user
                var user2 = new User
                {
                    UserName = "greg",
                    Password = "Test1!",
                    FirstName = "Greg",
                    LastName = "Smeg",
                    Email = "johnnytest123@zmail.com"
                };

                repository.SaveNewUser(user).Wait();
                repository.SaveNewUser(user2).Wait();

                var fl = new FriendList { FriendId = user.Id, RequestedFriendId = user2.Id };
                repository.friendList.Add(fl);
                context.SaveChanges();

                Assert.NotEmpty(userController.DisplayAllFriendRequests(user2.Id).Result);
            }
        }

        [Fact]
        public void AreWeFriendsTest()
        {

        }
    }
}
