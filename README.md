Smart App


Overview


The Smart App is a web application developed using ASP.NET Core 8 on the backend, Angular 13.3.0 on the frontend, Google Place API for Address look up and Entity Framework Core with an in-memory database. It follows the repository pattern for data access and management.

Features

Allows users to manage customer data.

Provides CRUD (Create, Read, Update, Delete) operations for customers.

Uses Angular for a responsive and dynamic user interface and using Google Place API to suggest address.

Utilizes ASP.NET Core WebAPI to handle backend logic and data management.

Implements Entity Framework Core with an in-memory database for efficient data storage and retrieval.

Follows the repository pattern to separate concerns and enhance maintainability.

Prerequisites

Before running the Smart App, ensure you have the following prerequisites installed:

Node.js (v14.x or higher)

.NET Core SDK 8

Angular CLI 13.3.0

Visual Studio Code or Visual Studio IDE

Getting Started

Follow these steps to get the Smart App up and running:

Backend (ASP.NET Core WebAPI):

Clone the repository to your local machine.

Navigate to the backend directory.

Open the solution file (SmartExercise.sln) in Visual Studio or Visual Studio Code.

Build the solution to restore dependencies.

Run the application.

Enter your API keys in both production and development environment.

Change the trusted domains as needed. 

Frontend (Angular):

Navigate to the frontend directory.

Install dependencies by running npm install.

Start the Angular development server by running ng serve.

Enter your Google Place API key and APIKey to authenticate to backend APIs in both production and development environment files.

Open your browser and navigate to https://127.0.0.1:4200/.


Configuration

Backend

Modify the appsettings.json file in the backend project to configure database connections, APIKey, Trusted domains and other settings if necessary.

Frontend

Modify the environment.ts file in the backend project to configure APIKey, Google Place API info  and other settings if necessary.

Folder Structure

backend: SmartExercise.Server Contains the ASP.NET Core WebAPI project.

frontend: smartexercise.client Contains the Angular project.

Technologies Used

ASP.NET Core 8

Xunit 2.7.0

Moq 4.20.70

Angular 13.3.0

Jasmine 3.10.0

Karma 6.3.0

Entity Framework Core 8.0.3

In-memory Database

Repository Pattern

Contributing
Contributions to the Smart App are welcome. Please follow the guidelines in the CONTRIBUTING.md file.

License
This project is licensed under the MIT License.

Screenshots:
APIs

![image](https://github.com/georgevanh/SmartApp/assets/163656914/4bc4e5e9-c03b-47f8-a1d0-f02bfcc6a848)

![image](https://github.com/georgevanh/SmartApp/assets/163656914/04f05a9b-14fe-4c2a-bcdf-34e631720084)

Front pages:
Home Page:
![image](https://github.com/georgevanh/SmartApp/assets/163656914/157c77c2-1b8e-4227-9596-eab4bdfc133d)
Customer Adding Page with Address Look up:
![image](https://github.com/georgevanh/SmartApp/assets/163656914/61fe32b5-f229-48f4-ac72-ccf4522db964)

Only adding customer with valid Australian Mobile Number:

![image](https://github.com/georgevanh/SmartApp/assets/163656914/5a5aec32-bc46-4362-9de8-cf8d2910c1eb)

![image](https://github.com/georgevanh/SmartApp/assets/163656914/bfe112a6-e1f9-4eef-9afc-e6c7f3b85f88)


List page:

![image](https://github.com/georgevanh/SmartApp/assets/163656914/a90d15ec-fc5e-4a0b-9f49-be515aed501f)

Detailed page:

![image](https://github.com/georgevanh/SmartApp/assets/163656914/867d822e-1076-4d76-b309-203b41629137)










