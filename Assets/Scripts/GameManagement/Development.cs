using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Development
{
    [XmlElement(ElementName = "name")]
    public string name { get; set; }

    [XmlElement("subtype")]
    public string subtype { get; set; }

    [XmlElement("buffs")]
    public Buffs buffs { get; set; }

    [XmlElement("cost")]
    public Cost cost { get; set; }

    [XmlElement("rates")]
    public Rate rate { get; set; }

    [XmlElement("required")]
    public Requirements requirements { get; set; }

    public List<int?> requiredMinAttributes { get; set; }
    public List<int?> requiredMaxAttributes { get; set; }
    public HashSet<string> requiredResources { get; set; }
    public HashSet<HashSet<string>> optionalResources { get; set; }
    public HashSet<string> requiredResourceType { get; set; }
    public HashSet<string> requiredDevelopments { get; set; }
    public HashSet<HashSet<string>> optionalDevelopments { get; set; }
    public HashSet<string> requiredDevelopmentTypes { get; set; }

    public int? requiredMinTemp = null;
    public int? requiredMaxTemp = null;
    public bool? requiredSeasons = null;

    public void Initialize() {
        if(requirements != null) {
            optionalResources = new HashSet<HashSet<string>>();
            optionalDevelopments = new HashSet<HashSet<string>>();
            requiredMinAttributes = requirements.GetMinAttributes();
            requiredMaxAttributes = requirements.GetMaxAttributes();
            requiredMinTemp = requirements.minTemperature;
            requiredMaxTemp = requirements.maxTemperature;
            requiredSeasons = requirements.seasons;
            requiredDevelopments = requirements.GetRequiredDevelopments();
            foreach(string s in requirements.GetOptionalDevelopments()) {
                optionalDevelopments.Add(new HashSet<string>(s.Split(',')));
            }
            requiredResources = new HashSet<string>(requirements.resources);
            foreach(string s in requirements.optionalResources) {
                optionalResources.Add(new HashSet<string>(s.Split(',')));
            }
            requiredResourceType = new HashSet<string>(requirements.resourceTypes);
        }
        else {
            requiredMinAttributes = new List<int?>();
            requiredMaxAttributes = new List<int?>();
            requiredResources = new HashSet<string>();
            requiredResourceType = new HashSet<string>();
            requiredDevelopments = new HashSet<string>();
            requiredDevelopmentTypes = new HashSet<string>();
            optionalResources = new HashSet<HashSet<string>>();
            optionalDevelopments = new HashSet<HashSet<string>>();
        }
    }

}

public class Buffs {
    [XmlElement("aggression")]
    public int aggression { get; set; }

    [XmlElement("creativity")]
    public int creativity { get; set; }

    [XmlElement("productivity")]
    public int productivity { get; set; }

    [XmlElement("spirituality")]
    public int spirituality { get; set; }

    [XmlElement("curiousity")]
    public int curiousity { get; set; }

    [XmlElement("courage")]
    public int courage { get; set; }

    public List<int> GetBuffList() {
        return new List<int>() { aggression, creativity, productivity, spirituality, curiousity, courage };
    }

    public void printBuffs() {
        Debug.Log(aggression);
        Debug.Log(creativity);
        Debug.Log(productivity);
        Debug.Log(spirituality);
        Debug.Log(curiousity);
        Debug.Log(courage);
    }
}

public class Cost {
    [XmlElement("wealth")]
    public int wealth { get; set; }

    [XmlElement("intelligence")]
    public int intelligence { get; set; }

    [XmlElement("production")]
    public int production { get; set; }
}

public class Rate {
    [XmlElement("health")]
    public double health { get; set; }

    [XmlElement("wealth")]
    public double wealth { get; set; }

    [XmlElement("population")]
    public double population { get; set; }

    [XmlElement("intelligence")]
    public double intelligence { get; set; }

    [XmlElement("production")]
    public double production { get; set; }

    public double GetOverall() {
        return health + wealth + population + intelligence + production;
    }

    public List<double> GetRateList() {
        return new List<double>() { health, wealth, population, intelligence, production };
    }
}

public class Requirements {
    [XmlElement("minAggression")]
    public int? minAggression { get; set; }

    [XmlElement("maxAggression")]
    public int? maxAggression { get; set; }

    [XmlElement("minCreativity")]
    public int? minCreativity { get; set; }

    [XmlElement("maxCreativity")]
    public int? maxCreativity { get; set; }

    [XmlElement("minProductivity")]
    public int? minProductivity { get; set; }

    [XmlElement("maxProductivity")]
    public int? maxProductivity { get; set; }

    [XmlElement("minSpirituality")]
    public int? minSpirituality { get; set; }

