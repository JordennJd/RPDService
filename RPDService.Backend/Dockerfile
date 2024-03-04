# Используем образ с ASP.NET Core SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY *.csproj .
RUN dotnet restore

# Копируем остальные файлы проекта и выполняем сборку
COPY . .
# COPY ./Migrations ./Migrations

RUN dotnet publish -c Release -o out

# Используем образ с ASP.NET Core Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
COPY ./Data/Data.xlsx ./

ENTRYPOINT ["dotnet", "RPDSerice.dll"]
