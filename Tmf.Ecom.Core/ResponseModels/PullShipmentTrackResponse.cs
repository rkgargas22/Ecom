using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tmf.Ecom.Core.ResponseModels;

[XmlRoot(ElementName = "field")]
public class Field
{
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; } = string.Empty;
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; } = string.Empty;
    [XmlText]
    public string Text { get; set; } = string.Empty;
    [XmlElement(ElementName = "object")]
    public Object Object { get; set; }
}

[XmlRoot(ElementName = "object")]
public class Object
{
    [XmlElement(ElementName = "field")]
    public List<Field> Field { get; set; }
    [XmlAttribute(AttributeName = "pk")]
    public string Pk { get; set; } = string.Empty;
    [XmlAttribute(AttributeName = "model")]
    public string Model { get; set; } = string.Empty;
}

[XmlRoot(ElementName = "ecomexpress-objects")]
public class PullShipmentTrackResponse
{ 
    [XmlElement(ElementName = "object")]
    public Object? Object { get; set; }

    [XmlAttribute(AttributeName = "version")]
    public string Version { get; set; } = string.Empty;
}









