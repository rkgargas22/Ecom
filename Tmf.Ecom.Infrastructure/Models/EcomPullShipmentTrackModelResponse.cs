using System.Xml.Serialization;

namespace Tmf.Ecom.Infrastructure.Models;

[XmlRoot(ElementName = "field")]
public class Field
{
    //[XmlAttribute(AttributeName = "type")]
    //public string Type { get; set; }
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlText]
    public string Text { get; set; }
    [XmlElement(ElementName = "object")]
    public Object Object { get; set; }
}

[XmlRoot(ElementName = "object")]
public class Object
{
    [XmlElement(ElementName = "field")]
    public List<Field> Field { get; set; }
    //[XmlAttribute(AttributeName = "pk")]
    //public string Pk { get; set; }
    [XmlAttribute(AttributeName = "model")]
    public string Model { get; set; }
}

[XmlRoot(ElementName = "ecomexpress-objects")]
public class EcomPullShipmentTrackModelResponse
{
    [XmlElement(ElementName = "object")]
    public Object Object { get; set; }

    //[XmlAttribute(AttributeName = "version")]
    //public string Version { get; set; }
}
