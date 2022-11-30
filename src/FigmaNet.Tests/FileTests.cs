using FluentAssertions;
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
        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.Trackizer.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();

        FigmaSerializer serializer = new();

        GetFileResult res = serializer.Deserialize<GetFileResult>(jsonContent);
        res.Name.Should().Be("Trackizer");
    }

    [Fact]
    public async Task Should_Correctly_Parse_Large_Sized_File()
    {
        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.FixMaxDepthBug.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();

        FigmaSerializer serializer = new();

        GetFileResult res = serializer.Deserialize<GetFileResult>(jsonContent);
        res.Name.Should().Be("Social App - Free UI Kit 📱 (Community)");
    }

    [Fact]
    public async Task Should_Correctly_Deserialize_GetFileNodesResult()
    {
        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.GetFileNodeResult.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();

        FigmaSerializer serializer = new();

        GetFileNodesResult<FRAME> res = serializer.Deserialize<GetFileNodesResult<FRAME>>(jsonContent);
        
        res.Name.Should().Be("Trackizer");
        res.Nodes.Should().HaveCount(1);
        res.Nodes["32:4472"].Document.Name.Should().Be("Button/Apple");

    }

    [Fact]
    public async Task Should_Correctly_Deserialize_GetImageResult_Model()
    {
        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.GetImageResult.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();

        FigmaSerializer serializer = new();

        GetImageResult res = serializer.Deserialize<GetImageResult>(jsonContent);

        res.Error.Should().BeNull();
        res.Images.Should().NotBeNull();
        res.Images.Count.Should().Be(1);
    }

    [Fact]
    public async Task Should_Correctly_Deserialize_GetImageFillResult_Model()
    {
        using var srJson = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("FigmaNet.Tests.TestFiles.GetImageFillResult.json") ?? throw new InvalidOperationException());
        var jsonContent = await srJson.ReadToEndAsync();

        FigmaSerializer serializer = new();

        GetImageFillsResult res = serializer.Deserialize<GetImageFillsResult>(jsonContent);

        res.Status.Should().Be(200);
        res.Error.Should().Be(false);
        res.Meta.Should().NotBeNull();
        res.Meta!.Images.Count.Should().Be(20);
    }

}
