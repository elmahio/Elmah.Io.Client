AutoRest.exe -Namespace Elmah.Io.Client -AddCredentials -CodeGenerator CSharp -OutputDirectory src\Elmah.Io.Client -Input https://api.elmah.io:443/swagger/docs/v3

dotnet build Elmah.Io.Client