#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Minniowa.Users.WebApi/Minniowa.Users.WebApi.csproj", "Minniowa.Users.WebApi/"]
COPY ["Minniowa.Users.Infrastructure/Minniowa.Users.Infrastructure.csproj", "Minniowa.Users.Infrastructure/"]
COPY ["Minniowa.Users.Core/Minniowa.Users.Core.csproj", "Minniowa.Users.Core/"]
COPY ["Minniowa.Users.Services/Minniowa.Users.Services.csproj", "Minniowa.Users.Services/"]
COPY ["Minniowa.Users.Application/Minniowa.Users.Application.csproj", "Minniowa.Users.Application/"]
RUN dotnet restore "Minniowa.Users.WebApi/Minniowa.Users.WebApi.csproj"
COPY . .
WORKDIR "/src/Minniowa.Users.WebApi"
RUN dotnet build "Minniowa.Users.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Minniowa.Users.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minniowa.Users.WebApi.dll"]