# FigmaNet
This library allows to connect to Figma REST API endpoint and get access to your project definitions. Could be useful if you want to create plugin or tools for Figma. 

This package is used by the MauiReactor.FignaPlugin tool to automatically generate [MauiReactor](https://github.com/adospace/reactorui-maui) code from Figma projects.

# Usage

```csharp
FigmaApi api = new(personalAccessToken: <token>);

GetFileResult res = await api.GetFile(<file_id>);
```
