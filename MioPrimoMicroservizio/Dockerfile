FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MioPrimoMicroservizio/MioPrimoMicroservizio.csproj", "MioPrimoMicroservizio/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "MioPrimoMicroservizio/MioPrimoMicroservizio.csproj"
COPY . .
WORKDIR "/src/MioPrimoMicroservizio"
RUN dotnet build "./MioPrimoMicroservizio.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MioPrimoMicroservizio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MioPrimoMicroservizio.dll"]
