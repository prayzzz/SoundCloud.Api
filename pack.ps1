Write-Host "Executing dotnet pack"

$Version = git describe --tags
dotnet pack src/SoundCloud.Api/SoundCloud.Api.csproj --force -c Release -p:Version=$Version