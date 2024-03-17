Smart App


Overview


The Smart App is a web application developed using ASP.NET Core 8 on the backend, Angular 13.3.0 on the frontend, Google Place API for Address look up and Entity Framework Core with an in-memory database. It follows the repository pattern for data access and management.
The app is mobile friendly and provide a web Single Page Application with APIs for capturing customer data and display in a list and detailed view.

Features

Allows users to manage customer data and can allow other apps to consume the Web APIs via REST protocols.

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

Enter your owned API keys in both production and development config files as needed. You can enhance the security of the key by storing on KeyVault or encrypt the keys.

Change the trusted domains as needed. 

Frontend (Angular):

Navigate to the frontend directory.

Install dependencies by running npm install.

Start the Angular development server by running ng serve.

Enter your owned Google Place API key and APIKey to authenticate to backend APIs in both production and development environment files. . You can enhance the security of the key by storing on KeyVault or encrypt the keys.

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

![image](https://github.com/georgevanh/Smart-App/assets/163656914/5c114080-e1d8-4b73-b9dd-f6a5d03e04c9)


![image](https://github.com/georgevanh/Smart-App/assets/163656914/e8f0d6e7-0590-4114-b9f3-fdb41240d096)


Front pages:

Home Page:

![image](https://github.com/georgevanh/SmartApp/assets/163656914/157c77c2-1b8e-4227-9596-eab4bdfc133d)

Customer Adding Page with Address Look up:

![image](https://github.com/georgevanh/Smart-App/assets/163656914/56605e3a-795a-4f24-a8c9-9d7ca88f101a)


Only adding customer with valid Australian Mobile Number:

![image](https://github.com/georgevanh/Smart-App/assets/163656914/84a2f7b9-41f3-4ab3-8d05-2dcf5fc765ca)


![image](https://github.com/georgevanh/Smart-App/assets/163656914/7e4f1649-f08f-443c-8751-56380b3c98f1)



List page:

![image](https://github.com/georgevanh/Smart-App/assets/163656914/a596e559-3b05-464c-899b-7e7edb1e1f60)


Detailed page:

![image](https://github.com/georgevanh/Smart-App/assets/163656914/4efbd829-443f-4086-b094-086eb42dc5fe)











