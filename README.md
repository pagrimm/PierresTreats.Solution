# Pierre's Sweet and Savory Treats
**Weekly Independent Project for Epicodus**  
**By Peter Grimm, 08.14.2020**

## Description

Weekly independent project for Epicodus, an MVC web application to market Pierre's sweet and savory treats. Designed to showcase my knowledge and skills with user authentication using Identity.

## Specifications
* Users should be able to create, read, update, and delete sweets and treats.
* A user should be able to log in and log out.
* Only logged in users should have create, update and delete functionality.
* All users should be able to have read functionality.
* There should be a many-to-many relationship between Treats and Flavors.
* A user should be able to navigate to a splash page that lists all treats and flavors.
* Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.

## Setup/Installation Requirements
* .NET Core 2.2 will need to be installed, it can be found here https://dotnet.microsoft.com/download/dotnet-core/2.2
* For Mac users, download MySQL here: https://dev.mysql.com/downloads/file/?id=484914
* For Windows users, download MySQL here: https://dev.mysql.com/downloads/file/?id=484919
* Install MySQL and set the system path, more information on how to do that can be found here: https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql
* Clone the GitHub repository by running `git clone https://github.com/pagrimm/Factory.Solution.git` in the terminal
* Navigate to the newly created `PierresTreats.Solution` folder
* Navigate to the `PierresTreats` subfolder
* Run `dotnet restore` to get application dependencies
* Run `dotnet build` to build the application
* Run `dotnet ef database update` to create the application database
* Run `dotnet run` to run the application
* The web app will now be available on `http://localhost:5000/` in your browser

## Technologies Used

C#  
.NET Core 2.2  
ASP.NET Core MVC  
Entity Framework Core 2.2.6 

## Legal

Copyright (c) 2020, Peter Grimm  
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) This software is licensed under the MIT license.