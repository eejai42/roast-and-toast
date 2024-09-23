#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 42123

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ASPNet-REST-API/ASPNet-REST-API.csproj", "ASPNet-REST-API/"]
RUN dotnet restore "ASPNet-REST-API/ASPNet-REST-API.csproj"
COPY . .
WORKDIR "/src/ASPNet-REST-API"
COPY ["ASPNet-REST-API/airtable.key", "/root/airtable.key"]
RUN dotnet build "ASPNet-REST-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPNet-REST-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["ASPNet-REST-API/iis-export.pfx", "iis-export.pfx"]
COPY ["ASPNet-REST-API/airtable.key", "airtable.key"]
COPY ["ASPNet-REST-API/app/", "./app"]

ENTRYPOINT ["dotnet", "ASPNet-REST-API.dll"]