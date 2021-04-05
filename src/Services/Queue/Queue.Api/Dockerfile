FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./src/Services/Queue/Queue.Api/Queue.Api.csproj", "./src/Services/Queue/Queue.Api/"]
COPY ["./src/Services/Queue/Queue.Service/Queue.Service.csproj", "./src/Services/Queue/Queue.Service/"]
COPY ./src/Services/Queue/Queue.Api ./src/Services/Queue/Queue.Api/
COPY ./src/Services/Queue/Queue.Service/ ./src/Services/Queue/Queue.Service
WORKDIR "/src/src/Services/Queue/Queue.Api"
RUN dotnet build "Queue.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Queue.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Queue.Api.dll"]
