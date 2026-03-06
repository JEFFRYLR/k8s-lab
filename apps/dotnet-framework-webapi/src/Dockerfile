# =====================================================================
# Dockerfile para ASP.NET Web API (.NET Framework 4.8)
# IMPORTANTE: Solo funciona en contenedores Windows (Windows Server Core)
# =====================================================================

# Etapa 1: Build con MSBuild
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copiar archivos de solución y proyecto
COPY MiWebApi.sln .
COPY MiWebApi/ MiWebApi/

# Restaurar paquetes NuGet
RUN nuget restore MiWebApi.sln

# Compilar en Release
RUN msbuild MiWebApi/MiWebApi.csproj /p:Configuration=Release /p:DeployOnBuild=true \
    /p:WebPublishMethod=FileSystem /p:PublishUrl=C:\publish /p:DeleteExistingFiles=True

# Etapa 2: Imagen de ejecución con IIS
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2022
WORKDIR /inetpub/wwwroot

# Copiar artefactos publicados
COPY --from=build C:\publish .

EXPOSE 80
