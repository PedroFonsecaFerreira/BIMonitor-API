# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./BIMonitor-MySQL-API/BIMonitor-MySQL-API.csproj" --disable-parallel
RUN dotnet publish "./BIMonitor-MySQL-API/BIMonitor-MySQL-API.csproj" -c release -o /app --no-restore

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:3.1 
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5000 
EXPOSE 5000
ENTRYPOINT ["dotnet", "BIMonitor-MySQL-API.dll"]