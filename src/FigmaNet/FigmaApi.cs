using System.Text.Json.Serialization;
using System.Web;

namespace FigmaNet;

public class FigmaApi
{
    public const string API_DOMAIN = "https://api.figma.com";
    public const string API_VER = "v1";

    private readonly HttpClient _httpClient;
    private readonly string? _personalAccessToken;
    private readonly string? _oAuthToken;
    private readonly FigmaSerializer _serializer = new();

    public FigmaApi(HttpClient? httpClient = null, string? personalAccessToken = null,  string? oAuthToken = null)
    {
        if (personalAccessToken == null && oAuthToken == null)
        {
            throw new InvalidOperationException();
        }

        _httpClient = httpClient ?? new HttpClient();
        _personalAccessToken = personalAccessToken;
        _oAuthToken = oAuthToken;

        if (_personalAccessToken != null)
        {
            _httpClient.DefaultRequestHeaders.Add("X-Figma-Token", _personalAccessToken);
        }

        if (_oAuthToken != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(_oAuthToken);
        }        
    }

    private async Task<T> Request<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

#if DEBUG
        var jsonContent = await response.Content.ReadAsStringAsync();

        return _serializer.Deserialize<T>(jsonContent);
#else
        var stream = await response.Content.ReadAsStreamAsync();

        return _serializer.Deserialize<T>(stream);
#endif
    }

    private static string ToQueryString(Dictionary<string, string?> nvc)
        => string.Join("&", nvc
            .Where(_ => _.Value != null)
            .Select(_ => $"{HttpUtility.UrlEncode(_.Key)}={HttpUtility.UrlEncode(_.Value)}"));

    public async Task<GetFileResult> GetFile(string fileKey, GetFileOptions? options = null)
    {
        return await Request<GetFileResult>($"{API_DOMAIN}/{API_VER}/files/{fileKey}?{ToQueryString(new Dictionary<string, string?>
        {
            {"version", options?.Version },
            {"ids", options?.Ids == null ? null : string.Join(",",options.Ids) },
            {"depth", options?.Depth?.ToString() },
            {"geometry", options?.Geometry?.ToString() },
            {"plugin_data", options?.Plugin_data },
            {"branch_data", options?.BranchData.ToString() },
        })}");
    }

    public async Task<GetFileNodesResult<T>> GetFileNodes<T>(string fileKey, string[] ids, GetFileNodesOptions? options = null) where T : Node
    {
        return await Request<GetFileNodesResult<T>>($"{API_DOMAIN}/{API_VER}/files/{fileKey}/nodes?{ToQueryString(new Dictionary<string, string?>
        {
            {"ids", string.Join(",", ids) },
            {"version", options?.Version },
            {"depth", options?.Depth?.ToString() },
            {"geometry", options?.Geometry?.ToString() },
            {"plugin_data", options?.Plugin_data },
        })}");
    }

    public async Task<GetImageResult> GetImage(string fileKey, string[] ids, double scale, GetImageFormat format, GetImageOptions? options = null)
    {
        scale = Math.Clamp(scale, 0.01, 4.0);
        return await Request<GetImageResult>($"{API_DOMAIN}/{API_VER}/images/{fileKey}?{ToQueryString(new Dictionary<string, string?>
        {
            {"ids", string.Join(",", ids) },
            {"scale", scale.ToString() },
            {"format", format.ToString().ToLowerInvariant() },
            {"svg_include_id", options?.SvgIncludeId?.ToString() },
            {"svg_simplify_stroke", options?.SvgSimplifyStroke?.ToString() },
            {"use_absolute_bounds", options?.UseAbsoluteBounds?.ToString() },
            {"version", options?.Version },
        })}");
    }

    public async Task<GetImageFillsResult> GetImageFills(string fileKey)
    {
        return await Request<GetImageFillsResult>($"{API_DOMAIN}/{API_VER}/files/{fileKey}/images");
    }
}

// FIGMA FILES
// -----------------------------------------------------------------

