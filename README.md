# ScratchCardApp

ScratchCardApp is an ASP.NET Core application that provides a RESTful API and an MVC interface for managing WAEC scratch cards. The application allows you to generate, list, purchase, and use scratch cards.

## Features

- **Generate Scratch Cards**: Create a set of unique scratch cards.
- **List Scratch Cards**: Retrieve a list of all available scratch cards.
- **Purchase Scratch Cards**: Mark a card as purchased.
- **Use Scratch Cards**: Validate and mark a card as used.

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQLite for the database
- xUnit for unit testing
- Moq for mocking dependencies in tests
- FluentAssertions for readable assertions in tests

## Project Structure

- **Controllers**
  - `ScratchCardsController.cs`
- **Views**
  - **ScratchCards**
    - `Index.cshtml`
    - `Generate.cshtml`
- **Models**
  - `ScratchCard.cs`
- **Services**
  - `IScratchCardService.cs`
  - `ScratchCardService.cs`
- **Repositories**
  - `IScratchCardRepository.cs`
  - `ScratchCardRepository.cs`
- **Data**
  - `AppDbContext.cs`
- `Program.cs`
- **ScratchCardServiceTests**
  - `ScratchCardServiceTests.cs`



## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite

### Setup

1. **Clone the repository**

   ``bash
   git clone https://github.com/yourusername/ScratchCardApp.git
   cd ScratchCardApp

### Set up the database connection

Update the connection string in `appsettings.json`:

	``json
	{
	  "ConnectionStrings": {
		"DefaultConnection": "Data Source=waecards.db"
	  }
	}

### Apply database migrations

	``bash
	dotnet ef migrations add InitialCreate
	dotnet ef database update
		

### Run the application

	``bash
	dotnet run

# Navigate to the application

Open your browser and go to https://localhost:port to interact with the MVC application.

## Usage

### API Endpoints
- **Generate Scratch Cards**
  ``http
  POST /api/scratchcards/generate?count={count}

- **List Scratch Cards**
 ``http
 GET /api/scratchcards
 
- **Purchase a Scratch Card**
  ``http
  POST /api/scratchcards/purchase/{id}

- **Use a Scratch Card**
  ``http
  POST /api/scratchcards/use/{id}


### MVC Interface

- **List Scratch Cards**: Navigate to the root URL `/` to see the list of scratch cards.
- **Generate Scratch Cards**: Navigate to `/ScratchCards/Generate` to generate new scratch cards.

## Running Unit Tests

The project includes unit tests for the `ScratchCardService`. To run the tests, use the following command:

	``bash
	dotnet test

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request. Contributions are welcome!

## License

This project is licensed under the MIT License. See the LICENSE file for more information.

