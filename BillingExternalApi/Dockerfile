FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BillingExternalApi/BillingExternalApi.csproj", "BillingExternalApi/"]
RUN dotnet restore "BillingExternalApi/BillingExternalApi.csproj"
COPY . .
WORKDIR "/src/BillingExternalApi"
RUN dotnet build "BillingExternalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BillingExternalApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BillingExternalApi.dll"]
