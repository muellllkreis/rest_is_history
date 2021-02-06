using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetRandomPlanetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRandomPlanetLocation() {
        Vector3 planetCenter = transform.position;
        RaycastHit hit;
        Vector3 direction = Quaternion.Euler(0, 0, 10) * Vector3.forward;
        Vector3 direction1 = Quaternion.Euler(0, 0, 20) * Vector3.forward;
        Vector3 direction2 = Quaternion.Euler(0, 0, 30) * Vector3.forward;
        Vector3 direction3 = Quaternion.Euler(0, 0, 40) * Vector3.forward;
        Vector3 direction4 = Quaternion.Euler(0, 0, 50) * Vector3.forward;
        Vector3 direction5 = Quaternion.Euler(0, 0, 60) * Vector3.forward;
        Vector3 direction6 = Quaternion.Euler(0, 0, 70) * Vector3.forward;
        Vector3 direction7 = Quaternion.Euler(0, 0, 80) * Vector3.forward;
        Ray ray = new Ray(planetCenter, direction);
        Debug.DrawRay(planetCenter, direction * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction1 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction2 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction3 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction4 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction5 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction6 * 10000f, Color.white, 2000, true);
        Debug.DrawRay(planetCenter, direction7 * 10000f, Color.white, 2000, true);
    }
}
