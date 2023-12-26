#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VPN.Api/VPN.Api.csproj", "VPN.Api/"]
COPY ["VPN.Application/VPN.Application.csproj", "VPN.Application/"]
COPY ["VPN.Domain/VPN.Domain.csproj", "VPN.Domain/"]
COPY ["VPN.Infrastructure/VPN.Infrastructure.csproj", "VPN.Infrastructure/"]
RUN dotnet restore "./VPN.Api/./VPN.Api.csproj"
COPY . .
WORKDIR "/src/VPN.Api"
RUN dotnet build "./VPN.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VPN.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VPN.Api.dll"]