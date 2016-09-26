using UnityEngine;
using System.Collections;

public class Character_Controller : MonoBehaviour {
    private GameObject SelectedObj;
    private GameObject OptionsMenu;
    private bool selected;
    private bool mousein;
    private bool moving;
    private Transform currentTarget;

    public float charactermovespeed;

    private Train_Controller TrainController;

    private Rigidbody2D rb;

    public int ID;

	// Use this for initialization
	void Start () {
        SelectedObj = transform.FindChild("Selected").gameObject;
        OptionsMenu = transform.FindChild("OptionsMenu").gameObject;
        TrainController = GameObject.FindGameObjectWithTag("Train").GetComponent<Train_Controller>();
        rb = this.GetComponent<Rigidbody2D>();
       
    }
	
	// Update is called once per frame
	void Update () {

       
        //if we click on the player & were mousing over then we become permanently selected
        if (Input.GetKeyDown(KeyCode.Mouse0) && mousein)
        {
            //tells mouse exit not to disable the texture
            selected = true;
            //start the options menu
            OptionsMenu.SetActive(true);
        }
        //we pressed outside of the character to deselect it
        if (Input.GetKeyDown(KeyCode.Mouse0) && !mousein)
        {
            //tells the mouse exit to disable the texture from now on
            selected = false;
            //deactivate obj as mouseexit is only called once
            SelectedObj.SetActive(false);
        }
    }
     
    //if we hover our mouse over the character
    void OnMouseEnter()
    {

       
        //then display the selected square
        SelectedObj.SetActive(true);

        //mouse is over char
        mousein = true;
    }

    //if we exit hovering the char
    void OnMouseExit()
    {
        //mouse is not over the char
        mousein = false;
        //if were not selected
        if (!selected)
        {
            //then disable square
            SelectedObj.SetActive(false);
            //then disable OptionsUI
            OptionsMenu.SetActive(false);

        }
    }

    //called when we press a button
    public void MoveToNewLocation(Transform Position)
    {
        //disable the options menu
        OptionsMenu.SetActive(false);

        //if we already have someone in the position we are heading too, then we need to cancel out
        if (Position.name == "Drive" && TrainController.Driver != null || Position.name == "Fix" && TrainController.Mechanic != null)
        {
            return;
        }

        //set our new position in the train
        SetPositionInTrain(Position);

        currentTarget = Position.transform;
        Debug.Log("Moving " + transform.name + " too " + currentTarget.name);
        StartCoroutine(Movement(false, false, true));
        moving = true;

        //call the train update method
        TrainController.UpdateTrain();

    }

    public void CancelAll()
    {
        if (TrainController.Driver == this.gameObject)
        {
            //we are driving so we need to notify the train controller that we arent anymore
            TrainController.Driver = null;
        }
        //check if were a mechanic
        if (TrainController.Mechanic == this.gameObject)
        {
            TrainController.Mechanic = null;

        }

        //cancel options out
        OptionsMenu.SetActive(false);
    }

   public IEnumerator Movement(bool moveright,bool moveleft, bool firstrun)
    {
        if (firstrun)
        {
            if (currentTarget.localPosition.x - transform.localPosition.x > transform.localPosition.x)
            {
                yield return moveright = true;
                //Debug.Log("Moving Right");
            }
            else
            {
                yield return moveleft = true;
                //Debug.Log("Moving Left");
            }
            firstrun = false;
        }
        while (moving)
        {

           // Debug.Log(Vector2.Distance(transform.position, currentTarget.position));

            //checking how close we are
            if (Vector2.Distance(transform.position, currentTarget.position) >= 5F)
            {
                //move right
                if (moveright)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * charactermovespeed);

                }

                //move left
                if (moveleft)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * charactermovespeed);

                }
            }
            else
            {
                //cancel out if were near
                StopCoroutine("Movement");
                //cancel the moving loop
                moving = false;
            }
            yield return new WaitForSeconds(.0003F);
        }
        
    }

    //called when we need to change our pos
    public void SetPositionInTrain(Transform NewPosOntrain)
    {
        //check if we are meant to be driving!
        if (TrainController.Driver == this.gameObject)
        {
            //we are driving so we need to notify the train controller that we arent anymore
            TrainController.Driver = null;
        }
        //check if were a mechanic
        if (TrainController.Mechanic == this.gameObject)
        {
            TrainController.Mechanic = null;

        }
        //if we are fixing the train we get assigned as a mechanic
        if (NewPosOntrain.name == "Fix")
        {
            TrainController.Mechanic = this.gameObject;
        }
        //if we are driving the train we get assigned as a driver
        if (NewPosOntrain.name == "Drive")
        {
            TrainController.Driver = this.gameObject;
        }


    }

    //collision detection
    void OnCollisionEnter2D(Collision2D col)
    {
        //Checks that the rigidbody on the character is disabled if they are hit by something that should knock them

        if(col.transform.tag != "Train")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        
    }
}
