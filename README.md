# InHarmony

## Introduction

  InHarmony is a social space for independent musicians. The main purpose of inHarmony is to share original music, collaborate, make friends, discover new artists, and expand your fanbase. This project was developed with ASP.NET Web API in the Visual Studio IDE. It implements song management, account and message services by making use of simple HTTP methods. It utilizes Spotify and Soundcloud third-party APIs for finding published songs on the respective platforms. Early API development testing was done using Postman. The backend services are tested with mock data via xUnit tests, and the frontend is tested with spy objects via Jasmine and Karma. An Azure DevOps pipeline is used for CI/CD and the results of the tests are published to SonarCloud.

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
  
## Features

Currently implemented:
  - Register, login and edit users
  - Add other users as friends
  - Send messages to friends
  - Upload, search and delete songs
  - Songs can be searched by lyrics
  - Favorite songs

To-do list:
  - Search by genre
  - Create a user playlist
  - Login with SoundCloud/Spotify Option
  - Authentication with Okta

  
## Getting Started

  - You can try the project out at ```inharmony.azurewebsites.net```
  
### Alternatively you can run the project locally

#### Via CMD line

(Due to lack of resources, this process has only been tested on a Windows machine. Consequently, the following commands may not work on Unix.)
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

## Usage

> TODO: Update this section
Create a user... Log in... etc.

## Contributors

> Ryan Archer, Joel Barnum, Dayton Schuh, Andrew Stefanshyn

## License

This project uses the following license: [MIT](https://github.com/12142020-dotnet-uta/P2_RepeatCommitters/blob/main/LICENSE).
