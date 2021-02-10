# InHarmony

## Introduction
  This project was developed with ASP.NET Web API in the Visual Studio IDE. It implements post, account and message services by making use of HTTP methods. It utilizes spotify and soundcloud third party APIs. Early development testing was done using Postman, but has since been updated to test through xUnit tests. Frontend is tested with Jasmine and Karma. An Azure DevOps pipeline is used for CI/CD and the results of the tests are published to SonarCloud.

## Tech Stack
  - C#
  - ASP.NET MVC
  - Postman
  - xUnit
  - Jasmine
  - Karma
  - Azure DevOps
  - SonarCloud  
  
## Running the Project
  - You can try the project out at ```inharmony.azurewebsites.net```
### Alternatively you can run the project locally
  - Clone the repository
  ```git clone https://github.com/12142020-dotnet-uta/P2_RepeatCommitters```
  - Navigate to the correct directory
  ```cd P2_RepeatCommitters```

    
## API Routing
|                            URI                             |     Method    |            Response            |
| ---------------------------------------------------------- | ------------- |--------------------------------|
| /api/v1/register                                           |      POST     | 201: Success <br> 409: Failure |
| /api/v1/create_post                                        |      POST     | 201: Success <br> 409: Failure |
| /api/v1/send_message                                       |      POST     | 201: Success <br> 409: Failure |
| /api/v1/update_email                                       |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/increment_karma                                    |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/decrement_karma                                    |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/upvote_post/<int:post_id>                          |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/downvote_post/<int:post_id>                        |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/favorite_message/<int:message_id>                  |      PUT      | 202: Success <br> 404: Failure |
| /api/v1/retrieve_post/<int:post_id>                        |      GET      | 202: Success <br> 404: Failure |
| /api/v1/list_posts_by_comm/<string:community>/<int:number> |      GET      | 202: Success <br> 404: Failure |
| /api/v1/list_posts/<int:number>                            |      GET      | 202: Success <br> 404: Failure |
| /api/v1/list_post_votes/<int:post_id>                      |      GET      | 202: Success <br> 404: Failure |
| /api/v1/deactivate_account/<int:user_id>                   |     DELETE    | 202: Success <br> 404: Failure |
| /api/v1/delete_post/<int:post_id>                          |     DELETE    | 202: Success <br> 404: Failure |
| /api/v1/delete_message/<int:message_id>                    |     DELETE    | 202: Success <br> 404: Failure |
