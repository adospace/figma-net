namespace FigmaNet.Tests;

public class FileApiTests
{
    [Fact]
    public async Task Should_Load_Files_From_Project()
    {
        var pat = Environment.GetEnvironmentVariable("FIGMA_NET_PAT") ?? throw new InvalidOperationException();

        FigmaApi api = new(personalAccessToken: pat);

        var res = await api.GetFile("LqSUXxik7wINF08GbFFT4F");
    }
}