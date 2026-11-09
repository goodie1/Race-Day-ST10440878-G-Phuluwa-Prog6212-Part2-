# RaceDay – Race Event Management System

## 1. Project Overview

RaceDay is a web-based Race Event Management System developed using C# and ASP.NET Core. The purpose of the system is to simplify the management of running events by allowing organisers to create and manage race events, categories, participant enrolments and race results.

The system provides two main applications:

- **RaceDay API:** Handles communication with the database and manages the application's data and business logic.
- **RaceDay MVC Web Application:** Provides the user interface through which organisers and participants interact with the system.

The two applications communicate using HTTP requests, allowing information to be transferred between the website and the API.

## 2. Main Objectives

The main objectives of RaceDay are to:

- Allow users to register and log in.
- Provide different functionality for organisers and participants.
- Allow organisers to create and manage race events.
- Store event information, including the event name, date, time, location, description and race route.
- Allow organisers to create race categories.
- Allow participants to view available race events and categories.
- Allow organisers to enter and manage race results.
- Allow users to retrieve and view race information.

## 3. System Users

RaceDay supports two main user roles.

### 3.1 Organisers

Organisers are responsible for managing race events and their information.

Their functionality includes:

- Registering and logging into the system.
- Creating new race events.
- Providing event details and race routes.
- Viewing events they have created.
- Creating race categories.
- Entering race results.
- Managing race information.

### 3.2 Participants

Participants use the system to access race information.

Their functionality includes:

- Registering and logging into the system.
- Viewing available race events.
- Viewing race categories.
- Accessing race results.

## 4. Main System Features

### 4.1 User Authentication

The authentication functionality allows users to register and log into RaceDay.

The system stores user information and identifies users according to their assigned roles.

The role-based functionality helps ensure that organisers and participants access the appropriate features.

### 4.2 Event Management

The event management feature allows organisers to create race events.

Event information includes:

- Event name.
- Date and time.
- Event type.
- Event description.
- Location.
- Race route.
- Weather information.
- Organiser identification.

The system stores event information in the database so that it can be retrieved and displayed through the website.

### 4.3 Category Management

Organisers can create race categories for different types of races.

Category information includes:

- Category type.
- Race distance.
- Age requirement.

Participants can view the available categories to understand which race categories are offered.

### 4.4 Participant Enrolment

The enrolment functionality handles participant enrolment information.

It supports the process of managing participation in race events.

### 4.5 Race Results

The results functionality allows organisers to enter race results.

Result information includes:

- Finish time.
- Race position.
- Average pace.

The results can be retrieved and displayed through the website.

## 5. System Architecture

RaceDay uses a two-part application structure.

### RaceDay API

The API is responsible for processing requests and communicating with the SQL Server database.

It contains:

- Controllers – receive and process API requests.
- Services – contain the application's business logic and database operations.
- Database connections – connect the application to SQL Server.

### RaceDay MVC

The MVC application provides the website interface.

It follows the Model-View-Controller architecture:

- **Models:** Represent the information used by the application.
- **Views:** Display information to users.
- **Controllers:** Handle user requests and coordinate communication with the API.
- **Services:** Communicate with the RaceDay API using HTTP requests.

## 6. Technologies Used

The following technologies are used in the development of RaceDay:

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| ASP.NET Core Web API | Backend application |
| ASP.NET Core MVC | Website development |
| SQL Server LocalDB | Database management |
| ADO.NET | Database connectivity |
| HTML | Website structure |
| CSS | Website styling |
|
