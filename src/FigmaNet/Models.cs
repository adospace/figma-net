using System.Runtime.Serialization;
using System.Text.Json.Serialization;

/*
 * Missing properties
CANVAS - FlowStartingPoints
CANVAS - PrototypeDevice
TEXT - LayoutVersion         
 */

namespace FigmaNet;

/*A string enum with value, describing the end caps of vector paths. */
public enum StrokeCap
{
    NONE,
    ROUND,
    SQUARE,
    LINE_ARROW,
    TRIANGLE_ARROW,
}
/*Where stroke is drawn relative to the vector outline as a string enum */
public enum StrokeAlign
{
    INSIDE,
    OUTSIDE,
    CENTER,
}
/*A string enum with value, describing how corners in vector paths are rendered. */
public enum StrokeJoin
{
    MITER,
    BEVEL,
    ROUND,
}
public enum ImageType
{
    JPG,
    PNG,
    SVG,
    PDF,
}
/*A string enum with value, indicating the type of boolean operation applied */
public enum BooleanOperationType
{
    UNION,
    INTERSECT,
    SUBTRACT,
    EXCLUDE,
}
/*Text casing applied to the node, default is the original casing */
public enum TextCase
{
    ORIGINAL,
    UPPER,
    LOWER,
    TITLE,
    SMALL_CAPS,
    SMALL_CAPS_FORCED,
}
/*Text decoration applied to the node */
public enum TextDecoration
{
    NONE,
    STRIKETHROUGH,
    UNDERLINE,
}
/*Dimensions along which text will auto resize, default is that the text does not auto-resize. */
public enum TextAutoResize
{
    NONE,
    HEIGHT,
    WIDTH_AND_HEIGHT,
    TRUNCATE,
}
/*The unit of the line height value specified by the user. */
public enum LineHeightUnit
{
    PIXELS,
    [EnumMember(Value = "FONT_SIZE_%")]
    FONT_SIZE_PERC,
    [EnumMember(Value = "INTRINSIC_%")]
    INTRINSIC_PERC,
}

public enum ConstrainType
{
    /*Scale by value */
    SCALE,
    /*Scale proportionally and set width to value */
    WIDTH,
    /*Scale proportionally and set width to value */
    HEIGHT,
}


/*Format and size to export an asset at */
public record ExportSetting
{
    /*File suffix to append to all filenames */
    [JsonPropertyName("suffix")]
    public required string Suffix { get; set; }
    /*Image type string enum that supports values "JPG" "PNG" "SVG" and "PDF" */
    [JsonPropertyName("format")]
    public required ImageType Format { get; set; }
    /*Constraint that determines sizing of exported asset */
    [JsonPropertyName("constraint")]
    public required Constrain Constraint { get; set; }
}
/*Sizing constraint for exports */
public record Constrain
{
    /** Type of constraint to apply; string enum with potential values below * "SCALE": Scale by value * "WIDTH": Scale proportionally and set width to value * "HEIGHT": Scale proportionally and set height to value */
    [JsonPropertyName("type")]
    public required ConstrainType Type { get; set; }
    /*See type property for effect of this field */
    [JsonPropertyName("value")]
    public required double Value { get; set; }
}
/*A rectangle that expresses a bounding box in absolute coordinates */
public record Rectangle
{
    /*X coordinate of top left corner of the rectangle */
    [JsonPropertyName("x")]
    public double? X { get; set; }
    /*Y coordinate of top left corner of the rectangle */
    [JsonPropertyName("y")]
    public double? Y { get; set; }
    /*Width of the rectangle */
    [JsonPropertyName("width")]
    public double? Width { get; set; }
    /*Height of the rectangle */
    [JsonPropertyName("height")]
    public double? Height { get; set; }
}

/*Layout constraint relative to containing Frame */
public record LayoutConstraint
{
    /** Vertical constraint as an enum * "TOP": Node is laid out relative to top of the containing frame * "BOTTOM": Node is laid out relative to bottom of the containing frame * "CENTER": Node is vertically centered relative to containing frame * "TOP_BOTTOM": Both top and bottom of node are constrained relative to containing frame (node stretches with frame) * "SCALE": Node scales vertically with containing frame */
    [JsonPropertyName("vertical")]
    public required LayoutConstraintVertical Vertical { get; set; }
    /** Horizontal constraint as an enum * "LEFT": Node is laid out relative to left of the containing frame * "RIGHT": Node is laid out relative to right of the containing frame * "CENTER": Node is horizontally centered relative to containing frame * "LEFT_RIGHT": Both left and right of node are constrained relative to containing frame (node stretches with frame) * "SCALE": Node scales horizontally with containing frame */
    [JsonPropertyName("horizontal")]
    public required LayoutConstraintHorizontal Horizontal { get; set; }
}

