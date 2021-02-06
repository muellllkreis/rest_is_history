using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Civilization : MonoBehaviour
{
    // -1 because of NONE element in enum
    static int NUMBER_OF_ATTRIBUTES = Enum.GetNames(typeof(Attributes)).Length - 1;
    static int NUMBER_OF_PARAMETERS = Enum.GetNames(typeof(Parameters)).Length;

    public GameObject settlerPrefab;

    public List<int> attributes = new List<int>() { 50, 50, 50, 50, 50, 50 };
    List<TechnologyDev> technologies = new List<TechnologyDev>();
    List<DiscoveryDev> discoveries = new List<DiscoveryDev>();
    List<BuildingDev> buildings = new List<BuildingDev>();

    private Environment environment;

    public List<double> parameters = new List<double>() { 50d, 0d, 10d, 0d, 0d };
    private double health = 50;
    private double wealth = 0;
    private double population = 10;
    private double intelligence = 0;
    private double production = 0;
    public List<double> rates = new List<double>() { 0d, 0d, 0d, 0d, 0d };

    public Civilization(Environment env) {
        environment = env;
    }

    public void Awake() {
        GameObject settlerObj = Instantiate(settlerPrefab, new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        Settler settler = settlerObj.GetComponent<Settler>();
        settler.Init(this);
    }

    private Attributes FindOrientation() {
        int max = -1;
        int maxIndex = -1;
        bool uniqueMax = true;
        for(int i = 0; i < this.attributes.Count; i++) {
            if(attributes[i] > max) {
                max = attributes[i];
                maxIndex = i;
                uniqueMax = true;
            }
            else if(attributes[i] == max) {
                maxIndex = -1;
                uniqueMax = false;
            }
        }
        if(!uniqueMax) {
            return (Attributes) (-1);
        }
        else {
            return (Attributes) maxIndex;
        }
    }

    private List<Development> GetAllUnlockedDevelopments() {
        List<Development> allDevs = new List<Development>();
        allDevs.AddRange(technologies);
        allDevs.AddRange(discoveries);
        allDevs.AddRange(buildings);
        return allDevs;
    }

    public HashSet<Development> FindAvailableDevelopments(HashSet<Development> allDevelopments) {
        HashSet<Development> availableDevelopments = new HashSet<Development>();
        //Debug.Log("Size of allDevelopments: " + allDevelopments.Count);
        foreach(Development d in allDevelopments) {
            // low chance of random discovery
            double discoverChance = (new System.Random()).NextDouble();
            //Debug.Log("DiscoverChance: " + discoverChance + " Name: " + d.name);
            if (discoverChance < 0.75 && d.GetType() == typeof(DiscoveryDev)) continue;
            List<Development> allUnlockedDevs = GetAllUnlockedDevelopments();

            // dev already unlocked?
            if (allUnlockedDevs.Select(dev => dev.name).ToArray().Contains(d.name)) continue;

            // no requirements?
            if (d.requirements == null) {
                availableDevelopments.Add(d);
                continue;
            }

            bool available = true;
            // check attributes (min max)
            for(int i = 0; i < NUMBER_OF_ATTRIBUTES; i++) {
                if(!(d.requiredMinAttributes[i] <= attributes[i]) && (d.requiredMaxAttributes[i] == 0 || d.requiredMaxAttributes[i] >= attributes[i])) {
                    available = false;
                    break;
                }
            }

            // check required resources
            foreach(string s in d.requiredResources) {
                if(environment.resources == null) {
                    available = false;
                    break;
                }
                else if(!environment.resources.Select(res => res.name).ToArray().Contains(s)) {
                    available = false;
                    break;
                }
            }

            // check developments
            foreach(string s in d.requiredDevelopments) {
                if(!allUnlockedDevs.Select(dev => dev.name).ToArray().Contains(s)) {
                    available = false;
                    break;
                }
            }

            // check subtypes
            foreach(string s in d.requiredDevelopmentTypes) {
                if(!allUnlockedDevs.Select(dev => dev.subtype).ToArray().Contains(s)) {
                    available = false;
                    break;
                }
            }

            // check resource types
            foreach(string s in d.requiredResourceType) {
                if(!environment.resources.Select(res => res.GetType().Name).ToArray().Contains(s)) {
                    available = false;
                    break;
                }
            }

            // check optional devs
            foreach(HashSet<string> optional in d.optionalDevelopments) {
                if(!optional.Any(option => allUnlockedDevs.Select(dev => dev.name).ToArray().Contains(option))) {
                    available = false;
                    break;
                }
            }

            // check optional resources
            foreach (HashSet<string> optional in d.optionalResources) {
                if (environment.resources == null) {
                    available = false;
                    break;
                }
                else if (!optional.Any(option => environment.resources.Select(res => res.name).ToArray().Contains(option))) {
                    available = false;
                    break;
                }
            }

            // check costs
            if(d.cost != null && (d.cost.wealth > wealth || d.cost.intelligence > intelligence || d.cost.production > production)) {
                available = false;
                continue;
            }
            if (available) availableDevelopments.Add(d);
        }
        return availableDevelopments;
    }

    public Development ChooseDevelopment(HashSet<Development> availableDevelopments) {
        Development choice = null;
        double maxRate = -100d;
        Attributes orientation = FindOrientation();

        if(true) { // if orientation == null
            foreach(Development d in availableDevelopments) {

                // we don't want multiples of subtypes (e.g. stone and wooden HUT)
                List<Development> allUnlockedDevs = GetAllUnlockedDevelopments();
                if(d.subtype != null && allUnlockedDevs.Select(dev => dev.subtype).ToArray().Contains(d.subtype)) {
                    continue;
                }

                double rate = d.rate.GetOverall();
                if(rate > maxRate) {
                    maxRate = rate;
                    choice = d;
                }
            }
        }
        else {
            // optimize based on orientation
        }
        return choice;
    }

    public bool ApplyDevelopment(Development dev) {
        if(dev == null) {
            return false;
        }
        // apply costs
        if(dev.cost != null) {
            wealth -= dev.cost.wealth;
            intelligence -= dev.cost.intelligence;
            production -= dev.cost.production;
        }
        
        // apply buffs
        List<int> buffList = dev.buffs.GetBuffList();
        for (int i = 0; i < NUMBER_OF_ATTRIBUTES; i++) {
            attributes[i] += buffList[i];
        }

        // apply rates
        List<double> rateList = dev.rate.GetRateList();
        for (int i = 0; i < NUMBER_OF_PARAMETERS; i++) {
            rates[i] += rateList[i];
        }

        Type devType = dev.GetType();
        if(devType == typeof(TechnologyDev)) {
            technologies.Add((TechnologyDev) dev);
        }
        else if(devType == typeof(DiscoveryDev)) {
            discoveries.Add((DiscoveryDev) dev);
        }
        else if(devType == typeof(BuildingDev)) {
            buildings.Add((BuildingDev) dev);
        }
        else {
            // default case;
        }
        return true;
    } 

    public void ApplyRates() {
        if((health < 100 && rates[(int) Parameters.HEALTH] > 0 || health > 0 && rates[(int) Parameters.HEALTH] < 0)) {
            health += rates[(int) Parameters.HEALTH];
            if (health > 100) health = 100;
            if (health < 0) health = 0;
        }

        wealth += rates[(int) Parameters.WEALTH];

        if(health >= 50) {
            population += rates[(int) Parameters.POPULATION];
        }
        else {
            population -= (100 - health) * (new System.Random()).NextDouble();
        }

        intelligence += rates[(int) Parameters.INTELLIGENCE];

        production += rates[(int)Parameters.PRODUCTION];
    }
}
