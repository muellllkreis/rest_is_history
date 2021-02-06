using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

    private Mesh myMesh;
    public float Radius;
    public float angle;

    public float segments = 2;
    private float segmentAngle;

    private Vector3[] verts;
    private Vector3[] normals;
    private int[] triangles;
    private Vector2[] uvs;

    private float actualAngle;

    public Material mat;

    void Start() {
        MeshFilter MeshF = gameObject.AddComponent<MeshFilter>();
        MeshRenderer MeshR = gameObject.AddComponent<MeshRenderer>();
        MeshCollider MeshMC = gameObject.AddComponent<MeshCollider>();
        MeshMC.convex = true;
        MeshMC.isTrigger = true;

        MeshR.material = mat;

        //MESH
        myMesh = gameObject.GetComponent<MeshFilter>().mesh;

        //BUILD THE MESH
        buildMesh();

        MeshMC.sharedMesh = myMesh;
    }

    void buildMesh() {
        // Grab the Mesh off the gameObject
        //Clear the mesh
        myMesh.Clear();

        // Calculate actual pythagorean angle
        actualAngle = 90.0f - angle;

        // Segment Angle
        segmentAngle = angle * 2 / segments;

        // Initialise the array lengths
        verts = new Vector3[(int)segments * 3];
        normals = new Vector3[(int)segments * 3];
        triangles = new int[(int)segments * 3];
        uvs = new Vector2[(int)segments * 3];

        // Initialise the Array to origin Points
        for (int i = 0; i < verts.Length; i++) {
            verts[i] = new Vector3(0, 0, 0);
            normals[i] = Vector3.up;
        }

        // Create a dummy angle
        float a = actualAngle;

        // Create the Vertices
        for (int i = 1; i < verts.Length; i += 3) {
            verts[i] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a) * Radius, // x
                                                  0,                      // y
                                                  Mathf.Sin(Mathf.Deg2Rad * a) * Radius);  // z

            a += segmentAngle; //print(a);

            verts[i + 1] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a) * Radius, // x
                                                      0,                      // y
                                                      Mathf.Sin(Mathf.Deg2Rad * a) * Radius);  // z          
        }

        // Create Triangle
        for (int i = 0; i < triangles.Length; i += 3) {
            triangles[i] = 0;
            triangles[i + 1] = i + 2;
            triangles[i + 2] = i + 1;
        }

        // Generate planar UV Coordinates
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }

        // Put all these back on the mesh
        myMesh.vertices = verts;
        myMesh.normals = normals;
        myMesh.triangles = triangles;
        myMesh.uv = uvs;
    }

    void OnTriggerEnter(Collider collision) {
        Debug.Log("Collided!");
        GetComponent<Renderer>().material.SetColor("_Color", Color.green);

    }

    void OnTriggerExit(Collider collision) {
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
}