/** This type is a string enum with the following possible values * Normal blends: * "PASS_THROUGH" (Only applicable to objects with children) * "NORMAL" * *Darken: *"DARKEN" * "MULTIPLY" * "LINEAR_BURN" * "COLOR_BURN" * *Lighten: *"LIGHTEN" * "SCREEN" * "LINEAR_DODGE" * "COLOR_DODGE" * *Contrast: *"OVERLAY" * "SOFT_LIGHT" * "HARD_LIGHT" * *Inversion: *"DIFFERENCE" * "EXCLUSION" * *Component: *"HUE" * "SATURATION" * "COLOR" * "LUMINOSITY" */
public enum BlendMode
{
    /*(Only applicable to objects with children) */
    PASS_THROUGH,
    /*(Only applicable to objects with children) */
    NORMAL,
    /*Darken */
    DARKEN,
    MULTIPLY,
    LINEAR_BURN,
    COLOR_BURN,
    /*Lighten */
    LIGHTEN,
    SCREEN,
    LINEAR_DODGE,
    COLOR_DODGE,
    /*Contrast */
    OVERLAY,
    SOFT_LIGHT,
    HARD_LIGHT,
    /*Inversion */
    DIFFERENCE,
    EXCLUSION,
    /*Component */
    HUE,
    SATURATION,
    COLOR,
    LUMINOSITY,
}
/** Enum describing animation easing curves * This type is a string enum with the following possible values * "EASE_IN": Ease in with an animation curve similar to CSS ease-in. * "EASE_OUT": Ease out with an animation curve similar to CSS ease-out. * "EASE_IN_AND_OUT": Ease in and then out with an animation curve similar to CSS ease-in-out. * "LINEAR": No easing, similar to CSS linear. */
public enum EasingType
{
    /*Ease in with an animation curve similar to CSS ease-in. */
    EASE_IN,
    /*Ease out with an animation curve similar to CSS ease-out. */
    EASE_OUT,
    /*Ease in and then out with an animation curve similar to CSS ease-in-out. */
    EASE_IN_AND_OUT,
    /*No easing, similar to CSS linear. */
    LINEAR,
}
public enum LayoutConstraintVertical
{
    TOP,
    BOTTOM,
    CENTER,
    TOP_BOTTOM,
    SCALE,
}
public enum LayoutConstraintHorizontal
{
    LEFT,
    RIGHT,
    CENTER,
    LEFT_RIGHT,
    SCALE,
}

public enum LayoutAlign
{
    /*Determines if the layer should stretch along the parent’s counter axis. This property is only provided for direct children of auto-layout frames. */
    INHERIT,
    STRETCH,
    /*In horizontal auto-layout frames, "MIN" and "MAX" correspond to "TOP" and "BOTTOM". In vertical auto-layout frames, "MIN" and "MAX" correspond to "LEFT" and "RIGHT". */
    MIN,
    CENTER,
    MAX,
}

public enum LayoutGridPattern
{
    COLUMNS,
    ROWS,
    GRID,
}

public enum LayoutGridAlignment
{
    MIN,
    MAX,
    CENTER,
    STRETCH,
}

public enum PaintType
{
    SOLID,
    GRADIENT_LINEAR,
    GRADIENT_RADIAL,
    GRADIENT_ANGULAR,
    GRADIENT_DIAMOND,
    IMAGE,
    EMOJI,
}
public enum PaintSolidScaleMode
{
    FILL,
    FIT,
    TILE,
    STRETCH,
}

public enum NodeTypes
{
    /*The root node */
    DOCUMENT,
    /*Represents a single page */
    CANVAS,
    /*A node of fixed size containing other nodes */
    FRAME,
    /*A logical grouping of nodes */
    GROUP,
    /*A vector network, consisting of vertices and edges */
    VECTOR,
    /*A group that has a boolean operation applied to it */
    BOOLEAN,
    BOOLEAN_OPERATION,
    /*A regular star shape */
    STAR,
    /*A straight line */
    LINE,
    /*An ellipse */
    ELLIPSE,
    /*A regular n-sided polygon */
    REGULAR_POLYGON,
    /*A rectangle */
    RECTANGLE,
    /*A text box */
    TEXT,
    /*A rectangular region of the canvas that can be exported */
    SLICE,
    /*A node that can have instances created of it that share the same properties */
    COMPONENT,
    /*A node that can have instances created of it that share the same properties */
    COMPONENT_SET,
    /*An instance of a component, changes to the component result in the same changes applied to the instance */
    INSTANCE,
}

public record Node
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("visible")]
    public required bool Visible { get; set; }
    [JsonPropertyName("type")]
    public required NodeTypes Type { get; set; }
    [JsonPropertyName("pluginData")]
    public required object PluginData { get; set; }
    [JsonPropertyName("sharedPluginData")]
    public required object SharedPluginData { get; set; }
    [JsonPropertyName("isFixed")]
    public bool? IsFixed { get; set; }


}

//Data on the frame a component resides in
public record FrameInfo
{
    //Id of the frame node within the figma file
    [JsonPropertyName("nodeId")]
    public required string NodeId { get; set; }
    //Name of the frame
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    //Background color of the frame
    [JsonPropertyName("backgroundColor")]
    public required string BackgroundColor { get; set; }
    //Id of the frame's residing page
    [JsonPropertyName("pageId")]
    public required string PageId { get; set; }
    //Name of the frame's residing page
    [JsonPropertyName("pageName")]
    public required string PageName { get; set; }
}

/*Data on the "containingStateGroup" a component resides in */
/*Notice: at the moment is not documented in the REST API documentation. I have raised the issue * (https://forum.figma.com/t/missing-containingstategroup-parameter-in-documentation-for-frameinfo/2558) * and filed a bug with the support but no one replied. From what I understand this extra parameters are * added when a component is a variant within a component_set (the name/nodeId are of the parent component_set) */
public record ContainingStateGroup
{
    /*Name of the element's residing "state group" (likely a component_set) */
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    /*Id of the element's residing "state group" (likely a component_set) */
    [JsonPropertyName("nodeId")]
    public required string NodeId { get; set; }
}

/*An arrangement of published UI elements that can be instantiated across figma files */
public record Component
{
    /*The key of the component */
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    /*The name of the component */
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    /*The description of the component as entered in the editor */
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    /*The ID of the component set if the component belongs to one */
    [JsonPropertyName("componentSetId")]
    public string? ComponentSetId { get; set; }
    /*The documentation links for this component */
    [JsonPropertyName("documentationLinks")]
    public required DocumentationLinks[] DocumentationLinks { get; set; }
}

