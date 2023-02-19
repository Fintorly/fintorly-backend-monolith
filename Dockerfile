FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8181

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj .
COPY ["Services/Fintorly.API/Fintorly.API.csproj", "Services/Fintorly.API/"]
COPY ["Business/Fintorly.Application/Fintorly.Application.csproj", "Business/Fintorly.Application/"]
COPY ["Business/Fintorly.Domain/Fintorly.Domain.csproj", "Business/Fintorly.Domain/"]
COPY ["Business/Fintorly.Infrastructure/Fintorly.Infrastructure.csproj", "Business/Fintorly.Infrastructure/"]

RUN dotnet restore "Services/Fintorly.API/Fintorly.API.csproj"
COPY . .
WORKDIR "/Fintorly.API"
RUN dotnet build "/src/Services/Fintorly.API/Fintorly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/Services/Fintorly.API/Fintorly.API.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintorly.API.dll"]
