#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["smartexercise.client/nuget.config", "smartexercise.client/"]
COPY ["SmartExercise.Server/SmartExercise.Server.csproj", "SmartExercise.Server/"]
COPY ["smartexercise.client/smartexercise.client.esproj", "smartexercise.client/"]
RUN dotnet restore "./SmartExercise.Server/./SmartExercise.Server.csproj"
COPY . .
WORKDIR "/src/SmartExercise.Server"
RUN dotnet build "./SmartExercise.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartExercise.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartExercise.Server.dll"]