/*Guides to align and place objects within a frame */
public record LayoutGrid
{
    /** Orientation of the grid as a string enum * "COLUMNS": Vertical grid * "ROWS": Horizontal grid *"GRID": Square grid */
    [JsonPropertyName("pattern")]
    public required LayoutGridPattern Pattern { get; set; }
    /*Width of column grid or height of row grid or square grid spacing */
    [JsonPropertyName("sectionSize")]
    public required double SectionSize { get; set; }
    /*Is the grid currently visible? */
    [JsonPropertyName("visible")]
    public required bool Visible { get; set; }
    /*Color of the grid */
    [JsonPropertyName("color")]
    public required Color Color { get; set; }
    /** Positioning of grid as a string enum * "MIN": Grid starts at the left or top of the frame * "MAX": Grid starts at the right or bottom of the frame * "CENTER": Grid is center aligned */
    [JsonPropertyName("alignment")]
    public required LayoutGridAlignment Alignment { get; set; }
    /*Spacing in between columns and rows */
    [JsonPropertyName("gutterSize")]
    public required double GutterSize { get; set; }
    /*Spacing before the first column or row */
    [JsonPropertyName("offset")]
    public required double Offset { get; set; }
    /*Number of columns or rows */
    [JsonPropertyName("count")]
    public required int Count { get; set; }
}

public enum AxisSizingMode
{
    FIXED,
    AUTO,
}

/*Represents a link to documentation for a component. */
public record DocumentationLinks
{
    /*Should be a valid URI (e.g. https://www.figma.com). */
    [JsonPropertyName("uri")]
    public required string Uri { get; set; }
}

/** NOT DOCUMENTED * * Data on component's containing page if component resides in a multi-page file */
public record PageInfo
{
}

public interface INodeContainer
{
    Node[] Children { get; }
}

/*The root node */
public record DOCUMENT : Node, INodeContainer
{
    /*An array of canvases attached to the document */
    [JsonPropertyName("children")]
    public required Node[] Children { get; set; }
    /*Undocumented SCROLL|...*/
    [JsonPropertyName("scrollBehavior")]
    public required string ScrollBehavior { get; set; }
}

/*An RGBA color */
public record Color
{
    /*Red channel value between 0 and 1 */
    [JsonPropertyName("r")]
    public required double R { get; set; }
    /*Green channel value between 0 and 1 */
    [JsonPropertyName("g")]
    public required double G { get; set; }
    /*Blue channel value between 0 and 1 */
    [JsonPropertyName("b")]
    public required double B { get; set; }
    /*Alpha channel value between 0 and 1 */
    [JsonPropertyName("a")]
    public required double A { get; set; }
}

public enum EffectType
{
    INNER_SHADOW,
    DROP_SHADOW,
    LAYER_BLUR,
    BACKGROUND_BLUR,
}

public record Effect
{
    [JsonPropertyName("type")]
    public EffectType Type { get; set; }
    /*Is the effect active? */
    [JsonPropertyName("visible")]
    public required bool Visible { get; set; }
    /*Radius of the blur effect (applies to shadows as well) */
    [JsonPropertyName("radius")]
    public required double Radius { get; set; }
}
public record EffectShadow : Effect
{
    /*The color of the shadow */
    [JsonPropertyName("color")]
    public required Color Color { get; set; }
    /*Blend mode of the shadow */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
    /*How far the shadow is projected in the x and y directions */
    [JsonPropertyName("offset")]
    public required Vector Offset { get; set; }
    /*How far the shadow spreads */
    [JsonPropertyName("spread")]
    public required double Spread { get; set; }
}

public record Paint
{
    [JsonPropertyName("type")]
    public PaintType Type { get; set; }
    /*`default: true` Is the paint enabled? */
    [JsonPropertyName("visible")]
    public bool? Visible { get; set; }
    /*`default: 1` Overall opacity of paint (colors within the paint can also have opacity values which would blend with this) */
    [JsonPropertyName("opacity")]
    public double? Opacity { get; set; }
}
public record PaintSolid : Paint
{
    /*Solid color of the paint */
    [JsonPropertyName("color")]
    public required Color Color { get; set; }
    /*How this node blends with nodes behind it in the scene (see blend mode section for more details) */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
}
public record PaintGradient : Paint
{
    /** How this node blends with nodes behind it in the scene (see blend mode section for more details) */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
    /** This field contains three vectors each of which are a position in normalized object space (normalized object space is if the top left corner of the bounding box of the object is (0 0) and the bottom right is (1 1)). The first position corresponds to the start of the gradient (value 0 for the purposes of calculating gradient stops) the second position is the end of the gradient (value 1) and the third handle position determines the width of the gradient (only relevant for non-linear gradients). */
    [JsonPropertyName("gradientHandlePositions")]
    public required Vector[] GradientHandlePositions { get; set; }
    /** Positions of key points along the gradient axis with the colors anchored there. Colors along the gradient are interpolated smoothly between neighboring gradient stops. */
    [JsonPropertyName("gradientStops")]
    public required ColorStop[] GradientStops { get; set; }
}

public record PaintImage : Paint
{
    /*Image scaling mode */
    [JsonPropertyName("scaleMode")]
    public required PaintSolidScaleMode ScaleMode { get; set; }
    /*Image reference get it with `Api.getImage` */
    [JsonPropertyName("imageRef")]
    public required string ImageRef { get; set; }
    /*Affine transform applied to the image only present if scaleMode is STRETCH */
    [JsonPropertyName("imageTransform")]
    public double[][]? ImageTransform { get; set; }
    /*Amount image is scaled by in tiling only present if scaleMode is TILE */
    [JsonPropertyName("scalingFactor")]
    public double? ScalingFactor { get; set; }
    /*Image rotation in degrees. */
    [JsonPropertyName("rotation")]
    public required double Rotation { get; set; }
    /*A reference to the GIF embedded in this node if the image is a GIF. To download the image using this reference use the GET file images endpoint to retrieve the mapping from image references to image URLs */
    [JsonPropertyName("gifRef")]
    public required string GifRef { get; set; }
    /*How this node blends with nodes behind it in the scene (see blend mode section for more details) */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
}

