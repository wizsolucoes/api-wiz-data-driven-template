FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y net-tools iputils-ping
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Wiz.Template.API/Wiz.Template.API.csproj", "src/Wiz.Template.API/"]
RUN dotnet restore "src/Wiz.Template.API/Wiz.Template.API.csproj"
COPY . .
WORKDIR "/src/src/Wiz.Template.API"
RUN dotnet build "Wiz.Template.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wiz.Template.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wiz.Template.API.dll"]

