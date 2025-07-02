# Freelancer Management API

A RESTful API built with .NET 8 and Clean Architecture for managing freelancers database.

## 🚀 Features

- **CRUD Operations**: Create, Read, Update, Delete freelancers
- **Search Functionality**: Wildcard search by username and email
- **Archive/Unarchive**: Soft delete functionality
- **One-to-Many Relationships**: Skillsets and hobbies management
- **Clean Architecture**: Separation of concerns with proper layering
- **Unit Tests**: Comprehensive test coverage
- **Simple Frontend**: Web interface for all operations

## 🏗️ Architecture

```
FreelancerAPI/
├── src/
│   ├── FreelancerAPI.Domain/          # Core business logic
│   ├── FreelancerAPI.Application/     # Use cases and interfaces  
│   ├── FreelancerAPI.Infrastructure/  # Data access layer
│   └── FreelancerAPI.WebAPI/         # API controllers and UI
└── tests/
    └── FreelancerAPI.Tests/          # Unit tests
```

## 🛠️ Technologies Used

- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database (Docker container)
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **xUnit & Moq** - Unit testing
- **Bootstrap 5** - Frontend styling
- **Swagger/OpenAPI** - API documentation

## 📋 Prerequisites

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 or VS Code

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone [your-repo-url]
cd FreelancerAPI
```

### 2. Start SQL Server Container
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver --hostname sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Setup Database
```bash
cd src/FreelancerAPI.WebAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

### 5. Access the Application
- **Web Interface**: https://localhost:7xxx/
- **Swagger UI**: https://localhost:7xxx/swagger
- **API Base**: https://localhost:7xxx/api/freelancers

## 📚 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/freelancers` | Get all freelancers |
| GET | `/api/freelancers/{id}` | Get freelancer by ID |
| POST | `/api/freelancers` | Create new freelancer |
| PUT | `/api/freelancers/{id}` | Update freelancer |
| PATCH | `/api/freelancers/{id}` | Partial update |
| DELETE | `/api/freelancers/{id}` | Delete freelancer |
| GET | `/api/freelancers/search?term={term}` | Search freelancers |
| POST | `/api/freelancers/{id}/archive` | Archive freelancer |
| POST | `/api/freelancers/{id}/unarchive` | Unarchive freelancer |

## 🧪 Running Tests

```bash
cd tests/FreelancerAPI.Tests
dotnet test
```

## 📝 Sample API Usage

### Create Freelancer
```json
POST /api/freelancers
{
  "username": "johndoe",
  "email": "john@example.com",
  "phoneNumber": "+1234567890",
  "skillsets": ["C#", "JavaScript", "React"],
  "hobbies": ["Reading", "Gaming", "Cooking"]
}
```

### Response
```json
{
  "id": 1,
  "username": "johndoe",
  "email": "john@example.com",
  "phoneNumber": "+1234567890",
  "isArchived": false,
  "skillsets": ["C#", "JavaScript", "React"],
  "hobbies": ["Reading", "Gaming", "Cooking"],
  "createdAt": "2025-07-02T10:30:00Z",
  "updatedAt": "2025-07-02T10:30:00Z"
}
```

## 🌐 Deployment

### Local Development
1. Ensure Docker is running with SQL Server container
2. Run `dotnet run` from WebAPI project
3. Access at `https://localhost:7xxx`

### Production Deployment Options
- **Azure App Service** with Azure SQL Database
- **AWS Elastic Beanstalk** with RDS
- **Heroku** with PostgreSQL add-on
- **Docker** containerization

## 🏆 Highlights

✅ **Clean Architecture** - Proper separation of concerns  
✅ **SOLID Principles** - Well-structured, maintainable code  
✅ **RESTful Design** - Standard HTTP verbs and status codes  
✅ **Database Design** - Normalized schema with relationships  
✅ **Error Handling** - Comprehensive error responses  
✅ **Testing** - Unit tests with mocking  
✅ **Documentation** - Swagger/OpenAPI integration  
✅ **UI Interface** - Simple web frontend  

## 👨‍💻 Author

**Your Name**
- GitHub: [@RaisHelmy](https://github.com/RaisHelmy)
- LinkedIn: [Rais Helmy](https://linkedin.com/in/raishelmy97)
- Email: your.email@example.com

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.