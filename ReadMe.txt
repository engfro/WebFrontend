https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish

dotnet publish -r win-x64 
dotnet publish -r win-x64 -c release
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true 
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishReadyToRunShowWarnings=true
dotnet publish -r win-x64 -c release -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishReadyToRunShowWarnings=true -p:PublishTrimmed=true