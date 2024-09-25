# BidCalculationTool

## Introduction
The Bid Calculation API provides services for calculating bids and managing related data. This API is designed to be robust and efficient, utilizing Entity Framework Core (EF Core) with a code-first approach for database interactions. The project also includes a dedicated test project that employs the xUnit framework to ensure the reliability and functionality of the API.

## Prerequisites
To run this API, make sure you have the following installed:
- **Visual Studio** (latest version)

## Database connection
Before running the API, you need to configure your local database connection.
1. Open the appsettings.json file located in the root of the project.
2. Find the BidCalcDbConnection connection string and update it with your local server name and database name.
   Example,
   "ConnectionStrings": {
    "BidCalcDbConnection": "Server=your_server_name;Database=your_database_name;Trusted_Connection=True;TrustServerCertificate=True"
   }

## Create Initial Database: 
After updating the connection string, you need to run the following commands to create the initial database and seed it with data.
Open the Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).

Run the following command to add migrations:
1. Add-Migration InitialCreate (you can replace InitialCreate with any name you prefer for your migration)
2. Update-Database

## Running the API
After setting up the database, you can run the API.
Press F5 or click the Start button in Visual Studio to launch the API.
The API should now be running and accessible at the specified local URL.


