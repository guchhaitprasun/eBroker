# Clean Project
dotnet clean --configuration Release

# Build With Release Configuration
dotnet build --configuration Release

# Test the codebase and generate code coverage xml 
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

# Generate code coverage report
reportgenerator -reports:".\eBroker.Tests\coverage.cobertura.xml" -targetdir:".\eBroker.Tests\CoverageReport" -reporttypes:Html
start-sleep 1

# Open report in default browser
Invoke-Item .\eBroker.Tests\CoverageReport\index.html
