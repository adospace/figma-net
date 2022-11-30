# FigmaNet
This library allows to connect to Figma REST API endpoint and get access to your project definitions. Could be useful if you want to create plugin or tools for Figma. 

This package is used by the MauiReactor.FignaPlugin tool to automatically generate [MauiReactor](https://github.com/adospace/reactorui-maui) code from Figma projects.

[![Build status](https://ci.appveyor.com/api/projects/status/kwlmsw628u1i1m2x?svg=true)](https://ci.appveyor.com/project/adospace/figma-net) [![Nuget](https://img.shields.io/nuget/v/FigmaNet)](https://www.nuget.org/packages/FigmaNet) 

## Usage

- Obtain your personal access token from https://www.figma.com/developers/api

- Install the Nuget package:

```
dotnet add package FigmaNet
```

 - Get the file definition
```csharp
FigmaApi api = new(personalAccessToken: <token>);

GetFileResult res = await api.GetFile(<file_id>);
```

where <file_id> is visible from the url of your project when you open it in Figma (i.e. 'https://www.figma.com/file/<file_id>/.....')
