using UnityEngine;
using System.Collections;

public class Train_Controller : MonoBehaviour {


    private float currentspeed;

    public float MaxSpeed;
    public float MinSpeed;

    //upgrade this to arrays for multiple drivers
    public GameObject Driver, Mechanic;
	// Use this for initialization
	void Start () {
	if(this.tag != "Train")
        {
            Debug.Log("You havent tagged the train!");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Driver != null)
        {
            //translate the train to the right based on the current speed
            transform.Translate(Vector2.right * currentspeed * Time.deltaTime);
        }
	}

    public void OnGUI()
    {
        //display our current speed
        GUILayout.Label("Current Speed: " + currentspeed +"Mph");

        if(Driver == null)
        {
            GUILayout.Label("YOU HAVE NO DRIVER");
        }
        else
        {
            GUILayout.Label("Current Driver: " + Driver.name);
        }
    }

    //called by the gui button
    public void IncreaseSpeed()
    {
        //increase speed
        if(currentspeed < MaxSpeed && Driver != null)
        {
            StartCoroutine("TweenSpeed", true);
        }
    }


    public IEnumerator TweenSpeed(bool TweenDirection)
    {
        //if the player is increasing the speed
        if(TweenDirection == true)
        {
            float i = currentspeed + 10;
            while(currentspeed < i)
            {
                currentspeed += 1F;
                yield return new WaitForSeconds(.5F);
            }
        }
    }

    public IEnumerator StopTrain()
    {
        while (currentspeed > 0)
        {
            currentspeed -= 1F;
            yield return new WaitForSeconds(1F);
        }
    }

    public void UpdateTrain()
    {
        if(Driver == null)
        {
            StartCoroutine(StopTrain());
        }
    }
}
