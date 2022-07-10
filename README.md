# Tv Show Api

Tv Show Api provides a REST API that allows you to insert series into the database through a worker.
List series and their details. It is also possible to create a list of your favorite movies and series.

Questions and requests related to API access should be sent to the email rodrigomac.rb@gmail.com.br.

Resources available for access via API:

Users
* [**Insert a new user**](#reference/resource/user)
* [**Generate access token**](#reference/resource/user)

Tv show
* [**List all series**](#reference/resource/TvShow)
* [**List all series by name**](#reference/resource/TvShow)
* [**List all series by genre**](#reference/resource/TvShow)
* [**List details of a given series**](#reference/resource/TvShow)
* [**Add a series to favorites**](#reference/resource/TvShow)
* [**List all a user's favorites**](#reference/resource/TvShow)
* [**Remove series from favorites list**](#reference/resource/TvShow)

## Methods
Requests to the API must follow the standards:
| Method | Description |
|---|---|
| `GET` | Returns information from one or more records. |
| `POST` | Used to create a new record. |
| `PUT` | Update record data or change its status. |
| `DELETE` | Removes a system registry. |

## Answers

| Code | Description |
|---|---|
| `200` | Request executed successfully (success).|
| `400` | Validation errors or the entered fields do not exist in the system.|
| `401` | Unauthorized user.|
| `500` | Server processing difficulty.|

## 🚀 Starting

These instructions will allow you to get a working copy of the project on your local machine for development and testing purposes.
See **Deployment** for how to deploy the project.


### 📋 Prerequisites

- Sql server instance;
- sdk .net 5.0

### 🔧 Installation

Follow the steps below to successfully execute the project:
###### Step 1:
Configure the connection string in the following files:
- WorkenContext.cs;
- Context.cs;
- appsettings.json (inside TvShowWebApi)

###### Step 2:
After installing the Sql Server, sdk and cofigurate the connection string, open the project in visual studio, in the nuget package manager console point the default project to Infrastructure and run the following command: 
> update-database -Context Context

###### Step 3:
Run Worker.

In project folder 6, right click on WorkerService project -> debug -> Start without debugging.

This execution will insert the TV series data into your database.

###### Step 4:
Set TvShowWebApi as startup project if not already set and run the application.

After this execution, the Swagger page will open and you can use the endpoints.

![image](https://user-images.githubusercontent.com/32282276/178128670-c60c5afa-6633-49f9-accf-58a9d71612ad.png)

# Resources
# User [/user]
In this resource group, the endpoints for creating a new user and generating a system access token are available.

The generated token is valid for 30 minutes.

### /api/AddUserIdentity (Create a new user) [POST]

+ Attributes (object)

    + name: user's name (string, required)
    + email: user's email (string, required)
    + password: user's password (string, required) - Passwords must have at least one non alphanumeric character, one lowercase ('a'-'z') and one uppercase ('A'-'Z').
    
+ Request (application/json)

    + Headers

            Allow Anonymous

    + Body

           {
              "name": "Joaquim Fenix",
              "email": "joaquim@email.com",
              "password": "123@Abc"
            }

+ Response 200 (application/json)

    + Body

            "User added successfully."
            
+ Response 400 (application/json)
  Validation errors or the entered fields do not exist in the system. (Password example)
  
    + Body

            [
              {
                "code": "PasswordTooShort",
                "description": "Passwords must be at least 6 characters."
              },
              {
                "code": "PasswordRequiresNonAlphanumeric",
                "description": "Passwords must have at least one non alphanumeric character."
              },
              {
                "code": "PasswordRequiresLower",
                "description": "Passwords must have at least one lowercase ('a'-'z')."
              },
              {
                "code": "PasswordRequiresUpper",
                "description": "Passwords must have at least one uppercase ('A'-'Z')."
              }
            ]
            
### /api/CreateTokenIdentity (Generate a user token) [POST]

+ Attributes (object)

    + email: user's email (string, required)
    + password: user's password (string, required)
    
+ Request (application/json)

    + Headers

            Allow Anonymous

    + Body

           {              
              "email": "joaquim@email.com",
              "password": "123@Abc"
            }

+ Response 200 (application/json)

    + Body

            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJSb2RyaWdvIEJhcmNlbG9zIEZyYW5jbyIsImp0aSI6ImUyNzgwOTM3LTg5ZDktNGE1MS04NzVhLWI2ZjJkMmEyZWMwMCIsImlkVXNlciI6IjMwZjc3NTE1LTM3YWQtNDZmNy1hZjA0LWJhN2VjNDQ1ZTMxYSIsImV4cCI6MTY1NzQyMjQzNSwiaXNzIjoiVGVzdGUuU2VjdXJpcnkuQmVhcmVyIiwiYXVkIjoiVGVzdGUuU2VjdXJpcnkuQmVhcmVyIn0.e_Ic7_WEwnxosBbFcQ4kMLl5Wj7Zt2w2GomfikdXDCI"
            
+ Response 400 (application/json)
  Validation errors or the entered fields do not exist in the system. (Password example)
  
    + Body
            
              "User don't exist in database"
            
# TvShow [/TvShow]

In this resource group you can list all series, series by name, genre, details of a series, add series to user favorites list, list user favorites and remove series from favorites list.

### /api/getAllTvShows (List all Tv Shows) [GET]
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          [
              {
                  "id": 1,
                  "referenceId": 35624,
                  "name": "The Flash",
                  "permalink": "the-flash",
                  "startDate": "2014-10-07",
                  "endDate": null,
                  "country": "US",
                  "network": "The CW",
                  "status": "Running",
                  "imagePath": "https://static.episodate.com/images/tv-show/thumbnail/35624.jpg",
                  "url": "https://www.episodate.com/tv-show/the-flash",
                  "description": "Barry Allen is a Central City police forensic scientist with a reasonably happy life, despite the childhood trauma of a mysterious red and yellow being killing his mother and framing his father. All that changes when a massive particle accelerator accident leads to Barry being struck by lightning in his lab. Coming out of coma nine months later, Barry and his new friends at STAR labs find that he now has the ability to move at superhuman speed. <br>Furthermore, Barry learns that he is but one of many affected by that event, most of whom are using their powers for evil. Determined to make a difference, Barry dedicates his life to fighting such threats, as The Flash. While he gains allies he never expected, there are also secret forces determined to aid and manipulate him for their own agenda.",
                  "descriptionSource": "http://www.imdb.com/title/tt3107288/plotsummary?ref_=tt_stry_pl",
                  "runtime": 60,
                  "genres": "Drama,Action,Science-Fiction",
                  "createDate": "2022-07-09T03:06:09.8900988",
                  "episodes": []
               }
          ]

+ Response 401 (application/json) 
  
    + Headers

             content-length: 0 
             date: Sun10 Jul 2022 02:49:27 GMT 
             server: Kestrel 
             www-authenticate: Bearer error="invalid_token" 


### /api/getAllTvShowsByName (List all Tv Shows by name) [GET]
+ Parameters
    + name (required, string, `Arrow`)
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          [
              {
                "id": 3,
                "referenceId": 29560,
                "name": "Arrow",
                "permalink": "arrow",
                "startDate": "2012-10-10",
                "endDate": null,
                "country": "US",
                "network": "The CW",
                "status": "Ended",
                "imagePath": "https://static.episodate.com/images/tv-show/thumbnail/29560.jpg",
                "url": "https://www.episodate.com/tv-show/arrow",
                "description": "Arrow is an American television series developed by writer/producers Greg Berlanti, Marc Guggenheim, and Andrew Kreisberg. It is based on the DC Comics character Green Arrow, a costumed crime-fighter created by Mort Weisinger and George Papp. It premiered in North America on The CW on October 10, 2012, with international broadcasting taking place in late 2012. Primarily filmed in Vancouver, British Columbia, Canada, the series follows billionaire playboy Oliver Queen, portrayed by Stephen Amell, who, five years after being stranded on a hostile island, returns home to fight crime and corruption as a secret vigilante whose weapon of choice is a bow and arrow. Unlike in the comic books, Queen does not go by the alias \"Green Arrow\". The series takes a realistic look at the Green Arrow character, as well as other characters from the DC Comics universe. Although Oliver Queen/Green Arrow had been featured in the television series Smallville from 2006 to 2011, the producers decided to start clean and find a new actor (Amell) to portray the character. Arrow focuses on the humanity of Oliver Queen, and how he was changed by time spent shipwrecked on an island. Most episodes have flashback scenes to the five years in which Oliver was missing.",
                "descriptionSource": "http://en.wikipedia.org/wiki/Arrow_%28TV_series%29",
                "runtime": 60,
                "genres": "Drama,Action,Science-Fiction",
                "createDate": "2022-07-09T03:06:13.1360799",
                "episodes": []
              }
          ]

### /api/getAllTvShowsByGenere (List all Tv Shows by genere) [GET]
+ Parameters
    + genere (required, string, `action`)
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          [
              {
                "id": 3,
                "referenceId": 29560,
                "name": "Arrow",
                "permalink": "arrow",
                "startDate": "2012-10-10",
                "endDate": null,
                "country": "US",
                "network": "The CW",
                "status": "Ended",
                "imagePath": "https://static.episodate.com/images/tv-show/thumbnail/29560.jpg",
                "url": "https://www.episodate.com/tv-show/arrow",
                "description": "Arrow is an American television series developed by writer/producers Greg Berlanti, Marc Guggenheim, and Andrew Kreisberg. It is based on the DC Comics character Green Arrow, a costumed crime-fighter created by Mort Weisinger and George Papp. It premiered in North America on The CW on October 10, 2012, with international broadcasting taking place in late 2012. Primarily filmed in Vancouver, British Columbia, Canada, the series follows billionaire playboy Oliver Queen, portrayed by Stephen Amell, who, five years after being stranded on a hostile island, returns home to fight crime and corruption as a secret vigilante whose weapon of choice is a bow and arrow. Unlike in the comic books, Queen does not go by the alias \"Green Arrow\". The series takes a realistic look at the Green Arrow character, as well as other characters from the DC Comics universe. Although Oliver Queen/Green Arrow had been featured in the television series Smallville from 2006 to 2011, the producers decided to start clean and find a new actor (Amell) to portray the character. Arrow focuses on the humanity of Oliver Queen, and how he was changed by time spent shipwrecked on an island. Most episodes have flashback scenes to the five years in which Oliver was missing.",
                "descriptionSource": "http://en.wikipedia.org/wiki/Arrow_%28TV_series%29",
                "runtime": 60,
                "genres": "Drama,Action,Science-Fiction",
                "createDate": "2022-07-09T03:06:13.1360799",
                "episodes": []
              }
          ]

### /api/GetTvShowDetailById (List Tv Shows by Id) [GET]
+ Parameters
    + tvShowId (required, integer, 3)
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          [
              {
                  "id": 3,
                  "referenceId": 29560,
                  "name": "Arrow",
                  "permalink": "arrow",
                  "startDate": "2012-10-10",
                  "endDate": null,
                  "country": "US",
                  "network": "The CW",
                  "status": "Ended",
                  "imagePath": "https://static.episodate.com/images/tv-show/thumbnail/29560.jpg",
                  "url": "https://www.episodate.com/tv-show/arrow",
                  "description": "Arrow is an American television series developed by writer/producers Greg Berlanti, Marc Guggenheim, and Andrew Kreisberg. It is based on the DC Comics character Green Arrow, a costumed crime-fighter created by Mort Weisinger and George Papp. It premiered in North America on The CW on October 10, 2012, with international broadcasting taking place in late 2012. Primarily filmed in Vancouver, British Columbia, Canada, the series follows billionaire playboy Oliver Queen, portrayed by Stephen Amell, who, five years after being stranded on a hostile island, returns home to fight crime and corruption as a secret vigilante whose weapon of choice is a bow and arrow. Unlike in the comic books, Queen does not go by the alias \"Green Arrow\". The series takes a realistic look at the Green Arrow character, as well as other characters from the DC Comics universe. Although Oliver Queen/Green Arrow had been featured in the television series Smallville from 2006 to 2011, the producers decided to start clean and find a new actor (Amell) to portray the character. Arrow focuses on the humanity of Oliver Queen, and how he was changed by time spent shipwrecked on an island. Most episodes have flashback scenes to the five years in which Oliver was missing.",
                  "descriptionSource": "http://en.wikipedia.org/wiki/Arrow_%28TV_series%29",
                  "runtime": 60,
                  "genres": "Drama,Action,Science-Fiction",
                  "createDate": "2022-07-09T03:06:13.1360799",
                  "episodes": [
                                {
                                  "id": 245,
                                  "season": 1,
                                  "number": 0,
                                  "name": "Pilot",
                                  "airDate": "2012-10-11 00:00:00",
                                  "showId": 3
                                }
                              ]
                }
            ]

### /api/addTvShowToFavorites (Add a Tv Show to a user's favorite list) [POST]
+ Parameters
    + userId (required, string, `30f77515-37ad-46f7-af04-ba7ec445e31a`)
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
                "id": 3,
                "referenceId": 29560
            }

+ Response 200 (application/json)
    + Body

            {
                  "id": 7,
                  "showId": 3,
                  "userId": "30f77515-37ad-46f7-af04-ba7ec445e31a"
            }


### /api/getAllFavoritesByUserId (List All favorites Tv Shows by UserId) [GET]
+ Parameters
    + userId (required, string, `30f77515-37ad-46f7-af04-ba7ec445e31a`)
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)
+ Body

            [              
                {
                  "id": 7,
                  "showId": 3,
                  "userId": "30f77515-37ad-46f7-af04-ba7ec445e31a"
                }
            ]

### /api/removeTvShowToFavorites (Remove a Tv Show from user's favorites) [DELETE]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
                  "id": 7,
                  "showId": 3,
                  "userId": "30f77515-37ad-46f7-af04-ba7ec445e31a"
            }

+ Response 200 (application/json)
    + Body


                  true


## 🛠️ Built with

* .Net version 5
* Entity Framework
* Sql Server

## ✒️ Author

* **Rodrigo Barcelos Franco** - *Documentation and development.* - (https://github.com/RB-Franco)

## 📄 Attention!

This project is still under construction along with its front end - (https://github.com/RB-Franco/TvShowsFront) for details.
