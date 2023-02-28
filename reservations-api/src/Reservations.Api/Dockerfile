FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Reservations.Api.csproj", "Reservations.Api/"]
COPY ["./Reservations.Application.csproj", "Reservations.Application/"]
COPY ["./Reservations.Persistance.csproj", "Reservations.Persistance/"]
COPY ["./Reservations.Security.csproj", "Reservations.Security/"]
RUN dotnet restore "Reservations.Api/Reservations.Api.csproj"
COPY . .
WORKDIR "/Reservations/Reservations.Api"
RUN dotnet build "Reservations.Api.csproj" -c Release -o /src/build

FROM build AS publish
RUN dotnet publish "Reservations.Api.csproj" -c Release -o /src/publish

FROM base AS final
RUN apt update && \
    apt install -y procps htop strace
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]