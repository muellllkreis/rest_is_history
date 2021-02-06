using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Civilization civ;
    DevelopmentContainer dc;
    ResourceContainer rc;

    // Start is called before the first frame update
    void Start()
    {
        rc = ResourceContainer.Load();
        dc = DevelopmentContainer.Load();
        Environment env = new Environment();
        civ = new Civilization(env);
        StartCoroutine(PlayForever());
    }

    void TakeTurn() {
        civ.ApplyRates();
        HashSet<Development> availableDevs = civ.FindAvailableDevelopments(dc.developmentSet);
        Development choice = civ.ChooseDevelopment(availableDevs);
        civ.ApplyDevelopment(choice);
        LogChoice(choice);
    }

    IEnumerator PlayForever() {
        while(true) {
            TakeTurn();
            yield return new WaitForSeconds(1);
        }
    }

    void LogChoice(Development choice) {
        if(choice == null) {
            Debug.Log("Nothing to develop left.");
            return;
        }
        Type devType = choice.GetType();
        if (devType == typeof(TechnologyDev)) {
            Debug.Log("Fascinating! Your people developed " + choice.name + "!");
        }
        else if (devType == typeof(DiscoveryDev)) {
            Debug.Log("Eureka! Your people discovered " + choice.name + "!");
        }
        else if (devType == typeof(BuildingDev)) {
            Debug.Log("Engineered to perfection! Your people can now build " + choice.name + "!");
        }
        else {
            // default case;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
