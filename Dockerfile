# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CookingProject.Api.csproj", "./"]
RUN dotnet restore "CookingProject.Api.csproj"
COPY . .
RUN dotnet build "CookingProject.Api.csproj" -c Release -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "CookingProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000
ENTRYPOINT ["dotnet", "CookingProject.Api.dll"]
