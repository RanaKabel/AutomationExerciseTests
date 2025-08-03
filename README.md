# AutomationExerciseTests

# AutomationExercise Test Suite

A  C# .NET test automation framework for testing the AutomationExercise.com website using both API and UI testing approaches.

##  Features

- **API Testing**: User registration and authentication endpoints
- **UI Testing**: End-to-end browser automation with Selenium WebDriver
- **Data-Driven Testing**: JSON-based test data management
- **Cross-Browser Support**: Firefox WebDriver with extensible options
- **CI/CD Integration**: GitHub Actions workflow for continuous testing
- **Page Object Model**: Maintainable and scalable UI test structure


## Tech Stack

- **.NET 8.0**: Target framework
- **NUnit**: Testing framework
- **Selenium WebDriver**: UI automation
- **RestSharp**: API testing
- **Newtonsoft.Json**: JSON data handling
- **GitHub Actions**: CI/CD pipeline

## Project Structure

```
AutomationExerciseTests/
├── .github/workflows/
│   └── ci.yml                 # CI/CD pipeline configuration
├── Api/
│   └── ApiClient.cs           # API client for authentication endpoints
├── Models/
│   ├── ApiResponse.cs         # API response model
│   └── TestData.cs           # Test data model
├── PageObjects/
│   ├── CreateAccountPage.cs   # Account creation page object
│   ├── HomePage.cs           # Home page object
│   └── SignupLoginPage.cs    # Login/signup page object
├── TestData/
│   ├── loginData.json        # Login test Data
│   └── registrationData.json # Registration test data
├── Tests/
│   ├── ApiAuthTests.cs       # API authentication tests
│   └── UIUserAuthTests.cs    # UI user authentication tests
└── Utils/
    └── TestUtils.cs          # Utility functions 
```

##  Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Firefox browser (for UI tests)
- Git

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd AutomationExerciseTests
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the solution:
```bash
dotnet build
```

##  Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Categories
```bash
# Run only API tests
dotnet test --filter "TestCategory=API"

# Run only UI tests
dotnet test --filter "TestCategory=UI"
```

### Run with Specific Configuration
```bash
dotnet test --configuration Release
```

```

##  CI/CD Pipeline

The project includes a GitHub Actions workflow (`.github/workflows/ci.yml`) that:
- Triggers on push to main branch and pull requests
- Sets up .NET  SDK
- Restores dependencies
- Builds the solution
- Runs all tests

##  Test Scenarios

### API Tests
- **User Registration**: Valid registration, duplicate email handling
- **User Authentication**: Valid login, invalid credentials

### UI Tests
- **End-to-End Registration**: Complete user registration flow
- **Login Validation**: User authentication through UI
- **Error Handling**: Duplicate registration scenarios

