#!/bin/bash

# Script to install ZorUI CLI as a global .NET tool

echo "🚀 Installing ZorUI CLI..."
echo ""

# Navigate to CLI directory
cd "$(dirname "$0")/src/ZorUI.CLI" || exit

# Build and pack
echo "📦 Building CLI tool..."
dotnet pack -c Release

if [ $? -ne 0 ]; then
    echo "❌ Build failed!"
    exit 1
fi

# Uninstall old version if exists
echo ""
echo "🗑️ Removing old version (if exists)..."
dotnet tool uninstall --global ZorUI.CLI 2>/dev/null

# Install new version
echo ""
echo "📥 Installing CLI tool..."
dotnet tool install --global --add-source ./bin/Release ZorUI.CLI

if [ $? -eq 0 ]; then
    echo ""
    echo "✅ ZorUI CLI installed successfully!"
    echo ""
    echo "📋 Available commands:"
    echo "  zorui new <template> [options]  - Create new project"
    echo "  zorui list                      - List available templates"
    echo "  zorui info                      - Show framework info"
    echo ""
    echo "🎯 Quick start:"
    echo "  zorui new desktop --name MyApp"
    echo "  cd MyApp"
    echo "  dotnet run"
    echo ""
    echo "📚 For more information:"
    echo "  zorui --help"
else
    echo ""
    echo "❌ Installation failed!"
    echo ""
    echo "Try:"
    echo "  1. Ensure .NET 8.0 SDK is installed"
    echo "  2. Run: dotnet --version"
    echo "  3. Check that ~/.dotnet/tools is in PATH"
    exit 1
fi
