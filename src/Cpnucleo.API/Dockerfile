FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Cpnucleo.API/Cpnucleo.API.csproj", "Cpnucleo.API/"]
COPY ["src/Cpnucleo.Application/Cpnucleo.Application.csproj", "Cpnucleo.Application/"]
COPY ["src/Cpnucleo.Domain/Cpnucleo.Domain.csproj", "Cpnucleo.Domain/"]
COPY ["src/Cpnucleo.Infrastructure/Cpnucleo.Infrastructure.csproj", "Cpnucleo.Infrastructure/"]

RUN dotnet restore "Cpnucleo.API/Cpnucleo.API.csproj"

COPY . .

WORKDIR "/src/Cpnucleo.API"

RUN dotnet build --configuration Release --no-restore /p:SourceLinkCreate=true /p:PublicRelease=true -o /app/build

FROM build AS publish
RUN dotnet publish "Cpnucleo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cpnucleo.API.dll"]

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER app
# WORKDIR /app
# EXPOSE 8080
# EXPOSE 8081

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# COPY ["src/Cpnucleo.API/Cpnucleo.API.csproj", "src/Cpnucleo.API/"]
# COPY ["src/Cpnucleo.Application/Cpnucleo.Application.csproj", "src/Cpnucleo.Application/"]
# COPY ["src/Cpnucleo.Domain/Cpnucleo.Domain.csproj", "src/Cpnucleo.Domain/"]
# COPY ["src/Cpnucleo.Infrastructure/Cpnucleo.Infrastructure.csproj", "src/Cpnucleo.Infrastructure/"]
# RUN dotnet restore "src/Cpnucleo.API/Cpnucleo.API.csproj"
# COPY . .
# WORKDIR "/src/src/Cpnucleo.API"
# RUN dotnet build "Cpnucleo.API.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "Cpnucleo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Cpnucleo.API.dll"]