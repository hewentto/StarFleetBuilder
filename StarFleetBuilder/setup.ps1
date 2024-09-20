# Check if .NET SDK is installed
if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host ".NET SDK is not installed. Please install .NET SDK 6.0+ and try again."
    exit 1
} else {
    Write-Host ".NET SDK is installed."
}

# Check if EF Core tools are installed
if (-not (Get-Command "dotnet-ef" -ErrorAction SilentlyContinue)) {
    Write-Host "Installing EF Core tools..."
    dotnet tool install --global dotnet-ef
} else {
    Write-Host "EF Core tools are already installed."
}

# Restore NuGet packages
Write-Host "Restoring NuGet packages..."
dotnet restore

# Apply migrations
Write-Host "Applying database migrations..."
dotnet ef database update

# Build the project
Write-Host "Building the project..."
dotnet build

# Output Complete and user can now run the project
Write-Host "Setup complete. You can now run the project."
