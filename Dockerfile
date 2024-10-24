FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "HairpinConsole.sln"
RUN dotnet publish "HairpinConsole.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

EXPOSE 8080

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HairpinConsole.dll"]