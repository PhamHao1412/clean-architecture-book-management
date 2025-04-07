# clean-architechture-book-management
## Overview

Welcome to MyProject Web API, a robust and scalable web application built using .NET Core and organized following Clean Architecture principles. This architecture ensures a clear separation of concerns, facilitating maintainability, testability, and scalability of the application.

## Project Structure

The project is divided into several layers, each responsible for different aspects of the application:

- **Core Layer**: Contains the core business logic and domain entities.
- **Application Layer**: Contains the application-specific logic, including services and mappers.
- **Infrastructure Layer**: Handles data access, repositories, and configuration settings.
- **Presentation Layer**: Manages the web API controllers, middleware, and routing logic.

### File Structure
<p align="center">
  <img style="display: block;-webkit-user-select: none;margin: auto;background-color: hsl(0, 0%, 90%);transition: background-color 300ms;" src="https://drive.google.com/file/d/1tmtp_J5zga7b2gukW4JSjuwCW7DHNYjY/view?usp=sharing">
</p>
## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) or any other database of your choice

### Installation

1. Clone the repository:
    ```sh
    git clone https://gitlab.kyanon.digital/Training-Intern-2024/haopham.git
    cd yourproject
    ```


2. Update the connection string in `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "YourConnectionStringHere"
      }
    }
    ```

3. Apply database migrations: 
    ```sh
    "update-database".
    ```
   
4. Run the application:
    ```sh
    "dotnet run"
      ```
5. Basic Auth information are in `appsettings.Development.json`.

Usage

After starting the application, you can access the API endpoints via:
https://localhost:7220/api/yourendpoints

Contributing

1. Fork the repository
2. Create a feature branch (git checkout -b feature/your-feature)
3. Commit your changes (git commit -am 'Add your feature')
4. Push to the branch (git push origin feature/your-feature)
5. Create a new Pull Request


Acknowledgements

- Clean Architecture by Milan Jovanović
- .NET Core Documentation
