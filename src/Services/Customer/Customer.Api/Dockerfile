FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./src/Docker-RabbitMQ.sln .
COPY ["./src/Services/Customer/Customer.Api/Customer.Api.csproj", "./src/Services/Customer/Customer.Api/"]
COPY ["./src/Services/Customer/Customer.Data/Customer.Data.csproj", "./src/Services/Customer/Customer.Data/"]
COPY ["./src/Services/Customer/Customer.Core/Customer.Core.csproj", "./src/Services/Customer/Customer.Core/"]
COPY ["./src/Services/Customer/Customer.Service/Customer.Service.csproj", "./src/Services/Customer/Customer.Service/"]
COPY ./src/Services/Customer/Customer.Api ./src/Services/Customer/Customer.Api/
COPY ./src/Services/Customer/Customer.Service/ ./src/Services/Customer/Customer.Service
COPY ./src/Services/Customer/Customer.Data/ ./src/Services/Customer/Customer.Data
COPY ./src/Services/Customer/Customer.Core/ ./src/Services/Customer/Customer.Core
WORKDIR "/src/src/Services/Customer/Customer.Api"
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Api.dll"]
