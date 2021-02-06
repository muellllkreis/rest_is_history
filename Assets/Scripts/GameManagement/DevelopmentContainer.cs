using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("developments")]
public class DevelopmentContainer {
    //[XmlArray("resources")]
    [XmlElement(typeof(TechnologyDev), ElementName = "technology")]
    [XmlElement(typeof(DiscoveryDev), ElementName = "discovery")]
    [XmlElement(typeof(BuildingDev), ElementName = "building")]
    public List<Development> developments = new List<Development>();
    public HashSet<Development> developmentSet = new HashSet<Development>();

    public static DevelopmentContainer Load() {
        TextAsset xml = Resources.Load<TextAsset>("developments");

        XmlSerializer serializer = new XmlSerializer(typeof(DevelopmentContainer));
        StringReader reader = new StringReader(xml.text);

        DevelopmentContainer developments = serializer.Deserialize(reader) as DevelopmentContainer;
        

        foreach (Development d in developments.developments) {
            Debug.Log(d.name);
            d.Initialize();
            //d.buffs.printBuffs();
            if (d.requirements != null) {
                foreach (KeyValuePair<string, object> kv in d.requirements.GetRequirements()) {
                    Debug.Log(kv.Key);
                }
            }
            //Debug.Log(d.buffs);
            developments.developmentSet.Add(d);
        }

        reader.Close();
        Debug.Log("Done reading");

        return developments;
    }
}
