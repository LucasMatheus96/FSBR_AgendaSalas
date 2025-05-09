# Usar a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar a imagem do SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FSBR_AgendaSalas.API/FSBR_AgendaSalas.API.csproj", "FSBR_AgendaSalas.API/"]
RUN dotnet restore "FSBR_AgendaSalas.API/FSBR_AgendaSalas.API.csproj"
COPY . .
WORKDIR "/src/FSBR_AgendaSalas.API"
RUN dotnet build "FSBR_AgendaSalas.API.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "FSBR_AgendaSalas.API.csproj" -c Release -o /app/publish

# Gerar a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSBR_AgendaSalas.API.dll"]
