dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet msbuild /t:Coverage