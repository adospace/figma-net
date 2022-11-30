using FigmaNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FigmaNet.Tests;

public class FileTests
{
    [Fact]
    public async Task Should_Correctly_Parse_Medium_Sized_File()
    {
        var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.Trackizer.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();
        var jsonOptions = new JsonSerializerSettings();
        jsonOptions.Converters.Add(new NodeConverter());
        jsonOptions.Converters.Add(new PaintConverter());
        var result = JsonConvert.DeserializeObject<FigmaNet.GetFileResult>(jsonContent, jsonOptions);

        /*
CANVAS - FlowStartingPoints
CANVAS - PrototypeDevice
TEXT - LayoutVersion         
         */
    }
}
