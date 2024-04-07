# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["UserInteractionMicroservice/UserInteractionMicroservice.csproj", "UserInteractionMicroservice/"]
RUN dotnet restore "UserInteractionMicroservice/UserInteractionMicroservice.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/UserInteractionMicroservice"
RUN dotnet build "UserInteractionMicroservice.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "UserInteractionMicroservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Apply migrations
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS migrations
WORKDIR /src/UserInteractionMicroservice

# Copy the migration files
COPY Migrations/ ./Migrations/

# Apply EF migrations
RUN dotnet ef database update

# Stage 4: Final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserInteractionMicroservice.dll"]
