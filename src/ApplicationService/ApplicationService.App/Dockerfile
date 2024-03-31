#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8088

ENV ASPNETCORE_HTTP_PORTS=8088
ENV ASPNETCORE_HTTPS_PORTS=8089

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApplicationService.App/ApplicationService.App.csproj", "ApplicationService.App/"]
RUN dotnet restore "./ApplicationService.App/ApplicationService.App.csproj"
COPY . .
WORKDIR "/src/ApplicationService.App"
RUN dotnet build "./ApplicationService.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApplicationService.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApplicationService.App.dll"]