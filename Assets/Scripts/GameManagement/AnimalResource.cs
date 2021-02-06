using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class AnimalResource : Resource
{
    [XmlElement("nature")]
    public Nature nature { get; set; }

    [XmlAttribute("carriesStructure")]
    public bool carriesStructure { get; set; }

    public AnimalResource() {
    }

    public AnimalResource(string name) : base(name) {
    }
}
