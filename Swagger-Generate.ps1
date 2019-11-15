Push-Location src

# Requires AutoRest 2.0.4263 with autorest.csharp 2.2.67 for now

AutoRest -Namespace Elmah.Io.Client -AddCredentials -CodeGenerator CSharp -OutputDirectory Elmah.Io.Client -Input https://api.elmah.io:443/swagger/docs/v3

dotnet build Elmah.Io.Client

Pop-Location