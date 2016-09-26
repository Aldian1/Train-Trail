using UnityEngine;
using System.Collections;

public class Notification_Handler : MonoBehaviour {

    bool InSpace;
    Animator ar;
	// Use this for initialization
	void Start () {
        ar = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Mouse has entered the clickable space
    public void HoveringOnObject()
    {
        ar.SetBool("Move", true);
        InSpace = true;
        Debug.Log("Notification Push");
    }
    //Mouse has exited the clickable space
    public void  HoveringOffObject()
    {
        ar.SetBool("Move", false);
        InSpace = false;
        Debug.Log("Notification Exit");
    }
}
