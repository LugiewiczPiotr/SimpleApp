# SimpleApp
This is an application that manages the product inventory. In this repository there are two projects of this application Model-View-Controller and API.

## Table of contents
* [Technologies MVC](#technologies-MVC)
* [Setup MVC](#setup-MVC) 
- [Technologies API](#technologies-API)
- [Setup API](#setup-API)

	
## Technologies MVC
Project is created with:
* .Net 6
* Entity Framework Core 6
* Autofac
* AutoMapper
* FLuentValidation.AspNetCore
	
## Setup MVC
1. clone this repository 
2. open SimpleApp\SimpleApp.sln in Visual Studio 2022 or older version. <br>
3. build the solution <br>
4. start SimpleApp.Web project <br><br>

	
## Technologies API
Project is created with:
* .Net 6
* Entity Framework Core 6
* Autofac
* AutoMapper
* FLuentValidation.AspNetCore
	
## Setup API
1. clone this repository 
2. open SimpleApp\SimpleApp.sln in Visual Studio 2022 or older vaersion. <br>
3. build the solution <br>
4. import SimpleApp.postman_collection and SimpleApp.postman_environment to postman.
5. start SimpleApp.WebApi project with launch profile SimpleApp.WebApi <br>
6. in postman collection create account like this ![image](https://user-images.githubusercontent.com/122226672/214619223-cba84fd5-b005-4f62-a6b2-87bbc5663e91.png)<br>
7. now you can login like this ![image](https://user-images.githubusercontent.com/122226672/214619747-ea6841de-ab0f-44f6-a195-4c612e19e074.png) Now you can test api (jwt token expiration time is 5 hours)
you can change time in SimpleApp.WebApi/appsettings.json in section JwtSettings).


