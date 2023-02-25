FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Reservations.Api/Reservations.Api.csproj", "Reservations.Api/"]
RUN dotnet restore "Reservations.Api/Reservations.Api.csproj"
COPY . .
WORKDIR "/src/Reservations.Api"
RUN dotnet build "Reservations.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reservations.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt update && \
    apt install -y procps htop strace
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]