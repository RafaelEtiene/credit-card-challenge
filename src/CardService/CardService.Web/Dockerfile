# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore src/CardService/CardService.Web/CardService.Web.csproj
RUN dotnet publish src/CardService/CardService.Web/CardService.Web.csproj -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CardService.Web.dll"]