using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class FoodResource : Resource
{
    [XmlAttribute("edible")]
    public bool edible { get; set; }

    [XmlElement("health")]
    public int health { get; set; }

    public FoodResource() { }

    public FoodResource(string name) : base(name) {
    }

}