/*A 2d vector */
public record Vector
{
    /*X coordinate of the vector */
    [JsonPropertyName("x")]
    public required double X { get; set; }
    /*Y coordinate of the vector */
    [JsonPropertyName("y")]
    public required double Y { get; set; }
}

/*List types are represented as string enums with one of these possible values: ORDERED: Text is an ordered list (numbered), UNORDERED: Text is an unordered list (bulleted), NONE: Text is plain text and not part of any list */
public enum LineTypes
{
    ORDERED,
    UNORDERED,
    NONE,
}

public enum PathWindingRule
{
    EVENODD,
    NONZERO,
}

/*A vector svg path */
public record Path
{
    /*A sequence of path commands in SVG notation */
    [JsonPropertyName("path")]
    public required string Data { get; set; }
    /*Winding rule for the path either "EVENODD" or "NONZERO" */
    [JsonPropertyName("windingRule")]
    public required PathWindingRule WindingRule { get; set; }
}

/*A relative offset within a frame */
public record FrameOffset
{
    /*Unique id specifying the frame */
    [JsonPropertyName("node_id")]
    public required string Node_id { get; set; }
    /*2d vector offset within the frame */
    [JsonPropertyName("node_offset")]
    public required Vector Node_offset { get; set; }
}

/*A position color pair representing a gradient stop */
public record ColorStop
{
    /*Value between 0 and 1 representing position along gradient axis */
    [JsonPropertyName("position")]
    public required double Position { get; set; }
    /*Color attached to corresponding position */
    [JsonPropertyName("color")]
    public required Color Color { get; set; }
}

public record Hyperlink
{
    /*Type of hyperlink */
    [JsonPropertyName("type")]
    public required string Type { get; set; } //'URL' | 'NODE'
    /*URL being linked to if URL type */
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    /*ID of frame hyperlink points to if NODE type */
    [JsonPropertyName("nodeID")]
    public required string NodeID { get; set; }
}

/*Metadata for character formatting */
public record TypeStyle
{
    /*Font family of text (standard name) */
    [JsonPropertyName("fontFamily")]
    public required string FontFamily { get; set; }
    /*PostScript font name */
    [JsonPropertyName("fontPostScriptName")]
    public required string FontPostScriptName { get; set; }
    /*Space between paragraphs in px 0 if not present */
    [JsonPropertyName("paragraphSpacing")]
    public double? ParagraphSpacing { get; set; }
    /*Paragraph indentation in px 0 if not present */
    [JsonPropertyName("paragraphIndent")]
    public double? ParagraphIndent { get; set; }
    /*Is text italicized? */
    [JsonPropertyName("italic")]
    public required bool Italic { get; set; }
    /*Numeric font weight */
    [JsonPropertyName("fontWeight")]
    public required double FontWeight { get; set; }
    /*Font size in px */
    [JsonPropertyName("fontSize")]
    public required double FontSize { get; set; }
    /*Text casing applied to the node default is the `ORIGINAL` casing */
    [JsonPropertyName("textCase")]
    public TextCase? TextCase { get; set; }
    /*Text decoration applied to the node default is `NONE` */
    [JsonPropertyName("textDecoration")]
    public TextDecoration? TextDecoration { get; set; }
    /*Dimensions along which text will auto resize default is that the text does not auto-resize. Default is `NONE` */
    [JsonPropertyName("textAutoResize")]
    public TextAutoResize? TextAutoResize { get; set; }
    /*Horizontal text alignment as string enum */
    [JsonPropertyName("textAlignHorizontal")]
    public required string TextAlignHorizontal { get; set; } //'LEFT'|'RIGHT'|'CENTER'|'JUSTIFIED'
    /*Vertical text alignment as string enum */
    [JsonPropertyName("textAlignVertical")]
    public required string TextAlignVertical { get; set; } //'TOP'|'CENTER'|'BOTTOM'
    /*Space between characters in px */
    [JsonPropertyName("letterSpacing")]
    public required double LetterSpacing { get; set; }
    /*Paints applied to characters */
    [JsonPropertyName("fills")]
    public required Paint[] Fills { get; set; }
    /*Link to a URL or frame */
    [JsonPropertyName("hyperlink")]
    public required Hyperlink Hyperlink { get; set; }

    ///*A map of OpenType feature flags to 1 or 0 1 if it is enabled and 0 if it is disabled. Note that some flags aren't reflected here. For example SMCP (small caps) is still represented by the textCase field. */
    [JsonPropertyName("opentypeFlags")]
    public required Dictionary<string, bool> OpentypeFlags { get; set; }

    ///*Line height in px */
    [JsonPropertyName("lineHeightPx")]
    public required double LineHeightPx { get; set; }
    /*@deprecated Line height as a percentage of normal line height. This is deprecated; in a future version of the API only lineHeightPx and lineHeightPercentFontSize will be returned. */
    [JsonPropertyName("lineHeightPercent")]
    public double? LineHeightPercent { get; set; }
    /*Line height as a percentage of the font size. Only returned when lineHeightPercent is not 100 */
    [JsonPropertyName("lineHeightPercentFontSize")]
    public double? LineHeightPercentFontSize { get; set; }
    /*The unit of the line height value specified by the user. */
    [JsonPropertyName("lineHeightUnit")]
    public required LineHeightUnit LineHeightUnit { get; set; }
}


/*A set of properties that can be applied to nodes and published. Styles for a property can be created in the corresponding property's panel while editing a file */
public record Style
{
    /*The key of the style */
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    /*The name of the style */
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    /*The description of the style */
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    /*The type of style */
    [JsonPropertyName("styleType")]
    public required string StyleType { get; set; } //'FILL' | 'TEXT' | 'EFFECT' | 'GRID'
}

