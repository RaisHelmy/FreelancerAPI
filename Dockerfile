# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081
EXPOSE 8082

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["src/FreelancerAPI.WebAPI/FreelancerAPI.WebAPI.csproj", "src/FreelancerAPI.WebAPI/"]
COPY ["src/FreelancerAPI.Application/FreelancerAPI.Application.csproj", "src/FreelancerAPI.Application/"]
COPY ["src/FreelancerAPI.Infrastructure/FreelancerAPI.Infrastructure.csproj", "src/FreelancerAPI.Infrastructure/"]
COPY ["src/FreelancerAPI.Domain/FreelancerAPI.Domain.csproj", "src/FreelancerAPI.Domain/"]

RUN dotnet restore "src/FreelancerAPI.WebAPI/FreelancerAPI.WebAPI.csproj"

# Copy source code and build
COPY . .
WORKDIR "/src/src/FreelancerAPI.WebAPI"
RUN dotnet build "FreelancerAPI.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FreelancerAPI.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create non-root user for security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "FreelancerAPI.WebAPI.dll"]