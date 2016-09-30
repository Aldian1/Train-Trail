using UnityEngine;
using System.Collections;

public class Enemy_Train : MonoBehaviour {

    private Rigidbody2D RB;

    public float currentspeed;

    private GameObject PlayersTrain;

    private Train_Controller TrainControls;

    private bool activate;
	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody2D>();
        PlayersTrain = GameObject.FindGameObjectWithTag("Train");
        TrainControls = PlayersTrain.GetComponent<Train_Controller>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activate)
        {
            //get distance from player and base our current speed on it
            float I = Vector2.Distance(this.transform.position, PlayersTrain.transform.position);
            Debug.Log(I);
            if (I > 210)
            {
                currentspeed = TrainControls.currentspeed + 10;
            }
            else
            {
                currentspeed = TrainControls.currentspeed;
            }
            //Debug.Log("Force");
            //translate the train to the right based on the current speed
            //transform.Translate(Vector2.right * currentspeed * Time.deltaTime);
            //RB.AddForce(Vector2.right * currentspeed * 700 * Time.deltaTime);

            RB.velocity = new Vector2(1 * currentspeed, RB.velocity.y);


        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Train")
        {
            activate = true;
        }
    }
}
