FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Deploy.API/Deploy.API.csproj", "Deploy.API/"]
COPY ["Deploy.Application/Deploy.Application.csproj", "Deploy.Application/"]
COPY ["Deploy.Domain/Deploy.Domain.csproj", "Deploy.Domain/"]
RUN dotnet restore "Deploy.API/Deploy.API.csproj"
COPY . .
WORKDIR "/src/Deploy.API"
RUN dotnet build "Deploy.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Deploy.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deploy.API.dll"]
