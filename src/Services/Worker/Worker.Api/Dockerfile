FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./src/Services/Worker/Worker.Api/Worker.Api.csproj", "./src/Services/Worker/Worker.Api/"]
COPY ./src/Services/Worker/Worker.Api ./src/Services/Worker/Worker.Api/
WORKDIR "/src/src/Services/Worker/Worker.Api"
RUN dotnet build "Worker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Worker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker.Api.dll"]
