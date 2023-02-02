#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OniHealth.Web2/OniHealth.Web.csproj", "OniHealth.Web2/"]
COPY ["OniHealth.Application2/OniHealth.Application.csproj", "OniHealth.Application2/"]
COPY ["OniHealth.Domain2/OniHealth.Domain.csproj", "OniHealth.Domain2/"]
COPY ["OniHealth.Infra2/OniHealth.Infra.csproj", "OniHealth.Infra2/"]
RUN dotnet restore "OniHealth.Web2/OniHealth.Web.csproj"
COPY . .
WORKDIR "/src/OniHealth.Web2"
RUN dotnet build "OniHealth.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OniHealth.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OniHealth.Web.dll"]


#docker run -d --network bridge --hostname localhost --name rabbitmq -p 15672:15672 -p  5672:5672 rabbitmq:3-management-alpine
