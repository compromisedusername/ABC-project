# ABC Project

This project is a part of the ABC Corporation.

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
  - [Connection String](#connection-string)
  - [API Key for Exchange Rate Service](#api-key-for-exchange-rate-service)
- [Usage](#usage)
  - [Register/Login as Admin](#registerlogin-as-admin)
  - [Authorize](#authorize)
- [Database Schema](#database-schema)

## Installation

To install and run the project, follow these steps:

1. Clone the repository:

   ```
   git clone https://github.com/your-username/ABC-project.git
   cd ABC-project
   ```
2. Install the necessary dependencies:

   ```
   dotnet restore
   ```

## Configuration

Before running the project, you need to configure the connection string and the API key for the exchange rate service.

### Connection String

Update the connection string in the `appsettings.json` file:

  ```
   json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your-connection-string-here"
     }
   }
  ```

### API Key for Exchange Rate Service

Configure your own API key for the exchange rate service. Use the following command to set the API key:

   ```
   dotnet user-secrets set "Rates:ServiceApiKey" "your-api-key"
   ```

## Usage

### Register/Login as Admin

To use the admin functionalities, register or login as an admin with the following credentials:

- **Username:** admin
- **Password:** admin

### Authorize

After logging in, click the "Authorize" button to access the secure endpoints.

![Authorize](https://github.com/compromisedusername/ABC-project/assets/100251433/6e8dcac2-3c58-435f-9d08-f00d0f61239b)

## Database Schema

Below is the Entity-Relationship Diagram (ERD) for the database schema used in this project.

![ERD Diagram](https://github.com/compromisedusername/ABC-project/assets/100251433/9b06adae-d79d-4b66-9b8a-9544b2fd7543)
Made in https://my.vertabelo.com

---

If you have any questions or issues, please feel free to open an issue on GitHub.

Happy coding!