/*Represents a single page */
public record CANVAS : Node, INodeContainer
{
    /*An array of top level layers on the canvas */
    [JsonPropertyName("children")]
    public required Node[] Children { get; set; }
    /*Background color of the canvas */
    [JsonPropertyName("backgroundColor")]
    public required Color BackgroundColor { get; set; }
    /*default: [] An array of export settings representing images to export from the canvas */
    [JsonPropertyName("exportSettings")]
    public required ExportSetting[] ExportSettings { get; set; }
    /*Node ID that corresponds to the start frame for prototypes */
    [JsonPropertyName("prototypeStartNodeID")]
    public string? PrototypeStartNodeID { get; set; }
    /*The bounds of the rendered node in the file in absolute space coordinates */
    [JsonPropertyName("absoluteRenderBounds")]
    public required Rectangle AbsoluteRenderBounds { get; set; }
    /*Undocumented SCROLL|...*/
    [JsonPropertyName("scrollBehavior")]
    public required string ScrollBehavior { get; set; }

    //[JsonPropertyName("flowStartingPoints")]
    //public FlowStartingPoint[]? FlowStartingPoints { get; set; }
}

public record StrokeWeights
{
    [JsonPropertyName("top")]
    public required double Top { get; set; }
    [JsonPropertyName("right")]
    public required double Right { get; set; }
    [JsonPropertyName("left")]
    public required double Left { get; set; }
    [JsonPropertyName("bottom")]
    public required double Bottom { get; set; }
}

