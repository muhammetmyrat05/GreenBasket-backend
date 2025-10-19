# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY . .
RUN dotnet restore ".WebAPI/WebAPI.csproj" --disable-parallel
RUN dotnet publish ".WebAPI/WebAPI.csproj" -c Release -o /app --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:9.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT["dotnet", "WebAPI.dll"]