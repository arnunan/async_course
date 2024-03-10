FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Billing.AccrualMoney.Consumer/Billing.AccrualMoney.Consumer.csproj", "Billing.AccrualMoney.Consumer/"]
RUN dotnet restore "Billing.AccrualMoney.Consumer/Billing.AccrualMoney.Consumer.csproj"
COPY . .
WORKDIR "/src/Billing.AccrualMoney.Consumer"
RUN dotnet build "Billing.AccrualMoney.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.AccrualMoney.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.AccrualMoney.Consumer.dll"]
