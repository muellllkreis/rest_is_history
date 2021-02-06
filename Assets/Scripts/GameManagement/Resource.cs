using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public abstract class Resource
{
    [XmlElement(ElementName = "name")]
    public string name { get; set; }

    public Resource() { }

    public Resource(string name) {
        this.name = name;
    }
}
