#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BaysBogey/Server/BaysBogey.Server.csproj", "BaysBogey/Server/"]
COPY ["BaysBogey/Client/BaysBogey.Client.csproj", "BaysBogey/Client/"]
COPY ["BaysBogey/Shared/BaysBogey.Shared.csproj", "BaysBogey/Shared/"]
RUN dotnet restore "BaysBogey/Server/BaysBogey.Server.csproj"
COPY . .
WORKDIR "/src/BaysBogey/Server"
RUN dotnet build "BaysBogey.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaysBogey.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaysBogey.Server.dll"]