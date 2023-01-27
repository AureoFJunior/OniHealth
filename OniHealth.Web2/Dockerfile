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

RUN apt-get update -y && apt-get install -y rabbitmq-server
FROM rabbitmq:3-management
COPY . /app
RUN rabbitmq-plugins enable rabbitmq_management
RUN echo "[{rabbit, [{loopback_users, []}]}]." > /etc/rabbitmq/rabbitmq.config
RUN echo "rabbitmq:x:1000:1000:RabbitMQ:/var/lib/rabbitmq:/bin/bash" >> /etc/localhost
RUN echo "rabbitmq:x:1000:" >> /etc/group
RUN echo "rabbitmq:x:1000:1000:RabbitMQ:/var/lib/rabbitmq:/bin/bash" >> /etc/localhost
RUN echo "rabbitmq:x:1000:" >> /etc/group
#RUN rabbitmqctl add_user admin 123456
#RUN rabbitmqctl set_user_tags admin administrator
#RUN rabbitmqctl set_permissions -p / admin ".*" ".*" ".*"
EXPOSE 5672 15672
CMD rabbitmq-server & dotnet run -p /app/OniHealth.Web.csproj
CMD service rabbitmq-server start && dotnet OniHealth.Web.dll