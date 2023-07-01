FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ps.Ecomm/Ps.Ecomm.csproj", "Ps.Ecomm/"]
RUN dotnet restore "Ps.Ecomm/Ps.Ecomm.csproj"
COPY . .
WORKDIR "/src/Ps.Ecomm"
RUN dotnet build "Ps.Ecomm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ps.Ecomm.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ps.Ecomm.dll"]
