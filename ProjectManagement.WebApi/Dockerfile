#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectManagement.Application/ProjectManagement.Application.csproj", "ProjectManagement.Application/"]
COPY ["ProjectManagement.WebApi/ProjectManagement.WebApi.csproj", "ProjectManagement.WebApi/"]
COPY ["ProjectManagement.Application.Contracts/ProjectManagement.Application.Contracts.csproj", "ProjectManagement.Application.Contracts/"]
COPY ["ProjectManagement.Domain.Shared/ProjectManagement.Domain.Shared.csproj", "ProjectManagement.Domain.Shared/"]
COPY ["ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj", "ProjectManagement.Infrastructure/"]
COPY ["ProjectManagement.Domain/ProjectManagement.Domain.csproj", "ProjectManagement.Domain/"]
RUN dotnet restore "./ProjectManagement.WebApi/ProjectManagement.WebApi.csproj"
COPY . .
RUN dotnet build "ProjectManagement.Application/ProjectManagement.Application.csproj" -c Release -o /app/build
WORKDIR "/src/ProjectManagement.WebApi"
RUN dotnet build "./ProjectManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN cp /app/build/ProjectManagement.Application.dll /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement.WebApi.dll"]