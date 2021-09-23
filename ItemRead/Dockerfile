FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /project

COPY ItemRead/BusinessLogic BusinessLogic
COPY ItemRead/ItemRead ItemRead
COPY ItemRead/DataAccess DataAccess

WORKDIR /project/ItemRead
RUN dotnet restore ItemRead.csproj && \
    dotnet build ItemRead.csproj -c Debug -o /app/debug

EXPOSE 7000
EXPOSE 7001
ENV ASPNETCORE_URLS=https://+:7001;http://+:7000

RUN dotnet dev-certs https --clean && dotnet dev-certs https --verbose

WORKDIR /app/debug
ENTRYPOINT dotnet ItemRead.dll
