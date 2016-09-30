using UnityEngine;
using System.Collections;

public class Train_Controller : MonoBehaviour {


    public float currentspeed;

    public float MaxSpeed;
    public float MinSpeed;

    public Rigidbody2D RB;
    //upgrade this to arrays for multiple drivers
    public GameObject Driver, Mechanic;
	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody2D>();
	if(this.tag != "Train")
        {
            Debug.Log("You havent tagged the train!");
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Driver != null)
        {
            //Debug.Log("Force");
            //translate the train to the right based on the current speed
            //transform.Translate(Vector2.right * currentspeed * Time.deltaTime);
            //RB.AddForce(Vector2.right * currentspeed * 700 * Time.deltaTime);
            RB.velocity= new Vector2(1 * currentspeed, RB.velocity.y);


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
        if (Mechanic != null)
        { GUILayout.Label("Current Mechanic: " + Mechanic.name); }
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
