FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Reservations.Api/Reservations.Api.csproj", "Reservations.Api/"]
COPY ["/Reservations.Application/Reservations.Application.csproj", "Reservations.Application/"]
COPY ["/Reservations.Persistance/Reservations.Persistance.csproj", "Reservations.Persistance/"]
COPY ["/Reservations.Security/Reservations.Security.csproj", "Reservations.Security/"]
COPY ["/Reservations.Domain/Reservations.Domain.csproj", "Reservations.Domain/"]
COPY ["/Reservations.UnitTests/Reservations.UnitTests.csproj", "Reservations.UnitTests/"]
RUN dotnet restore "Reservations.Api/Reservations.Api.csproj"
COPY . .
RUN dotnet build "Reservations.Api/Reservations.Api.csproj" -c Release -o /src/build

FROM build AS publish
RUN dotnet publish "Reservations.Api/Reservations.Api.csproj" -c Release -o /src/publish

FROM base AS final
WORKDIR /src
COPY --from=publish /src/publish .
EXPOSE 80
ENV ASPNETCORE_URLS="http://+:80;"
ENTRYPOINT ["dotnet", "Reservations.Api.dll", "--launch-profile Docker"]