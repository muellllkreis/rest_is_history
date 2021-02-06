using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("resources")]
public class ResourceContainer
{
    //[XmlArray("resources")]
    [XmlElement(typeof(MaterialResource), ElementName = "material")]
    [XmlElement(typeof(FoodResource), ElementName = "grain")]
    public List<Resource> resources = new List<Resource>();

    public static ResourceContainer Load() {
        string appPath = Application.dataPath;
        string path = appPath + "/Resources/resources.xml";
        Debug.Log(path);
        TextAsset xml = Resources.Load<TextAsset>("resources");

        XmlSerializer serializer = new XmlSerializer(typeof(ResourceContainer));
        StringReader reader = new StringReader(xml.text);

        ResourceContainer resources = serializer.Deserialize(reader) as ResourceContainer;

       foreach(Resource r in resources.resources) {
            Debug.Log(r);
            Debug.Log(r.name);
       }

        reader.Close();
        Debug.Log("Done reading");

        return resources;
    }
}