/*A node of fixed size containing other nodes */
public record FRAME : Node, INodeContainer
{
    /*An array of nodes that are direct children of this node */
    [JsonPropertyName("children")]
    public required Node[] Children { get; set; }
    /*If true layer is locked and cannot be edited default `false` */
    [JsonPropertyName("locked")] public bool? Locked { get; set; }
    /*@deprecated Background of the node. This is deprecated as backgrounds for frames are now in the fills field. */
    [JsonPropertyName("background")]
    public required Paint[] Background { get; set; }
    /*@deprecated Background color of the node. This is deprecated as frames now support more than a solid color as a background. Please use the background field instead. */
    [JsonPropertyName("backgroundColor")]
    public Color? BackgroundColor { get; set; }
    /*An array of fill paints applied to the node */
    [JsonPropertyName("fills")]
    public required Paint[] Fills { get; set; }
    /*An array of stroke paints applied to the node */
    [JsonPropertyName("strokes")]
    public required Paint[] Strokes { get; set; }
    /*The weight of strokes on the node */
    [JsonPropertyName("strokeWeight")]
    public double? StrokeWeight { get; set; }
    /*The weight of strokes on different side of the node */
    [JsonPropertyName("individualStrokeWeights")]
    public StrokeWeights? IndividualStrokeWeights { get; set; }
    /*Position of stroke relative to vector outline as a string enum */
    [JsonPropertyName("strokeAlign")]
    public required StrokeAlign StrokeAlign { get; set; }
    /*Radius of each corner of the frame if a single radius is set for all corners */
    [JsonPropertyName("cornerRadius")]
    public required double CornerRadius { get; set; }
    /*Array of length 4 of the radius of each corner of the rectangle starting in the top left and proceeding clockwise */
    [JsonPropertyName("rectangleCornerRadii")]
    public required double[] RectangleCornerRadii { get; set; }
    /*default: [] An array of export settings representing images to export from node */
    [JsonPropertyName("exportSettings")]
    public required ExportSetting[] ExportSettings { get; set; }
    /*How this node blends with nodes behind it in the scene (see blend mode section for more details) */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
    /*default: false Keep height and width constrained to same ratio */
    [JsonPropertyName("preserveRatio")]
    public required bool PreserveRatio { get; set; }
    /*Horizontal and vertical layout constraints for node */
    [JsonPropertyName("constraints")]
    public required LayoutConstraint Constraints { get; set; }
    /*Determines if the layer should stretch along the parent’s counter axis. This property is only provided for direct children of auto-layout frames. */
    [JsonPropertyName("layoutAlign")]
    public required LayoutAlign LayoutAlign { get; set; }
    /*default: 0. This property is applicable only for direct children of auto-layout frames ignored otherwise. Determines whether a layer should stretch along the parent’s primary axis. A 0 corresponds to a fixed size and 1 corresponds to stretch. */
    [JsonPropertyName("layoutGrow")]
    public double? LayoutGrow { get; set; }
    /*default: null Node ID of node to transition to in prototyping */
    [JsonPropertyName("transitionNodeID")]
    public string? TransitionNodeID { get; set; }
    /*default: null The duration of the prototyping transition on this node (in milliseconds). */
    [JsonPropertyName("transitionDuration")]
    public double? TransitionDuration { get; set; }
    /*default: null The easing curve used in the prototyping transition on this node. */
    [JsonPropertyName("transitionEasing")]
    public EasingType? TransitionEasing { get; set; }
    /*default: 1 Opacity of the node */
    [JsonPropertyName("opacity")]
    public required double Opacity { get; set; }
    /*Bounding box of the node in absolute space coordinates */
    [JsonPropertyName("absoluteBoundingBox")]
    public required Rectangle AbsoluteBoundingBox { get; set; }
    /*The bounds of the rendered node in the file in absolute space coordinates */
    [JsonPropertyName("absoluteRenderBounds")]
    public required Rectangle AbsoluteRenderBounds { get; set; }
    /*Undocumented SCROLL|...*/
    [JsonPropertyName("scrollBehavior")]
    public required string ScrollBehavior { get; set; }
    /*Width and height of element. This is different from the width and height of the bounding box in that the absolute bounding box represents the element after scaling and rotation. Only present if geometry=paths is passed */
    [JsonPropertyName("size")]
    public Vector? Size { get; set; }
    /*The top two rows of a matrix that represents the 2D transform of this node relative to its parent. The bottom row of the matrix is implicitly always (0 0 1). Use to transform coordinates in geometry. Only present if geometry=paths is passed */
    [JsonPropertyName("relativeTransform")]
    public double[][]? RelativeTransform { get; set; }
    /*Does this node clip content outside of its bounds? */
    [JsonPropertyName("clipsContent")]
    public required bool ClipsContent { get; set; }
    /*Whether this layer uses auto-layout to position its children. default NONE */
    [JsonPropertyName("layoutMode")]
    public required string LayoutMode { get; set; }//'NONE' | 'HORIZONTAL' | 'VERTICAL'
    /*Whether the primary axis has a fixed length (determined by the user) or an automatic length (determined by the layout engine). This property is only applicable for auto-layout frames. Default AUTO */
    [JsonPropertyName("primaryAxisSizingMode")]
    public required AxisSizingMode PrimaryAxisSizingMode { get; set; }
    /*Whether the counter axis has a fixed length (determined by the user) or an automatic length (determined by the layout engine). This property is only applicable for auto-layout frames. Default AUTO */
    [JsonPropertyName("counterAxisSizingMode")]
    public required AxisSizingMode CounterAxisSizingMode { get; set; }
    /*Determines how the auto-layout frame’s children should be aligned in the primary axis direction. This property is only applicable for auto-layout frames. Default MIN */
    [JsonPropertyName("primaryAxisAlignItems")]
    public required string PrimaryAxisAlignItems { get; set; }//'MIN' | 'CENTER' | 'MAX' | 'SPACE_BETWEEN'
    /*Determines how the auto-layout frame’s children should be aligned in the counter axis direction. This property is only applicable for auto-layout frames. Default MIN */
    [JsonPropertyName("counterAxisAlignItems")]
    public required string CounterAxisAlignItems { get; set; }//'MIN' | 'CENTER' | 'MAX' | 'BASELINE'
    /*default: 0. The padding between the left border of the frame and its children. This property is only applicable for auto-layout frames. */
    [JsonPropertyName("paddingLeft")]
    public required double PaddingLeft { get; set; }
    /*default: 0. The padding between the right border of the frame and its children. This property is only applicable for auto-layout frames. */
    [JsonPropertyName("paddingRight")]
    public required double PaddingRight { get; set; }
    /*default: 0. The padding between the top border of the frame and its children. This property is only applicable for auto-layout frames. */
    [JsonPropertyName("paddingTop")]
    public required double PaddingTop { get; set; }
    /*default: 0. The padding between the bottom border of the frame and its children. This property is only applicable for auto-layout frames. */
    [JsonPropertyName("paddingBottom")]
    public required double PaddingBottom { get; set; }
    /*@deprecated default: 0. The horizontal padding between the borders of the frame and its children. This property is only applicable for auto-layout frames. Deprecated in favor of setting individual paddings. */
    [JsonPropertyName("horizontalPadding")]
    public required double HorizontalPadding { get; set; }
    /*@deprecated default: 0. The vertical padding between the borders of the frame and its children. This property is only applicable for auto-layout frames. Deprecated in favor of setting individual paddings. */
    [JsonPropertyName("verticalPadding")]
    public required double VerticalPadding { get; set; }
    /*default: 0. The distance between children of the frame. This property is only applicable for auto-layout frames. */
    [JsonPropertyName("itemSpacing")]
    public required double ItemSpacing { get; set; }
    /**default: false. Applicable only if layoutMode != "NONE". */
    [JsonPropertyName("itemReverseZIndex")]
    public required bool ItemReverseZIndex { get; set; }
    /**default: false. Applicable only if layoutMode != "NONE". */
    [JsonPropertyName("strokesIncludedInLayout")]
    public required bool StrokesIncludedInLayout { get; set; }
    /*Defines the scrolling behavior of the frame if there exist contents outside of the frame boundaries. The frame can either scroll vertically horizontally or in both directions to the extents of the content contained within it. This behavior can be observed in a prototype. Default NONE */
    [JsonPropertyName("overflowDirection")]
    public required string OverflowDirection { get; set; }//'NONE'|'HORIZONTAL_SCROLLING'|'VERTICAL_SCROLLING'|'HORIZONTAL_AND_VERTICAL_SCROLLING';
    /*default: [] An array of layout grids attached to this node (see layout grids section for more details). GROUP nodes do not have this attribute */
    [JsonPropertyName("layoutGrids")]
    public LayoutGrid[]? LayoutGrids { get; set; }
    /*default: [] An array of effects attached to this node (see effects section for more details) */
    [JsonPropertyName("effects")]
    public required Effect[] Effects { get; set; }
    /*default: false Does this node mask sibling nodes in front of it? */
    [JsonPropertyName("isMask")]
    public required bool IsMask { get; set; }
    /*default: false Does this mask ignore fill style (like gradients) and effects? */
    [JsonPropertyName("isMaskOutline")]
    public required bool IsMaskOutline { get; set; }
    /*default: AUTO */
    [JsonPropertyName("layoutPositioning")]
    public required string LayoutPositioning { get; set; }//'AUTO'|'ABSOLUTE'
    /*A mapping of a StyleType to style ID (see Style) of styles present on this node. The style ID can be used to look up more information about the style in the top-level styles field. */
    [JsonPropertyName("styles")]
    public Dictionary<string, string>? Styles { get; set; } //Key='FILL' | 'TEXT' | 'EFFECT' | 'GRID'


}

/** A node that can have instances created of it that share the same properties */
public record COMPONENT : FRAME { }
/** A node that can have instances created of it that share the same properties */
public record COMPONENT_SET : FRAME { }

