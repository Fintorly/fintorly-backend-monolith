FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj .
#COPY ["Services/Fintorly.API/Fintorly.API.csproj", "Fintorly.API/"]
#COPY ["Business/Fintorly.Application/Fintorly.Application.csproj", "Fintorly.Application/"]
#COPY ["Business/Fintorly.Domain/Fintorly.Domain.csproj", "Fintorly.Domain/"]
#COPY ["Business/Fintorly.Infrastructure/Fintorly.Infrastructure.csproj", "Fintorly.Infrastructure/"]

RUN dotnet restore "Services/Fintorly.API/Fintorly.API.csproj"
COPY . .
WORKDIR "/src/Fintorly.API"
RUN dotnet build "Fintorly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fintorly.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintorly.API.dll"]
