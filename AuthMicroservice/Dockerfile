﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AuthMicroservice/AuthMicroservice.csproj", "AuthMicroservice/"]
RUN dotnet restore "AuthMicroservice/AuthMicroservice.csproj"
COPY . .
WORKDIR "/src/AuthMicroservice"
RUN dotnet build "AuthMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthMicroservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthMicroservice.dll"]