    [XmlElement("maxSpirituality")]
    public int? maxSpirituality { get; set; }

    [XmlElement("minCuriosity")]
    public int? minCuriosity { get; set; }

    [XmlElement("maxCuriosity")]
    public int? maxCuriosity { get; set; }

    [XmlElement("minCourage")]
    public int? minCourage { get; set; }

    [XmlElement("maxCourage")]
    public int? maxCourage { get; set; }

    [XmlElement("minTemperature")]
    public int? minTemperature { get; set; }

    [XmlElement("maxTemperature")]
    public int? maxTemperature { get; set; }

    [XmlElement("seasons")]
    public bool? seasons { get; set; }

    [XmlArray("terrain")]
    [XmlArrayItem("value")]
    public List<string> terrains { get; set; }

    [XmlArray("resources")]
    [XmlArrayItem("value")]
    public List<string> resources { get; set; }

    [XmlArray("optionalResources")]
    [XmlArrayItem("value")]
    public List<string> optionalResources { get; set; }

    [XmlArray("discoveries")]
    [XmlArrayItem("value")]
    public List<string> discoveries { get; set; }

    [XmlArray("optionalDiscoveries")]
    [XmlArrayItem("value")]
    public List<string> optionalDiscoveries { get; set; }

    [XmlArray("politics")]
    [XmlArrayItem("value")]
    public List<string> politics { get; set; }

    [XmlArray("optionalPolitics")]
    [XmlArrayItem("value")]
    public List<string> optionalPolitics { get; set; }

    [XmlArray("society")]
    [XmlArrayItem("value")]
    public List<string> society { get; set; }

    [XmlArray("optionalSociety")]
    [XmlArrayItem("value")]
    public List<string> optionalSociety { get; set; }

    [XmlArray("cultures")]
    [XmlArrayItem("value")]
    public List<string> cultures { get; set; }

    [XmlArray("optionalCultures")]
    [XmlArrayItem("value")]
    public List<string> optionalCultures { get; set; }

    [XmlArray("religions")]
    [XmlArrayItem("value")]
    public List<string> religions { get; set; }

    [XmlArray("optionalReligions")]
    [XmlArrayItem("value")]
    public List<string> optionalReligions { get; set; }

    [XmlArray("military")]
    [XmlArrayItem("value")]
    public List<string> military { get; set; }

    [XmlArray("optionalMilitary")]
    [XmlArrayItem("value")]
    public List<string> optionalMilitary { get; set; }

    [XmlArray("technologies")]
    [XmlArrayItem("value")]
    public List<string> technologies { get; set; }

    [XmlArray("optionalTechnologies")]
    [XmlArrayItem("value")]
    public List<string> optionalTechnologies { get; set; }

    [XmlArray("technologyType")]
    [XmlArrayItem("value")]
    public List<string> technologyTypes { get; set; }

    [XmlArray("optionalTechnologyTypes")]
    [XmlArrayItem("value")]
    public List<string> optionalTechnologyTypes { get; set; }

    [XmlArray("resourceType")]
    [XmlArrayItem("value")]
    public List<string> resourceTypes { get; set; }

    [XmlArray("optionalResourceTypes")]
    [XmlArrayItem("value")]
    public List<string> optionalResourceTypes { get; set; }

    [XmlArray("buildings")]
    [XmlArrayItem("value")]
    public List<string> buildings { get; set; }

    [XmlArray("optionalBuildings")]
    [XmlArrayItem("value")]
    public List<string> optionalBuildings { get; set; }

    public List<int?> GetMinAttributes() {
        List<int?> attributes = new List<int?>();
        if (minAggression != null) {
            attributes.Add(minAggression);
        }
        else {
            attributes.Add(-1);
        }
        if (minCreativity != null) {
            attributes.Add(minCreativity);
        } 
        else {
            attributes.Add(-1);
        }
        if (minProductivity != null) {
            attributes.Add(minProductivity);
        }
        else {
            attributes.Add(-1);
        }
        if (minSpirituality != null) {
            attributes.Add(minSpirituality);
        }
        else {
            attributes.Add(-1);
        }
        if (minCuriosity != null) {
            attributes.Add(minCuriosity);
        }
        else {
            attributes.Add(-1);
        }
        if (minCourage != null) {
            attributes.Add(minCourage);
        }
        else {
            attributes.Add(-1);
        }
        return attributes;
    }

