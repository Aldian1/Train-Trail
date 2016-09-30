using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {


    private int counter;
    public string[] text;
    private Text txt;
	// Use this for initialization
	void Start () {
        txt = GetComponentInChildren<Text>();
        counter = 0;
	}

    public void nexttext()
    {
        if (counter != 10)
        {
            counter += 1;

            txt.text = text[counter];
            txt.text.Replace("\\n", "\n");
        }
    }

}