// GetFile
public class GetFileOptions
{
    /** A specific version ID to get. Omitting this will get the current version of the file */
    public string? Version { get; set; }
    /** If specified, only a subset of the document will be returned corresponding to the nodes listed, their children, and everything between the root node and the listed nodes */
    public string[]? Ids{ get; set; }
    /** Positive integer representing how deep into the document tree to traverse. For example, setting this to 1 returns only Pages, setting it to 2 returns Pages and all top level objects on each page. Not setting this parameter returns all nodes */
    public int? Depth { get; set; }
    /** Set to "paths" to export vector data */
    //geometry?: 'paths' { get; set; }
    public string? Geometry {  get; set; }
    /** A comma separated list of plugin IDs and/or the string "shared". Any data present in the document written by those plugins will be included in the result in the `pluginData` and `sharedPluginData` properties. */
    public string? Plugin_data { get; set; }
    /** Set to returns branch metadata for the requested file */
    public bool BranchData { get; set; }
}

public record GetFileResult
{
    [JsonPropertyName("name")]
    public required string Name {  get; set; }

    [JsonPropertyName("lastModified")]
    public required DateTime LastModified { get; set; }

    [JsonPropertyName("document")]
    public required DOCUMENT Document { get; set; }

    [JsonPropertyName("thumbnailUrl")]
    public required string ThumbnailUrl { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("schemaVersion")]
    public required string SchemaVersion { get; set; }

    [JsonPropertyName("mainFileKey")]
    public string? MainFileKey { get; set; }
}

// GetFileNodes<T>
public class GetFileNodesOptions
{
    /** A specific version ID to get. Omitting this will get the current version of the file */
    public string? Version { get; set; }
    /** Positive integer representing how deep into the document tree to traverse. For example, setting this to 1 returns only Pages, setting it to 2 returns Pages and all top level objects on each page. Not setting this parameter returns all nodes */
    public int? Depth { get; set; }
    /** Set to "paths" to export vector data */
    //geometry?: 'paths' { get; set; }
    public string? Geometry { get; set; }
    /** A comma separated list of plugin IDs and/or the string "shared". Any data present in the document written by those plugins will be included in the result in the `pluginData` and `sharedPluginData` properties. */
    public string? Plugin_data { get; set; }
}

public record GetFileNodesResultNode<T> where T : Node
{
    [JsonPropertyName("document")]
    public required T Document { get; set; }

    [JsonPropertyName("components")]
    public required Dictionary<string, Component> Components { get; set; }

    [JsonPropertyName("schemaVersion")]
    public required int SchemaVersion { get; set; }

    [JsonPropertyName("styles")]
    public required Dictionary<string, Style> Styles { get; set; }
}

public record GetFileNodesResult<T> where T : Node
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("lastModified")]
    public required DateTime LastModified { get; set; }

    [JsonPropertyName("thumbnailUrl")]
    public required string ThumbnailUrl { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("err")]
    public string? Error { get; set; }

    [JsonPropertyName("nodes")]
    public required Dictionary<string, GetFileNodesResultNode<T>> Nodes { get; set; }
}

// GetImage
public class GetImageOptions
{
    /** A specific version ID to get. Omitting this will get the current version of the file */
    public string? Version { get; set; }
    /** Whether to include id attributes for all SVG elements. `Default: false` */
    public bool? SvgIncludeId { get; set; }
    /** Use the full dimensions of the node regardless of whether or not it is cropped or the space around it is empty. Use this to export text nodes without cropping. `Default: false` */
    public bool? SvgSimplifyStroke { get; set; }
    /** A specific version ID to get. Omitting this will get the current version of the file */
    public bool? UseAbsoluteBounds { get; set; }
}

public enum GetImageFormat
{
    Jpg,
    Png,
    Svg,
    Pdf,
}

public record GetImageMeta
{
    [JsonPropertyName("images")]
    public required Dictionary<string, string?> Images { get; set; }
}

public record GetImageResult
{
    [JsonPropertyName("err")]
    public string? Error { get; set; }

    [JsonPropertyName("images")]
    public required Dictionary<string, string?> Images { get; set; }
}

// GetImageFills
public record GetImageFillsResult
{
    [JsonPropertyName("err")]
    public bool? Error { get; set; }

    [JsonPropertyName("meta")]
    public GetImageMeta? Meta { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }
}