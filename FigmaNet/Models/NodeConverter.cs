using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FigmaNet.Models;

public class NodeConverter : GenericEnumTypeConverter<NodeTypes, Node>
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

public class PaintConverter : GenericEnumTypeConverter<PaintType, Paint>
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

public abstract class GenericEnumTypeConverter<TEnum, T> : JsonConverter where TEnum : struct
{
    public GenericEnumTypeConverter()
    {

    }

    protected abstract Type GetTypeForEnumValue(TEnum value);

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

    private static HashSet<string> _missingProps = new HashSet<string>();

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        // Load JObject from stream
        JObject jObject = JObject.Load(reader);

        if (!Enum.TryParse<TEnum>(jObject["type"]?.Value<string>(), out var nodeType))
        {
            throw new InvalidOperationException($"Node type not found: {jObject["type"]?.Value<string>() ?? "null"}");
        }

        var node = jObject.ToObject(GetTypeForEnumValue(nodeType)).EnsureNotNull();

        serializer.Populate(jObject.CreateReader(), node);

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
