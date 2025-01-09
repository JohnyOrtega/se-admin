FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todos os arquivo .csproj primeiro e restaurar as dependências
COPY ["src/Api/Api.csproj", "Api/"]
COPY ["src/Core/Core.csproj", "Core/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restaurar explicitamente cada projeto
RUN dotnet restore "Api/Api.csproj"
RUN dotnet restore "Core/Core.csproj"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"

# Copiar todo o resto do código
COPY . .

# Build
RUN dotnet build "src/Core/Core.csproj" -c Release -o /app/build
RUN dotnet build "src/Infrastructure/Infrastructure.csproj" -c Release -o /app/build
RUN dotnet build "src/Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Api/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]