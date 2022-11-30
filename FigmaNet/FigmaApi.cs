using System;
using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FigmaNet;

public class FigmaApi
{
    public const string API_DOMAIN = "https://api.figma.com";
    public const string API_VER = "v1";

    private readonly HttpClient _httpClient;
    private readonly string? _personalAccessToken;
    private readonly string? _oAuthToken;

    public FigmaApi(HttpClient? httpClient = null, string? personalAccessToken = null,  string? oAuthToken = null)
    {
        if (personalAccessToken == null && oAuthToken == null)
        {
            throw new InvalidOperationException();
        }

        _httpClient = httpClient ?? new HttpClient();
        _personalAccessToken = personalAccessToken;
        _oAuthToken = oAuthToken;
        /*
         appendHeaders = (headers: { [x: string]: string }) => {
        if (this.personalAccessToken) headers['X-Figma-Token'] = this.personalAccessToken;
        if (this.oAuthToken) headers['Authorization'] =  `Bearer ${this.oAuthToken}`;
    }
         */

        if (_personalAccessToken != null)
        {
            _httpClient.DefaultRequestHeaders.Add("X-Figma-Token", _personalAccessToken);
        }

        if (_oAuthToken != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(_oAuthToken);
        }        
    }
    
    /*
    request: ApiRequestMethod = async <T>(url: string, opts?: { method: AxiosMethod, data: string }) => {
        const headers = {};
        this.appendHeaders(headers);

        const axiosParams: AxiosRequestConfig = {
            url,
            ...opts,
            headers,
        };

        const res = await axios(axiosParams);
        if (Math.floor(res.status / 100) !== 2) throw res.statusText;
        return res.data;
    };     
     */
    private async Task<T> Request<T>(string url)
    {
        //var response = await _httpClient.GetFromJsonAsync<T>(url);

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var res = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(res) ?? throw new InvalidOperationException();
    }

    private static string ToQueryString(Dictionary<string, string?> nvc)
        => string.Join("&", nvc
            .Where(_ => _.Value != null)
            .Select(_ => $"{HttpUtility.UrlEncode(_.Key)}={HttpUtility.UrlEncode(_.Value)}"));

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
        /** A comma separated list of plugin IDs and/or the string "shared". Any data present in the document written by those plugins will be included in the result in the `pluginData` and `sharedPluginData` properties. */
        public string? Plugin_data { get; set; }
        /** Set to returns branch metadata for the requested file */
        //branch_data ?: boolean { get; set; }
    }

    public async Task<GetFileResult> GetFile(string fileKey, GetFileOptions? options = null)
    {
        /*
        const queryParams = toQueryParams({ ...opts, ids: opts && opts.ids && opts.ids.join(',') });
        return this.request<GetFileResult>(`${API_DOMAIN}/${API_VER}/files/${fileKey}?${queryParams}`);         
        */
        return await Request<GetFileResult>($"{API_DOMAIN}/{API_VER}/files/{fileKey}?{ToQueryString(new Dictionary<string, string?>
        {
            {"version", options?.Version },
            {"ids", options?.Ids == null ? null : string.Join(",",options.Ids) },
            {"depth", options?.Depth?.ToString() },
            {"plugin_data", options?.Plugin_data },
        })}");
    }
}

//public class FileApi
//{
//    private readonly FigmaApi _figmaApi;

//    internal FileApi(FigmaApi figmaApi) 
//    {
//        _figmaApi = figmaApi;
//    }



//}


// FIGMA FILES
// -----------------------------------------------------------------

//export interface GetFileResult
//{
//    name: string,
//    lastModified: string,
//    thumbnailUrl: string,
//    version: string,
//    document: Node<'DOCUMENT'>,
//    components: { [nodeId: string]: Component
//},
//    schemaVersion: number,
//    styles: { [styleName: string]: Style },
//    mainFileKey ?: string;
//branches ?: ProjectFile[]
//}

public record GetFileResult
{
    [JsonPropertyName("name")]
    public required string Name {  get; set; }

    [JsonPropertyName("lastModified")]
    public required DateTime LastModified { get; set; }

    [JsonPropertyName("document")]
    public required Models.DOCUMENT Document { get; set; }

    [JsonPropertyName("thumbnailUrl")]
    public required string ThumbnailUrl { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("schemaVersion")]
    public required string SchemaVersion { get; set; }

    [JsonPropertyName("mainFileKey")]
    public string? MainFileKey { get; set; }
}