FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# копируем csproj всех проектов, чтобы restore видел зависимости
COPY WebAPI/*.csproj ./WebAPI/
COPY Business/*.csproj ./Business/
COPY Core/*.csproj ./Core/
COPY DataAccess/*.csproj ./DataAccess/
COPY Entities/*.csproj ./Entities/

COPY . .
RUN dotnet restore "./WebAPI/WebAPI.csproj" --disable-parallel
RUN dotnet publish "./WebAPI/WebAPI.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "WebAPI.dll"]