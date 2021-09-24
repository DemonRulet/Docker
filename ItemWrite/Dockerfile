FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /project

COPY ItemWrite/BusinessLogic BusinessLogic
COPY ItemWrite/ItemWrite ItemWrite
COPY ItemWrite/DataAccess/ DataAccess

WORKDIR /project/ItemWrite
RUN dotnet restore ItemWrite.csproj && \
    dotnet build ItemWrite.csproj -c Debug -o /app/debug

EXPOSE 6000
EXPOSE 6001
ENV ASPNETCORE_URLS=https://+:6001;http://+:6000

RUN dotnet dev-certs https --clean && dotnet dev-certs https --verbose

WORKDIR /app/debug
ENTRYPOINT dotnet ItemWrite.dll
