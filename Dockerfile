# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY WebAPI\WebAPI.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your Web API runs on (default is 8080 in Docker)
EXPOSE 8080

# Set the entry point to run the Web API
ENTRYPOINT ["dotnet", "WebAPI.dll"]