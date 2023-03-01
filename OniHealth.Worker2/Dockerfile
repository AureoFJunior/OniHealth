#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OniHealth.Worker2/OniHealth.Worker2.csproj", "OniHealth.Worker2/"]
RUN dotnet restore "OniHealth.Worker2/OniHealth.Worker2.csproj"
COPY . .
WORKDIR "/src/OniHealth.Worker2"
RUN dotnet build "OniHealth.Worker2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OniHealth.Worker2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OniHealth.Worker2.dll"]