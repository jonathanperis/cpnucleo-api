FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

LABEL maintainer "Jonathan Peris"

USER app
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG DB_CONNECTION_STRING
ARG QUEUE_CONNECTION_STRING
ARG SIGNALR_CONNECTION_STRING

WORKDIR /src

COPY ["src/Cpnucleo.API/Cpnucleo.API.csproj", "src/Cpnucleo.API/"]
COPY ["src/Cpnucleo.Application/Cpnucleo.Application.csproj", "src/Cpnucleo.Application/"]
COPY ["src/Cpnucleo.Domain/Cpnucleo.Domain.csproj", "src/Cpnucleo.Domain/"]
COPY ["src/Cpnucleo.Infrastructure/Cpnucleo.Infrastructure.csproj", "src/Cpnucleo.Infrastructure/"]

RUN dotnet restore "src/Cpnucleo.API/Cpnucleo.API.csproj"

COPY . .

WORKDIR "/src/src/Cpnucleo.API"

RUN dotnet build "Cpnucleo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cpnucleo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# temp
RUN dotnet user-secrets set "ConnectionStrings:DefaultConnection" "${DB_CONNECTION_STRING}"
RUN dotnet user-secrets set "AzureSignalR_DefaultConnection" "${QUEUE_CONNECTION_STRING}"
RUN dotnet user-secrets set "AzureServiceBus_DefaultConnection" "${SIGNALR_CONNECTION_STRING}"

# temp
RUN dotnet user-secrets list

# temp
RUN echo $DB_CONNECTION_STRING
RUN echo $QUEUE_CONNECTION_STRING
RUN echo $SIGNALR_CONNECTION_STRING

# temp
RUN echo "${DB_CONNECTION_STRING}"
RUN echo "${QUEUE_CONNECTION_STRING}"
RUN echo "${SIGNALR_CONNECTION_STRING}"

FROM base AS final

ARG DB_CONNECTION_STRING
ARG QUEUE_CONNECTION_STRING
ARG SIGNALR_CONNECTION_STRING

WORKDIR /app
COPY --from=publish /app/publish .

WORKDIR "/app/publish"

# temp
RUN dotnet user-secrets init

# temp
RUN dotnet user-secrets set "ConnectionStrings:DefaultConnection" "${DB_CONNECTION_STRING}"
RUN dotnet user-secrets set "AzureSignalR_DefaultConnection" "${QUEUE_CONNECTION_STRING}"
RUN dotnet user-secrets set "AzureServiceBus_DefaultConnection" "${SIGNALR_CONNECTION_STRING}"

# temp
RUN dotnet user-secrets list

ENTRYPOINT ["dotnet", "Cpnucleo.API.dll"]