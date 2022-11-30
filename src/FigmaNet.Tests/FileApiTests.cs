using FluentAssertions;

namespace FigmaNet.Tests;

public class FileApiTests
{
    [Fact]
    public async Task Should_Load_Files_From_Project()
    {
        var pat = Environment.GetEnvironmentVariable("FIGMA_NET_PAT") ?? throw new InvalidOperationException();

        FigmaApi api = new(personalAccessToken: pat);

        GetFileResult res = await api.GetFile("jNO0GzgcEqCKejRPOY2QnR");
        
        res.Name.Should().Be("Trackizer");
    }

    [Fact]
    public async Task Should_Load_Single_Nodes_From_Files()
    {
        var pat = Environment.GetEnvironmentVariable("FIGMA_NET_PAT") ?? throw new InvalidOperationException();

        FigmaApi api = new(personalAccessToken: pat);

        GetFileNodesResult<FRAME> res = await api.GetFileNodes<FRAME>("LqSUXxik7wINF08GbFFT4F", ids: new[] { "32:4472" });

        res.Name.Should().Be("Trackizer");
        res.Nodes.Should().HaveCount(1);
        res.Nodes["32:4472"].Document.Name.Should().Be("Button/Apple");

    }

    [Fact]
    public async Task Should_Load_Images_From_File()
    {
        var pat = Environment.GetEnvironmentVariable("FIGMA_NET_PAT") ?? throw new InvalidOperationException();

        FigmaApi api = new(personalAccessToken: pat);

        GetImageResult res = await api.GetImage("LqSUXxik7wINF08GbFFT4F", new[] { "32:4472" }, scale: 1.0, format: GetImageFormat.Jpg);

        res.Error.Should().BeNull();
        res.Images.Should().NotBeNull();
        res.Images.Count.Should().Be(1);
    }

    [Fact]
    public async Task Should_Load_Images_Fills_From_File()
    {
        var pat = Environment.GetEnvironmentVariable("FIGMA_NET_PAT") ?? throw new InvalidOperationException();

        FigmaApi api = new(personalAccessToken: pat);

        GetImageFillsResult res = await api.GetImageFills("LqSUXxik7wINF08GbFFT4F");

        res.Status.Should().Be(200);
    }
}