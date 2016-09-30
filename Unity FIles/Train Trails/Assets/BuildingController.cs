using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BuildingController : MonoBehaviour {


    private GameObject lockedonobject;

    private GameObject LastObjToBePlaced;

    public Text fundstext;

    public int funds;

    private bool LockObjectToMouse;

    private SpriteRenderer buildarea;

    public bool InBuildArea;

    public GameObject[] Setup;
	// Use this for initialization
	void Start () {
        buildarea = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        fundstext.text = "$ " + funds.ToString();

        if(LockObjectToMouse == true)
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            lockedonobject.transform.position = pz;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && InBuildArea == true)
        {
            BuyPart();
            return;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && LockObjectToMouse == true)
        {
            CancelBuild();
            return;
        }
	}


    //gets passed the train to build from the specific button
    //load from resources for future optimisation
    public void SpawnPart(GameObject Part)
    {
        buildarea.enabled = true;
        lockedonobject = Instantiate(Part, Vector2.zero, Quaternion.identity) as GameObject;
        LockObjectToMouse = true;
    }

    public void BuyPart()
    {
        buildarea.enabled = false;
        LastObjToBePlaced = lockedonobject;
        LockObjectToMouse = false;
        lockedonobject = null;
    }

    public void CancelBuild()
    {
        buildarea.enabled = false;
        LockObjectToMouse = false;
        Destroy(lockedonobject);
    }

    void OnMouseDown()
    {
        
        InBuildArea = true;
      
    }
    
        
    

    public void OnMouseUp()
    {
        InBuildArea = false;
    }
}
