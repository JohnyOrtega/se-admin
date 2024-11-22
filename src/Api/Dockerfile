# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution and project files
COPY src/*.sln ./
COPY src/Core/*.csproj ./Core/
COPY src/Infrastructure/*.csproj ./Infrastructure/
COPY src/Api/*.csproj ./Api/

# Restore the dependencies for all projects
RUN dotnet restore

# Copy the entire source code
COPY src/ ./ 

# Build the Web API project in Release mode
WORKDIR /app/Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published application from the build container
COPY --from=build /app/publish .

# Set the entry point to start the Web API
ENTRYPOINT ["dotnet", "Api.dll"]