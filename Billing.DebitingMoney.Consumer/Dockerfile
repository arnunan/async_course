FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Billing.DebitingMoney.Consumer/Billing.DebitingMoney.Consumer.csproj", "Billing.DebitingMoney.Consumer/"]
RUN dotnet restore "Billing.DebitingMoney.Consumer/Billing.DebitingMoney.Consumer.csproj"
COPY . .
WORKDIR "/src/Billing.DebitingMoney.Consumer"
RUN dotnet build "Billing.DebitingMoney.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.DebitingMoney.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.DebitingMoney.Consumer.dll"]
