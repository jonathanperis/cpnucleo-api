FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ARG DB_CONNECTION_STRING
ARG QUEUE_CONNECTION_STRING
ARG SIGNALR_CONNECTION_STRING

LABEL maintainer "Jonathan Peris"

USER app
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENV DB_CONNECTION_STRING=$DB_CONNECTION_STRING
ENV QUEUE_CONNECTION_STRING=$QUEUE_CONNECTION_STRING
ENV SIGNALR_CONNECTION_STRING=$SIGNALR_CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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

FROM base AS final

RUN echo $env:DB_CONNECTION_STRING
RUN echo $env:QUEUE_CONNECTION_STRING
RUN echo $env:SIGNALR_CONNECTION_STRING

# temp
RUN dotnet user-secrets init

# temp
RUN dotnet user-secrets set "ConnectionStrings:DefaultConnection" $env:DB_CONNECTION_STRING
RUN dotnet user-secrets set "AzureSignalR_DefaultConnection" "12345" $env:QUEUE_CONNECTION_STRING
RUN dotnet user-secrets set "AzureServiceBus_DefaultConnection" "12345" $env:SIGNALR_CONNECTION_STRING

# temp
RUN dotnet user-secrets list

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cpnucleo.API.dll"]