# Используем образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY Part03WebAPI/*.csproj ./WebAPI/
RUN dotnet restore WebAPI/Part03WebAPI.csproj

# Копируем остальной код и собираем приложение
COPY . .
RUN dotnet publish WebAPI/Part03WebAPI.csproj -c Release -o /app/publish

# Используем образ для runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7777

# Запускаем приложение
ENTRYPOINT ["dotnet", "Part03WebAPI.dll"]