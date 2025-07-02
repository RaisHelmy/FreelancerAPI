# Freelancer Management API - ETIQA IT Assessment

A comprehensive RESTful API built with .NET 8 and Clean Architecture for managing CDN (Complete Developer Network) freelancers database, designed to meet all ETIQA IT Backend Developer Assessment requirements.

## 🌐 Live Demo

- **Live Application**: https://dotnetassessment.raishelmy.uk
- **Swagger Documentation**: https://dotnetassessment.raishelmy.uk/swagger
- **GitHub Repository**: https://github.com/RaisHelmy/FreelancerAPI

## 📋 Assessment Requirements Compliance

This project fully satisfies all ETIQA IT assessment requirements:

### ✅ Core Requirements
- **RESTful API**: Complete CRUD operations using standard HTTP verbs
- **User Model**: Implements all required attributes (username, email, phone, skillsets, hobbies)
- **Database Design**: One-to-many relationships for skillsets and hobbies
- **RDBMS Integration**: SQL Server with Entity Framework Core
- **Wildcard Search**: Advanced search functionality by username and email
- **Clean Architecture**: Proper layered architecture with separation of concerns
- **Unit Testing**: Comprehensive test coverage with xUnit and Moq
- **Archive/Unarchive**: Soft delete functionality for freelancer management
- **Web Interface**: Simple, responsive UI for all operations

### 🎯 Bonus Features Implemented
- **Client-side Development**: Bootstrap 5 responsive web interface
- **Error Handling**: Comprehensive error responses and validation
- **Fluent Validation**: Input validation with FluentValidation library
- **Design Patterns**: Repository pattern, Dependency Injection, AutoMapper
- **Documentation**: Complete Swagger/OpenAPI documentation
- **Containerization**: Docker support for easy deployment

## 🚀 Key Features

- **Complete CRUD Operations**: Create, Read, Update, Delete freelancers
- **Advanced Search**: Wildcard search by username and email
- **Soft Delete**: Archive/Unarchive functionality
- **Relationship Management**: One-to-many relationships for skillsets and hobbies
- **Clean Architecture**: Domain-driven design with proper layering
- **Comprehensive Testing**: Unit tests with high coverage
- **Modern UI**: Responsive web interface built with Bootstrap 5
- **API Documentation**: Interactive Swagger UI
- **Production Ready**: Deployed and accessible online

## 🏗️ Architecture

Following Clean Architecture principles as required:

```
FreelancerAPI/
├── src/
│   ├── FreelancerAPI.Domain/          # Core business logic & entities
│   │   ├── Entities/                  # Domain entities (Freelancer, Skillset, Hobby)
│   │   └── Interfaces/                # Repository interfaces
│   ├── FreelancerAPI.Application/     # Use cases and business rules
│   │   ├── DTOs/                      # Data Transfer Objects
│   │   ├── Interfaces/                # Service interfaces
│   │   ├── Services/                  # Business logic implementation
│   │   └── Validators/                # FluentValidation rules
│   ├── FreelancerAPI.Infrastructure/  # Data access layer
│   │   ├── Data/                      # DbContext and configurations
│   │   ├── Repositories/              # Repository implementations
│   │   └── Migrations/                # EF Core migrations
│   └── FreelancerAPI.WebAPI/         # API controllers and UI
│       ├── Controllers/               # RESTful API endpoints
│       ├── wwwroot/                   # Static files and web interface
│       └── Program.cs                 # Application entry point
└── tests/
    └── FreelancerAPI.Tests/          # Unit tests with Moq
```

## 🛠️ Technology Stack

### Backend Technologies
- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Relational database management system
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation framework

### Frontend Technologies
- **HTML5 & CSS3** - Modern web standards
- **Bootstrap 5** - Responsive UI framework
- **JavaScript (Vanilla)** - Client-side functionality
- **Fetch API** - Modern HTTP client

### Development & Testing
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework for unit tests
- **Swagger/OpenAPI** - API documentation and testing
- **Docker** - Containerization support

## 📚 Complete API Endpoints

All required HTTP verbs implemented as per assessment requirements:

| Method | Endpoint | Description | Purpose |
|--------|----------|-------------|---------|
| **GET** | `/api/freelancers` | Get all freelancers | List all registered freelancers |
| **GET** | `/api/freelancers/{id}` | Get freelancer by ID | Retrieve specific freelancer details |
| **POST** | `/api/freelancers` | Create new freelancer | Register new freelancer |
| **PUT** | `/api/freelancers/{id}` | Update freelancer | Complete update of freelancer data |
| **PATCH** | `/api/freelancers/{id}` | Partial update | Update specific fields only |
| **DELETE** | `/api/freelancers/{id}` | Delete freelancer | Remove freelancer from system |
| **GET** | `/api/freelancers/search?term={term}` | Wildcard search | Search by username or email |
| **POST** | `/api/freelancers/{id}/archive` | Archive freelancer | Soft delete functionality |
| **POST** | `/api/freelancers/{id}/unarchive` | Unarchive freelancer | Restore archived freelancer |

## 📝 Data Model

