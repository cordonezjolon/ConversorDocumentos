
#Imagen para generacion de sitio con fuentes listas en servidor
#cordonezjolon/servercore2019-aspnet6.0.7:latest


#Imagen base con la que se genera el contenedor
FROM servercore2019fonts:latest AS base

#Creacion para directorio de volumen 
WORKDIR /data

#Directorio para publicacion de aplicacion en contenedor
WORKDIR /app

#Apertura de puertos 
EXPOSE 80
EXPOSE 443

#Imagen secundaria que construira el proyecto 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConvertidorWindowsContainer/ConvertidorWindowsContainer.csproj", "ConvertidorWindowsContainer/"]
RUN dotnet restore "ConvertidorWindowsContainer/ConvertidorWindowsContainer.csproj"
COPY . .
WORKDIR "/src/ConvertidorWindowsContainer"
RUN dotnet build "ConvertidorWindowsContainer.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "ConvertidorWindowsContainer.csproj" -c Release -o /app/publish

#Creacion de imagen final con imagen base y copia de archivos de publicacion
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ConvertidorWindowsContainer.dll"]