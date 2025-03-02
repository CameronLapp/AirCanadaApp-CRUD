# AirCanadaApp

AirCanadaApp is a web application for managing flight data and ticket orders using **ASP.NET Core MVC**. It demonstrates how to implement **CRUD operations** for flight data and ticket bookings, providing users with the ability to:

- View available flights
- Book tickets
- Edit or delete bookings
- View ticket details

## Features

- **Flight Management**: Create, read, update, and delete flight data including flight number, cities of departure and arrival, departure and arrival times, and prices.
- **Ticket Management**: Book tickets for available flights, view ticket details, and modify or cancel bookings.
- **User Authentication**: Secure areas using ASP.NET Core's **[Authorize]** attribute, ensuring only authorized users can access certain actions.
- **Database Integration**: Uses **Entity Framework Core** to interact with an SQL database for storing flight and ticket information.

## Technologies Used

- **ASP.NET Core MVC**: A web framework for building web applications.
- **Entity Framework Core**: Object-Relational Mapping (ORM) to handle database interactions.
- **SQL Server**: The database used for storing flight and ticket information.
- **Razor Views**: For rendering dynamic HTML content on the server side.

## Installation and Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/cameronlapp/AirCanadaApp-CRUD.git
   cd AirCanadaApp-CRUD
