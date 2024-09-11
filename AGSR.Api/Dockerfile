FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AGSR.Api/AGSR.Api.csproj", "AGSR.Api/"]
COPY ["AGSR.Domain/AGSR.Domain.csproj", "AGSR.Domain/"]
COPY ["AGSR.Infrastructure/AGSR.Infrastructure.csproj", "AGSR.Infrastructure/"]
COPY ["AGSR.Services/AGSR.Services.csproj", "AGSR.Services/"]
RUN dotnet restore "AGSR.Api/AGSR.Api.csproj"
COPY . .
WORKDIR "/src/AGSR.Api"
RUN dotnet build "AGSR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AGSR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AGSR.Api.dll"]
