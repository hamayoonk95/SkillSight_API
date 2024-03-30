# SkillSight Backend

## Overview

The SkillSight backend powers the core logic of the SkillSight platform, handling data processing, API endpoints for the frontend, and interactions with the database. Built with .NET Core, it provides a robust and scalable foundation for the application.

## Prerequisites

-   .NET Core SDK (Version specified, e.g., 3.1 or 5.0)
-   Any IDE that supports .NET development (e.g., Visual Studio, Visual Studio Code)

## Running the .NET Backend

1. Clone the repository to your local machine.
2. Open the solution in your IDE.
3. Ensure all NuGet packages are restored.
4. Set the startup project to the backend project.
5. Run the application. This can typically be done with a 'Run' button in your IDE or via the command line with:
   dotnet run

6. The backend should now be up and serving requests at the configured port (e.g., `localhost:5000`).

## Configuration

-   Before running, make sure to update the `appsettings.json` file with your database connection strings and any other necessary configuration settings.