    public List<int?> GetMaxAttributes() {
        List<int?> attributes = new List<int?>();
        if (maxAggression != null) {
            attributes.Add(maxAggression);
        }
        else {
            attributes.Add(-1);
        }
        if (maxCreativity != null) {
            attributes.Add(maxCreativity);
        }
        else {
            attributes.Add(-1);
        }
        if (maxProductivity != null) {
            attributes.Add(maxProductivity);
        }
        else {
            attributes.Add(-1);
        }
        if (maxSpirituality != null) {
            attributes.Add(maxSpirituality);
        }
        else {
            attributes.Add(-1);
        }
        if (maxCuriosity != null) {
            attributes.Add(maxCuriosity);
        }
        else {
            attributes.Add(-1);
        }
        if (maxCourage != null) {
            attributes.Add(maxCourage);
        }
        else {
            attributes.Add(-1);
        }
        return attributes;
    }

    public HashSet<string> GetRequiredDevelopments() {
        List<string> developments = new List<string>();
        developments.AddRange(discoveries);
        developments.AddRange(politics);
        developments.AddRange(society);
        developments.AddRange(cultures);
        developments.AddRange(religions);
        developments.AddRange(military);
        developments.AddRange(technologies);
        developments.AddRange(buildings);
        return new HashSet<string>(developments);
    }

    public HashSet<string> GetOptionalDevelopments() {
        List<string> developments = new List<string>();
        developments.AddRange(optionalDiscoveries);
        developments.AddRange(optionalPolitics);
        developments.AddRange(optionalSociety);
        developments.AddRange(optionalCultures);
        developments.AddRange(optionalReligions);
        developments.AddRange(optionalMilitary);
        developments.AddRange(optionalTechnologies);
        developments.AddRange(optionalBuildings);
        return new HashSet<string>(developments);
    }

    public Dictionary<string, object> GetRequirements() {
        Dictionary<string, object> requirements = new Dictionary<string, object>();

        if (minAggression != null) requirements["minAggression"] = minAggression;
        if (maxAggression != null) requirements["maxAggression"] = maxAggression;
        if (minCreativity != null) requirements["minCreativity"] = minCreativity;
        if (maxCreativity != null) requirements["maxCreativity"] = maxCreativity;
        if (minProductivity != null) requirements["minProductivity"] = minProductivity;
        if (maxProductivity != null) requirements["maxProductivity"] = maxProductivity;
        if (minSpirituality != null) requirements["minSpirituality"] = minSpirituality;
        if (maxSpirituality != null) requirements["maxSpirituality"] = maxSpirituality;
        if (minCuriosity != null) requirements["minCuriosity"] = minCuriosity;
        if (maxCuriosity != null) requirements["maxCuriosity"] = maxCuriosity;
        if (minCourage != null) requirements["minCourage"] = minCourage;
        if (maxCourage != null) requirements["maxCourage"] = maxCourage;
        if (minTemperature != null) requirements["minTemperature"] = minTemperature;
        if (maxTemperature != null) requirements["maxTemperature"] = maxTemperature;
        if (seasons != null) requirements["seasons"] = seasons;
        if (terrains.Count != 0) requirements["terrains"] = terrains;
        if (resources.Count != 0) requirements["resources"] = resources;
        if (optionalResources.Count != 0) requirements["optionalResources"] = optionalResources;
        if (discoveries.Count != 0) requirements["discoveries"] = discoveries;
        if (optionalDiscoveries.Count != 0) requirements["optionalDiscoveries"] = optionalDiscoveries;
        if (politics.Count != 0) requirements["politics"] = politics;
        if (optionalPolitics.Count != 0) requirements["optionalPolitics"] = optionalPolitics;
        if (society.Count != 0) requirements["society"] = society;
        if (optionalSociety.Count != 0) requirements["optionalSociety"] = optionalSociety;
        if (cultures.Count != 0) requirements["cultures"] = cultures;
        if (optionalCultures.Count != 0) requirements["optionalCultures"] = optionalCultures;
        if (religions.Count != 0) requirements["religions"] = religions;
        if (optionalReligions.Count != 0) requirements["optionalReligions"] = optionalReligions;
        if (military.Count != 0) requirements["military"] = military;
        if (optionalMilitary.Count != 0) requirements["optionalMilitary"] = optionalMilitary;
        if (technologies.Count != 0) requirements["technologies"] = technologies;
        if (optionalTechnologies.Count != 0) requirements["optionalTechnologies"] = optionalTechnologies;
        if (technologyTypes.Count != 0) requirements["technologyTypes"] = technologyTypes;
        if (optionalTechnologyTypes.Count != 0) requirements["optionalTechnologyTypes"] = optionalTechnologyTypes;
        if (resourceTypes.Count != 0) requirements["resourceTypes"] = resourceTypes;
        if (optionalResourceTypes.Count != 0) requirements["optionalResourceTypes"] = optionalResourceTypes;
        if (buildings.Count != 0) requirements["buildings"] = buildings;
        if (optionalBuildings.Count != 0) requirements["optionalBuildings"] = buildings;

        return requirements;
    }
}



