Write-Host "Executing dotnet pack"

$Version = git describe --tags
dotnet pack src/SoundCloud.Api/SoundCloud.Api.csproj --force -c Release -p:Version=$Version

# For Prerelase packages:
# dotnet pack src/SoundCloud.Api/SoundCloud.Api.csproj --force -c Release -p:Version=3.0.0 -p:PackageVersion=3.0.0-alpha01 -p:FileVersion=3.0.0-alpha01 -p:InformationalVersion=3.0.0-alpha01
