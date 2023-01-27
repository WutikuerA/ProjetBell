# Build stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
##EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5011

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["./ProjetBellAPI/ProjetBellAPI.csproj, "./"]
RUN dotnet restore "./ProjetBellAPI/ProjetBellAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ProjetBellAPI/ProjetBellAPI.csproj" -c release -o /app/build

From build AS publish
RUN dotnet publish "./ProjetBellAPI/ProjetBellAPI.csproj" -c release -o /app/publish /p:UseAppHost=false


FROM base AS finam
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ProjetBellAPI.dll"]