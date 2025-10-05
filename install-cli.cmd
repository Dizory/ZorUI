@echo off
REM Script to install ZorUI CLI as a global .NET tool (Windows)

echo 🚀 Installing ZorUI CLI...
echo.

REM Navigate to CLI directory
cd /d "%~dp0\src\ZorUI.CLI"

REM Build and pack
echo 📦 Building CLI tool...
dotnet pack -c Release

if errorlevel 1 (
    echo ❌ Build failed!
    pause
    exit /b 1
)

REM Uninstall old version if exists
echo.
echo 🗑️ Removing old version (if exists)...
dotnet tool uninstall --global ZorUI.CLI 2>nul

REM Install new version
echo.
echo 📥 Installing CLI tool...
dotnet tool install --global --add-source .\bin\Release ZorUI.CLI

if errorlevel 0 (
    echo.
    echo ✅ ZorUI CLI installed successfully!
    echo.
    echo 📋 Available commands:
    echo   zorui new ^<template^> [options]  - Create new project
    echo   zorui list                      - List available templates
    echo   zorui info                      - Show framework info
    echo.
    echo 🎯 Quick start:
    echo   zorui new desktop --name MyApp
    echo   cd MyApp
    echo   dotnet run
    echo.
    echo 📚 For more information:
    echo   zorui --help
) else (
    echo.
    echo ❌ Installation failed!
    echo.
    echo Try:
    echo   1. Ensure .NET 8.0 SDK is installed
    echo   2. Run: dotnet --version
    echo   3. Check that %%USERPROFILE%%\.dotnet\tools is in PATH
)

echo.
pause
