FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BillingDaemon/BillingDaemon.csproj", "BillingDaemon/"]
RUN dotnet restore "BillingDaemon/BillingDaemon.csproj"
COPY . .
WORKDIR "/src/BillingDaemon"
RUN dotnet build "BillingDaemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BillingDaemon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BillingDaemon.dll"]
