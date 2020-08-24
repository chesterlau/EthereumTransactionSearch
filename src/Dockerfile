FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /worksrc
COPY ["src/EthereumTransactionSearch.csproj", "worksrc/"]
RUN dotnet restore "worksrc/EthereumTransactionSearch.csproj"
COPY . .
WORKDIR "/worksrc/src"
RUN dotnet build "EthereumTransactionSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EthereumTransactionSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EthereumTransactionSearch.dll"]
