# NTier-SqlKata-GraphQL

This project aims to generate a multi-tier architecture using SqlKata for dynamic SQL query generation and management. The project automatically generates C# classes, Data Access Layer (DAL), Business Layer, API controllers, and GraphQL schema, queries, and types from database tables.

## Requirements

- Node.js
- .NET 8.0 SDK
- MSSQL Database

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/NazimMertBilgi/NTier-SqlKata-GraphQL
    cd NTier-SqlKata-GraphQL
    ```

2. Install the required Node.js dependencies:
    ```sh
    npm install
    ```

3. Configure the `packageSettings.json` file:

   
   <b>!IMPORTANT! Do not change the packageName and slnFileName values, this is handled by changePackageName.js.</b>
    ```json
    {
      "dbConnection": {
        "server": "YOUR_SERVER",
        "database": "YOUR_DATABASE",
        "options": {
          //example
          //"trustedConnection": true
        }
      }
    }
    ```

## Usage

1. Change Solution Package Name
    ```sh
    npm run change-package-name
    ```
    and follow the steps that appear on the screen.

2. Run the code generator:
    ```sh
    npm run generate
    ```
now you are ready 😊 look at your project files and see the changes for yourself.

## Project Structure

- `NTier-SqlKata.sln`: Solution file
- `NTier.API/`: API layer
  - `Controllers/`: API controllers
  - `Schema/`: GraphQL schema, queries, and types
  - `Program.cs`: Main entry point for the API
- `NTier.Business/`: Business layer
  - `Abstract/`: Abstract business services
  - `Concrete/`: Concrete business services
- `NTier.Core/`: Core layer
  - `Classes/`: Core classes
  - `Entities/`: Entity interfaces and classes
  - `Models/`: Data models
- `NTier.DataAccess/`: Data access layer
  - `Abstract/`: Abstract data access services
  - `Concrete/`: Concrete data access services
- `NTier.Entities/`: Entity classes

## generateNTier.js

This file is responsible for generating C# classes, Data Access Layer (DAL), Business Layer, API controllers, and GraphQL schema, queries, and types from database tables.

### Functions

- `getTablesAndColumns()`: Retrieves tables and columns from the database.
- `generateEntities(tables)`: Generates entity classes.
- `generateAbstractDALFiles(tables)`: Generates abstract DAL files.
- `generateConcreteDALFiles(tables)`: Generates concrete DAL files.
- `generateServiceFiles(tables)`: Generates business service files.
- `generateManagerFiles(tables)`: Generates business manager files.
- `generateDependencyInjectionRegistrations(tables)`: Generates dependency injection registrations.
- `generateControllerFiles(tables)`: Generates API controller files.
- `generateModelFiles(tables)`: Generates model files.
- `generateGraphQLSchemaFiles(tables)`: Generates GraphQL schema files.
- `generateGraphQLQueryFiles(tables)`: Generates GraphQL query files.
- `generateGraphQLTypeFiles(tables)`: Generates GraphQL type files.
- `generateGraphQLSchemaDependencyInjection(tables)`: Generates GraphQL schema dependency injection registrations.

### Others

Star and fork the project. Thank you.
