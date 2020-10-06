https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish

# Use this one
dotnet publish -r win-x64 

# Create windows service
New-Service -Name "WebFrontend windows service" -BinaryPathName <insert-path-here>\WebFrontend\bin\Debug\netcoreapp3.1\win-x64\publish\WebFrontend.exe
https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services