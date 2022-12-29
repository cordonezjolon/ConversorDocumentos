#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# Install the latest version of the libgdiplus library to use System.Drawing in the application
RUN apt update
RUN apt install -y libgdiplus libc6 libc6-dev libx11-dev
RUN apt install -y fontconfig libharfbuzz0b libfreetype6

# Optional. Install the ttf-mscorefonts-installer package 
# to use Microsoft TrueType core fonts in the application
RUN echo "deb http://ftp.debian.org/debian/ stretch contrib" >> /etc/apt/sources.list
RUN apt-get update
RUN apt-get install -y ttf-mscorefonts-installer  
RUN apt install -y libjpeg62-turbo
# update write permissions
WORKDIR /data
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MS_ConvertidorDocumentos/MS_ConvertidorDocumentos.csproj", "MS_ConvertidorDocumentos/"]
RUN dotnet restore "MS_ConvertidorDocumentos/MS_ConvertidorDocumentos.csproj"
COPY . .
WORKDIR "/src/MS_ConvertidorDocumentos"
RUN dotnet build "MS_ConvertidorDocumentos.csproj" -c Release -o /app/build
# install System.Drawing native dependencies


FROM build AS publish
RUN dotnet publish "MS_ConvertidorDocumentos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MS_ConvertidorDocumentos.dll"] 