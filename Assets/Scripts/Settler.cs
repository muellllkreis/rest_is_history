using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour
{
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector3 movement;
    private float timeLeft;
    private Rigidbody rb;
    private bool isInitialized = false;
    private GameObject fieldOfViewObj;
    private FieldOfView fieldOfView;

    Civilization civilization;
    public void Init(Civilization civ) {
        civilization = civ;
        fieldOfViewObj = transform.GetChild(0).gameObject;
        fieldOfView = fieldOfViewObj.GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody>();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            transform.position = new Vector3(hit.point.x, hit.point.y + transform.localScale.y / 2, hit.point.z);
        }
        isInitialized = true;
    }

    void explore() {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            movement = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInitialized) {
            explore();
        }
    }

    void FixedUpdate() {
        rb.AddForce(movement * maxSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 40f);
        Debug.Log(movement);
        Debug.Log("moving");
    }
}
