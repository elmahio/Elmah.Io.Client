# Elmah.Io.Client

[![Build status](https://github.com/elmahio/Elmah.Io.AspNetCore/workflows/build/badge.svg)](https://github.com/elmahio/Elmah.Io.AspNetCore/actions?query=workflow%3Abuild)
[![NuGet](https://img.shields.io/nuget/vpre/Elmah.Io.Client.svg)](https://www.nuget.org/packages/Elmah.Io.Client)

Raw client for communicating with the [elmah.io API](https://elmah.io/api/v3).

## Usage

To start logging, create a new instance of the `ElmahioAPI` class:

```csharp
client = ElmahioAPI.Create(apiKey);
```

where `apiKey` is your API key found on your profile page at elmah.io. Make sure to share this instance as a singleton.

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

Parts of this client is auto generated using [NSwag](https://github.com/RicoSuter/NSwag). NSwag is an Open Source tool that can generate clients for many languages (like C#) from Swagger/OpenAPI 2.0 and 3.0 specs.

To generate a client from the most reason API specs, you first need to install NSwag. We recommend that you use the NPM package which can be installed with this:

```bash
npm install nswag -g
```

With this NSwag CLI tool, you can execute the following command in this folder to generate the newest client.

```bash
nswag run
```