### Freelancer Entity (Main Table)
```json
{
  "id": "integer (Primary Key)",
  "username": "string (Required, Unique)",
  "email": "string (Required, Valid Email)",
  "phoneNumber": "string (Required)",
  "isArchived": "boolean (Soft Delete Flag)",
  "createdAt": "datetime",
  "updatedAt": "datetime",
  "skillsets": "List<Skillset> (One-to-Many)",
  "hobbies": "List<Hobby> (One-to-Many)"
}
```

### One-to-Many Relationships
- **Skillsets Table**: `Id`, `Name`, `FreelancerId` (Foreign Key)
- **Hobbies Table**: `Id`, `Name`, `FreelancerId` (Foreign Key)

## 🧪 Testing Strategy

Comprehensive unit testing implemented:

```bash
# Run all tests
cd tests/FreelancerAPI.Tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage Areas
- **Repository Layer**: Data access operations
- **Service Layer**: Business logic validation
- **Controller Layer**: API endpoint behavior
- **Validation**: FluentValidation rules testing

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Docker Desktop (for SQL Server)
- Visual Studio 2022 or VS Code

### Quick Start
```bash
# 1. Clone the repository
git clone https://github.com/RaisHelmy/FreelancerAPI
cd FreelancerAPI

# 2. Start SQL Server container
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver --hostname sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest

# 3. Setup database
cd src/FreelancerAPI.Infrastructure
dotnet ef database update --startup-project ../FreelancerAPI.WebAPI

# 4. Run the application
cd ../FreelancerAPI.WebAPI
dotnet run
```

### Docker Deployment
```bash
# Run the containerized application
docker run -d \
  --name freelancer-api \
  --network host \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DB_HOST=your-db-host,1433 \
  -e DB_NAME=FreelancerApiDb \
  -e DB_USER=sa \
  -e DB_PASSWORD=YourStrong@Passw0rd \
  raishelmy/freelancer-api
```

## 📖 API Usage Examples

### Create New Freelancer
```bash
POST /api/freelancers
Content-Type: application/json

{
  "username": "johndoe",
  "email": "john.doe@example.com",
  "phoneNumber": "+60123456789",
  "skillsets": ["C#", "ASP.NET Core", "JavaScript", "React"],
  "hobbies": ["Reading", "Gaming", "Photography", "Cooking"]
}
```

### Search Freelancers
```bash
GET /api/freelancers/search?term=john
GET /api/freelancers/search?term=example.com
```

### Archive/Unarchive Operations
```bash
POST /api/freelancers/1/archive    # Soft delete
POST /api/freelancers/1/unarchive  # Restore
```

## 🌐 Web Interface Features

The simple web interface provides:

1. **Freelancer Listing**: View all registered freelancers with pagination
2. **Add New Freelancer**: Form-based registration with validation
3. **Update Freelancer**: Edit existing freelancer details
4. **Delete Functionality**: Remove freelancers from the system
5. **Search Capability**: Real-time search by username or email
6. **Archive Management**: Archive and unarchive freelancers
7. **Responsive Design**: Mobile-friendly Bootstrap 5 interface

## 🏆 Assessment Highlights

### Technical Excellence
✅ **Clean Architecture** - Domain-driven design with proper layering  
✅ **SOLID Principles** - Well-structured, maintainable codebase  
✅ **RESTful Design** - Standard HTTP verbs and status codes  
✅ **Database Design** - Normalized schema with one-to-many relationships  
✅ **Error Handling** - Comprehensive validation and error responses  
✅ **Unit Testing** - High test coverage with mocking  
✅ **Documentation** - Complete Swagger/OpenAPI integration  
✅ **Web Interface** - Responsive frontend implementation  

### Production Readiness
✅ **Live Deployment** - Accessible at https://dotnetassessment.raishelmy.uk  
✅ **Docker Support** - Containerized application  
✅ **Environment Configuration** - Production-ready settings  
✅ **Security** - Input validation and safe database operations  
✅ **Performance** - Optimized queries and caching strategies  

## 🔗 Repository Access

- **GitHub Profile**: [@RaisHelmy](https://github.com/RaisHelmy)
- **Public Repository**: [Available for ETIQA IT review](https://github.com/RaisHelmy/FreelancerAPI)
- **Live Demo**: https://dotnetassessment.raishelmy.uk
- **API Documentation**: https://dotnetassessment.raishelmy.uk/swagger

## 👨‍💻 Developer

**Rais Helmy**
- **GitHub**: [@RaisHelmy](https://github.com/RaisHelmy)
- **LinkedIn**: [Rais Helmy](https://linkedin.com/in/raishelmy97)
- **Email**: raishelmy97@gmail.com
- **Portfolio**: Available for ETIQA IT team review

## 🎯 Assessment Summary

This project demonstrates a complete understanding of modern .NET development practices, Clean Architecture principles, and full-stack development capabilities. The implementation exceeds the basic requirements by including advanced features like FluentValidation, comprehensive testing, Docker support, and a production deployment.

**Ready for technical interview and code demonstration!**

---

*Built with ❤️ for ETIQA IT Backend Developer Assessment*