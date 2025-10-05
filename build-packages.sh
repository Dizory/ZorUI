#!/bin/bash

# Скрипт для сборки NuGet пакетов ZorUI

echo "🏗️ Сборка NuGet пакетов ZorUI..."

# Очистка предыдущих сборок
echo "🧹 Очистка старых пакетов..."
rm -rf ./nupkgs
mkdir -p ./nupkgs

# Сборка проектов
echo "📦 Сборка проектов..."

dotnet build src/ZorUI.Core/ZorUI.Core.csproj -c Release
dotnet build src/ZorUI.Styling/ZorUI.Styling.csproj -c Release
dotnet build src/ZorUI.Components/ZorUI.Components.csproj -c Release
dotnet build src/ZorUI.Rendering/ZorUI.Rendering.csproj -c Release

# Создание пакетов
echo "📦 Создание NuGet пакетов..."

dotnet pack src/ZorUI.Core/ZorUI.Core.csproj -c Release -o ./nupkgs
dotnet pack src/ZorUI.Styling/ZorUI.Styling.csproj -c Release -o ./nupkgs
dotnet pack src/ZorUI.Components/ZorUI.Components.csproj -c Release -o ./nupkgs
dotnet pack src/ZorUI.Rendering/ZorUI.Rendering.csproj -c Release -o ./nupkgs

echo "✅ Пакеты созданы в папке ./nupkgs/"
ls -lh ./nupkgs/

echo ""
echo "📋 Для использования локально:"
echo "   dotnet add package ZorUI.Core --source ./nupkgs"
echo ""
echo "📋 Для публикации на NuGet.org:"
echo "   dotnet nuget push ./nupkgs/*.nupkg --source https://api.nuget.org/v3/index.json --api-key YOUR_API_KEY"
