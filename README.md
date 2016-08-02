# Elmah.Io.Client

Raw client for communicating with the [elmah.io API](https://elmah.io/api/v3).

## Usage

To start logging, create a new instance of the `ElmahioAPI` class:

```csharp
client = ElmahioAPI.Create(apiKey);
```

where `apiKey` is your API key found on your profile page at elmah.io.

### Logging

Log messages either through the `log` method:

```csharp
client.Messages.Log(logId, new Exception(), Severity.Error, "This is a bug");
```

or through one of the helpers:

```csharp
client.Messages.Debug(logId, "A debug message");
client.Messages.Fatal(logId, exception, "This is a fatal bug");
```

### Creating logs

Logs are containers for log messages. To create a new log for a new application, microservice or similar, use the `Create` method:

```csharp
client.Logs.Create(new CreateLog("My log"));
```

## Developing

Parts of this client is auto generated using [AutoRest](https://github.com/Azure/autorest). AutoRest is a brilliant code generation tools by Microsoft, that takes swagger input and generates client libraries for a number of different programming languages (like C#).

To generate a client of the most recent code, execute the following in the root folder:

```
AutoRest.exe -Input https://api.elmah.io:443/swagger/docs/v3 -AddCredentials true -Namespace Elmah.Io.Client -OutputDirectory Elmah.Io.Client
```