public record INSTANCE : COMPONENT
{
    [JsonPropertyName("componentId")]
    public required string ComponentId { get; set; }
}

//Paint metadata to override default paints
public record PaintOverride
{
    //Paints applied to characters
    [JsonPropertyName("fillsPaint")]
    public required Paint[] FillsPaint { get; set; }

    //ID of style node, if any, that this inherits fill data from
    [JsonPropertyName("inheritFillStyleIdString")]
    public required string? InheritFillStyleIdString { get; set; }

}

public record GROUP : FRAME { }

/*A vector network consisting of vertices and edges */
public record VECTOR : Node
{
    /*default: [] An array of export settings representing images to export from node */
    [JsonPropertyName("exportSettings")]
    public required ExportSetting[] ExportSettings { get; set; }
    /*If true layer is locked and cannot be edited default `false` */
    [JsonPropertyName("locked")]
    public bool? Locked { get; set; }
    /*How this node blends with nodes behind it in the scene (see blend mode section for more details) */
    [JsonPropertyName("blendMode")]
    public required BlendMode BlendMode { get; set; }
    /*default: false Keep height and width constrained to same ratio */
    [JsonPropertyName("preserveRatio")]
    public bool? PreserveRatio { get; set; }
    /*Determines if the layer should stretch along the parent’s counter axis. This property is only provided for direct children of auto-layout frames. */
    [JsonPropertyName("layoutAlign")]
    public required LayoutAlign LayoutAlign { get; set; }
    /*default: 0. This property is applicable only for direct children of auto-layout frames ignored otherwise. Determines whether a layer should stretch along the parent’s primary axis. A 0 corresponds to a fixed size and 1 corresponds to stretch. */
    [JsonPropertyName("layoutGrow")]
    public int? LayoutGrow { get; set; }
    /*Horizontal and vertical layout constraints for node */
    [JsonPropertyName("constraints")]
    public required LayoutConstraint Constraints { get; set; }
    /*default: null Node ID of node to transition to in prototyping */
    [JsonPropertyName("transitionNodeID")]
    public string? TransitionNodeID { get; set; }
    /*default: null The duration of the prototyping transition on this node (in milliseconds). */
    [JsonPropertyName("transitionDuration")]
    public int? TransitionDuration { get; set; }
    /*default: null The easing curve used in the prototyping transition on this node. */
    [JsonPropertyName("transitionEasing")]
    public EasingType? TransitionEasing { get; set; }
    /*default: 1 Opacity of the node */
    [JsonPropertyName("opacity")]
    public int? Opacity { get; set; }
    /*Bounding box of the node in absolute space coordinates */
    [JsonPropertyName("absoluteBoundingBox")]
    public required Rectangle AbsoluteBoundingBox { get; set; }
    /*The bounds of the rendered node in the file in absolute space coordinates */
    [JsonPropertyName("absoluteRenderBounds")]
    public required Rectangle AbsoluteRenderBounds { get; set; }
    /*Undocumented SCROLL|...*/
    [JsonPropertyName("scrollBehavior")]
    public required string ScrollBehavior { get; set; }
    /*Width and height of element. This is different from the width and height of the bounding box in that the absolute bounding box represents the element after scaling and rotation. Only present if geometry=paths is passed */
    [JsonPropertyName("size")]
    public Vector? Size { get; set; }
    /*The top two rows of a matrix that represents the 2D transform of this node relative to its parent. The bottom row of the matrix is implicitly always (0 0 1). Use to transform coordinates in geometry. Only present if geometry=paths is passed */
    [JsonPropertyName("relativeTransform")]
    public double[][]? RelativeTransform { get; set; }
    /*default: [] An array of effects attached to this node (see effects section for more details) */
    [JsonPropertyName("effects")]
    public Effect[]? Effects { get; set; }
    /*default: false Does this node mask sibling nodes in front of it? */
    [JsonPropertyName("isMask")]
    public bool? IsMask { get; set; }
    /*default: [] An array of fill paints applied to the node */
    [JsonPropertyName("fills")]
    public required Paint[] Fills { get; set; }
    /*Only specified if parameter geometry=paths is used. An array of paths representing the object fill */
    [JsonPropertyName("fillGeometry")]
    public Path[]? FillGeometry { get; set; }
    /*default: [] An array of stroke paints applied to the node */
    [JsonPropertyName("strokes")]
    public required Paint[] Strokes { get; set; }
    /*The weight of strokes on the node */
    [JsonPropertyName("strokeWeight")]
    public double? StrokeWeight { get; set; }
    /*The weight of strokes on different side of the node */
    [JsonPropertyName("individualStrokeWeights")]
    public StrokeWeights? IndividualStrokeWeights { get; set; }
    /*default: NONE. A string enum with value of "NONE" "ROUND" "SQUARE" "LINE_ARROW" or "TRIANGLE_ARROW" describing the end caps of vector paths. */
    [JsonPropertyName("strokeCap")]
    public StrokeCap? StrokeCap { get; set; }
    /*Only specified if parameter geometry=paths is used. An array of paths representing the object stroke */
    [JsonPropertyName("strokeGeometry")]
    public Path[]? StrokeGeometry { get; set; }
    /*Where stroke is drawn relative to the vector outline as a string enum "INSIDE": draw stroke inside the shape boundary "OUTSIDE": draw stroke outside the shape boundary "CENTER": draw stroke centered along the shape boundary */
    [JsonPropertyName("strokeAlign")]
    public required StrokeAlign StrokeAlign { get; set; }
    /*A string enum with value of "MITER" "BEVEL" or "ROUND" describing how corners in vector paths are rendered. */
    [JsonPropertyName("strokeJoin")]
    public StrokeJoin? StrokeJoin { get; set; }
    /*An array of floating point numbers describing the pattern of dash length and gap lengths that the vector path follows. For example a value of [1 2] indicates that the path has a dash of length 1 followed by a gap of length 2 repeated. */
    [JsonPropertyName("strokeDashes")]
    public int[]? StrokeDashes { get; set; }
    /*Only valid if strokeJoin is "MITER". The corner angle in degrees below which strokeJoin will be set to "BEVEL" to avoid super sharp corners. By default this is 28.96 degrees. */
    [JsonPropertyName("strokeMiterAngle")]
    public int? StrokeMiterAngle { get; set; }
    /*A mapping of a StyleType to style ID (see Style) of styles present on this node. The style ID can be used to look up more information about the style in the top-level styles field. */
    [JsonPropertyName("styles")]
    public Dictionary<string, string>? Styles { get; set; } //Key='FILL' | 'TEXT' | 'EFFECT' | 'GRID'
    /*default: AUTO */
    [JsonPropertyName("layoutPositioning")]
    public required string LayoutPositioning { get; set; } //'AUTO'|'ABSOLUTE'
    /** Radius of each corner of the rectangle */
    [JsonPropertyName("cornerRadius")]
    public required double CornerRadius { get; set; }

