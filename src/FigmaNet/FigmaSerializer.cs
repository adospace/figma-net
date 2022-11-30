using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace FigmaNet;

public class FigmaSerializer
{
    private readonly JsonSerializer _jsonSerializer;

    public FigmaSerializer()
    {
        var jsonOptions = new JsonSerializerSettings { MaxDepth = 128 };
        jsonOptions.Converters.Add(new NodeConverter());
        jsonOptions.Converters.Add(new PaintConverter());

        _jsonSerializer = JsonSerializer.Create(jsonOptions); 
    }

    [return: NotNull]
    public T Deserialize<T>(string jsonContent)
    {
        using var sr = new StringReader(jsonContent);
        using var jsonTextReader = new JsonTextReader(sr);            
        return _jsonSerializer.Deserialize<T>(jsonTextReader).EnsureNotNull();
    }

    [return: NotNull]
    public T Deserialize<T>(Stream jsonStream)
    {
        using var sr = new StreamReader(jsonStream);
        using var jsonTextReader = new JsonTextReader(sr);
        return _jsonSerializer.Deserialize<T>(jsonTextReader).EnsureNotNull();
    }
}

file class NodeConverter : GenericEnumTypeConverter<NodeTypes, Node>
{
    private static readonly Dictionary<NodeTypes, Type> _nodeTypeMapper = new()
    {
        { NodeTypes.DOCUMENT, typeof(DOCUMENT) },
        { NodeTypes.CANVAS, typeof(CANVAS) },
        { NodeTypes.FRAME, typeof(FRAME) },
        { NodeTypes.GROUP, typeof(GROUP) },
        { NodeTypes.VECTOR, typeof(VECTOR) },
        { NodeTypes.BOOLEAN, typeof(BOOLEAN) },
        { NodeTypes.BOOLEAN_OPERATION, typeof(BOOLEAN_OPERATION) },
        { NodeTypes.STAR, typeof(STAR) },
        { NodeTypes.LINE, typeof(LINE) },
        { NodeTypes.ELLIPSE, typeof(ELLIPSE) },
        { NodeTypes.REGULAR_POLYGON, typeof(REGULAR_POLYGON) },
        { NodeTypes.RECTANGLE, typeof(RECTANGLE) },
        { NodeTypes.TEXT, typeof(TEXT) },
        { NodeTypes.SLICE, typeof(SLICE) },
        { NodeTypes.COMPONENT, typeof(COMPONENT) },
        { NodeTypes.COMPONENT_SET, typeof(COMPONENT_SET) },
        { NodeTypes.INSTANCE, typeof(INSTANCE) },
    };

    protected override Type GetTypeForEnumValue(NodeTypes value)
        => _nodeTypeMapper[value];
}

file class PaintConverter : GenericEnumTypeConverter<PaintType, Paint>
{
    private static readonly Dictionary<PaintType, Type> _nodeTypeMapper = new()
    {
        { PaintType.SOLID, typeof(PaintSolid) },
        { PaintType.IMAGE, typeof(PaintImage) },
        { PaintType.GRADIENT_ANGULAR, typeof(PaintGradient) },
        { PaintType.GRADIENT_DIAMOND, typeof(PaintGradient) },
        { PaintType.GRADIENT_LINEAR, typeof(PaintGradient) },
        { PaintType.GRADIENT_RADIAL, typeof(PaintGradient) },
        { PaintType.EMOJI, typeof(Paint) },
    };

    protected override Type GetTypeForEnumValue(PaintType value)
        => _nodeTypeMapper[value];
}

file abstract class GenericEnumTypeConverter<TEnum, T> : JsonConverter where TEnum : struct
{
    public GenericEnumTypeConverter()
    {

    }

    protected abstract Type GetTypeForEnumValue(TEnum value);

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

#if DEBUG
    private static readonly HashSet<string> _missingProps = new();
#endif

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        // Load JObject from stream
        JObject jObject = JObject.Load(reader);

        System.Diagnostics.Debug.WriteLine($"type={jObject["type"]?.Value<string>()}");

        if (!Enum.TryParse<TEnum>(jObject["type"]?.Value<string>(), out var nodeType))
        {
            throw new InvalidOperationException($"Node type not found: {jObject["type"]?.Value<string>() ?? "null"}");
        }

        //System.Diagnostics.Debug.WriteLine(nodeType);

        var node = Activator.CreateInstance(GetTypeForEnumValue(nodeType)).EnsureNotNull();

        using var jsonReader = jObject.CreateReader();
        jsonReader.MaxDepth = serializer.MaxDepth;
        serializer.Populate(jsonReader, node);

#if DEBUG
        //if (node is Node nodeTyped && nodeTyped.Id == "42:2821")
        {
            var nodeTypeProps = node.GetType().GetProperties().Select(_ => _.Name);
            foreach (var item in jObject)
            {
                var propertyName = char.ToUpper(item.Key[0]) + item.Key.Substring(1);
                if (!nodeTypeProps.Contains(propertyName))
                {
                    //node = node;
                    var msg = $"{node.GetType().Name} - {propertyName}";
                    if (!_missingProps.Contains(msg))
                    {
                        _missingProps.Add(msg);
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                }
            }
        }
#endif
        //if (node is Node nodeTyped && nodeTyped.AdditionalData?.Count > 0)
        //{
        //    node = node;
        //}

        return node;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {

    }

    public override bool CanWrite => false;
}
