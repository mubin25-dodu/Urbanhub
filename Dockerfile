# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["UrbanHub/UrbanHub.web.csproj", "UrbanHub/"]
COPY ["UrbanHub.DTO/UrbanHub.DTO.csproj", "UrbanHub.DTO/"]
COPY ["Urbanhub.Entities/UrbanHub.Entities.csproj", "Urbanhub.Entities/"]
COPY ["UrbanhubAuth.repo/UrbanHubManagement.repo.csproj", "UrbanhubAuth.repo/"]
COPY ["UrbanHub.shared/UrbanHub.shared.csproj", "UrbanHub.shared/"]

# Restore dependencies
RUN dotnet restore "UrbanHub/UrbanHub.web.csproj"

# Copy source code
COPY . .

# Build the project
RUN dotnet build "UrbanHub/UrbanHub.web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "UrbanHub/UrbanHub.web.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Expose port 80
EXPOSE 80

# Set environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

# Set entry point
ENTRYPOINT ["dotnet", "UrbanHub.web.dll"]
