# InHarmony

## Introduction
  This project was developed with ASP.NET Web API in the Visual Studio IDE. It implements post, account and message services by making use of HTTP methods. It utilizes spotify and soundcloud third party APIs. Early development testing was done using Postman, but has since been updated to test through xUnit tests. Frontend is tested with Jasmine and Karma. An Azure DevOps pipeline is used for CI/CD and the results of the tests are published to SonarCloud.

## Tech Stack
  - C#
  - ASP.NET Web API
  - Postman
  - Angular
  - JavaScript
  - TypeScript
  - xUnit
  - Jasmine
  - Karma
  - Azure DevOps
  - SonarCloud  
  
## Running the Project
  - You can try the project out at ```inharmony.azurewebsites.net```
### Alternatively you can run the project locally
#### Via CMD line
  - Clone the repository
  ```git clone https://github.com/12142020-dotnet-uta/P2_RepeatCommitters```
  - Navigate to the correct directory
  ```cd P2_RepeatCommitters/FrontEnd```
  - Start the Angular service
  ```ng serve```
  - Open your internet browser and navigate to
  ```localhost:4200```

    
## API Routing
|                            URI                             |     Method    |            Response            |
| ---------------------------------------------------------- | ------------- |--------------------------------|
| /Home/SongEditHC                                           |      POST     | 201: Success <br> 409: Failure |
| /Song/uploadSong                                           |      POST     | 201: Success <br> 409: Failure |
| /user/CreateUser                                           |      POST     | 201: Success <br> 409: Failure |
| /user/RequestFriend                                        |      POST     | 201: Success <br> 409: Failure |
| /user/sendMessage                                          |      POST     | 201: Success <br> 409: Failure |
| /user/SaveEdit                                             |      PUT      | 202: Success <br> 404: Failure |
| /user/EditFriendStatus                                     |      PUT      | 202: Success <br> 404: Failure |
| /Home/Index                                                |      GET      | 202: Success <br> 404: Failure |
| /Song/getSong                                              |      GET      | 202: Success <br> 404: Failure |
| /Song/addSongToFavorites                                   |      GET      | 202: Success <br> 404: Failure |
| /Song/GetAllSongsByACertainUser                            |      GET      | 202: Success <br> 404: Failure |
| /Song/getOriginalsongSearch                                |      GET      | 202: Success <br> 404: Failure |
| /Song/getOriginalsongsByLyrics                             |      GET      | 202: Success <br> 404: Failure |
| /Song/getTop5Originals                                     |      GET      | 202: Success <br> 404: Failure |
| /Song/incrmentNumPlays                                     |      GET      | 202: Success <br> 404: Failure |
| /Song/GetUsersFavoriteSongs                                |      GET      | 202: Success <br> 404: Failure |
| /Song/Get5FavoriteSongsForUser                             |      GET      | 202: Success <br> 404: Failure |
| /Song/isSongAlreadyAFavorite                               |      GET      | 202: Success <br> 404: Failure |
| /Song/IsSongInDb                                           |      GET      | 202: Success <br> 404: Failure |
| /user/login                                                |      GET      | 202: Success <br> 404: Failure |
| /user/EditButton                                           |      GET      | 202: Success <br> 404: Failure |
| /user/SearchForUsers                                       |      GET      | 202: Success <br> 404: Failure |
| /user/GetFriends                                           |      GET      | 202: Success <br> 404: Failure |
| /user/GetFriendsAsUsers                                    |      GET      | 202: Success <br> 404: Failure |
| /user/GoToChat                                             |      GET      | 202: Success <br> 404: Failure |
| /user/BakToProfile                                         |      GET      | 202: Success <br> 404: Failure |
| /user/getAllUsers                                          |      GET      | 202: Success <br> 404: Failure |
| /user/getUserByIdaAsync                                    |      GET      | 202: Success <br> 404: Failure |
| /user/GetAllMessagesAsync                                  |      GET      | 202: Success <br> 404: Failure |
| /user/DisplayAllFriendRequests                             |      GET      | 202: Success <br> 404: Failure |
| /user/AreWeFriends                                         |      GET      | 202: Success <br> 404: Failure |
| /Song/DeleteSongFromFavorites                              |     DELETE    | 202: Success <br> 404: Failure |
| /Song/DeleteUploadedSong                                   |     DELETE    | 202: Success <br> 404: Failure |
| /user/DeleteFriend                                         |     DELETE    | 202: Success <br> 404: Failure |

