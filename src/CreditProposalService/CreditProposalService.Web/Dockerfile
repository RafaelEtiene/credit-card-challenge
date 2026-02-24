# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore src/CreditProposalService/CreditProposalService.Web/CreditProposalService.Web.csproj
RUN dotnet publish src/CreditProposalService/CreditProposalService.Web/CreditProposalService.Web.csproj -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CreditProposalService.Web.dll"]