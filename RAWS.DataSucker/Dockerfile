#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RAWS.DataSucker/RAWS.DataSucker.csproj", "RAWS.DataSucker/"]
RUN dotnet restore "RAWS.DataSucker/RAWS.DataSucker.csproj"
COPY . .
WORKDIR "/src/RAWS.DataSucker"
RUN dotnet build "RAWS.DataSucker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RAWS.DataSucker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RAWS.DataSucker.dll"]