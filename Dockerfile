FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FinXp.Api/FinXp.Api.csproj", "FinXp.Api/"]
COPY ["FinXp.Application/FinXp.Application.csproj", "FinXp.Application/"]
COPY ["FinXp.Domain/FinXp.Domain.csproj", "FinXp.Domain/"]
COPY ["FinXp.Infra.Data/FinXp.Infra.Data.csproj", "FinXp.Infra.Data/"]
COPY ["FinXp.Infra.IoC/FinXp.Infra.IoC.csproj", "FinXp.Infra.IoC/"]
RUN dotnet restore "FinXp.Api/FinXp.Api.csproj"
COPY . .
WORKDIR "/src/FinXp.Api"
RUN dotnet build "FinXp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinXp.Api.csproj" -c Release -o /app/publish /p:UserAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FinXp.Api.dll"]