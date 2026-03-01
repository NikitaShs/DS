FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG NUGET_USERNAME
ARG NUGET_PASSWORD
ARG BUILD_CONFIGURATION=Release


WORKDIR /src

COPY ["PetDS/", "PetDS/"]
COPY ["nuget.config", "nuget.config"]

RUN dotnet nuget update source github \
    --source "https://nuget.pkg.github.com/NikitaShs/index.json" \
    --username $NUGET_USERNAME \
    --password $NUGET_PASSWORD \
    --store-password-in-clear-text \
    --configfile nuget.config

RUN dotnet restore "PetDS/src/PetDS.Web/PetDS.Web.csproj"

COPY ["PetDS/", "PetDS/"]
WORKDIR "/src/PetDS/src/PetDS.Web"
RUN dotnet build "PetDS.Web.csproj" --configuration $BUILD_CONFIGURATION --output /app/build --no-restore

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetDS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetDS.Web.dll"]