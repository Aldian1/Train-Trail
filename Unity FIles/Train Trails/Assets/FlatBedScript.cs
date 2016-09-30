using UnityEngine;
using System.Collections;

public class FlatBedScript : MonoBehaviour {

    public Transform target;
    public float dirNum;


    void Start()
    {
        //connects a distance joint to the object and the other object
        CreateLink();
    }

    void CreateLink()
    {

    }

    public static float AngleDir(Vector2 A, Vector2 B)
    {
        return -A.x * B.y + A.y * B.x;
    }

}
