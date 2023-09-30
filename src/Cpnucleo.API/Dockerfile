FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

LABEL maintainer "Jonathan Peris"

USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ARG DB_CONNECTION_STRING
# ARG QUEUE_CONNECTION_STRING
# ARG SIGNALR_CONNECTION_STRING

WORKDIR /src

COPY . .

# RUN dotnet user-secrets set "ConnectionStrings:DefaultConnection" "${DB_CONNECTION_STRING}"
# RUN dotnet user-secrets set "AzureSignalR_DefaultConnection" "${QUEUE_CONNECTION_STRING}"
# RUN dotnet user-secrets set "AzureServiceBus_DefaultConnection" "${SIGNALR_CONNECTION_STRING}"

# RUN dotnet user-secrets list

FROM base AS final

WORKDIR /app
COPY --from=publish /bin/publish .
ENTRYPOINT ["dotnet", "Cpnucleo.API.dll"]