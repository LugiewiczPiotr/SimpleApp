# SimpleApp
This is an application that manages the product inventory. In this repository there are two projects of this application Model-View-Controller and API.

## Table of contents
* [Technologies ](#technologies)
* [Setup ](#setup-API)

	
## Technologies 
Project is created with:
* .Net 6
* Entity Framework Core 6
* Autofac
* AutoMapper
* FLuentValidation.AspNetCore
	
## Setup 
1. clone this repository 
2. open SimpleApp\SimpleApp.sln in Visual Studio 2022 or older vaersion. <br>
3. build the solution <br>
4. start project with launch profile SimpleApp.WebApi or SimpleApp.Web if you want run Web project. <br>
5. if you want to use the API it will proceed otherwise it's all about running the WEB project
6. import SimpleApp.postman_collection and SimpleApp.postman_environment to postman.
7. in postman collection create account like this ![image](https://user-images.githubusercontent.com/122226672/214619223-cba84fd5-b005-4f62-a6b2-87bbc5663e91.png)<br>
8. now you can login like this ![image](https://user-images.githubusercontent.com/122226672/214619747-ea6841de-ab0f-44f6-a195-4c612e19e074.png) Now you can test Api (jwt token expiration time is 5 hours, you can change time in SimpleApp.WebApi/appsettings.json in section JwtSettings).


