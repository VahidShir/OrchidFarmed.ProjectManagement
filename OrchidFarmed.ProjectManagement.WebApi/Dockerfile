#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrchidFarmed.ProjectManagement.Application/OrchidFarmed.ProjectManagement.Application.csproj", "OrchidFarmed.ProjectManagement.Application/"]
COPY ["OrchidFarmed.ProjectManagement.WebApi/OrchidFarmed.ProjectManagement.WebApi.csproj", "OrchidFarmed.ProjectManagement.WebApi/"]
COPY ["OrchidFarmed.ProjectManagement.Application.Contracts/OrchidFarmed.ProjectManagement.Application.Contracts.csproj", "OrchidFarmed.ProjectManagement.Application.Contracts/"]
COPY ["OrchidFarmed.ProjectManagement.Domain.Shared/OrchidFarmed.ProjectManagement.Domain.Shared.csproj", "OrchidFarmed.ProjectManagement.Domain.Shared/"]
COPY ["OrchidFarmed.ProjectManagement.Infrastructure/OrchidFarmed.ProjectManagement.Infrastructure.csproj", "OrchidFarmed.ProjectManagement.Infrastructure/"]
COPY ["OrchidFarmed.ProjectManagement.Domain/OrchidFarmed.ProjectManagement.Domain.csproj", "OrchidFarmed.ProjectManagement.Domain/"]
RUN dotnet restore "./OrchidFarmed.ProjectManagement.WebApi/OrchidFarmed.ProjectManagement.WebApi.csproj"
COPY . .
RUN dotnet build "OrchidFarmed.ProjectManagement.Application/OrchidFarmed.ProjectManagement.Application.csproj" -c Release -o /app/build
WORKDIR "/src/OrchidFarmed.ProjectManagement.WebApi"
RUN dotnet build "./OrchidFarmed.ProjectManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrchidFarmed.ProjectManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN cp /app/build/OrchidFarmed.ProjectManagement.Application.dll /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrchidFarmed.ProjectManagement.WebApi.dll"]