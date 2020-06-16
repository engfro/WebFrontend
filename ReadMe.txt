https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish

dotnet publish -r win-x64 
dotnet publish -r win-x64 -c release

Seems to result in 404 Not found for files under the wwwroot when hosted as a windows service
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true

dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true 
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishReadyToRunShowWarnings=true
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishReadyToRunShowWarnings=true -p:PublishTrimmed=true