    [JsonPropertyName("fillOverrideTable")]
    public required PaintOverride? FillOverrideTable { get; set; }

    /** Array of length 4 of the radius of each corner of the rectangle, starting in the top left and proceeding clockwise */
    [JsonPropertyName("RectangleCornerRadii")]
    public double[]? RectangleCornerRadii { get; set; }

    /* Does this mask ignore fill style (like gradients) and effects?*/
    [JsonPropertyName("IsMaskOutline")]
    public bool IsMaskOutline { get; set; }
}


/** A group that has a boolean operation applied to it */
public record BOOLEAN : VECTOR, INodeContainer
{
    /** An array of nodes that are being boolean operated on */
    [JsonPropertyName("children")]
    public required Node[] Children { get; set; }
}

/** A group that has a boolean operation applied to it */
public record BOOLEAN_OPERATION : VECTOR, INodeContainer
{
    /** An array of nodes that are being boolean operated on */
    [JsonPropertyName("children")]
    public required Node[] Children { get; set; }

    /** A string enum with value of "UNION", "INTERSECT", "SUBTRACT", or "EXCLUDE" indicating the type of boolean operation applied */
    [JsonPropertyName("booleanOperation")]
    public required BooleanOperationType BooleanOperation { get; set; }

    //Does this mask ignore fill style (like gradients) and effects?
    [JsonPropertyName("isMaskOutlineBoolean")]
    public required bool IsMaskOutlineBoolean { get; set; }

}

/** A regular star shape */
public record STAR : VECTOR { }

/** A straight line */
public record LINE : VECTOR { }

public record ArcData
{
    [JsonPropertyName("startingAngle")]
    public required double StartingAngle { get; set; }
    [JsonPropertyName("endingAngle")]
    public required double EndingAngle { get; set; }
    [JsonPropertyName("innerRadius")]
    public required double InnerRadius { get; set; }
}

/** An ellipse */
public record ELLIPSE : VECTOR
{
    /** Start and end angles of the ellipse measured clockwise from the x axis, plus the inner radius for donuts */
    [JsonPropertyName("arcData")]
    public required ArcData ArcData { get; set; }
}

/** A regular n-sided polygon */
public record REGULAR_POLYGON : VECTOR { }

/** A rectangle */
public record RECTANGLE : VECTOR
{


}

/*A rectangular region of the canvas that can be exported */
public record SLICE : Node
{
    /*An array of export settings representing images to export from this node */
    [JsonPropertyName("exportSettings")]
    public required ExportSetting[] ExportSettings { get; set; }
    /*Bounding box of the node in absolute space coordinates */
    [JsonPropertyName("absoluteBoundingBox")]
    public required Rectangle AbsoluteBoundingBox { get; set; }
    /*Width and height of element. This is different from the width and height of the bounding box in that the absolute bounding box represents the element after scaling and rotation. Only present if geometry=paths is passed */
    [JsonPropertyName("size")]
    public Vector? Size { get; set; }
    /*The top two rows of a matrix that represents the 2D transform of this node relative to its parent. The bottom row of the matrix is implicitly always (0 0 1). Use to transform coordinates in geometry. Only present if geometry=paths is passed */
    [JsonPropertyName("relativeTransform")]
    public double[][]? RelativeTransform { get; set; }
}

/*A text box */
public record TEXT : VECTOR
{
    /*Text contained within text box */
    [JsonPropertyName("characters")]
    public required string Characters { get; set; }
    /*Style of text including font family and weight (see type style section for more information) */
    [JsonPropertyName("style")]
    public required TypeStyle Style { get; set; }
    /*Array with same number of elements as characters in text box each element is a reference to the styleOverrideTable defined below and maps to the corresponding character in the characters field. Elements with value 0 have the default type style */
    [JsonPropertyName("characterStyleOverrides")]
    public required int[] CharacterStyleOverrides { get; set; }
    /*Map from ID to TypeStyle for looking up style overrides */
    [JsonPropertyName("styleOverrideTable")]
    public required Dictionary<int, TypeStyle> StyleOverrideTable { get; set; }
    /*An array with the same number of elements as lines in the text node where lines are delimited by newline or paragraph separator characters. Each element in the array corresponds to the list type of a specific line. */
    [JsonPropertyName("lineTypes")]
    public required LineTypes[] LineTypes { get; set; }
    /*An array with the same number of elements as lines in the text node where lines are delimited by newline or paragraph separator characters. Each element in the array corresponds to the indentation level of a specific line. */
    [JsonPropertyName("lineIndentations")]
    public required int[] LineIndentations { get; set; }
}
