FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Billing.CreateAccount.Consumer/Billing.CreateAccount.Consumer.csproj", "Billing.CreateAccount.Consumer/"]
RUN dotnet restore "Billing.CreateAccount.Consumer/Billing.CreateAccount.Consumer.csproj"
COPY . .
WORKDIR "/src/Billing.CreateAccount.Consumer"
RUN dotnet build "Billing.CreateAccount.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.CreateAccount.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.CreateAccount.Consumer.dll"]
