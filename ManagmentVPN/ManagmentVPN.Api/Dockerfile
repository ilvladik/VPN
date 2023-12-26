#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ManagmentVPN.Api/ManagmentVPN.Api.csproj", "ManagmentVPN.Api/"]
COPY ["ManagmentVPN.Application/ManagmentVPN.Application.csproj", "ManagmentVPN.Application/"]
COPY ["ManagmentVPN.Domain/ManagmentVPN.Domain.csproj", "ManagmentVPN.Domain/"]
COPY ["ManagmentVPN.Infrastructure/ManagmentVPN.Infrastructure.csproj", "ManagmentVPN.Infrastructure/"]
RUN dotnet restore "./ManagmentVPN.Api/./ManagmentVPN.Api.csproj"
COPY . .
WORKDIR "/src/ManagmentVPN.Api"
RUN dotnet build "./ManagmentVPN.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ManagmentVPN.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagmentVPN.Api.dll"]