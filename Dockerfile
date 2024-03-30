# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory
WORKDIR /app/backend

# Copy the project file.
COPY ["SkillSight_API/skillsight.API.csproj", "./"]

# Restore Dependencies
RUN dotnet restore "skillsight.API.csproj"

# Copy the project files
COPY SkillSight_API/* .

# Build the application.
RUN dotnet publish "skillsight.API.csproj" -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/backend
COPY --from=build-env /app/backend/out .

# Configure the container to run as an executable
ENTRYPOINT ["dotnet", "skillsight.API.dll"]